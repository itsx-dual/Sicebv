using Cebv.core.data;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Cebv.features.formulario_cebv.reportante.presentation;

public partial class ReportanteViewModel : ObservableObject
{
    [ObservableProperty] private bool _esAnonimo;
    [ObservableProperty] private bool _fechaAproximada;
    
}