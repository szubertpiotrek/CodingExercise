using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Domain.DTOs
{
    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public record CustomerDto
    {
        [Required]
        public int Id { get; init; }
        [Required]
        public string? Firstname { get; init; }
        [Required]
        public string? Surname { get; init; }
    }
}
