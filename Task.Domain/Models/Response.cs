namespace Task.Domain.Models
{
    public class Response
    {
        public string Status { get; set; }
        public string MessageEn { get; set; }
        public string MessageAr { get; set; }
        public dynamic Errors { get; set; }
        public dynamic Data { get; set; }
    }
}
