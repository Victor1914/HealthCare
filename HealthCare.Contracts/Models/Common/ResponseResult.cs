namespace HealthCare.Contracts.Models.Common
{
    using Utilities.Enums;

    public class ResponseResult
    {
        public string Message { get; set; }

        public ResponseStatus ResponseStatus { get; set; }
    }
}
