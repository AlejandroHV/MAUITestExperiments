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
            OnInteracting?.Invoke(this, EventArgs.Empty);
        }
        else if (e.ActionType == SKTouchAction.Moved && e.InContact)
        {
            _path.LineTo(e.Location);
            
            InvalidateSurface();
            
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

    public string GetImage()
    {
        try
        {
            var sigWidth = 500;
            var sigHeight = 500;
            var imageInfo = new SKImageInfo(sigWidth, sigHeight);
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
            using var data = image.Encode(SKEncodedImageFormat.Png, 250);


            var filePath = Path.Combine(FileSystem.AppDataDirectory, "signature.png");
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            using (var stream = File.OpenWrite(filePath))
            {
                data.SaveTo(stream);
            }

            return filePath;
/*
            var imageStream = data.AsStream();
            imageStream.Position = 0; // Ensure the stream is at the beginning
            if (imageStream != null && imageStream.Length > 0)
            {
                return ImageSource.FromStream(() => imageStream);
            }
*/
            throw new InvalidOperationException("The image stream is empty or null.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}