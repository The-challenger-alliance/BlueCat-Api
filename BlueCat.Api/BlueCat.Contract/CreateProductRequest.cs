using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueCat.Contract
{
    public class CreateProductRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }


    public class CreateProductResponse : BaseInternalResponseDto
    {
        public bool ResponseResult { get; set; }
    }

    public abstract class BaseInternalResponseDto : BaseResponseDto
    {
        public BaseInternalResponseDto() : base() { }
    }

    public abstract class BaseResponseDto
    {
        public ResponseStatus ResponseStatus { get; set; }
    }

    public class ResponseStatus
    {
        public ResponseStatus(){}
        public ResponseStatus(string errorCode)
        {
            this.ErrorCode = errorCode;
        }
        public ResponseStatus(string errorCode, string message)
        {
            this.ErrorCode = errorCode;
            this.Message = message;
        }

        public string ErrorCode { get; set; }
        public string Message { get; set; }
    }
}
