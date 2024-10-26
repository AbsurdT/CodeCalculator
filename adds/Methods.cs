using CodeCalculator.adds;
using Newtonsoft.Json;

namespace CodeCalculator
{
    public class Methods
    {
        public static bool DecimalOrNot(string value, out decimal result)
        {
            value = value.Trim();
            if (value.Contains('.'))
            {
                value = value.Replace('.', ',');
            }
            bool decimalOrNot = decimal.TryParse(value, out decimal standart);
            result = 0m;
            if (decimalOrNot == true)
            {
                result = standart;
            }
            return decimalOrNot;
        }
        public static decimal Input(string type)
        {
            while (true)
            {
                Console.WriteLine($"Введите {type} значение:");
                string? input = Console.ReadLine();
                if (DecimalOrNot(input, out decimal result)== true)
                {
                    return result;
                }
                Console.WriteLine("Ошибка, попробуйте снова");
            }
        }
        
        
    }
    
}
