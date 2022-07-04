using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WaterMetersMonitor.Application.Exceptions
{
    [Serializable]
    public class ApiException : Exception
    {
        public string ErrorCode { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }

        public ApiException(HttpStatusCode code, string errCode) : base(errCode)
        {
            StatusCode = code;
            ErrorCode = errCode;
        }

        public ApiException(HttpStatusCode code, string errCode, string errMessage): base(errCode)
        {
            StatusCode = code;
            ErrorCode = errCode;
            Message = errMessage;
        }
    }
}
