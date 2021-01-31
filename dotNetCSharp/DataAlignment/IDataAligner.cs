using System;
using System.Collections.Generic;

namespace dotNetCSharp.DataAlignment
{
    public interface IDataAligner
    {
        IEnumerable<PointFullData> Align();
    }
}