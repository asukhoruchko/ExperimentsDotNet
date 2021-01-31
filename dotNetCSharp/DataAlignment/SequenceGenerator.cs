using System;
using System.Collections.Generic;

namespace dotNetCSharp.DataAlignment
{
    public class SequenceGenerator
    {
        private readonly Random _random = new();
        private const double Increment = 1.5d;
        
        private readonly double _precision;

        public SequenceGenerator(int precision)
        {
            _precision = 1/Math.Pow(10, precision);
        }
        
        public IEnumerable<Tuple<double, int>> Generate(int count)
        {
            using var currentPoint = GetNextPoint().GetEnumerator();
            
            for (var i = 0; i < count; ++i)
            {
                currentPoint.MoveNext();
                
                if (_random.Next(100) > 90) continue;
                
                yield return new Tuple<double, int>(currentPoint.Current, _random.Next(1000));
            }
        }

        private IEnumerable<double> GetNextPoint()
        {
            var current = 0.0d;
            while (true)
            {
                yield return current + _precision * (_random.Next(1) == 1 ? 1 : -1);
                current += Increment;
            }
        }
    }
}