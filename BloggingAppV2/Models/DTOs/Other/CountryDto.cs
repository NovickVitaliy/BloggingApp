using Microsoft.AspNetCore.Mvc;

namespace BloggingApp.Web.Models.DTOs;

public class CountryDto
{
    public string? Name { get; set; }
    public string? FlagUrl { get; set; }
}