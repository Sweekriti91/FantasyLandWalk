using FantasyLandWalk.ViewModels;

namespace FantasyLandWalk.Views;

public partial class JourneyMapPage : ContentPage
{
    public JourneyMapPage(JourneyMapViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
