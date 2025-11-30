namespace Online_Medical.ViewModel
{
    public class ClinicViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AddressSummary { get; set; } 
        public string Phone { get; set; }
        public int DoctorsCount { get; set; } 
    }
    public class ClinicListViewModel
    {
        public List<ClinicViewModel> Clinics { get; set; } = new List<ClinicViewModel>();
    }
}
