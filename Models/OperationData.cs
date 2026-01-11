namespace EducationalProject.Models
{
    public class OperationInfo
    {
        public string Description { get; set; }
        public string Example { get; set; }
        public string Result { get; set; }
    }

    public class AssignmentData
    {
        public int Value { get; set; }
        public List<OperationInfo> Operations { get; set; } = new();
        public int FinalValue { get; set; }
    }

    public class PracticalExample
    {
        public double Salary { get; set; }
        public double Groceries { get; set; }
        public double Entertainment { get; set; }
        public string Currency { get; set; }
        public List<string> Steps { get; set; } = new();
        public double FinalBudget { get; set; }
    }
}