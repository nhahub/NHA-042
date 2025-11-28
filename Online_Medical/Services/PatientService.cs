using Microsoft.AspNetCore.Identity;
using Online_Medical.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Online_Medical.Interface;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Online_Medical.Models;
using System.Collections.Generic;


namespace Online_Medical.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IWebHostEnvironment _hostEnvironment;

        public PatientService(IPatientRepository patientRepository, IWebHostEnvironment hostEnvironment)
        {
            _patientRepository = patientRepository;
            _hostEnvironment = hostEnvironment;
        }

        // 1. جلب البيانات للتفاصيل
        public Task<Patient?> GetPatientProfileDetailsAsync(string userId)
        {
            return _patientRepository.GetPatientWithUserByIdAsync(userId);
        }

        // 2. جلب البيانات للتعديل
        public async Task<PatientProfileViewModel?> GetPatientProfileForEditAsync(string userId)
        {
            var patient = await _patientRepository.GetPatientWithUserByIdAsync(userId);

            if (patient == null || patient.ApplicationUser == null) return null;

            return new PatientProfileViewModel
            {
                Id = patient.Id,
                FirstName = patient.ApplicationUser.FirstName,
                LastName = patient.ApplicationUser.LastName,
                Email = patient.ApplicationUser.Email,
                UserName = patient.ApplicationUser.UserName,
                PhoneNumber = patient.ApplicationUser.PhoneNumber,
                BirthDate = patient.ApplicationUser.DateOfBirth,
                Gender = patient.ApplicationUser.Gender,
                ExistingImageUrl = patient.ProfileImage ?? "/images/default.png"
            };
        }

        // 3. تحديث الملف الشخصي (منطق Edit POST الأصلي)
        public async Task<(IdentityResult identityResult, IEnumerable<IdentityError> passwordErrors, string? newImageUrl)>
            UpdatePatientProfileAsync(PatientProfileViewModel model, string wwwRootPath)
        {
            var user = await _patientRepository.FindUserByIdAsync(model.Id);
            var patientProfile = _patientRepository.GetById(model.Id);

            if (user == null || patientProfile == null)
                return (IdentityResult.Failed(), Enumerable.Empty<IdentityError>(), null);

            // ============================
            // A. منطق معالجة الصورة
            // ============================
            string newImageUrl = model.ExistingImageUrl;
            if (model.ProfileImage != null)
            {
                // ** كل منطق حفظ وحذف الصور ينتقل هنا **
                string fileName = Guid.NewGuid().ToString();
                var uploadsFolder = Path.Combine(wwwRootPath, "images", "PatientProfiles");
                var extension = Path.GetExtension(model.ProfileImage.FileName);

                if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

                if (!string.IsNullOrEmpty(model.ExistingImageUrl) && model.ExistingImageUrl != "/images/default.png")
                {
                    var oldImagePath = Path.Combine(wwwRootPath, model.ExistingImageUrl.TrimStart('/'));
                    if (File.Exists(oldImagePath)) File.Delete(oldImagePath);
                }

                newImageUrl = "/images/PatientProfiles/" + fileName + extension;
                var filePath = Path.Combine(wwwRootPath, newImageUrl.TrimStart('/'));

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ProfileImage.CopyToAsync(fileStream);
                }
            }

            // ============================
            // B. تحديث بيانات المستخدم
            // ============================
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.UserName = model.UserName;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.DateOfBirth = model.BirthDate;
            user.Gender = model.Gender;

            var identityResult = await _patientRepository.UpdateApplicationUserAsync(user);

            if (!identityResult.Succeeded) return (identityResult, Enumerable.Empty<IdentityError>(), null);

            // ============================
            // C. تغيير كلمة المرور (منطق التحقق)
            // ============================
            IEnumerable<IdentityError> passwordErrors = Enumerable.Empty<IdentityError>();
            if (!string.IsNullOrWhiteSpace(model.OldPassword) && !string.IsNullOrWhiteSpace(model.NewPassword))
            {
                if (model.NewPassword != model.ConfirmPassword)
                    return (IdentityResult.Failed(), new[] { new IdentityError { Description = "كلمة المرور الجديدة وتأكيدها غير متطابقين." } }, null);

                var checkOld = await _patientRepository.CheckPasswordAsync(user, model.OldPassword);
                if (!checkOld)
                    return (IdentityResult.Failed(), new[] { new IdentityError { Description = "كلمة المرور القديمة غير صحيحة." } }, null);

                var passwordResult = await _patientRepository.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

                if (!passwordResult.Succeeded)
                    passwordErrors = passwordResult.Errors;
            }

            // 4. تحديث جدول Patient وحفظ التغييرات
            patientProfile.ProfileImage = newImageUrl;
            _patientRepository.Update(patientProfile);
            await _patientRepository.SaveAsync();

            await _patientRepository.UpdateSecurityStampAsync(user);

            return (IdentityResult.Success, passwordErrors, newImageUrl);
        }
    }
}
