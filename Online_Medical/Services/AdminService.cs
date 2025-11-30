using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Online_Medical.ViewModel;
using Online_Medical.Models;

namespace Online_Medical.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;

        // يتم حقن (Injection) الـRepository هنا
        public AdminService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        // 1. منطق جلب قائمة المرضى (منطق Index)
        public async Task<List<AdminPatientViewModel>> GetAdminPatientListAsync()
        {
            var patients = await _adminRepository.GetAllPatientProfilesAsync();

            var model = patients.Select(p => new AdminPatientViewModel
            {
                Id = p.Id,
                Username = p.ApplicationUser.UserName,
                Email = p.ApplicationUser.Email,
                PhoneNumber = p.ApplicationUser.PhoneNumber,
                JoinDate = p.ApplicationUser.JoinDate,
                TotalAppointments = p.Appointments?.Count ?? 0
            }).ToList();

            return model;
        }


        // 2. منطق حذف المريض
        public async Task<IdentityResult> DeletePatientAsync(string userId)
        {
            // 1️⃣ جلب المستخدم (مش هنحذفه من Identity)
            var user = await _adminRepository.FindUserByIdAsync(userId);

            if (user == null)
            {
                // User مش موجود أصلاً → العملية نجحت تقنياً
                return IdentityResult.Success;
            }

            // 2️⃣ حذف Patient Profile + Cascade Delete على Appointments + Reviews
            await _adminRepository.DeletePatientProfileAsync(userId);

            // 3️⃣ ما نحذفش الـ ApplicationUser → User يفضل موجود
            return IdentityResult.Success;
        }
        //clinic

        public async Task<IdentityResult> CreateClinicAsync(ClinicCreateViewModel model)
        {
            // 1. إنشاء كيان العيادة
            var clinic = new Clinic
            {
                Name = model.Name,
                Street = model.Street,
                BuildingNumber = model.BuildingNumber ?? 0,
                Region = model.Region,
                City = model.City,
                Phone = model.Phone
            };

            // 2. حفظ العيادة في الذاكرة (لم يتم حفظها بعد في DB)
            await _adminRepository.AddClinicAsync(clinic);

            // 3. معالجة الأطباء وربطهم (اختياري)
            if (!string.IsNullOrWhiteSpace(model.DoctorIdentifiersInput))
            {
                // 🛑 فصل المعرفات (سواء بفاصلة أو سطر جديد)
                var identifiers = model.DoctorIdentifiersInput
                    .Split(new[] { ',', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => s.Trim())
                    .ToList();

                bool foundAtLeastOneDoctor = false;

                foreach (var identifier in identifiers)
                {
                    string? doctorId = await _adminRepository.FindDoctorIdByIdentifierAsync(identifier);

                    if (!string.IsNullOrEmpty(doctorId))
                    {
                        var doctorClinic = new DoctorClinic
                        {
                            DoctorId = doctorId,
                            ClinicId = clinic.Id // ID العيادة بعد الحفظ الأول
                        };
                        await _adminRepository.AddDoctorClinicAsync(doctorClinic);
                        foundAtLeastOneDoctor = true;
                    }
                    // 💡 ملاحظة: يمكن إضافة رسالة خطأ إذا لم يتم العثور على طبيب، لكننا سنكتفي بتجاهله هنا.
                }

                // إذا لم يتم العثور على أي طبيب وتم ترك الحقل فارغ، لا نرسل رسائل خطأ
                // لكن إذا كان هدفك هو ربط العيادة ولو بطبيب واحد على الأقل، يجب إضافة فحص هنا.
            }

            // 4. حفظ جميع التغييرات (العيادة وجميع الأطباء المرتبطين)
            // 🛑 نستخدم SaveChangesAsync على الـDbContext أو نطلبها من الـRepository
            await _adminRepository.SaveChangesAsync();

            return IdentityResult.Success;
        }

        public async Task<ClinicListViewModel> GetClinicListAsync()
        {
            var clinics = await _adminRepository.GetAllClinicsAsync();

            var viewModel = new ClinicListViewModel
            {
                Clinics = clinics.Select(c => new ClinicViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Phone = c.Phone,
                    // تجميع بيانات العنوان في سطر واحد
                    AddressSummary = $"{c.Street}, مبنى {c.BuildingNumber}, {c.City}",
                    DoctorsCount = c.DoctorClinics?.Count ?? 0 // حساب عدد الأطباء
                }).ToList()
            };

            return viewModel;
        }
        // في AdminService.cs

        public async Task<ClinicDetailsViewModel?> GetClinicDetailsAsync(int clinicId)
        {
            // يستدعي GetClinicByIdAsync الذي يجلب العيادة مع الأطباء وبيانات ApplicationUser
            var clinic = await _adminRepository.GetClinicByIdAsync(clinicId);

            if (clinic == null) return null;

            var viewModel = new ClinicDetailsViewModel
            {
                Id = clinic.Id,
                Name = clinic.Name,
                // 🛑 تعبئة حقول التفاصيل الكاملة
                Street = clinic.Street,
                BuildingNumber = clinic.BuildingNumber,
                Region = clinic.Region,
                City = clinic.City,
                Phone = clinic.Phone,

                Doctors = clinic.DoctorClinics
                    .Select(dc => new DoctorInClinicDetailsViewModel
                    {
                        DoctorId = dc.DoctorId,
                        FullName = $"{dc.Doctor.ApplicationUser.FirstName} {dc.Doctor.ApplicationUser.LastName}",
                        Username = dc.Doctor.ApplicationUser.UserName,
                        Email = dc.Doctor.ApplicationUser.Email
                    })
                    .ToList()
            };

            return viewModel;
        }

        public async Task<ClinicEditViewModel?> GetClinicForEditAsync(int id)
        {
            var clinic = await _adminRepository.GetClinicByIdAsync(id);
            if (clinic == null) return null;

            return new ClinicEditViewModel
            {
                Id = clinic.Id,
                Name = clinic.Name,
                Street = clinic.Street,
                BuildingNumber = clinic.BuildingNumber,
                Region = clinic.Region,
                City = clinic.City,
                Phone = clinic.Phone
            };
        }

        public async Task<IdentityResult> UpdateClinicAsync(ClinicEditViewModel model)
        {
            var clinic = await _adminRepository.GetClinicByIdAsync(model.Id);
            if (clinic == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Clinic not found" });
            }

            clinic.Name = model.Name;
            clinic.Street = model.Street;
            clinic.BuildingNumber = model.BuildingNumber ?? 0;
            clinic.Region = model.Region;
            clinic.City = model.City;
            clinic.Phone = model.Phone;

            await _adminRepository.UpdateClinicAsync(clinic);
            await _adminRepository.SaveChangesAsync();

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteClinicAsync(int clinicId)
        {
            var clinic = await _adminRepository.GetClinicByIdAsync(clinicId);
            if (clinic == null)
            {
                // Clinic not found, technically success as it's gone
                return IdentityResult.Success;
            }

            await _adminRepository.DeleteClinicAsync(clinicId);
            return IdentityResult.Success;
        }

    }
}
