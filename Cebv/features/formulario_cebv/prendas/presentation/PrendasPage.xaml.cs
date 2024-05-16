using System.Windows.Controls;
using Cebv.core.util;

namespace Cebv.features.formulario_cebv.prendas.presentation;

public partial class PrendasPage : Page
{
    public PrendasPage()
    {
        InitializeComponent();

        DescripcionTb.TextChanged += TextBoxHelper.UpperCaseText;
        DescripcionTb.TextChanged += TextBoxHelper.TrimmedText;
    }
}