using SkiaSharp;
using SkiaSharp.Views.Maui.Controls;
using Microsoft.Maui.Controls;
using SkiaSharp.Views.Maui;
using System;

namespace CameraTest1.CustomControl;


public class SignaturePad : SKCanvasView
{
    private SKPath _path = new SKPath();
    public event EventHandler OnInteracting;
    public event EventHandler OnReleasing;

    public SignaturePad()
    {
        EnableTouchEvents = true;
        Touch += OnTouch;

    }
    
    private void OnTouch(object? sender, SKTouchEventArgs e)
    {
        if (e.ActionType == SKTouchAction.Pressed)
        {
            _path.MoveTo(e.Location);
            
        }
        else if (e.ActionType == SKTouchAction.Moved && e.InContact)
        {
            _path.LineTo(e.Location);
            InvalidateSurface();
            OnInteracting?.Invoke(this, EventArgs.Empty);
        }
        else if (e.ActionType == SKTouchAction.Released)
        {
            _path.LineTo(e.Location);
            InvalidateSurface();
            OnReleasing.Invoke(this, EventArgs.Empty);
        }

        e.Handled = true;
        
    }

    protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
    {
        base.OnPaintSurface(e);

        var canvas = e.Surface.Canvas;
        canvas.Clear(SKColors.White);

        var paint = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            StrokeWidth = 4,
            Color = SKColors.Black,
            StrokeCap = SKStrokeCap.Round,
            IsAntialias = true
        };

        canvas.DrawPath(_path, paint);
    }

    public void Clear()
    {
        _path.Reset();
        InvalidateSurface();
    }

    public ImageSource GetImage()
    {
        try
        {
            var imageInfo = new SKImageInfo((int)Width, (int)Height);
            using var surface = SKSurface.Create(imageInfo);
            var canvas = surface.Canvas;

            canvas.Clear(SKColors.White);

            var paint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                StrokeWidth = 4,
                Color = SKColors.Black,
                StrokeCap = SKStrokeCap.Round,
                IsAntialias = true
            };

            canvas.DrawPath(_path, paint);

            using var image = surface.Snapshot();
            using var data = image.Encode(SKEncodedImageFormat.Png, 100);
           
            
            var filePath = Path.Combine("/Users/alejandroherreno/CameraTest1/Resources/Images/", "filename.png");
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            using (var stream = File.OpenWrite(filePath))
            {
                
                data.SaveTo(stream);
            }
            
            return  ImageSource.FromStream(() => new MemoryStream(data.ToArray()));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }
}