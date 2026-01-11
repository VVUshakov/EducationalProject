namespace EducationalProject.Models
{
    public class PasswordResult
    {
        public string Password { get; set; }
        public int Length { get; set; }
        public PasswordSettings Settings { get; set; }
        public string Strength { get; set; }

        public bool IsValid => !string.IsNullOrEmpty(Password) && Length > 0;
    }
}