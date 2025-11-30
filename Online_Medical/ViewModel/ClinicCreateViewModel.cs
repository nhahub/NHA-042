
using System.ComponentModel.DataAnnotations;

namespace Online_Medical.ViewModel

{
    public class ClinicCreateViewModel
    {
        [Required(ErrorMessage = "يجب إدخال اسم العيادة.")]
        [Display(Name = "اسم العيادة")]
        public string Name { get; set; }

        [Required(ErrorMessage = "يجب إدخال الشارع.")]
        [Display(Name = "الشارع")]
        public string Street { get; set; }

        [Display(Name = "رقم المبنى")]
        public int? BuildingNumber { get; set; }

        [Required(ErrorMessage = "يجب إدخال المنطقة.")]
        [Display(Name = "المنطقة")]
        public string Region { get; set; }

        [Required(ErrorMessage = "يجب إدخال المدينة.")]
        [Display(Name = "المدينة")]
        public string City { get; set; }

        [Display(Name = "رقم الهاتف")]
        public string? Phone { get; set; }

        // 🛑 الحقل الجديد: قائمة بمعرفات الأطباء (اختيارية وغير مطلوبة)
        [Display(Name = "إيميلات/يوزرنيم الأطباء (فصل بفاصلة أو سطر جديد)")]
        // نستخدم string واحد هنا وسنفصله في الـService لتسهيل إدخال المستخدم
        public string? DoctorIdentifiersInput { get; set; }
    }
}
