using System.ComponentModel.DataAnnotations;

public class Role
    {
        [Key]public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
    