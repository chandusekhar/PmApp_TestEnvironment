using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessLayer.Entities
{
    [Table("Users")]
    public class Users
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FriendlyName { get; set; }
        public string PrimaryEmail { get; set; }
        public string Telephone { get; set; }
        public string Role { get; set; }
        public string PasswordEnc { get; set; }
        public string UpdateTime { get; set; }
        public long Id { get; set; }
    }
}