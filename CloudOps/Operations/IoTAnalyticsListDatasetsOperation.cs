using Amazon;
using Amazon.IoTAnalytics;
using Amazon.IoTAnalytics.Model;
using Amazon.Runtime;

namespace CloudOps.Operations
{
    public class IoTAnalyticsListDatasetsOperation : Operation
    {
        public override string Name => "ListDatasets";

        public override string Description => "Retrieves information about data sets.";
 
        public override string RequestURI => "/datasets";

        public override string Method => "GET";

        public override string ServiceName => "IoTAnalytics";

        public override string ServiceID => "IoTAnalytics";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonIoTAnalyticsClient client = new AmazonIoTAnalyticsClient(creds, region);
            ListDatasetsResponse resp = new ListDatasetsResponse();
            do
            {
                ListDatasetsRequest req = new ListDatasetsRequest
                {
                    nextToken = resp.nextToken
                    ,
                    maxResults = maxItems
                                        
                };

                resp = client.ListDatasets(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.datasetSummaries)
                {
                    AddObject(obj);
                }
                
                foreach (var obj in resp.nextToken)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.nextToken));
        }
    }
}