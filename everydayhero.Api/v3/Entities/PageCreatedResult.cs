namespace everydayhero.Api.v3
{
    public class PageCreatedResult
    {
        public PageCreated page { get; set; }
        public User user { get; set; }
        public string activation_url { get; set; }
    }
}