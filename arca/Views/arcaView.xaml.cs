using arca.ViewModels;

namespace arca.Views;

public sealed partial class arcaView
{
    public arcaView(arcaViewModel viewModel)
    {
        DataContext = viewModel;
        InitializeComponent();
    }
}