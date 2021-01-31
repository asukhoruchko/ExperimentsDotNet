using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using dotNetCSharp.DataAlignment;

namespace dotNetCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            const int pointCount = 1000000;
            DoWork(new DataAligner(GenerateData(pointCount), pointCount, 0.001d));
            DoWork(new DataAlignerDict(GenerateData(pointCount), pointCount));
        }

        private static void DoWork(IDataAligner da)
        {
            var sw = new Stopwatch();
            sw.Start();
            var dict = new Dictionary<int, int>();
            foreach (var point in da.Align())
            {
                int count;
                if (dict.TryGetValue(point.Count, out count))
                {
                    count++;
                    dict[point.Count] = count;
                }
                else
                {
                    dict.Add(point.Count, 1);
                }
            }
            sw.Stop();

            Console.WriteLine($"{sw.ElapsedMilliseconds}");
            Console.WriteLine();
            /*foreach (var kv in dict.OrderBy(kv => kv.Value))
            {
                Console.WriteLine($"{kv.Key} {kv.Value}");
            }*/
        }

        private static List<IEnumerable<Tuple<double, int>>> GenerateData(int pointCount)
        {
            const int count = 10;
            var result = new List<IEnumerable<Tuple<double, int>>>(count);
            
            var sg = new SequenceGenerator(10);
            for (var i = 0; i < count; ++i)
            {
                result.Add(sg.Generate(pointCount));
            }

            return result;
        }
    }
}