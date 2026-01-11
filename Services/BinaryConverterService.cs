using EducationalProject.Core;
using EducationalProject.Models;

namespace EducationalProject.Services
{
    public class BinaryConverterService
    {
        public BinaryConversion ConvertToBinary(int decimalNumber)
        {
            var result = new BinaryConversion
            {
                DecimalValue = decimalNumber,
                BinaryValue = InputValidator.DecimalToBinary(decimalNumber, AppConfig.Binary.BitsCount)
            };

            return result;
        }

        public BinaryConversion ConvertToDecimal(string binaryString)
        {
            int decimalValue = InputValidator.BinaryToDecimal(binaryString);

            var result = new BinaryConversion
            {
                DecimalValue = decimalValue,
                BinaryValue = decimalValue >= 0
                    ? InputValidator.DecimalToBinary(decimalValue, AppConfig.Binary.BitsCount)
                    : null
            };

            return result;
        }

        public string GetBinaryInfo()
        {
            return $"Диапазон: {AppConfig.Binary.MinValue} - {AppConfig.Binary.MaxValue} (8 бит)\n" +
                   $"Биты: {AppConfig.Binary.BitsCount}\n" +
                   $"Максимальное значение: 11111111 (255)";
        }
    }
}