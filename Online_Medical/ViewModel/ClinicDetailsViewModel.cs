namespace Online_Medical.ViewModel
{
    public class ClinicDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // تفاصيل العنوان الكاملة (نستخدمها هنا لأنها صفحة التفاصيل)
        public string Street { get; set; }
        public int BuildingNumber { get; set; }
        public string Region { get; set; }
        public string City { get; set; }

        public string Phone { get; set; }

        // الخصائص الخاصة بالأطباء
        public int DoctorsCount => Doctors.Count;
        public List<DoctorInClinicDetailsViewModel> Doctors { get; set; } = new List<DoctorInClinicDetailsViewModel>();
    }

    // نموذج عرض الطبيب داخل صفحة التفاصيل
    public class DoctorInClinicDetailsViewModel
    {
        public string DoctorId { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
}

