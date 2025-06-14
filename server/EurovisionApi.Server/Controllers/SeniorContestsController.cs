using EurovisionApi.Server.Database;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EurovisionApi.Server.Controllers;

[Route("api/senior/contests")]
[ApiController]
public class SeniorContestsController : BaseContestsController
{
    public SeniorContestsController(DataContext dataContext)
        : base(dataContext.SeniorContests) { }
}
