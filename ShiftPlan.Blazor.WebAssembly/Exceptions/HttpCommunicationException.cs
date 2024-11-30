using System.Net;

namespace ShiftPlan.Blazor.WebAssembly.Exceptions;

public class HttpCommunicationException : Exception
{
	public HttpStatusCode ErrorCode { get; set; }

	public HttpCommunicationException() { }

	public HttpCommunicationException(string message) : base(message) { }

	public HttpCommunicationException(string message, HttpStatusCode errorCode) : base(message) { }

	public HttpCommunicationException(string message, Exception innerException) : base(message, innerException) { }
}
