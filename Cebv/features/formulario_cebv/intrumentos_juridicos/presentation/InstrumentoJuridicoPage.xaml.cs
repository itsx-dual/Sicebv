using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace Cebv.features.formulario_cebv.intrumentos_juridicos.presentation;

public partial class InstrumentoJuridicoPage : Page
{
    public InstrumentoJuridicoPage()
    {
        InitializeComponent();
        DataContext = App.Current.Services.GetService<InstrumentoJuridicoViewModel>();
    }
}