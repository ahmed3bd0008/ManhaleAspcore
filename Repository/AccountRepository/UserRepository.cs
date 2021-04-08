using ManhaleAspNetCore.Models;
using ManhaleAspNetCore.ModelView.Account;
namespace ManhaleAspNetCore.Repository.AccountRepository
{
    public class UserRepository:Repository<CustomIdentityUser>,IUserRepository
    {
        private readonly ManahelContext _context;
        public UserRepository(ManahelContext context):base(context)
        {
            _context = context;
        }
    }
}
