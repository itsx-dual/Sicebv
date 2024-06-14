using System.Windows.Controls;
using Cebv.core.util;
using System.Windows;
using TextBox = Wpf.Ui.Controls.TextBox;

namespace Cebv.features.formulario_cebv.folio_expediente.presentation;

public partial class FolioExpedientePage : Page
{
    public FolioExpedientePage()
    {
        InitializeComponent();
        TextBoxHelperMethod();
        //SubscribeTexBoxesEvents(this);
    }

    private void TextBoxHelperMethod()
    {
        FolioCEBVTb.TextChanged += TextBoxHelper.UpperCaseText;
        FolioCEBVTb.LostFocus += TextBoxHelper.TrimmedText;
        EstadoTb.TextChanged += TextBoxHelper.UpperCaseText;
        EstadoTb.LostFocus += TextBoxHelper.TrimmedText;
        MunicipioTb.TextChanged += TextBoxHelper.UpperCaseText;
        MunicipioTb.LostFocus += TextBoxHelper.TrimmedText;
        NumeroPersonasMismoEventoTb.TextChanged += TextBoxHelper.UpperCaseText;
        NumeroPersonasMismoEventoTb.LostFocus += TextBoxHelper.TrimmedText;
        FechaDesaparicionTb.TextChanged += TextBoxHelper.UpperCaseText;
        FechaDesaparicionTb.LostFocus += TextBoxHelper.TrimmedText;
        TerminacionEntidadTb.TextChanged += TextBoxHelper.UpperCaseText;
        TerminacionEntidadTb.LostFocus += TextBoxHelper.TrimmedText;
        NombreAsignoFolioTb.TextChanged += TextBoxHelper.UpperCaseText;
        NombreAsignoFolioTb.LostFocus += TextBoxHelper.TrimmedText;
        ObservacionesTb.TextChanged += TextBoxHelper.UpperCaseText;
        ObservacionesTb.LostFocus += TextBoxHelper.TrimmedText;
    }

    /*private void SubscribeTexBoxesEvents(DependencyObject depObj)
    {
        foreach (TextBox textBox in HelperMethods.FindVisualChildren<TextBox>(depObj))
        {
            textBox.TextChanged += TextBoxHelper.UpperCaseText;
            textBox.LostFocus += TextBoxHelper.TrimmedText;
        }
    }*/
}