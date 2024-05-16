using System.Windows.Controls;
using Cebv.core.util;

namespace Cebv.features.formulario_cebv.datos_del_reporte.presentation;

public partial class DatosReportePage : Page
{
    public DatosReportePage()
    {
        InitializeComponent();
        
        DependenciaOrigenTb.TextChanged += TextBoxHelper.UpperCaseText;
        DependenciaOrigenTb.LostFocus += TextBoxHelper.TrimmedText;
        
        FolioFubTb.TextChanged += TextBoxHelper.UpperCaseText;
        FolioFubTb.LostFocus += TextBoxHelper.TrimmedText;
        
        AutoridadIngresoFubTb.TextChanged += TextBoxHelper.UpperCaseText;
        AutoridadIngresoFubTb.LostFocus += TextBoxHelper.TrimmedText;
        
        NumeroReporteTb.TextChanged += TextBoxHelper.UpperCaseText;
        NumeroReporteTb.LostFocus += TextBoxHelper.TrimmedText;
        
        RadicaReporteTb.TextChanged += TextBoxHelper.UpperCaseText; 
        RadicaReporteTb.LostFocus += TextBoxHelper.TrimmedText;
        
        NombreServidorPublicoTb.TextChanged += TextBoxHelper.UpperCaseText;
        NombreServidorPublicoTb.LostFocus += TextBoxHelper.TrimmedText;
        
        NumeroAmparoBuscadorTb.TextChanged += TextBoxHelper.UpperCaseText;
        NumeroAmparoBuscadorTb.LostFocus += TextBoxHelper.TrimmedText;
        
        RadicaAmparoBuscadorTb.TextChanged += TextBoxHelper.UpperCaseText;
        RadicaAmparoBuscadorTb.LostFocus += TextBoxHelper.TrimmedText;
        
        NombreJuezTb.TextChanged += TextBoxHelper.UpperCaseText;
        NombreJuezTb.LostFocus += TextBoxHelper.TrimmedText;
        
        NumeroRecomendacionDerechosHumanosTb.TextChanged += TextBoxHelper.UpperCaseText;
        NumeroRecomendacionDerechosHumanosTb.LostFocus += TextBoxHelper.TrimmedText;
        
        RadicaRecomendacionDerechosHumanosTb.TextChanged += TextBoxHelper.UpperCaseText;
        RadicaRecomendacionDerechosHumanosTb.LostFocus += TextBoxHelper.TrimmedText;
        
        NombreServidorPulicoTb.TextChanged += TextBoxHelper.UpperCaseText;
        NombreServidorPulicoTb.LostFocus += TextBoxHelper.TrimmedText;
        
        OtroTb.TextChanged += TextBoxHelper.UpperCaseText;
        OtroTb.LostFocus += TextBoxHelper.TrimmedText;
    }
}