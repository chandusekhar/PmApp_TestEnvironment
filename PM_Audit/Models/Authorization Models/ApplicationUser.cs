using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PM_Audit.Models.Authorization_Models
{
    public class ApplicationUser : IUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FriendlyName { get; set; }
        public string PrimaryEmail { get; set; }
        public string Telephone { get; set; }
        public string Role { get; set; }
        public string PasswordEnc { get; set; }
        public string UpdateTime { get; set; }
        public string Id { get; set; }
       public List<string> Permissions { get; set; }
        public bool IsJazz { get; set; }

        public ApplicationUser() { }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUserManager manager)
        {
            try
            {
                var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

                userIdentity.AddClaim(new Claim(CustomClaims.Sid, this.Id.ToString()));
                userIdentity.AddClaim(new Claim(CustomClaims.Name, this.FriendlyName));
                userIdentity.AddClaim(new Claim(CustomClaims.FriendlyName, this.FriendlyName));
                userIdentity.AddClaim(new Claim(CustomClaims.UserName, this.UserName));
                userIdentity.AddClaim(new Claim(CustomClaims.Email, this.PrimaryEmail));
                userIdentity.AddClaim(new Claim(CustomClaims.IsJazz, this.IsJazz.ToString()));
                userIdentity.AddClaim(new Claim(CustomClaims.Email, this.PrimaryEmail));
                userIdentity.AddClaim(new Claim(CustomClaims.Permissions, string.Join(",", this.Permissions)));
                return userIdentity;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

      
    }
}