using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreTodo.Data;
using AspNetCoreTodo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreTodo.Services
{
    public class ManageUsersServices : IManageUsersServices
    {
        public async Task<bool> DelUserAsync(Guid Id, UserManager<ApplicationUser> user)
        {
             if( (
                 await user.Users.Where(x => x.Id.Equals(Id)).SingleOrDefaultAsync()
                 )!=null)
                 {
                     Console.WriteLine("entrei no if");
                     return true;
                 }

            return true;
        
        }
    }
}