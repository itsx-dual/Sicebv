using System.Collections.ObjectModel;
using System.Windows;
using Cebv.core.domain;
using Cebv.core.util.reporte.viewmodels;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Cebv.features.components.edit_telefono;

public partial class EditTelefonoViewModel : ObservableObject
{
    [ObservableProperty] private ObservableCollection<Catalogo> _companiasTelefonicas = [];
    [ObservableProperty] private Telefono _telefono;
    [ObservableProperty] private Visibility _verCompanias = Visibility.Visible;

    public EditTelefonoViewModel() => InitAsync();

    private async void InitAsync()
    {
        if (VerCompanias == Visibility.Visible) CompaniasTelefonicas = await 
            CebvNetwork.GetRoute<Catalogo>("companias-telefonicas");
    }
}