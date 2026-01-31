namespace EducationalProject.Core.Services
{
    public class CalculatorService
    {
        public double Add(double a, double b) => a + b;
        public double Subtract(double a, double b) => a - b;
        public double Multiply(double a, double b) => a * b;

        public double Divide(double a, double b)
        {
            if(b == 0)
                throw new DivideByZeroException("Деление на ноль невозможно!");
            return a / b;
        }

        public double Power(double number, int power)
        {
            return Math.Pow(number, power);
        }

        public double SquareRoot(double number)
        {
            if(number < 0)
                throw new ArgumentException("Корень из отрицательного числа!");
            return Math.Sqrt(number);
        }
    }
}
