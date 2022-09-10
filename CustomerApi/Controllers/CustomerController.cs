using CustomerApi.Dtos;
using CustomerApi.Repositories;
using CustomerApi.UseCases;
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
        private readonly IUpdateCustomerUseCase _updateCustomerUseCase;

        public CustomerController(
            CustomerDatabaseContext customerDatabaseContext,
            IUpdateCustomerUseCase updateCustomerUseCase
            )
        {
            this._customerDatabaseContext = customerDatabaseContext;
            this._updateCustomerUseCase = updateCustomerUseCase;
        }
    


            [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CustomerDto>))]
        public async Task<IActionResult> GetCustomers()
        {
            var result = _customerDatabaseContext
                .Customer.Select(customer => customer.ToDto())
                .ToList();

            return new OkObjectResult(result);
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
            var result = await _customerDatabaseContext.Delete(id);
            return new OkObjectResult(result);
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
            var result = await this._updateCustomerUseCase.Execute(customer);
            if (result == null) return new NotFoundResult();
            return new OkObjectResult(result);
        }
    }
}
