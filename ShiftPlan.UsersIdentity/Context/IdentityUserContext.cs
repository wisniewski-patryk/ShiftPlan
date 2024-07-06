using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ShiftPlan.UsersIdentity;

public class IdentityUserContext(DbContextOptions<IdentityUserContext> options) : IdentityDbContext<IdentityUser>(options)
{
}
