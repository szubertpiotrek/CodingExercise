using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace Domain.DTOs
{
    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public record CustomersGetResponseDto
    {
        public IEnumerable<CustomerDto>? Customers { get; init; }
    }
}
