using System.Net;

namespace Appointments.Domain.DTOs
{
    public class ExceptionResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public string Detail { get; set; } = string.Empty;
        public string Instance { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string StackTrace { get; set; } = string.Empty;
    }
}