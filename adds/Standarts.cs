using Newtonsoft.Json;
using CodeCalculator;
namespace CodeCalculator.adds
{
    public class Standarts
    {
        public DataModel standarts = new DataModel();
        public Standarts()
        {
            Task task = SetStandarts();
            task.Wait();
        }
        private async Task SetStandarts()
        {
            try
            {
                var json = await File.ReadAllTextAsync("ValueData.json");
                standarts = JsonConvert.DeserializeObject<DataModel>(json);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
        }
        public void PrintStandarts()
        {
            Console.WriteLine("Текущие эталонные значения:");
            Console.WriteLine($"Время 1:    {standarts.Time1}");
            Console.WriteLine($"Время 2:    {standarts.Time2}");
            Console.WriteLine($"Объем  :    {standarts.Volume}");
        }
        public async Task UpdateStandarts()
        {
            while (true)
            {
                string yesOrNot = Console.ReadLine();
                Console.WriteLine();
                if (yesOrNot == "да")
                {
                    Console.WriteLine("Значение 1 времени:");
                    var time1 = Methods.Input(ValueTypes.Standart);
                    Console.WriteLine();

                    Console.WriteLine("Значение 2 времени:");
                    var time2 = Methods.Input(ValueTypes.Standart);
                    Console.WriteLine();

                    Console.WriteLine("Значение объема:");
                    var volume = Methods.Input(ValueTypes.Standart);
                    Console.WriteLine();

                    DataModel standart = new DataModel()
                    {
                        Time1 = time1,
                        Time2 = time2,
                        Volume = volume
                    };
                    try
                    {
                        var json = JsonConvert.SerializeObject(standart);
                        await File.WriteAllTextAsync("ValueData.json", json);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    await SetStandarts();
                    Console.Clear();
                    PrintStandarts();
                    Console.WriteLine();
                    return;
                }
                else if (yesOrNot == "нет")
                {
                    return;
                }
                else
                {
                    Console.WriteLine("Введите да или нет");
                }
            }
        }
    }
}
