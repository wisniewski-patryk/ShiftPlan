using Microsoft.EntityFrameworkCore;
using ShiftPlan.Api.Context;
using ShiftPlan.UsersIdentity.Context;
using Xunit.Abstractions;
using Xunit.Sdk;

[assembly: Xunit.TestFramework("ShiftPlan.Api.Tests.Setup.TestRunStart", "ShiftPlan.Api.Tests")]
namespace ShiftPlan.Api.Tests.Setup;

public class TestRuneStart : XunitTestFramework
{
	public TestRuneStart(IMessageSink messageSink) : base(messageSink)
	{
		var shiftPlanOptions = new DbContextOptionsBuilder<ShiftPlanContext>()
			.UseNpgsql("Host=db;Port=54321;Database=ShiftPlan;Username=postgres;Password=dev_password");
		var shiftPlanContext = new ShiftPlanContext(shiftPlanOptions.Options);
		shiftPlanContext.Database.Migrate();

		var identityOptions = new DbContextOptionsBuilder<IdentityUserContext>()
			.UseNpgsql("Host=db;Port=54321;Database=Identity;Username=postgres;Password=dev_password");
		var identityContext = new IdentityUserContext(identityOptions.Options);
		identityContext.Database.Migrate();
	}
}
