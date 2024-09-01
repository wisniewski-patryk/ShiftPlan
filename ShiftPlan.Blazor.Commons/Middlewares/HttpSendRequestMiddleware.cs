using BlazorBootstrap;
using Blazored.SessionStorage;
using ShiftPlan.Blazor.Commons.Helpers;
using System.Net.Http.Headers;

namespace ShiftPlan.Blazor.Commons.Middlewares;

public class HttpSendRequestMiddleware(ToastService toastService, ISessionStorageService sessionStorage) : DelegatingHandler
{
	protected override HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken)
	{
		return base.Send(request, cancellationToken);
	}

	protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
	{
		var token = await sessionStorage.GetItemAsStringAsync("accessToken", cancellationToken);
		request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
		var response = await base.SendAsync(request, cancellationToken);
		if (!response.IsSuccessStatusCode)
			toastService.Notify(ToastNotificationHelper.ErrorToastMessage("Error", response.ReasonPhrase ?? "Unknow error"));

		return response;
	}
}
