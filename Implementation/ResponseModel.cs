
namespace Alpha_Bank.Implementation
{
    public class ResponseModel
    {
        public Response Response { get; set; }
        public string Reason { get; set; }
    }

    public enum Response
    {
        Success,
        Failed
    }
}
