namespace everydayhero.Api.v3
{
    public class PageCreated
    {
        public int id { get; set; }
        public int owner_id { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public int charity_id { get; set; }
        public int campaign_id { get; set; }
        public string slug { get; set; }
        public bool gift_aid_eligible { get; set; }
        public string image_file_name { get; set; }
        public string image_content_type { get; set; }
        public string image_file_size { get; set; }
        public string image_updated_at { get; set; }
        public string story { get; set; }
        public string state { get; set; }
        public string expires_at { get; set; }
        public int target_cents { get; set; }
        public string owner_type { get; set; }
        public string image_fingerprint { get; set; }
        public int cached_amount_cents { get; set; }
        public int cached_offline_amount_cents { get; set; }
        public int cached_online_amount_cents { get; set; }
        public string name { get; set; }
        public bool hide_suggestions { get; set; }
        public string campaign_date { get; set; }
        public int cached_gift_aid_amount_cents { get; set; }
        public object data { get; set; }
    }
}