using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PM_Audit.Models.Authorization_Models
{
    public class ApplicationUserStore<T> : IUserStore<T> where T : ApplicationUser
    {

        System.Threading.Tasks.Task IUserStore<T, string>.CreateAsync(T user)
        {
            //Create /Register New User 
            throw new NotImplementedException();
        }

        System.Threading.Tasks.Task IUserStore<T, string>.DeleteAsync(T user)
        {
            //Delete User 
            throw new NotImplementedException();
        }

        System.Threading.Tasks.Task<T> IUserStore<T, string>.FindByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        System.Threading.Tasks.Task<T> IUserStore<T, string>.FindByNameAsync(string userName)
        {
            throw new NotImplementedException();
        }

        System.Threading.Tasks.Task IUserStore<T, string>.UpdateAsync(T user)
        {
            //Update User Profile 
            throw new NotImplementedException();
        }

        void IDisposable.Dispose()
        {
            // throw new NotImplementedException(); 

        }
    }
}