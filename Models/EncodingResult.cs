namespace EducationalProject.Models
{
    public class EncodingResult
    {
        public string OriginalName { get; set; }
        public string EncodedName { get; set; }
        public string MethodName { get; set; }
        public int MethodChoice { get; set; }

        public bool IsValid => !string.IsNullOrEmpty(EncodedName);
    }
}