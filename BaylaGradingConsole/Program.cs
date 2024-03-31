using BaylaGradingSystem.Object;
using BaylaGradingSystem;
using System.Data;

namespace BaylaGradingConsole
{
    public class Program
    {
        public static void Main()
        {
            string filePath = "data/students.csv";
            DataTable dataTable = new DataTable();
            using (StreamReader reader = new StreamReader("C:\\Users\\gmrmc\\source\\repos\\BaylaGradingSystem\\BaylaGradingSystem\\table.csv"))
            {
                string[] headers = reader.ReadLine().Split(',');
                foreach (string header in headers)
                {
                    dataTable.Columns.Add(header);
                }

                while (!reader.EndOfStream)
                {
                    string[] rows = reader.ReadLine().Split(',');
                    DataRow dataRow = dataTable.NewRow();
                    for (int i = 0; i < headers.Length; i++)
                    {
                        dataRow[i] = rows[i];
                    }
                    dataTable.Rows.Add(dataRow);
                }
            }

            List<double> prelimGrades = dataTable.AsEnumerable().Select(row => Convert.ToDouble(row["prelim"])).ToList();
            List<double> midtermGrades = dataTable.AsEnumerable().Select(row => Convert.ToDouble(row["midterm"])).ToList();
            List<double> finalsGrades = dataTable.AsEnumerable().Select(row => Convert.ToDouble(row["finals"])).ToList();

            Dictionary<string, double> prelimStats = Stats.PeriodStats(prelimGrades);
            Dictionary<string, double> midtermStats = Stats.PeriodStats(midtermGrades);
            Dictionary<string, double> finalsStats = Stats.PeriodStats(finalsGrades);

            Console.WriteLine("\n===== PRELIM CLASS STATISTICS =====");
            foreach (KeyValuePair<string, double> entry in prelimStats)
            {
                Console.WriteLine($"{entry.Key.ToUpper()}  ->  {Math.Round(entry.Value, 2)}");
            }

            Console.WriteLine("\n===== MIDTERM CLASS STATISTICS =====");
            foreach (KeyValuePair<string, double> entry in midtermStats)
            {
                Console.WriteLine($"{entry.Key.ToUpper()}  ->  {Math.Round(entry.Value, 2)}");
            }

            Console.WriteLine("\n===== FINAL CLASS STATISTICS =====");
            foreach (KeyValuePair<string, double> entry in finalsStats)
            {
                Console.WriteLine($"{entry.Key.ToUpper()}  ->  {Math.Round(entry.Value, 2)}");
            }


            List<Student> studentStats = Stats.StudentData(dataTable);
            Console.WriteLine("\n           STUDENT STATISTICS           ");
            foreach (Student studentStat in studentStats)
            {
                Console.WriteLine("---------------------------------------");
                Console.WriteLine($"Name: {studentStat.name}");

                foreach (Grades grade in studentStat.grades)
                {
                    Console.WriteLine($"{grade.period.ToUpper()} Grade: {grade.value}");
                }

                Console.WriteLine($"\nFINAL AVERAGE: {studentStat.average}");
            }
        }
    }
}