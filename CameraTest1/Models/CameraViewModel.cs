using System.Collections.Generic;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CameraTest1.Models;

public partial class CameraViewModel: ObservableObject
{
    [ObservableProperty]
    private CameraInfo? _selectedCameraInfo;

    [ObservableProperty]
    private IReadOnlyList<CameraInfo> _availableCameras;
    
    

}