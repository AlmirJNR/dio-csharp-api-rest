using CsharpApiRest.Models;
using CsharpApiRest.Enums;
using Microsoft.AspNetCore.Mvc;

namespace CsharpApiRest.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class SeriesController : ControllerBase
{
    private readonly SeriesRepository _seriesRepository;

    public SeriesController(SeriesRepository seriesRepository)
    {
        _seriesRepository = seriesRepository;
    }

    /// <summary>
    /// Creates a new Serie
    /// </summary>
    /// <response code="201">Returns the created serie</response>
    /// <response code="400">If there is an error</response>
    [HttpPost("new")]
    [ProducesResponseType(typeof(SerieModel), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostNewSerie([FromBody] SerieModel serie)
    {
        if (string.IsNullOrWhiteSpace(serie.Title))
        {
            return BadRequest("Serie Title is obrigatory");
        }

        if ((int)serie.Genre < 0 || (int)serie.Genre >= Enum.GetNames<GenreEnum>().Length)
        {
            return BadRequest($"Serie Genre must be between 0 and {Enum.GetNames<GenreEnum>().Length - 1}");
        }

        _seriesRepository.Create(serie);
        return Created(string.Empty, serie);
    }

    /// <summary>
    /// Gets all registered series
    /// </summary>
    /// <response code="200">Returns all series even when it's empty</response>
    [HttpGet("all")]
    [ProducesResponseType(typeof(List<SerieModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllSeries() => Ok(_seriesRepository.ReadAll());

    /// <summary>
    /// Gets registered serie by id
    /// </summary>
    /// <response code="200">Returns serie</response>
    /// <response code="404">Returns Serie with the current id was not found</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(SerieModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSerieById([FromRoute] int id)
    {
        if (!_seriesRepository.ReadById(id, out SerieModel? serieModel))
        {
            return NotFound($"Serie with the current id was not found");
        }

        return Ok(serieModel);
    }

    /// <summary>
    /// Update serie by id
    /// </summary>
    /// <response code="200">Returns updated serie</response>
    /// <response code="404">Returns Serie with the current id was not found</response>
    [HttpPatch("{id}")]
    [ProducesResponseType(typeof(SerieModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateSerieById([FromRoute] int id, [FromBody] SerieModel serieModel)
    {
        if (!_seriesRepository.Update(id, serieModel))
        {
            return NotFound($"Serie with the current id was not found");
        }

        return Ok(serieModel);
    }

    /// <summary>
    /// Delete all series
    /// </summary>
    /// <response code="200">Returns Series has been deleted</response>
    [HttpDelete("all")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteAllSeries()
    {
        _seriesRepository.DeleteAll();
        return Ok("Series has been deleted");
    }

    /// <summary>
    /// Delete serie by id
    /// </summary>
    /// <response code="200">Returns Serie has been deleted</response>
    /// <response code="404">Returns Serie with the current id was not found</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteSerieById([FromRoute] int id)
    {
        if (!_seriesRepository.DeleteById(id))
        {
            return NotFound($"Serie with the current id was not found");
        }

        return Ok("Serie has been deleted");
    }
}
