using EurovisionApi.Server.Database;
using Microsoft.AspNetCore.Mvc;

namespace EurovisionApi.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CountriesController : ControllerBase
{
    private readonly DataContext _dataContext;

    public CountriesController(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    [HttpGet]
    public IReadOnlyDictionary<string, string> GetCountries()
    {
        return _dataContext.Countries;
    }

    [HttpGet("{code}")]
    public ActionResult<string> GetCountryName(string code)
    {
        return _dataContext.Countries.TryGetValue(code, out string name)
            ? name
            : NotFound();
    }
}
