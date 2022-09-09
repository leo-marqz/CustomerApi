using CustomerApi.Dtos;
using CustomerApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CustomerApi.Controllers
{

    [ApiController] //define que todo el controlador sera parte de una api
    //[Authorize] no permite hacer uso de este controlador si no esta autorizado
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private readonly CustomerDatabaseContext _customerDatabaseContext;

        public CustomerController(CustomerDatabaseContext customerDatabaseContext)
        {
            this._customerDatabaseContext = customerDatabaseContext;
        }
    


            [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerDto))]
        public async Task<IActionResult> GetCustomers()
        {
            List<CustomerDto> customers = new List<CustomerDto>();
            var customer = new CustomerDto();
            customer.Id = 1001;
            customer.FirstName = "Leo";
            customer.LastName = "Marqz";
            customer.Email = "leomarqz200@gmail.com";
            customer.Phone = "74811897";
            customer.Address = "caserio llano alegre";
            customers.Add(customer);
            return new OkObjectResult(customers);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCustomer(long id)
        {
            CustomerEntity result = await _customerDatabaseContext.Get(id);
            return new OkObjectResult(result.ToDto());

        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<IActionResult> DeleteCustomer(long id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CustomerDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateCustomer(CreateCustomerDto customer)
        {
            CustomerEntity result = await this._customerDatabaseContext.Add(customer);

            return new CreatedResult($"https://localhost:7291/api/customer/{result.Id}", null);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerDto))]
        public async Task<IActionResult> UpdateCustomer(CustomerDto customer)
        {
            throw new NotImplementedException();
        }
    }
}
