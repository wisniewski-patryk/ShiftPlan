using Microsoft.EntityFrameworkCore;
using ShiftPlan.Api.Context;
using ShiftPlan.UsersIdentity.Context;
using Xunit.Abstractions;
using Xunit.Sdk;

[assembly: Xunit.TestFramework("ShiftPlan.Api.Tests.Setup.TestRunStart", "ShiftPlan.Api.Tests")]
namespace ShiftPlan.Api.Tests.Setup;

public class TestRunStart : XunitTestFramework
{
	public TestRunStart(IMessageSink messageSink) : base(messageSink)
	{
		var shiftPlanOptions = new DbContextOptionsBuilder<ShiftPlanContext>()
			.UseInMemoryDatabase("TestDatabase");
		var shiftPlanContext = new ShiftPlanContext(shiftPlanOptions.Options);
		shiftPlanContext.Database.Migrate();

		var identityOptions = new DbContextOptionsBuilder<IdentityUserContext>()
			.UseInMemoryDatabase("TestDatabase");
		var identityContext = new IdentityUserContext(identityOptions.Options);
		identityContext.Database.Migrate();
	}
}
