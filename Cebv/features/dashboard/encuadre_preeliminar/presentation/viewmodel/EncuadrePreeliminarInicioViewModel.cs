using System.ComponentModel.DataAnnotations;
using Cebv.core.domain;
using Cebv.core.util.reporte.viewmodels;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Cebv.features.dashboard.encuadre_preeliminar.presentation.viewmodel;

public partial class EncuadrePreeliminarViewModel
{
    [ObservableProperty] 
    [Required(ErrorMessage = "Se debe escoger el tipo del medio por el cual el reporte es recibido.")]
    private Catalogo? _tipoMedioSelected;
    
    async partial void OnTipoMedioSelectedChanged(Catalogo? value)
    {
        if (value is null) return;
        Medios = await CebvNetwork.GetByFilter<MedioConocimiento>("medios", "tipo_medio_id", value.Id.ToString()!);
    }
}