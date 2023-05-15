using System.Net;
using API.Interfaces;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodingExercise.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v1/customers")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly ILogger<CustomerController> _logger;
        public CustomerController(ICustomerService customerService, ILogger<CustomerController> logger) 
        {
            _customerService = customerService;
            _logger = logger;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            _logger.LogInformation($"Started removing customer with id {id}");
            try
            {
                await _customerService.DeleteCustomerAsync(id);
            }
            catch (DbUpdateException)
            {
                _logger.LogInformation($"Could not remove customer with id {id}");
                return NotFound();
            }
            _logger.LogInformation($"Removed succesfully customer with id {id}");
            return Ok();
        }

        [HttpGet]
        [ProducesResponseType(typeof(CustomersGetResponseDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCustomers() 
        {
            _logger.LogInformation("Started getting all customers");
            var customers = await _customerService.GetCustomerListAsync();
            _logger.LogInformation("Retrieved all customers");
            return Ok(customers);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CustomerCreateResponseDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddCustomer([FromBody] CustomerCreateRequestDto customerCreateRequest)
        {
            _logger.LogInformation("Started adding customer");
            CustomerCreateResponseDto customerCreateResponse;
            try
            {
                customerCreateResponse = await _customerService.AddCustomerAsync(customerCreateRequest);
            }
            catch (DbUpdateException)
            {
                _logger.LogInformation($"Could not add customer: {customerCreateRequest?.Customer?.Firstname} {customerCreateRequest?.Customer?.Surname}");
                return BadRequest();
            }
            _logger.LogInformation($"Added customer with id {customerCreateResponse?.Customer?.Id}");
            return Ok(customerCreateResponse);
        }
    }
}
