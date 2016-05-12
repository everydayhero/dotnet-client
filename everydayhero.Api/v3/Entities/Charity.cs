using System.Collections.Generic;

namespace everydayhero.Api.v3
{
    public class Charity
    {
        public string id { get; set; }
        public string name { get; set; }
        public string slug { get; set; }
        public string country_code { get; set; }
        public string description { get; set; }
        public bool gift_aid { get; set; }
        public string logo_url { get; set; }
        public string url { get; set; }
        public string get_started_url { get; set; }
        public string donate_url { get; set; }
        public Image image { get; set; }
        public string twitter_url { get; set; }
        public string facebook_url { get; set; }
        public string website_url { get; set; }
        public string tax_number { get; set; }
        public string tax_number_label { get; set; }
        public string registration_number { get; set; }
        public string registration_number_label { get; set; }
        public string merchant_name { get; set; }
        public int page_count { get; set; }
        public string public_email { get; set; }
        public string phone { get; set; }
        public string financial_context_id { get; set; }
        public string merchant_id { get; set; }
        public List<Cause> causes { get; set; }
        public Currency currency { get; set; }
        public LastYearDonationTotal last_year_donation_total { get; set; }
    }
}