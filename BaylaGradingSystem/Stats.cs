using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BaylaGradingSystem.Object;

namespace BaylaGradingSystem
{
    public class Stats
    {
        public static Dictionary<string, double> PeriodStats(List<double> grades)
        {
            Dictionary<string, double> stats = new Dictionary<string, double> {
                { "mean", Formula.Mean(grades) },
                { "median", Formula.Median(grades) },
                { "mode", Formula.Mode(grades).FirstOrDefault()},
                { "range", Formula.Range(grades) },
                { "variance", Formula.Variance(grades) },
                { "standard_dev", Formula.StandardDev(grades) }
            };
            return stats;
        }
        
        public static List<Student> StudentData(DataTable dataTable)
        {
            List<Student> studentStats = new List<Student>();

            foreach (DataRow row in dataTable.Rows)
            {
                double prelimGrade = Math.Floor(Convert.ToDouble(row["prelim"]));
                double midtermGrade = Math.Floor(Convert.ToDouble(row["midterm"]));
                double finalsGrade = Math.Floor(Convert.ToDouble(row["finals"]));

                double finalAverage = Formula.FinalAvg(new List<double> { prelimGrade, midtermGrade, finalsGrade });

                studentStats.Add(new Student
                {
                    name = row["name"].ToString(),
                    grades = new List<Grades> {
                        new Grades { period = "prelim", value = prelimGrade },
                        new Grades { period = "midterm", value = midtermGrade },
                        new Grades { period = "finals", value = finalsGrade }
                    },
                    average = finalAverage
                }); ;
            }

            return studentStats;
        }
    }
}
