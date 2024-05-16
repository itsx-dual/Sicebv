using System.Windows;
using System.Windows.Controls;
using Cebv.core.util;

namespace Cebv.features.reportante.presentation;

public partial class ReportanteDetailPage : Page
{
    public ReportanteDetailPage()
    {
        InitializeComponent();

        NombreColectivoTb.TextChanged += TextBoxHelper.UpperCaseText;
        NombreColectivoTb.LostFocus += TextBoxHelper.TrimmedText;
        
        InformacionRelevanteTb.TextChanged += TextBoxHelper.UpperCaseText;
        InformacionRelevanteTb.LostFocus += TextBoxHelper.TrimmedText;
    }
}