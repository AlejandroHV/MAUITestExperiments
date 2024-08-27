using CommunityToolkit.Mvvm.ComponentModel;

namespace CameraTest1.Models;


public partial class TaggingViewModel :ObservableObject
{
    [ObservableProperty]
    private ImageSource _currentImage;
    
    [ObservableProperty]
    private List<ImageSource> _images;
}