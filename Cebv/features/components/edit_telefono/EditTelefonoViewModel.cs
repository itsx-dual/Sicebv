using System.Collections.ObjectModel;
using Cebv.core.domain;
using Cebv.core.util.reporte.viewmodels;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Cebv.features.components.edit_telefono;

public partial class EditTelefonoViewModel : ObservableObject
{
    [ObservableProperty] private ObservableCollection<Catalogo> _companiasTelefonicas = [];
    [ObservableProperty] private Telefono _telefono;

    public EditTelefonoViewModel() => InitAsync();
    private async void InitAsync() => CompaniasTelefonicas = await CebvNetwork.GetRoute<Catalogo>("companias-telefonicas");
}