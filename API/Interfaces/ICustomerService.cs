using Domain.DTOs;

namespace API.Interfaces
{
    public interface ICustomerService
    {
        public Task<CustomersGetResponseDto> GetCustomerListAsync();
        public Task DeleteCustomerAsync(int id);
        public Task<CustomerCreateResponseDto> AddCustomerAsync(CustomerCreateRequestDto customer);
    }
}
