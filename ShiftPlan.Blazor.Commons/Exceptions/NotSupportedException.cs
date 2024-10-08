namespace ShiftPlan.Blazor.Commons.Exceptions;

public class NotSupportedException : Exception
{
	public NotSupportedException(string message) : base(message) { }

	public NotSupportedException(string message, Exception innerException) : base(message, innerException) { }
}
