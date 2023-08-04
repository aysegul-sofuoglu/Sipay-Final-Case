namespace Schema
{
    public class TokenResponse
    {
        public DateTime ExpireTime { get; set; }
        public string AccessToken { get; set; }
        public string Email { get; set; }
    }
}
