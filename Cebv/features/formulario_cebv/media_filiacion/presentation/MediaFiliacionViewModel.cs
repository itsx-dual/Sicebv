using System.Collections.ObjectModel;
using Cebv.features.formulario_cebv.persona_desaparecida.data;
using Cebv.features.formulario_cebv.persona_desaparecida.domain;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

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
        WeakReferenceMessenger.Default.Send(new AddApodoMessage(Apodo));
    }
    
}