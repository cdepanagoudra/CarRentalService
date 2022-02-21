using System.ComponentModel.DataAnnotations;
namespace CAR_RENTAL_SERVICE.Models
{
    public class User
    {
        public int Id{get; set;}
        
        [Required]
        public string UserName {get; set;}
        [Required]
        public string Password {get; set;}
        public void Deconstruct(out string userName, out string password)
        {
            userName = this.UserName;
            password = this.Password;
        }
    }
}