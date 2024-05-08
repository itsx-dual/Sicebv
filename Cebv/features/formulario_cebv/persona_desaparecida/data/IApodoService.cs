using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Cebv.features.formulario_cebv.persona_desaparecida.data;

public interface IApodoService
{
    ObservableCollection<string> Apodos { get; set; }

    public void AddApodo(string Apodo);

}

public class ApodoService : IApodoService
{
    public ObservableCollection<string> Apodos { get; set; }
    public void AddApodo(string Apodo)
    {
        Apodos.Add(Apodo);
    }
}