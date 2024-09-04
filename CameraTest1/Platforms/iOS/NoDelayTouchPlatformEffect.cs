
using Microsoft.Maui.Controls.Platform;
using UIKit;

namespace  CameraTest1.IOS;

public class NoDelayTouchPlatformEffect : Microsoft.Maui.Controls.Platform.PlatformEffect
{

    protected override void OnAttached()
    {
       
        var scrollView = FindScrollView(Control);
        if (scrollView != null)
        {
            scrollView.DelaysContentTouches = false;
            System.Diagnostics.Debug.WriteLine("ScrollView found and DelaysContentTouches set to false.");
        }
        else
        {
            System.Diagnostics.Debug.WriteLine("ScrollView not found.");
        }
        
        
    }

    protected override void OnDetached()
    {
        
    }
    
    private UIScrollView FindScrollView(UIView view)
    {
        if (view is UIScrollView scrollView)
            return scrollView;

        foreach (var subview in view.Subviews)
        {
            var result = FindScrollView(subview);
            if (result != null)
                return result;
        }

        return null;
    }
}