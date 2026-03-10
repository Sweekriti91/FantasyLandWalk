using FantasyLandWalk.ViewModels;

namespace FantasyLandWalk.Views;

public partial class MapSelectionPage : ContentPage
{
    public MapSelectionPage(MapSelectionViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
