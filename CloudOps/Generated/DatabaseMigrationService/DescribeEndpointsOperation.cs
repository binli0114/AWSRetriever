using Amazon;
using Amazon.DatabaseMigrationService;
using Amazon.DatabaseMigrationService.Model;
using Amazon.Runtime;

namespace CloudOps.DatabaseMigrationService
{
    public class DescribeEndpointsOperation : Operation
    {
        public override string Name => "DescribeEndpoints";

        public override string Description => "Returns information about the endpoints for your account in the current region.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "DatabaseMigrationService";

        public override string ServiceID => "Database Migration Service";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonDatabaseMigrationServiceConfig config = new AmazonDatabaseMigrationServiceConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonDatabaseMigrationServiceClient client = new AmazonDatabaseMigrationServiceClient(creds, config);
            
            DescribeEndpointsResponse resp = new DescribeEndpointsResponse();
            do
            {
                DescribeEndpointsRequest req = new DescribeEndpointsRequest
                {
                    Marker = resp.Marker
                    ,
                    MaxRecords = maxItems
                                        
                };

                resp = client.DescribeEndpoints(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Endpoints)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.Marker));
        }
    }
}