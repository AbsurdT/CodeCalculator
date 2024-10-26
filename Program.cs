using OfficeOpenXml;
using CodeCalculator;
using CodeCalculator.adds;

ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
Standarts standarts = new Standarts();
standarts.PrintStandarts();
Console.WriteLine();
Console.WriteLine("Изменить эталонные значения? да/нет");
await standarts.UpdateStandarts();
var standart = standarts.standarts;

while (true)
{
    Console.WriteLine("Значение 1 времени:");
    while (true)
    {
        decimal timeValueActual1 = Methods.Input(ValueTypes.Actual);
        Value timeValue1 = new Value(standart.Time1, timeValueActual1, ValueTypes.Time);
        if(timeValue1.LetterCode.Length == 2)
        {
            Console.WriteLine($"Буквенный код: {timeValue1.LetterCode}");
            Console.WriteLine();
            break;
        }
        Console.WriteLine(timeValue1.LetterCode);
        Console.WriteLine();
        Console.WriteLine("Попробуйте снова:");
    }

    Console.WriteLine("Значение 2 времени:");
    while (true)
    {
        decimal timeValueActual2 = Methods.Input(ValueTypes.Actual);
        Value timeValue2 = new Value(standart.Time2, timeValueActual2, ValueTypes.Time);
        if (timeValue2.LetterCode.Length == 2)
        {
            Console.WriteLine($"Буквенный код: {timeValue2.LetterCode}");
            Console.WriteLine();
            break;
        }
        Console.WriteLine(timeValue2.LetterCode);
        Console.WriteLine();
        Console.WriteLine("Попробуйте снова:");
    }

    Console.WriteLine("Значение объёма:");
    while (true)
    {
        decimal volumeValueActual = Methods.Input(ValueTypes.Actual);
        Value volumeValue = new Value(standart.Volume, volumeValueActual, ValueTypes.Volume);
        if (volumeValue.LetterCode.Length == 2)
        {
            Console.WriteLine($"Буквенный код: {volumeValue.LetterCode}");
            Console.WriteLine();
            break;
        }
        Console.WriteLine(volumeValue.LetterCode);
        Console.WriteLine();
        Console.WriteLine("Попробуйте снова:");
    }
    Console.WriteLine("Для ввода новых показаний нажмите любую кнопку");
    Console.ReadKey();
    Console.Clear();
    Console.WriteLine("Текущие эталонные значения:");
    Console.WriteLine($"Время 1:    {standart.Time1}");
    Console.WriteLine($"Время 2:    {standart.Time2}");
    Console.WriteLine($"Объем  :    {standart.Volume}");
    Console.WriteLine();
}



