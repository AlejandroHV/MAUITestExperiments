using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CameraTest1.Models;


public partial class MainPageViewModel : ObservableObject
{
    private ObservableCollection<Items> _collectionItems =  new ObservableCollection<Items>() { new Items { Id = 1, Name = "Alej" }, new Items { Id =2, Name = "Pere" } };

    public ObservableCollection<Items> CollectionItems
    {
        get => _collectionItems;
        set
        {
            if (_collectionItems != value)
            {
                _collectionItems = value;
                OnPropertyChanged(nameof(CollectionItems));  // Notify the UI that the property has changed.
            }
        }
    }
     [ObservableProperty] private bool _isDrawing;
}