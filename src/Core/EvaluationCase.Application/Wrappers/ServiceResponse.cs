using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationCase.Application.Wrappers
{
    public class ServiceResponse<T>: BaseResponse
    {
        public T Value { get; set; }

        public ServiceResponse(T value)
        {
            Value = value;
        }
    }
}
