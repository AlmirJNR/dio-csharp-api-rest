using System.Text.Json;
using CsharpApiRest.Enums;
using CsharpApiRest.Interfaces;

namespace CsharpApiRest.Models;

public class SerieModel : IIdentifiable
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public GenreEnum Genre { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime InsertedAt { get; set; } = DateTime.Now.Date;
    public bool Deleted { get; set; } = false;
    public DateTime? DeletedAt { get; set; } = null;

    public override string ToString() => JsonSerializer.Serialize(this);
}