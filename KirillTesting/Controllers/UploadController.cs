using DevExpress.Data.Helpers;
using KirillTesting.ImportExport;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KirillTesting.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UploadController : ControllerBase
{
    private readonly PersonsImporter _personsImporter;
    public UploadController(PersonsImporter personsImporter)
    {
        _personsImporter = personsImporter;
    }

    [HttpPost("[action]")]
    public async Task<ActionResult> UploadAsync(IFormFile myFile)
    {
        try
        {
            using (var stream = new MemoryStream())
            {
                myFile.CopyTo(stream);
                stream.Seek(0, SeekOrigin.Begin);
                await _personsImporter.ImportAsync(stream);
            }
        }
        catch(Exception ex)
        {
            return BadRequest();
        }
        return Ok();
    }
}
