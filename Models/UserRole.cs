using System.ComponentModel.DataAnnotations;

public class UserRole
    {
        [Key]public int UserId { get; set; }
        public int RoleId  { get; set; }
    }
    