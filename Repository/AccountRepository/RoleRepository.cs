using ManhaleAspNetCore.Models;
using ManhaleAspNetCore.ModelView.Account;

namespace ManhaleAspNetCore.Repository.AccountRepository
{
    public class RoleRepository:Repository<CustomerIdentityRole>,IRoleRepository
    {
        private readonly ManahelContext _context;
        public RoleRepository(ManahelContext context):base(context)
        {
            _context = context;
        }
    }
}
