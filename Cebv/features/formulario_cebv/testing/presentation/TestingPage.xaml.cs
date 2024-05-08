using System.Windows.Controls;

namespace Cebv.features.formulario_cebv.testing.presentation;

public partial class TestingPage : Page
{
    public DateTime hora; 
    public TestingPage()
    {
        InitializeComponent();
        hora = DateTime.Now;
    }
}