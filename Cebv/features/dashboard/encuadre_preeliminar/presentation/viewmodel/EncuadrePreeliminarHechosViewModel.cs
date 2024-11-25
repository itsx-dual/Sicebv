using System.ComponentModel.DataAnnotations;
using Cebv.core.domain;
using Cebv.core.util.reporte.viewmodels;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Cebv.features.dashboard.encuadre_preeliminar.presentation.viewmodel;

public partial class EncuadrePreeliminarViewModel
{
    [Required(ErrorMessage = "Se necesita registrar el estado donde ocurrio el suceso.")]
    [ObservableProperty] private Estado? _estadoSelected;
    
    [ObservableProperty] 
    [Required(ErrorMessage = "Se necesita registrar el municipio donde ocurrio el suceso.")]
    private Municipio? _municipioSelected;
    
    [Required(ErrorMessage = "Se necesita registrar una fecha de desaparición.")]
    [ObservableProperty] private DateTime _fechaDesaparicion = DateTime.Today;
    
    [ObservableProperty] private TimeSpan _horaDesaparicion;
    [ObservableProperty] private bool _seDesconoceFechaExactaHechos;
    async partial void OnEstadoSelectedChanged(Estado? value)
    {
        if (value == null) return;
        Municipios = await CebvNetwork.GetByFilter<Municipio>("municipios", "estado_id", value.Id);
    }

    async partial void OnMunicipioSelectedChanged(Municipio? value)
    {
        if (value == null) return;
        Asentamientos = await CebvNetwork.GetByFilter<Asentamiento>("asentamientos", "municipio_id", value.Id);
    }
    
    partial void OnSeDesconoceFechaExactaHechosChanged(bool value)
    {
        if (value)
        {
            Reporte.HechosDesaparicion!.FechaDesaparicion = null;
            Reporte.HechosDesaparicion.FechaDesaparicion = FechaDesaparicion;
        }
        else
        {
            Reporte.HechosDesaparicion!.FechaDesaparicion = FechaDesaparicion;
            Reporte.HechosDesaparicion.FechaDesaparicion = null;
        }
    }
    
    partial void OnFechaDesaparicionChanged(DateTime value)
    {
        DiferenciaFechas(FechaNacimientoDesaparecido, FechaDesaparicion);
        if (SeDesconoceFechaNacimientoDesaparecido)
        {
            Desaparecido.EdadMomentoDesaparicionAnos = AnosDesaparecido;
            Desaparecido.EdadMomentoDesaparicionMeses = MesesDesaparecido;
            Desaparecido.EdadMomentoDesaparicionDias = DiasDesaparecido;
        }

        if (SeDesconoceFechaExactaHechos)
        {
            Reporte.HechosDesaparicion!.FechaDesaparicion = value;
        }
        else
        {
            Reporte.HechosDesaparicion!.FechaDesaparicion = value;
        }
    }
}