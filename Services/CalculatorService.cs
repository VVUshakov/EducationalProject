using EducationalProject.Models;

namespace EducationalProject.Services
{
    public class CalculatorService
    {
        // Основной метод вычисления
        public CalculationData Calculate(CalculationData data)
        {
            try
            {
                data.Result = data.Operation switch
                {
                    '+' => data.FirstNumber + data.SecondNumber,
                    '-' => data.FirstNumber - data.SecondNumber,
                    '*' => data.FirstNumber * data.SecondNumber,
                    '/' => data.SecondNumber != 0 ? data.FirstNumber / data.SecondNumber : double.NaN,
                    '%' => data.SecondNumber != 0 ? data.FirstNumber % data.SecondNumber : double.NaN,
                    _ => double.NaN
                };

                // Если деление - вычисляем остаток
                if(data.Operation == '/' && data.SecondNumber != 0 && !double.IsNaN(data.Result))
                {
                    double remainder = data.FirstNumber % data.SecondNumber;
                    if(System.Math.Abs(remainder) > 0.001)
                    {
                        data.Remainder = remainder;
                    }
                }
            }
            catch
            {
                data.Result = double.NaN;
            }

            return data;
        }

        // Упрощённый метод для простых операций
        public double SimpleCalculate(double a, double b, char operation)
        {
            return operation switch
            {
                '+' => a + b,
                '-' => a - b,
                '*' => a * b,
                '/' => b != 0 ? a / b : double.NaN,
                '%' => b != 0 ? a % b : double.NaN,
                _ => double.NaN
            };
        }
    }
}