using System.ComponentModel.DataAnnotations;

namespace CustomerApi.Dtos
{
    public class CreateCustomerDto
    {
        [Required(ErrorMessage = "El Nombre es obligatorio")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "El Apellido es obligatorio")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "El Email es obligatorio")]
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
