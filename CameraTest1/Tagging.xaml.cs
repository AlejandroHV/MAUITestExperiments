using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraTest1;

public partial class Tagging : ContentPage
{
    public Tagging()
    {
        InitializeComponent();
    }

    private async void Button_OnClicked(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new PhotoView());
    }
}