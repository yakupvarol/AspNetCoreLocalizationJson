using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreLocalizationJson.Controllers
{
    //[Route("test")]
    [Route("{lang:lang}")]
    public class TestRouteController : Controller
    {
        //https://docs.microsoft.com/tr-tr/aspnet/core/mvc/controllers/routing?view=aspnetcore-3.1
        //http://www.borakasmer.com/asp-net-mvc-5de-attribute-routing/
        //https://www.yusufsezer.com.tr/asp-net-mvc-routing/

        [Route("Hizmetler/{id?}")]
        public IActionResult Index(int? id)
        {
            return View();
        }


        /*
        [HttpPost, ActionName("sil")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("~/books/{name:maxlength(5)?}")]
        public ActionResult Index(string name)
        {
            ViewBag.Title = name;
            return View();
        }

        [Route("~/books/{id:int}")]
        public ActionResult Index(int id)
        {
            ViewBag.Title = id;
            return View();
        }
        
        [Route("~/books/{id:int:min(1)}")]
        public ActionResult Index(int id)
        {
            ViewBag.Title = id;
            return View();
        }

        [Route("~/books/{name:allowlist(bora|duru)}")]
        public ActionResult Index(string name)
        {
            ViewBag.Title = name;
            return View();
        }

        [HttpGet("loh/{size=85000}")]
        public int GetLOH1(int size)
        {
            return new byte[size].Length;
        }

        [Route("")]
        [Route("Home")]
        [Route("[controller]/[action]")]
        public IActionResult Index()
        {
            return null;
        }

        [HttpGet]
        public IActionResult ListProducts()
        {
            return null;
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            return null;
        }

        [HttpGet("int/{id:int}")]
        public IActionResult GetIntProduct(int id)
        {
            return null;
        }

        [HttpPost("save")]
        public IActionResult PostIntsave(int id)
        {
            return null;
        }

        [HttpGet("/products2/{id}", Name = "Products_List")]
        public IActionResult GetProductTest(int id)
        {
            return null;
        }

        [Route("About")]
        public IActionResult About()
        {
            return null;
        }

        [Route("Home", Order = 2)]
        [Route("Home/MyIndex")]
        public IActionResult MyIndex()
        {
            return null;
        }

        [HttpGet("[controller]/[action]")]  // Matches '/Products20/List'
        public IActionResult List()
        {
            return null;
        }

        [HttpGet("[controller]/[action]/{id}")]   // Matches '/Products20/Edit/{id}'
        public IActionResult Edit(int id)
        {
            return null;
        }

        [HttpPost("product14/{id:int}")]
        public IActionResult ShowProduct(int id)
        {
            return null;
        }

        [HttpGet("custom/url/to/destination")]
        public IActionResult Destination()
        {
            return null;
        }
        */
    }
}


/*
 <tbody><tr><td>alpha</td><td>{x:alpha}</td></tr><tr><td>bool</td><td>{x:bool}</td></tr><tr><td>datetime</td><td>{x:datetime}</td></tr><tr><td>decimal</td><td>{x:decimal}</td></tr><tr><td>double</td><td>{x:double}</td></tr><tr><td>float</td><td>{x:float}</td></tr><tr><td>guid</td><td>{x:guid}</td></tr><tr><td>int</td><td>{x:int}</td></tr><tr><td>length</td><td>{x:length(6)}<br>{x:length(1,20)}</td></tr><tr><td>long</td><td>{x:long}</td></tr><tr><td>max</td><td>{x:max(10)}</td></tr><tr><td>maxlength</td><td>{x:maxlength(10)}</td></tr><tr><td>min</td><td>{x:min(10)}</td></tr><tr><td>minlength</td><td>{x:minlength(10)}</td></tr><tr><td>range</td><td>{x:range(10,50)}</td></tr><tr><td>regex</td><td>{x:regex(^\d{3}-\d{3}-\d{4}$)}</td></tr></tbody>
     */
     /*
[HttpPost("UploadFiles")]
//OPTION A: Disables Asp.Net Core's default upload size limit
[DisableRequestSizeLimit]
//OPTION B: Uncomment to set a specified upload file limit
//[RequestSizeLimit(40000000)] 

public async Task<IActionResult> Post(List<IFormFile> files)
{
    var uploadSuccess = false;
    string uploadedUri = null;

    foreach (var formFile in files)
    {
        if (formFile.Length <= 0)
        {
            continue;
        }

        // NOTE: uncomment either OPTION A or OPTION B to use one approach over another

        // OPTION A: convert to byte array before upload
        //using (var ms = new MemoryStream())
        //{
        //    formFile.CopyTo(ms);
        //    var fileBytes = ms.ToArray();
        //    uploadSuccess = await UploadToBlob(formFile.FileName, fileBytes, null);

        //}

        // OPTION B: read directly from stream for blob upload      
        using (var stream = formFile.OpenReadStream())
        {
            (uploadSuccess, uploadedUri) = await UploadToBlob(formFile.FileName, null, stream);
            TempData["uploadedUri"] = uploadedUri;
        }

    }

    if (uploadSuccess)
        return View("UploadSuccess");
    else
        return View("UploadError");
}

private async Task<(bool, string)> UploadToBlob(string filename, byte[] imageBuffer = null, Stream stream = null)
{
    CloudStorageAccount storageAccount = null;
    CloudBlobContainer cloudBlobContainer = null;
    string storageConnectionString = _configuration["storageconnectionstring"];

    // Check whether the connection string can be parsed.
    if (CloudStorageAccount.TryParse(storageConnectionString, out storageAccount))
    {
        try
        {
            // Create the CloudBlobClient that represents the Blob storage endpoint for the storage account.
            CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();

            // Create a container called 'uploadblob' and append a GUID value to it to make the name unique. 
            cloudBlobContainer = cloudBlobClient.GetContainerReference("uploadblob" + Guid.NewGuid().ToString());
            await cloudBlobContainer.CreateAsync();

            // Set the permissions so the blobs are public. 
            BlobContainerPermissions permissions = new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            };
            await cloudBlobContainer.SetPermissionsAsync(permissions);

            // Get a reference to the blob address, then upload the file to the blob.
            CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(filename);

            if (imageBuffer != null)
            {
                // OPTION A: use imageBuffer (converted from memory stream)
                await cloudBlockBlob.UploadFromByteArrayAsync(imageBuffer, 0, imageBuffer.Length);
            }
            else if (stream != null)
            {
                // OPTION B: pass in memory stream directly
                await cloudBlockBlob.UploadFromStreamAsync(stream);
            }
            else
            {
                return (false, null);
            }

            return (true, cloudBlockBlob.SnapshotQualifiedStorageUri.PrimaryUri.ToString());
        }
        catch (StorageException ex)
        {
            return (false, null);
        }
        finally
        {
            // OPTIONAL: Clean up resources, e.g. blob container
            //if (cloudBlobContainer != null)
            //{
            //    await cloudBlobContainer.DeleteIfExistsAsync();
            //}
        }
    }
    else
    {
        return (false, null);
    }

}
*/