using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CameraTest1.Models;

public partial class Items : ObservableObject
{

    [ObservableProperty] private int _id;

    [ObservableProperty] private string _name;

    [ObservableProperty] private ImageSource _signatureImage;

    public event PropertyChangedEventHandler? PropertyChanged;

    [RelayCommand]
    private void IncreaseCount()
    {
		
    }
}