using EducationalProject.Core;
using EducationalProject.Models;

namespace EducationalProject.Services
{
    public class BinaryConverterService
    {
        private int bitsCount = AppConfig.BinaryConverterConfig.BitsCount;

        public BinaryConversion ConvertToBinary(int decimalNumber)
        {
            var result = new BinaryConversion
            {
                DecimalValue = decimalNumber,
                BinaryValue = InputValidator.DecimalToBinary(decimalNumber, bitsCount)
            };

            return result;
        }

        public BinaryConversion ConvertToDecimal(string binaryString)
        {
            int decimalValue = InputValidator.BinaryToDecimal(binaryString);

            string binaryValue;

            if(decimalValue >= 0)
            {
                binaryValue = InputValidator.DecimalToBinary(decimalValue, bitsCount);
            }
            else
            {
                binaryValue = null;
            }

            var result = new BinaryConversion
            {
                DecimalValue = decimalValue,
                BinaryValue = binaryValue
            };

            return result;
        }

        public string GetBinaryInfo()
        {
            int minValue = AppConfig.BinaryConverterConfig.MinValue;
            int maxValue = AppConfig.BinaryConverterConfig.MaxValue;

            return $"Диапазон: {minValue} - {maxValue} ({bitsCount} бит)\n" +
                   $"Биты: {bitsCount}\n" +
                   $"Максимальное значение: 11111111 (255)";
        }
    }
}