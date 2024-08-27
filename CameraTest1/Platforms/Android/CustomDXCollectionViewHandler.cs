

using Android.Content;
using AndroidX.RecyclerView.Widget;
using CameraTest1.CustomControl;
using DevExpress.Android.CollectionView;
using DevExpress.Maui.CollectionView.Internal;

namespace CameraTest1.Android;

public class CustomDXCollectionViewHandler : DXCollectionViewHandler
    {
        protected override void ConnectHandler(DXListViewNative platformView)
        {
            base.ConnectHandler(platformView);

            // Update scroll enabled state after handler is connected
            UpdateScrollEnabled(((ToggleScrollCollectionView)VirtualView)?.IsScrollEnabled ?? true);
        }

        public void UpdateScrollEnabled(bool isEnabled)
        {
            // Get the RecyclerView from the DXListViewNative
            var recyclerView = GetRecyclerView(PlatformView);
            if (recyclerView != null && recyclerView.GetLayoutManager() is CustomLinearLayoutManager layoutManager)
            {
                layoutManager.ScrollEnabled = isEnabled;
            }
        }

        protected override DXListViewNative CreatePlatformView()
        {
            var dxListViewNative = base.CreatePlatformView();

            // Get the RecyclerView and set a custom LayoutManager
            var recyclerView = GetRecyclerView(dxListViewNative);
            if (recyclerView != null)
            {
                recyclerView.SetLayoutManager(new CustomLinearLayoutManager(recyclerView.Context, LinearLayoutManager.Vertical, false));
            }

            return dxListViewNative;
        }

        private RecyclerView GetRecyclerView(DXListViewNative dxListViewNative)
        {
            // Assuming DXListViewNative has a method or property to get the internal RecyclerView
            return dxListViewNative?.GetChildAt(0) as RecyclerView;
        }
    }

    public class CustomLinearLayoutManager : LinearLayoutManager
    {
        public bool ScrollEnabled { get; set; } = true;

        public CustomLinearLayoutManager(Context context, int orientation, bool reverseLayout)
            : base(context, orientation, reverseLayout)
        {
        }

        public override bool CanScrollVertically()
        {
            return ScrollEnabled && base.CanScrollVertically();
        }

        public override bool CanScrollHorizontally()
        {
            return ScrollEnabled && base.CanScrollHorizontally();
        }
    }