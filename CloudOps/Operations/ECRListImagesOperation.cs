using Amazon;
using Amazon.ECR;
using Amazon.ECR.Model;
using Amazon.Runtime;

namespace CloudOps.Operations
{
    public class ECRListImagesOperation : Operation
    {
        public override string Name => "ListImages";

        public override string Description => "Lists all the image IDs for a given repository. You can filter images based on whether or not they are tagged by setting the tagStatus parameter to TAGGED or UNTAGGED. For example, you can filter your results to return only UNTAGGED images and then pipe that result to a BatchDeleteImage operation to delete them. Or, you can filter your results to return only TAGGED images to list all of the tags in your repository.";
 
        public override string RequestURI => "";

        public override string Method => "";

        public override string ServiceName => "ECR";

        public override string ServiceID => "ECR";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonECRClient client = new AmazonECRClient(creds, region);
            Response resp = new Response();
            do
            {
                ListImagesRequest req = new ListImagesRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListImages(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.ImageIds)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}