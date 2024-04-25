using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Cebv.features.Prendas.data.Prendas_Vestir;

public class Grupo_pertenencias : INotifyPropertyChanged
{
    private ObservableCollection<string> _items;
    public ObservableCollection<string> Items
    {
        get { return _items; }
        set 
        {
            _items = value;
            OnPropertyChanged(nameof(Items));
        }
    }

    public Grupo_pertenencias()
    {
        Items = new ObservableCollection<string>();
        AgregarItems();
    }

    private void AgregarItems()
    {
        Items.Add("PRENDA DE VESTIR");
        Items.Add("ALHAJA");
        Items.Add("ACCESORIOS DE DAMA Y CABALLERO");
        Items.Add("OTRO");
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}