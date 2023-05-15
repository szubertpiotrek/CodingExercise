using AutoMapper;
using Domain.DTOs;
using Domain.Entities;

namespace Infrastructure.Mappers
{
    public class CustomerMappingProfile: Profile
    {
        public CustomerMappingProfile() 
        {
            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerDto, Customer>();
            CreateMap<CustomerCreateRequestDto, Customer>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Customer.Id))
                .ForMember(dest => dest.Firstname, opts => opts.MapFrom(src => src.Customer.Firstname))
                .ForMember(dest => dest.Surname, opts => opts.MapFrom(src => src.Customer.Surname));
            CreateMap<Customer, CustomerCreateResponseDto>()
                .ForMember(dest => dest.Customer, opts => opts.MapFrom(src => src));
            CreateMap<IEnumerable<Customer>, CustomersGetResponseDto>()
                .ForMember(dest => dest.Customers, opts => opts.MapFrom(src => src)); 
        }
    }
}
