using Microsoft.AspNetCore.Mvc.Rendering;

namespace Online_Medical.Services
{
    public enum Gender
    {
        
        Unselected = 0,
        Male = 1,
        Female = 2,
        PreferNotToSay = 3
    }

    public class GenderList
    {
        public static List<SelectListItem> GetEnumSelectList<T>() where T : struct, Enum
        {
            return Enum.GetValues(typeof(T))
                .Cast<Gender>()
                .Where(g => g != Gender.Unselected)
                .Select(g => new SelectListItem
                {
                    // Text is the display name (e.g., "Male", "NonBinary")
                    Text = g.ToString(),

                    // Value is the underlying integer value (or a string representation of the value)
                    Value = ((int)g).ToString()
                })
                .ToList();
        }
    }
}
