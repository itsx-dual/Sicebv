using CommunityToolkit.Mvvm.Input;
using Wpf.Ui;

namespace Cebv.features.formulario_cebv.presentation;

public partial class FormularioNavigation(INavigationService navigationService)
{
    [RelayCommand]
    private void NavigateForward(Type type)
    {
        navigationService.Navigate(type);
    }

    [RelayCommand]
    private void NavigateBack()
    {
        navigationService.GoBack();
    }
}