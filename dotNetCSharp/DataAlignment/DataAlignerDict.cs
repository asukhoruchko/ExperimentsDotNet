using System;
using System.Collections.Generic;
using System.Linq;

namespace dotNetCSharp.DataAlignment
{
    public class DataAlignerDict : IDataAligner
    {
        private readonly List<IEnumerable<Tuple<double, int>>> _dataToProcess;
        private readonly Dictionary<double, PointFullData> _result;

        public DataAlignerDict(List<IEnumerable<Tuple<double, int>>> dataToProcess, int pointsCount)
        {
            _dataToProcess = dataToProcess;
            _result = new Dictionary<double, PointFullData>(pointsCount);
        }
        
        public IEnumerable<PointFullData> Align()
        {
            foreach (var input in _dataToProcess)
            {
                ProcessSingleInput(input);
            }

            return _result
                .OrderBy(kv => kv.Key)
                .Select(kv =>
                {
                    kv.Value.Avg = Convert.ToInt32(kv.Value.Avg / kv.Value.Count);
                    
                    return kv.Value;
                });
        }

        private void ProcessSingleInput(IEnumerable<Tuple<double, int>> input)
        {
            foreach (var pointData in input)
            {
                var point = Math.Round(pointData.Item1, 1);

                PointFullData value;
                if (_result.TryGetValue(point, out value))
                {
                    value.Count++;
                    value.Avg += pointData.Item2;
                }
                else
                {
                    _result.Add(point, new PointFullData
                    {
                        Point = point,
                        Count = 1,
                        Avg = pointData.Item2
                    });
                }
            }
        }
    }
}