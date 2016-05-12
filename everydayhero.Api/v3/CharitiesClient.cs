using System.Collections.Generic;
using System.Net;
using everydayhero.Api.Constants;
using everydayhero.Api.Exceptions;
using RestSharp;

namespace everydayhero.Api.v3
{
    public class CharitiesClient : ClientBase
    {
        protected RestClient RestClient;

        internal CharitiesClient(RestClient restClient)
        {
            RestClient = restClient;
        }

        public List<Charity> GetCharities(string[] campaignIds)
        {
            var request = CreateRequest(EndPointConstants.ApiBaseV2, ServiceNameConstants.Charities, string.Format("campaign_ids={0}", string.Join(",", campaignIds)), Method.GET);
            var result = RestClient.Execute<CharitiesResult>(request);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                return result.Data.charities;
            }
            throw ExceptionHandler.GetFailedRequestException(result);
        }

        public Charity GetCharity(string campaignId)
        {
            var request = CreateRequest(EndPointConstants.ApiBaseV2, string.Format("{0}/{1}", ServiceNameConstants.Charities, campaignId), string.Empty, Method.GET);
            var result = RestClient.Execute<CharityResult>(request);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                return result.Data.charity;
            }
            throw ExceptionHandler.GetFailedRequestException(result);
        }
    }
}