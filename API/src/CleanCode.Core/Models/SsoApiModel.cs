namespace CleanCode.Core.Models
{
    public class SsoApiModel
    {
        public string Url { get; set; }
        public SsoEndpoint Endpoint { get; set; }
    }

    public class SsoEndpoint
    {
        public string Application { get; set; }
        public string ValidateToken { get; set; }
        public string Logout { get; set; }
    }
}
