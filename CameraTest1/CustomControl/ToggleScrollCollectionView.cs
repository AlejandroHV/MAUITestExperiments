#if IOS
using CameraTest1.IOS;
#else 
using CameraTest1.Handler;
#endif
using DevExpress.Maui.CollectionView;

namespace CameraTest1.CustomControl;

public class ToggleScrollCollectionView: DXCollectionView
{
    public static readonly BindableProperty IsScrollEnabledProperty =
        BindableProperty.Create(nameof(IsScrollEnabled), typeof(bool), typeof(ToggleScrollCollectionView), true, propertyChanged: OnIsScrollEnabledChanged);

    public bool IsScrollEnabled
    {
        get => (bool)GetValue(IsScrollEnabledProperty);
        set => SetValue(IsScrollEnabledProperty, value);
    }

    private static void OnIsScrollEnabledChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is ToggleScrollCollectionView collectionView && collectionView.Handler is CustomDXCollectionViewHandler handler)
        {
            handler.UpdateScrollEnabled((bool)newValue);
        }
    }
}