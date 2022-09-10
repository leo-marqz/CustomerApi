using CustomerApi.Dtos;
using CustomerApi.Repositories;

namespace CustomerApi.UseCases
{

    public interface IUpdateCustomerUseCase
    {
        Task<CustomerDto?> Execute(CustomerDto customer);
    }

    public class UpdateCustomerUseCase : IUpdateCustomerUseCase
    {
        private readonly CustomerDatabaseContext _customerDatabaseContext;

        public UpdateCustomerUseCase(CustomerDatabaseContext customerDatabaseContext)
        {
            this._customerDatabaseContext = customerDatabaseContext;
        }

        public async Task<CustomerDto?> Execute(CustomerDto customer)
        {
            CustomerEntity entity = await _customerDatabaseContext.Get(customer.Id);

            if (entity == null) return null;

            entity.Id = customer.Id;
            entity.FirstName = customer.FirstName;
            entity.LastName = customer.LastName;
            entity.Email = customer.Email;
            entity.Phone = customer.Phone;
            entity.Address = customer.Address;

            await _customerDatabaseContext.Update(entity);
            return entity.ToDto();
        }
    }
}
