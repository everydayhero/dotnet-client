using System.Collections.Generic;
using System.Net;
using everydayhero.Api.Constants;
using everydayhero.Api.Exceptions;
using RestSharp;

namespace everydayhero.Api.v3
{
    /// <summary>
    ///     Interacts with the Pages api.  This is available through the <see cref="Client" /> class.
    /// </summary>
    public class PagesClient : ClientBase
    {
        protected RestClient RestClient;

        internal PagesClient(RestClient restClient)
        {
            RestClient = restClient;
        }

        public PageCreatedResult CreateSupporterPage(PageCreationFields<string> options)
        {
            var request = CreateRequest(EndPointConstants.ApiBaseV3, ServiceNameConstants.Pages, null, Method.POST, options);
            var result = RestClient.Execute<PageCreatedResult>(request);
            if (result.StatusCode == HttpStatusCode.OK || result.StatusCode == HttpStatusCode.Created)
            {
                return result.Data;
            }
            throw ExceptionHandler.GetFailedRequestException(result);
        }

        public PageCreatedResult CreateSupporterPage(PageCreationFields<UserAddress>  options)
        {
            var request = CreateRequest(EndPointConstants.ApiBaseV3, ServiceNameConstants.Pages, null, Method.POST, options);
            var result = RestClient.Execute<PageCreatedResult>(request);
            if (result.StatusCode == HttpStatusCode.OK || result.StatusCode == HttpStatusCode.Created)
            {
                return result.Data;
            }
            throw ExceptionHandler.GetFailedRequestException(result);
        }

        public List<Page> GetSupporterPages(int page, int limit, string campaignUid)
        {
            var request = CreateRequest(EndPointConstants.ApiBaseV2, ServiceNameConstants.Pages,
                $"limit={limit}&page={page}&campaign_uid={campaignUid}", Method.GET);
            var result = RestClient.Execute<PagesResult>(request);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                return result.Data.pages;
            }
            throw ExceptionHandler.GetFailedRequestException(result);
        }
        public List<Page> GetSupporterPages(int[] pageIds)
        {
            var request = CreateRequest(EndPointConstants.ApiBaseV2, ServiceNameConstants.Pages,
                $"limit={pageIds.Length}&page={1}&id={string.Join(",", pageIds)}", Method.GET);
            var result = RestClient.Execute<PagesResult>(request);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                return result.Data.pages;
            }
            throw ExceptionHandler.GetFailedRequestException(result);
        }

        public Page UpdateSupporterPage(int id, PageCreationFields<string> updateDetails)
        {
            var request = CreateRequest(EndPointConstants.ApiBaseV3, ServiceNameConstants.Pages + $"/{id}", null, Method.PUT, updateDetails);
            var result = RestClient.Execute<PageResult>(request);
            if (result.StatusCode == HttpStatusCode.OK || result.StatusCode == HttpStatusCode.Accepted) //TODO: check the return code on a succesful result
            {
                return result.Data.page;
            }
            throw ExceptionHandler.GetFailedRequestException(result);
        }

        public Page UpdateSupporterPage(int id, PageCreationFields<UserAddress> updateDetails)
        {
            var request = CreateRequest(EndPointConstants.ApiBaseV3, ServiceNameConstants.Pages + $"/{id}", null, Method.PUT, updateDetails);
            var result = RestClient.Execute<PageResult>(request);
            if (result.StatusCode == HttpStatusCode.OK || result.StatusCode == HttpStatusCode.Accepted) //TODO: check the return code on a succesful result
            {
                return result.Data.page;
            }
            throw ExceptionHandler.GetFailedRequestException(result);
        }
    }
}