using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCoreTodo.Models;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreTodo.Services
{
    public interface IManageUsersServices
    {
        Task<bool> DelUserAsync(Guid Id,UserManager<ApplicationUser> user);
    }
}