using CustomerApi.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CustomerApi.Repositories
{
    public class CustomerDatabaseContext : DbContext
    {

        public CustomerDatabaseContext(
            DbContextOptions<CustomerDatabaseContext> options
        ): base(options)
        {
           
        }

        public DbSet<CustomerEntity> Customer { get; set; }

        //public async Task<CustomerEntity> GetAll()
        //{
        //    return await Customer.
        //}

        public async Task<CustomerEntity> Get(long id)
        {
            return await Customer.FirstAsync(x => x.Id == id);
        }

        public async Task<CustomerEntity> Add(CreateCustomerDto customer)
        {
            CustomerEntity entity = new CustomerEntity()
            {
                Id = null,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                Phone = customer.Phone,
                Address = customer.Address
            };
            EntityEntry<CustomerEntity> response = await Customer.AddAsync(entity);
            await SaveChangesAsync();
            return await Get(response.Entity.Id ?? throw new Exception("No se ha podido guardar!!") );
        }

        public async Task<bool> Delete(long id)
        {
            CustomerEntity entity = await Get(id);
            Customer.Remove(entity);
            SaveChanges();
            return true;
        }
    }   

    public class CustomerEntity
    {
        public long? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public CustomerDto ToDto()
        {
            return new CustomerDto()
            {
                Id = Id ?? throw new Exception("El Id no puede ser null"),
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Phone = Phone,
                Address = Address
            };
        }
    }
}
