using OfficeOpenXml;

namespace CodeCalculator
{
    public class Value
    {
        public decimal Standart { get; set; }
        public decimal Actual { get; set; }
        public string LetterCode { get; private set; }
        public Value(decimal standart, decimal actual, string type)
        {
            Standart = standart;
            Actual = actual;
            LetterCode = LetterCodeSet(type).Result;
        }
        private async Task<string> LetterCodeSet(string type)
        {
            var fi = new FileInfo($@"{Directory.GetCurrentDirectory()}\Коды смарт.xlsx");
            decimal delta = Actual - Standart;
            using (var package = new ExcelPackage(fi))
            {
                var worksheet = package.Workbook.Worksheets[type];
                int maxRows = worksheet.Dimension.End.Row;        // включая пустые строки
                int maxColumns = worksheet.Dimension.End.Column;

                Task<int> rows = Task.Run(() => NotNullCounter(worksheet, maxRows, ElementType.Row));
                Task<int> colums = Task.Run(() => NotNullCounter(worksheet, maxColumns, ElementType.Col));
                await Task.WhenAll(rows, colums);

                int firstColRows = rows.Result;
                int firstRowCols = colums.Result;

                for (int i = 2; i <= firstRowCols; i++)
                {
                    for (int j = 2; j <= firstColRows; j++)
                    {
                        var cellValue = worksheet.Cells[j, i].Text;
                        if (string.IsNullOrWhiteSpace(cellValue))
                        {
                            continue;
                        }
                        string[] range = cellValue.Split(';');
                        bool left = Methods.DecimalOrNot(range[0], out var leftBorder);
                        bool right = Methods.DecimalOrNot(range[^1], out var rightBorder);
                        if (range.Length > 2 || left == false || right == false)
                        {
                            return $"Ошибка в экселе ячейка: {worksheet.Cells[j, i]}";
                        }
                        else if (left && right)
                        {
                            if (delta >= leftBorder && delta <= rightBorder)
                            {
                                return worksheet.Cells[1, i].Text + worksheet.Cells[j, 1].Text;
                            }
                        }
                    }
                }
            }
            return "Ошибка: Отсутствует отклонение";
        }
        private int NotNullCounter(ExcelWorksheet worksheet, int total, ElementType type)
        {
            int nonEmptyCount = 1;
            for (int i = 2; i <= total; i++)      // определяем количество непустых строк по нулевому столбцу, так как он имеет наиб длину
            {
                string cellValue;
                if(type == ElementType.Row)
                {
                    cellValue = worksheet.Cells[i,1].Text;
                }
                else
                {
                    cellValue = worksheet.Cells[1, i].Text;
                }
                if (!string.IsNullOrWhiteSpace(cellValue))
                {
                    nonEmptyCount++;
                }
                else { break; }
            }
            return nonEmptyCount;
        }
        private enum ElementType
        {
            Row,
            Col,
        }
    }
}
