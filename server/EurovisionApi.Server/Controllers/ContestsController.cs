using EurovisionApi.Server.Database;
using EurovisionApi.Server.Database.Models.Schemes;
using EurovisionApi.Server.Database.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EurovisionApi.Server.Controllers;

[Route("api/[controller]")]
[Route("api/junior/[controller]")]
[ApiController]
public class ContestsController : ControllerBase
{
    private readonly DataContext _dataContext;

    private ContestRepository ContestRepository
    {
        get
        {
            string routeTemplate = ControllerContext.ActionDescriptor.AttributeRouteInfo.Template;

            return routeTemplate.StartsWith("api/junior/")
                ? _dataContext.JuniorContests
                : _dataContext.SeniorContests;
        }
    }

    public ContestsController(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    [HttpGet("years")]
    public IEnumerable<int> GetAllYears()
    {
        return ContestRepository.GetYears();
    }

    [HttpGet]
    public IEnumerable<ContestReference> GetAllContests()
    {
        return ContestRepository.GetContests();
    }

    [HttpGet("{year:int}")]
    public ActionResult<Contest> GetContestByYear(int year)
    {
        Contest contest = ContestRepository.GetContest(year);

        return contest != null ? contest : NotFound();
    }

    [HttpGet("{year:int}/contestants/{id:int}")]
    public ActionResult<Contestant> GetContestantById(int year, int id)
    {
        Contestant contestant = ContestRepository.GetContestant(year, id);

        return contestant != null ? contestant : NotFound();
    }
}
