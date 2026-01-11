namespace EducationalProject.Models
{
    public class PasswordSettings
    {
        public bool UseLower { get; set; } = true;
        public bool UseUpper { get; set; } = true;
        public bool UseDigits { get; set; } = true;
        public bool UseSpecial { get; set; } = true;

        public bool HasAnySetting => UseLower || UseUpper || UseDigits || UseSpecial;
        public int SettingsCount => (UseLower ? 1 : 0) + (UseUpper ? 1 : 0) +
                                  (UseDigits ? 1 : 0) + (UseSpecial ? 1 : 0);
    }
}