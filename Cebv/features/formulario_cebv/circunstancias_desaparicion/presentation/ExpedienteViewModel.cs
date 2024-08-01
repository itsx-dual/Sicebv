using System.Collections.ObjectModel;
using Cebv.core.modules.persona.data;
using Cebv.features.formulario_cebv.circunstancias_desaparicion.domain;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Cebv.features.formulario_cebv.circunstancias_desaparicion.presentation;

public partial class ExpedienteViewModel : ObservableObject
{
    [ObservableProperty] private string? _nombre;
    [ObservableProperty] private string? _primerApellido;
    [ObservableProperty] private string? _segundoApellido;
    
    [ObservableProperty] private ObservableCollection<Persona> _personas = new();
    [ObservableProperty] private ObservableCollection<Expediente> _expedientes = new();
    
    /**
     * Logica para la relación de los expedientes.
     */
    [RelayCommand]
    private async Task BuscarPersona()
    {
        Personas = await CircunstanciaDesaparicionNetwork.BuscarPersona(
            Nombre,
            PrimerApellido,
            SegundoApellido
        );
    }
    
    [RelayCommand]
    private void AddExpediente(Persona persona)
    {
        if (Expedientes.Any(p => p.Persona.Id == persona.Id)) return;

        var viewModel = new RelacionarExpedienteViewModel(persona);

        // Suscribirse al evento de guardado
        viewModel.GuardarExpediente += OnExpedienteGuardado;

        // Abrir la ventana de edición de la prenda
        var dialog = new AgregarExpediente { DataContext = viewModel };

        // Configurar la acción de cierre para la ventana de edición
        if (dialog.DataContext is RelacionarExpedienteViewModel vm)
        {
            vm.CloseAction = () => dialog.Close();
        }

        dialog.ShowDialog();
    }

    private void OnExpedienteGuardado(object? sender, Expediente expediente)
    {
        if (sender is not RelacionarExpedienteViewModel) return;

        Expedientes.Add(expediente);
    }

    [RelayCommand]
    private void RemoveExpediente(Expediente expediente)
    {
        Expedientes.Remove(expediente);
    }
}