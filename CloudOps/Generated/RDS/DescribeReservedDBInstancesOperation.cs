using Amazon;
using Amazon.RDS;
using Amazon.RDS.Model;
using Amazon.Runtime;

namespace CloudOps.RDS
{
    public class DescribeReservedDBInstancesOperation : Operation
    {
        public override string Name => "DescribeReservedDBInstances";

        public override string Description => "Returns information about reserved DB instances for this account, or about a specified reserved DB instance.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "RDS";

        public override string ServiceID => "RDS";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonRDSClient client = new AmazonRDSClient(creds, region);
            DescribeReservedDBInstancesResponse resp = new DescribeReservedDBInstancesResponse();
            do
            {
                DescribeReservedDBInstancesRequest req = new DescribeReservedDBInstancesRequest
                {
                    Marker = resp.Marker
                    ,
                    MaxRecords = maxItems
                                        
                };

                resp = client.DescribeReservedDBInstances(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.ReservedDBInstances)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.Marker));
        }
    }
}