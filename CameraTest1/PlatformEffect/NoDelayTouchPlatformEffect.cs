using Microsoft.Maui.Controls.Platform;

namespace CameraTest1.PlatformEffect;

#if WINDOWS
internal class NoDelayTouchPlatformEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
        }

        protected override void OnDetached()
        {
        }
    }
#else
internal class NoDelayTouchPlatformEffect : Microsoft.Maui.Controls.Platform.PlatformEffect
{
    protected override void OnAttached()
    {
    }

    protected override void OnDetached()
    {
    }
}
#endif