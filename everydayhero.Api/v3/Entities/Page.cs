using System.Collections.Generic;

namespace everydayhero.Api.v3
{
    public class Page
    {
       
        public int id { get; set; }
        public string slug { get; set; }
        public bool gift_aid_eligible { get; set; }
        public string charity_uid { get; set; }
        public string campaign_uid { get; set; }
        public int owner_uid { get; set; }
        public string owner_type { get; set; }
        public int uid { get; set; }
        public string state { get; set; }
        public int target_cents { get; set; }
        public string name { get; set; }
        public string team_uid { get; set; }
        public List<string> team_member_uids { get; set; }
        public string team_leader_page_uid { get; set; }
        public string expires_at { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public string url { get; set; }
        public Amount amount { get; set; }
        public int cached_offline_amount_cents { get; set; }
        public FitnessActivityOverview fitness_activity_overview { get; set; }
        public string invitation_id { get; set; }
        public Coordinate coordinate { get; set; }
        public string story { get; set; }
        public Image image { get; set; }
    }
}