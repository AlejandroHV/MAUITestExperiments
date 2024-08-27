using CameraTest1.CustomControl;
using DevExpress.iOS.ListView;
using DevExpress.Maui.CollectionView;
using DevExpress.Maui.CollectionView.Internal;
using Microsoft.Maui.Controls.Compatibility.Platform.iOS;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls.Platform;
using UIKit;

namespace CameraTest1.IOS;

public class CustomDXCollectionViewHandler : DXCollectionViewHandler
{
    protected override void ConnectHandler(DXListView platformView)
    {
        base.ConnectHandler(platformView);

        var scrollView = FindUIScrollView(platformView);

        if (scrollView != null)
        {
            UpdateScrollEnabled(((ToggleScrollCollectionView)VirtualView)?.IsScrollEnabled ?? true);
        }
    }

    public void UpdateScrollEnabled(bool isEnabled)
    {
        var scrollView = FindUIScrollView(PlatformView);
        if (scrollView != null)
        {
            scrollView.ScrollEnabled = isEnabled;
        }
    }

    private UIScrollView FindUIScrollView(UIView view)
    {
        if (view is UIScrollView scrollView)
            return scrollView;

        foreach (var subview in view.Subviews)
        {
            var result = FindUIScrollView(subview);
            if (result != null)
                return result;
        }

        return null;
    }
}