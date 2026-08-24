namespace Application.Dto
{
    public class ServiceResponse
    {
        public string Message { get; set; }
        public bool Success {  get; set; }
        public ServiceResponse(bool success, string message)

        {
            Success = success;
            Message = message;
        }
    }
}
