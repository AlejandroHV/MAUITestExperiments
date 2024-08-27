using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CameraTest1.Models;
using CommunityToolkit.Maui.Core;

namespace CameraTest1;

public partial class SignatureControl : ContentView
{
    private Items _viewModel;
    
    public bool IsDrawing
    {
        get => (bool)GetValue(IsDrawingProperty);
        set => SetValue(IsDrawingProperty, value);
    }
    
    public static readonly BindableProperty IsDrawingProperty = BindableProperty.Create(
        nameof(IsDrawing),
        typeof(bool), typeof(SignatureControl), false);

    
    public SignatureControl()
    {
        InitializeComponent();
    }

    protected override void OnBindingContextChanged()
    {
        if (BindingContext is not Items viewModel)
        {
            return;
        }

        _viewModel = viewModel;
        
        base.OnBindingContextChanged();
    }

    private void Reset_OnClicked(object? sender, EventArgs e)
    {
        IsDrawing = false;
        // signaturePad.Clear();
    }
    
    private void SaveImage_OnClicked(object? sender, EventArgs e)
    {
        try
        {
            IsDrawing = true;
            // var signatureImage = signaturePad.GetImage();
            //Image.Source = signatureImage;
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            throw;
        }
       
    }

    private void SignaturePad_OnUnfocused(object? sender, FocusEventArgs e)
    {
        
    }

    private void SignaturePad_OnOnInteracting(object? sender, EventArgs e)
    {
        IsDrawing = false;
    }

    private void SignaturePad_OnOnReleasing(object? sender, EventArgs e)
    {
        IsDrawing = true;
    }
}