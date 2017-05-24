using BlueCat.Api.Entity.Entity;
using System.Collections.Generic;

namespace BlueCat.Contract
{
    public class GetStudentsRequest
    {

        public string Name { get; set; }
    }

    public class GetStudentsResponse : BaseInternalResponseDto
    {
        public List<Student> Students { get; set; }
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
        public ResponseStatus() { }
        public ResponseStatus(string responseCode)
        {
            this.ResponseCode = responseCode;
        }
        public ResponseStatus(string responseCode, string message)
        {
            this.ResponseCode = responseCode;
            this.Message = message;
        }

        public string ResponseCode { get; set; }
        public string Message { get; set; }
    }
}
