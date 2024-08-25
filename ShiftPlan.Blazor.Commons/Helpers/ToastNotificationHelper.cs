using BlazorBootstrap;

namespace ShiftPlan.Blazor.Commons.Helpers;

public static class ToastNotificationHelper
{
	public static ToastMessage ErrorToastMessage(string title, string message)
	{
		return new()
		{
			HelpText = DateTime.Now.ToString("D HH:mm:ss"),
			Title = title,
			AutoHide = true,
			Message = message,
			Type = ToastType.Danger,
		};
	}

	public static ToastMessage SuccessToastMessage(string title, string message)
	{
		return new()
		{
			HelpText = DateTime.Now.ToString("dddd HH:mm:ss"),
			Title = title,
			AutoHide = true,
			Message = message,
			Type = ToastType.Success,
		};
	}
}
