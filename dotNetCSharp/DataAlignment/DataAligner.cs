using System;
using System.Collections.Generic;
using System.Linq;

namespace dotNetCSharp.DataAlignment
{
    public class DataAligner : IDataAligner
    {
        private const double Increment = 1.5d;
        
        private readonly int _pointsCount;
        private readonly List<DataLineman> _dataSources;

        public DataAligner(List<IEnumerable<Tuple<double, int>>> dataToProcess, int pointsCount, double scatter)
        {
            _dataSources = dataToProcess
                .Select(source => new DataLineman(source, scatter))
                .ToList();
            
            _pointsCount = pointsCount;
        }

        public IEnumerable<PointFullData> Align()
        {
            foreach (var currentPoint in GetPoints())
            {
                var total = 0;
                var count = 0;
                foreach (var source in _dataSources)
                {
                    int value;
                    if (!source.GetCurrentValue(currentPoint, out value)) continue;

                    total += value;
                    ++count;
                }

                yield return new PointFullData
                {
                    Point = currentPoint,
                    Count = count,
                    Avg = Convert.ToInt32(total / count)
                };
            }
        }

        private IEnumerable<double> GetPoints()
        {
            var current = 0.0d;
            for (var i = 0; i < _pointsCount; ++i)
            {
                yield return current;
                current += Increment;
            }
        }
    }
}