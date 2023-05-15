using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Domain.DTOs
{
    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public record CustomerCreateRequestDto
    {
        [Required]
        public CustomerDto? Customer { get; init; }
    }
}
