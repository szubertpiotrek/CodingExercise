using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace Domain.DTOs
{
    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public record CustomerCreateResponseDto
    {
        public CustomerDto? Customer { get; init; }
    }
}
