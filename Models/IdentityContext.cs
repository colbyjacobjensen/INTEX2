using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace INTEX2.Models
{
	public class IdentityContext : IdentityDbContext<IdentityUser>
	{
		public IdentityContext(DbContextOptions<IdentityContext> options)
			: base(options) { }
	}
}