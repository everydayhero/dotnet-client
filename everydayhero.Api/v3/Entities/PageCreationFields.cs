namespace everydayhero.Api.v3
{
    public class PageCreationFields
    {
        /// <summary>
        ///     The uid of the user that you want to create a page for.
        /// </summary>
        public string uid { get; set; }

        /// <summary>
        ///     The dollar amount that you are aiming to raise (must be above 0).
        /// </summary>
        public string target { get; set; }

        /// <summary>
        ///     The desired name for your new supporter page. Defaults to the user’s preferred name.
        /// </summary>
        public string name { get; set; }

        /// <summary>
        ///     ISO 8601 Format
        /// </summary>
        public string expires_at { get; set; }

        /// <summary>
        ///     A URL-friendly version of the name parameter. Can only contain lower case characters, numbers, and hyphens. The
        ///     slug will form part of the page URL and must be unique within the campaign. Maximum 60 characters.
        /// </summary>
        public string slug { get; set; }

        public string user_email { get; set; }
        public string user_name { get; set; }

        /// <summary>
        ///     Your birthday, format “YYYY-MM-DD”. In some countries there are age restrictions on fundraising and further action
        ///     might need to be taken depending on the age provided.
        /// </summary>
        public string birthday { get; set; }

        public string campaign_id { get; set; }
        public string charity_id { get; set; }
        public string image { get; set; }
    }
}