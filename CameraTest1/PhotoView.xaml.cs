using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CameraTest1.Models;
using CameraTest1.Services;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Core.Primitives;
using CommunityToolkit.Maui.Views;

namespace CameraTest1;

public partial class PhotoView : ContentPage
{

    private ICameraProvider _cameraProvider;
    private CameraViewModel _viewModel;
    public PhotoView(  )
    {
        InitializeComponent();
        _cameraProvider = ServiceHelpers.GetService<ICameraProvider>();
    }

    protected  override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        
        await _cameraProvider.RefreshAvailableCameras(CancellationToken.None);
        
        _viewModel = new CameraViewModel();
        var initialCamera =  _cameraProvider.AvailableCameras.FirstOrDefault(c => c.Position == CameraPosition.Front);

        _viewModel.SelectedCameraInfo = initialCamera;
        CameraView.SelectedCamera = initialCamera;
        CameraView.ImageCaptureResolution = new Size(1600, 1280);

    }

    protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
    {
        base.OnNavigatedFrom(args);
        CameraView.MediaCaptured -= CameraView_OnMediaCaptured;
        CameraView.Handler?.DisconnectHandler();
    }

    private async  void Button_OnClicked(object? sender, EventArgs e)
    {
        
        await  CameraView.CaptureImage(CancellationToken.None);
    }

    private async void CameraView_OnMediaCaptured(object? sender, MediaCapturedEventArgs e)
    {
        if (Dispatcher.IsDispatchRequired)
        {
            Dispatcher.Dispatch( async () => await Navigation.PushAsync(new Tagging()
            {
                BindingContext = new TaggingViewModel()
                {
                    CurrentImage = ImageSource.FromStream(() => e.Media)
                }
            })); 
        }
        

        //Image.Source = ImageSource.FromStream(() => e.Media);
    }

    private void CameraSwitch_OnClicked(object? sender, EventArgs e)
    {
        var newCamera = _cameraProvider.AvailableCameras.FirstOrDefault(x => x.Position != _viewModel.SelectedCameraInfo.Position) ?? _viewModel.SelectedCameraInfo;
        CameraView.SelectedCamera = newCamera;

        _viewModel.SelectedCameraInfo = newCamera;
    }

    private void Flash_OnClicked(object? sender, EventArgs e)
    {
        if (_viewModel.SelectedCameraInfo.IsFlashSupported)
        {
            CameraView.CameraFlashMode = CameraView.CameraFlashMode == CameraFlashMode.Off ? CameraFlashMode.On : CameraFlashMode.Off;
        }
        
    }

    private void ZoomIn_OnClicked(object? sender, EventArgs e)
    {
        CameraView.ZoomFactor += 0.1f;
    }
    
    private void ZoomOut_OnClicked(object? sender, EventArgs e)
    {
        CameraView.ZoomFactor -= 0.1f;
    }
}