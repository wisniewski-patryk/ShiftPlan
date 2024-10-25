using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ShiftPlan.UsersIdentity.Context;

public class IdentityUserContext(DbContextOptions<IdentityUserContext> options) : IdentityDbContext<Models.User>(options)
{
}
