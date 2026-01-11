namespace EducationalProject.Models
{
    public class BinaryConversion
    {
        public int DecimalValue { get; set; }
        public string BinaryValue { get; set; }
        public string FormattedBinary => $"{BinaryValue.Substring(0, 4)} {BinaryValue.Substring(4)}";
        public bool IsValid => !string.IsNullOrEmpty(BinaryValue) && DecimalValue >= 0;
    }
}