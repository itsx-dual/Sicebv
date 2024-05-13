using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Cebv.features.formulario_cebv.media_filiacion.presentation;

public partial class MediaFiliacionViewModel: ObservableObject
{
    [ObservableProperty]
    private string _apodo = String.Empty;

    partial void OnApodoChanged(string value)
    {
        Console.WriteLine(Apodo);
    }

    [RelayCommand]
    public void AddApodo()
    {
        Console.WriteLine(Apodo);
    }
    
}