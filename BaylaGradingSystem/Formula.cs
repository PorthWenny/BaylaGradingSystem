using BaylaGradingSystem.Object;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaylaGradingSystem
{
    public class Formula
    {
        public static double Mean (List<double> all_grades)
        {
            return all_grades.Sum() / all_grades.Count();
        }

        public static double Median (List<double> all_grades)
        {
            List<double> sorted_grades = all_grades.OrderBy(g => g).ToList();
            if (sorted_grades.Count % 2 != 0) {
                return sorted_grades[(sorted_grades.Count / 2)];
            } else {
                return (sorted_grades[(sorted_grades.Count / 2)] + sorted_grades[(sorted_grades.Count / 2) - 1]) / 2.0;
            }
        }

        public static List<double> Mode (List<double> all_grades)
        {
            Dictionary<double, int> counts = new Dictionary<double, int>();

            foreach (var grade in all_grades) {
                if (counts.ContainsKey(grade)) {
                    counts[grade]++;
                } else {
                    counts[grade] = 1;
                }
            }

            int max = counts.Values.Max();

            List<double> modes = counts.Where(pair => pair.Value == max).Select(pair => pair.Key).ToList();

            if (modes.Count != counts.Count)
            {
                return modes;
            }

            return new List<double>();
        }

        public static double Range (List<double> all_grades)
        {
            return all_grades.Max() - all_grades.Min();
        }

        public static double Variance (List<double> all_grades)
        {
            double mean = Mean(all_grades);
            double sum_sq = all_grades.Sum(g => Math.Pow(g - mean, 2));
            return sum_sq / all_grades.Count;
        }

        public static double StandardDev (List<double> all_grades)
        {
            return Math.Sqrt(Variance(all_grades));
        }

        public static double FinalAvg (List<double> all_grades)
        {
            return Math.Ceiling(all_grades[0] * 0.3 + all_grades[1] * 0.3 + all_grades[2] * 0.4);
        }
    }
}
