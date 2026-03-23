using FantasyLandWalk.ViewModels;

namespace FantasyLandWalk.Views;

public partial class JourneyStatsPage : ContentPage
{
    public JourneyStatsPage(JourneyStatsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
