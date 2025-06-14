using EurovisionApi.Server.Database.Models.Schemes;
using EurovisionApi.Server.Database.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EurovisionApi.Server.Controllers;

public abstract class BaseContestsController : ControllerBase
{
    private readonly ContestRepository _repository;

    public BaseContestsController(ContestRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("years")]
    public IEnumerable<int> GetAllYears()
    {
        return _repository.GetYears();
    }

    [HttpGet]
    public IEnumerable<ContestReference> GetAllContests()
    {
        return _repository.GetContests();
    }

    [HttpGet("{year:int}")]
    public ActionResult<Contest> GetContestByYear(int year)
    {
        Contest contest = _repository.GetContest(year);

        return contest != null ? contest : NotFound();
    }

    [HttpGet("{year:int}/contestants/{id:int}")]
    public ActionResult<Contestant> GetContestantById(int year, int id)
    {
        Contestant contestant = _repository.GetContestant(year, id);

        return contestant != null ? contestant : NotFound();
    }
}
