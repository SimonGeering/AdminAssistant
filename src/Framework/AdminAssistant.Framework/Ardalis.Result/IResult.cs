//----------------------------------------------------------------------------------------------------------------------
//     Author:  Steve Smith (https://github.com/ardalis)
//     GitHub:  https://github.com/ardalis/Result/blob/master/src/Ardalis.Result/IResult.cs
//     License: MIT
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Ardalis.Result
{
    public interface IResult
    {
        ResultStatus Status { get; }
        IEnumerable<string> Errors { get; }
        Dictionary<string, string> ValidationErrors { get; }
        Type ValueType { get; }
        Object GetValue();
    }
}
