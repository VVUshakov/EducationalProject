namespace EducationalProject.Models
{
    public class CalculationData
    {
        public double FirstNumber { get; set; }
        public double SecondNumber { get; set; }
        public char Operation { get; set; }
        public double Result { get; set; }
        public double? Remainder { get; set; }
        public bool IsValid => !double.IsNaN(Result);

        // Для удобства - описание вычисление
        public string Description => $"{FirstNumber} {Operation} {SecondNumber}";
    }
}