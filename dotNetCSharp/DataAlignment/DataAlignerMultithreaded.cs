using System;
using System.Collections.Generic;
using System.Linq;

namespace dotNetCSharp.DataAlignment
{
    public class DataAlignerMultithreaded : IDataAligner
    {
        private const double Increment = 1.5d;
        
        private readonly int _pointsCount;
        private readonly List<DataLineman> _dataSources;

        public DataAlignerMultithreaded(List<IEnumerable<Tuple<double, int>>> dataToProcess, int pointsCount, double scatter)
        {
            _dataSources = dataToProcess
                .Select(source => new DataLineman(source, scatter))
                .ToList();
            
            _pointsCount = pointsCount;
        }
        
        public IEnumerable<PointFullData> Align()
        {
            throw new System.NotImplementedException();
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