using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.util.reporte.viewmodels;

public partial class MedioConocimiento : ObservableObject
{
    [JsonConstructor]
    public MedioConocimiento(
        int? id,
        Catalogo tipo_medio, 
        string? nombre)
    {
        Id = id;
        TipoMedio = tipo_medio;
        Nombre = nombre;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true; // Same object reference
        if (ReferenceEquals(obj, null)) return false; // Other object is null
        if (obj.GetType() != this.GetType()) return false; // Different types

        return Equals((MedioConocimiento) obj);
    }

    private bool Equals(MedioConocimiento medio)
    {
        return Id == medio.Id &&
               Nombre == medio.Nombre;
    }

    [ObservableProperty]
    private int? _id;
    
    [ObservableProperty]
    private Catalogo _tipoMedio;
    
    [ObservableProperty]
    private string? _nombre;
    
    /**
     * Hola tanil no se que madres le movi pero esto no jala
     */
}