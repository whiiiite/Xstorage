using Microsoft.EntityFrameworkCore;
using System.Security.Principal;
using Xstorage.Data;
using Xstorage.Entities.Models;

namespace Xstorage.Extentions
{
    public static class XstorageDbContextExtentions
    {

        public static async Task<User?> GetUserAsync(this XstorageDbContext context, IIdentity? currentIdentity)
        {
            if(currentIdentity == null)
            {
                return null;
            }

            User? user = await context.Users
                        .Where(x => x.Email == currentIdentity.Name)
                        .FirstOrDefaultAsync();

            return user;
        }
    }
}
