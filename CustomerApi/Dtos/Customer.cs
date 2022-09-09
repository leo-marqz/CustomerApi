using CustomerApi.Repositories;
using System.ComponentModel.DataAnnotations;

namespace CustomerApi.Dtos
{
    public class CustomerDto
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public static implicit operator CustomerDto(CustomerEntity v)
        {
            throw new NotImplementedException();
        }
    }
}
