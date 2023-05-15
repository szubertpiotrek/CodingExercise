using API.Interfaces;
using AutoMapper;
using Domain.DTOs;
using Domain.Entities;
using Domain.Interfces;

namespace API.Services.Customers
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomerService(IUnitOfWork unitOfWork, IMapper mapper) 
        { 
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CustomerCreateResponseDto> AddCustomerAsync(CustomerCreateRequestDto customer)
        {
            var repository = _unitOfWork.Repository<Customer>();
            var mappedCustomer = _mapper.Map<Customer>(customer);
            var createdCustomer = await repository.AddAsync(mappedCustomer);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<CustomerCreateResponseDto>(createdCustomer);
        }

        public async Task DeleteCustomerAsync(int id)
        {
            var repository = _unitOfWork.Repository<Customer>();
            repository.Delete(new Customer() { Id = id });
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<CustomersGetResponseDto> GetCustomerListAsync()
        {
            var repository = _unitOfWork.Repository<Customer>();
            var receivedCustomers = await repository.ListAsync();
            return _mapper.Map<CustomersGetResponseDto>(receivedCustomers);
        }
    }
}
