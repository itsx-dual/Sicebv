using Cebv.core.util.reporte.viewmodels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using static Cebv.core.util.CollectionsHelper;
using static Cebv.core.util.UiState;

namespace Cebv.features.formulario_cebv.reportante.presentation;

public partial class ReportanteViewModel
{
    [ObservableProperty] private Telefono _tP = new();

    #region TelefonoMovil

    [ObservableProperty] private Telefono _telefonoMovil = new() { EsMovil = true };

    [RelayCommand]
    private void OnAddTelefonoMovil()
    {
        if (TelefonoMovil.Numero is null) return;

        Reportante.Persona.Telefonos.Add(TelefonoMovil);

        SetObjectDefaultValues(TelefonoMovil, TP.TelefonoMovilDefault);
    }

    [RelayCommand]
    private void OnEditTelefonoMovil(Telefono telefono)
    {
        UiState = Edit;

        TelefonoMovil = telefono;
    }

    [RelayCommand]
    private void OnSaveTelefonoMovil()
    {
        if (TelefonoMovil.Numero is null) return;

        var index = Reportante.Persona.Telefonos.IndexOf(TelefonoMovil);

        if (index is -1) return;

        Reportante.Persona.Telefonos.Insert(index, TelefonoMovil);
        //SetDefaultTelefonoMovil();
        UiState = Normal;
    }

    [RelayCommand]
    private void OnCancelTelefonoMovil()
    {
        //SetDefaultTelefonoMovil();
        UiState = Normal;
    }

    #endregion
};