using EurovisionApi.Server.Database;
using Microsoft.AspNetCore.Mvc;

namespace EurovisionApi.Server.Controllers;

[Route("api/junior/contests")]
[ApiController]
public class JuniorContestsController : BaseContestsController
{
    public JuniorContestsController(DataContext dataContext)
        : base(dataContext.JuniorContests) { }
}
