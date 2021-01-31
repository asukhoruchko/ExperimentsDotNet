using System;
using System.Collections.Generic;

namespace dotNetCSharp.DataAlignment
{
    public class DataLineman
    {
        private bool _hasData;
        private IEnumerator<Tuple<double, int>> _source;
        private readonly double _scatter;

        public DataLineman(IEnumerable<Tuple<double, int>> source, double scatter)
        {
            _source = source.GetEnumerator();
            _hasData = _source.MoveNext();

            _scatter = scatter;
        }

        public bool GetCurrentValue(double point, out int value)
        {
            value = 0;
            if (!_hasData) return false;
            
            var result = false;

            var hi = point + _scatter;
            var lo = point - _scatter;
            
            var current = _source.Current;
            var currentPoint = current.Item1;

            if (currentPoint >= lo && currentPoint <= hi)
            {
                result = true;
                value = current.Item2;
                
                _hasData = _source.MoveNext();
            }

            return result;
        }
    }
}