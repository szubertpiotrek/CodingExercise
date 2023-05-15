using API.Services.Customers;
using AutoMapper;
using Domain.DTOs;
using Domain.Entities;
using Domain.Interfces;
using Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTests
{
    [TestClass]
    public class CustomerServiceTests
    {
        private Mock<IGenericRepository<Customer>> _customerRepository;
        private Mock<IUnitOfWork> _unitOfWork;
        private Mapper _mapper;

        private CustomerService _sut;

        [TestInitialize]
        public void Init()
        {
            _customerRepository = new Mock<IGenericRepository<Customer>>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _mapper = new Mapper(new MapperConfiguration(mc =>
            {
                mc.AddProfile(new CustomerMappingProfile());
            }));

            _sut = new CustomerService(_unitOfWork.Object, _mapper);
        }

        [TestMethod]
        public async Task GetCustomerListAsync_HasCustomers_ShouldReturnCustomerList()
        {
            // Arrange
            var customers = new List<Customer>()
            {
                new Customer() { Id = 1, Firstname = "Test Firstname 1", Surname = "Test Surname 1" },
                new Customer() { Id = 2, Firstname = "Test Firstname 2", Surname = "Test Surname 2" }
            };

            _customerRepository
                .Setup(x => x.ListAsync())
                .ReturnsAsync(customers);
            _unitOfWork
                .Setup(x => x.Repository<Customer>())
                .Returns(_customerRepository.Object);

            // Act
            var result = await _sut.GetCustomerListAsync();

            // Assert
            Assert.AreEqual(customers.Count(), result.Customers?.Count());
            Assert.AreEqual(customers[0].Id, result.Customers?.First().Id);
            Assert.AreEqual(customers[0].Firstname, result.Customers?.First().Firstname);
            Assert.AreEqual(customers[0].Surname, result.Customers?.First().Surname);
        }

        [TestMethod]
        public async Task GetCustomerListAsync_LackOfCustomers_ShouldReturnEmptyEnumerable()
        {
            // Arrange
            var customers = Enumerable.Empty<Customer>();

            _customerRepository
                .Setup(x => x.ListAsync())
                .ReturnsAsync(customers);
            _unitOfWork
                .Setup(x => x.Repository<Customer>())
                .Returns(_customerRepository.Object);

            // Act
            var result = await _sut.GetCustomerListAsync();

            // Assert
            Assert.AreEqual(customers.Count(), result.Customers?.Count());
            Assert.AreEqual(0, result.Customers?.Count());
        }

        [TestMethod]
        public async Task DeleteCustomerAsync_DeleteCustomer_ShouldCallOnce()
        {
            // Arrange
            var customer = new Customer() { Id = 1, Firstname = "Test Firstname 1", Surname = "Test Surname 1" };
            _customerRepository
                .Setup(x => x.Delete(It.IsAny<Customer>()));
            _unitOfWork
                .Setup(x => x.Repository<Customer>())
                .Returns(_customerRepository.Object);

            // Act
            await _sut.DeleteCustomerAsync(customer.Id);

            // Assert
            _customerRepository.Verify(x => x.Delete(It.IsAny<Customer>()), Times.Once());
        }

        [TestMethod]
        public async Task DeleteCustomerAsync_NotExistingCustomer_ShouldThrowException()
        {
            // Arrange
            var customer = new Customer() { Id = 1, Firstname = "Test Firstname 1", Surname = "Test Surname 1" };
            _customerRepository
                .Setup(x => x.Delete(It.IsAny<Customer>()))
                .Throws(new DbUpdateException());
            _unitOfWork
                .Setup(x => x.Repository<Customer>())
                .Returns(_customerRepository.Object);

            // Act
            var deleteCustomer = async () => await _sut.DeleteCustomerAsync(customer.Id);

            // Assert
            await Assert.ThrowsExceptionAsync<DbUpdateException>(deleteCustomer);
        }

        [TestMethod]
        public async Task AddCustomerAsync_CreateCustomer_ShouldReturnCreatedCustomer()
        {
            // Arrange
            var customer = new CustomerCreateRequestDto()
            {
                Customer = new CustomerDto
                {
                    Id = 1,
                    Firstname = "Test Firstname 1",
                    Surname = "Test Surname 1"
                }
            };

            var createdCustomer = new Customer
            {
                Id = 1,
                Firstname = "Test Firstname 1",
                Surname = "Test Surname 1"
            };

            _customerRepository
                .Setup(x => x.AddAsync(It.IsAny<Customer>()))
                .ReturnsAsync(createdCustomer);
            _unitOfWork
                .Setup(x => x.Repository<Customer>())
                .Returns(_customerRepository.Object);

            // Act
            var result = await _sut.AddCustomerAsync(customer);

            // Assert
            Assert.AreEqual(customer.Customer.Id, result?.Customer?.Id);
            Assert.AreEqual(customer.Customer.Firstname, result?.Customer?.Firstname);
            Assert.AreEqual(customer.Customer.Surname, result?.Customer?.Surname);
        }

        [TestMethod]
        public async Task AddCustomerAsync_ExistingCustomer_ShouldThrowException()
        {
            // Arrange
            var customer = new CustomerCreateRequestDto()
            {
                Customer = new CustomerDto
                {
                    Id = 1,
                    Firstname = "Test Firstname 1",
                    Surname = "Test Surname 1"
                }
            };
            _customerRepository
                .Setup(x => x.AddAsync(It.IsAny<Customer>()))
                .Throws(new DbUpdateException());
            _unitOfWork
                .Setup(x => x.Repository<Customer>())
                .Returns(_customerRepository.Object);

            // Act
            var deleteCustomer = async () => await _sut.AddCustomerAsync(customer);

            // Assert
            await Assert.ThrowsExceptionAsync<DbUpdateException>(deleteCustomer);
        }
    }
}