using System.Collections.ObjectModel;
using System.Configuration.Internal;
using System.Windows.Threading;
using Cebv.features.formulario_cebv.persona_desaparecida.data;
using Cebv.features.formulario_cebv.persona_desaparecida.domain;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace Cebv.features.formulario_cebv.persona_desaparecida.presentation;

public partial class PersonaDesaparecidaViewModel : ObservableObject, IRecipient<AddApodoMessage>
{
    [ObservableProperty] private bool _mostrarDireccion;
    [ObservableProperty] private ObservableCollection<string> _apodosList = new();
    private readonly IApodoService _apodoService;

    [ObservableProperty] private string _test = string.Empty;

    partial void OnTestChanged(string value)
    {
        Console.WriteLine(Test);
    }

    public PersonaDesaparecidaViewModel()
    {
        _apodoService = App.Current.Services.GetService<IApodoService>();
        WeakReferenceMessenger.Default.RegisterAll(this);
    }

    public void Receive(AddApodoMessage message)
    {
        _apodoService.Apodos.Add(message.Value);
    }

    public ObservableCollection<string> Apodos { get; set; }
}