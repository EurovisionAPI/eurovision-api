using System.Security.Cryptography;
using System.Text;
using EurovisionApi.Server.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace EurovisionApi.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DatasetController : ControllerBase
{
    private readonly DataContext _dataContext;
    private readonly Settings _settings;

    public DatasetController(DataContext dataContext, IOptions<Settings> options)
    {
        _dataContext = dataContext;
        _settings = options.Value;
    }

    [HttpGet("update")]
    public async Task<ActionResult> UpdateDatasetAsync(string releaseName)
    {
        // 1. Get github signature from header.
        if (!Request.Headers.TryGetValue("X-Hub-Signature-256", out StringValues signatureHeader))
            return Unauthorized("Signature not found");

        // 2. Calculate the HMAC SHA256 secret hash.
        byte[] secretBytes = Encoding.UTF8.GetBytes(_settings.UpdateDatasetSecret);
        byte[] releaseNameBytes = Encoding.UTF8.GetBytes(releaseName);
        using HMACSHA256 hmac = new HMACSHA256(secretBytes);
        byte[] hashBytes = hmac.ComputeHash(releaseNameBytes);

        // 3. Compare the received hash with the calculated hash.
        byte[] signatureBytes = Convert.FromHexString(signatureHeader);
        if (!CryptographicOperations.FixedTimeEquals(signatureBytes, hashBytes))
            return Unauthorized("Invalid signature");

        // 4. Update DataContext.
        await _dataContext.DownloadDatasetAsync(releaseName);

        return Ok();
    }
}
