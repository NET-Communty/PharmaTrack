using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstractions.HandlerResponse
{
    public class ServiceResponse<T>
    {
        public ServiceResponse()
        {
           
        }
        public ServiceResponse(T data,string? message = null)
        {
            Data = data;
            StatusCode = 200; // Default to OK
            Message = message ?? string.Empty;
        }

        public int StatusCode { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; }
        public T Data { get; set; }
      
    }
}
