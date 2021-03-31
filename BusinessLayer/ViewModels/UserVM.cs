using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ViewModels
{
  public class UserVM
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
        public bool IsJazz { get; set; }
        public string Path { get; set; }
       public List<string> Permissions { get; set; }
    }

    
}
