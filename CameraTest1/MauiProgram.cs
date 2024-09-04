using CameraTest1.Effect;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using DevExpress.Maui;
using DevExpress.Maui.CollectionView;
using SkiaSharp.Views.Maui.Controls.Hosting;
#if IOS
using  CameraTest1.IOS;
#elif ANDROID
using  CameraTest1.Android;
#else
using CameraTest1.PlatformEffects;
#endif



namespace CameraTest1;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkitCamera()
			.UseMauiCommunityToolkit()
			.UseSkiaSharp()
			.UseDevExpress()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddTransient<PhotoView>();
#if DEBUG
		builder.Logging.AddDebug();
#endif
		
		builder.ConfigureEffects(effects =>
		{
			effects.Add<NoDelayTouchEffect, NoDelayTouchPlatformEffect>();
		});
		
		#if IOS
		builder.ConfigureMauiHandlers(handlers =>
		{
			handlers.AddHandler<DXCollectionView, CustomDXCollectionViewHandler>();
		});
		#endif

		return builder.Build();
	}
}
