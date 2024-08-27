using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using DevExpress.Maui;
using DevExpress.Maui.CollectionView;
using SkiaSharp.Views.Maui.Controls.Hosting;
#if IOS
using CameraTest1.IOS;
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
		
		#if IOS
		builder.ConfigureMauiHandlers(handlers =>
		{
			handlers.AddHandler<DXCollectionView, CustomDXCollectionViewHandler>();
		});
		#endif

		return builder.Build();
	}
}
