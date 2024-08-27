using System.ComponentModel;
using CameraTest1.Models;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CameraTest1;

public partial class MainPage : ContentPage
{

	
	
	private List<Items> _collectionItems =  new List<Items>() { new Items { Id = 1, Name = "Alej" }, new Items { Id = 2, Name = "Pere" }, new Items { Id = 3, Name = "Pere" }  };

	private bool _isDrawing = true;


	public bool IsDrawing
	{
		get  => _isDrawing;
		set
		{
			_isDrawing = value;
			OnPropertyChanged(nameof(IsDrawing)); 
		} 
	}
	public List<Items> CollectionItems
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

	public MainPage()
	{
		InitializeComponent();
		BindingContext = this;
	}

	private async void OnCounterClicked(object sender, EventArgs e)
	{

		
		await Navigation.PushAsync(new PhotoView());

		
	}

	

	private void VisualElement_OnFocused(object? sender, FocusEventArgs e)
	{
		var strt = string.Empty;
		
	}
}



