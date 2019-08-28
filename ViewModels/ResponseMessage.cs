using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApplication.DataObjects
{
    public class ResponseMessage
    {
        public ResponseMessage()
        {
            StatusCode = 0;
            Message = "Sucessfull";
            Data = null;
        }
        public ResponseMessage(int statusCode, string message, object data)
        {
            this.StatusCode = statusCode;
            this.Message = message;
            this.Data = data;
        }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
