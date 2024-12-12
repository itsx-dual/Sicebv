using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Cebv.core.designsystem.controls;

[INotifyPropertyChanged]
public partial class ReporteCard : UserControl
{
    public ReporteCard()
    {
        InitializeComponent();
    }

    public static readonly DependencyProperty IdProperty = DependencyProperty.Register(
        nameof(Id), typeof(int), typeof(ReporteCard), new PropertyMetadata(default(int)));

    public int Id
    {
        get => (int)GetValue(IdProperty);
        set => SetValue(IdProperty, value);
    }

    public static readonly DependencyProperty MedioConocimientoGenericoProperty = DependencyProperty.Register(
        nameof(MedioConocimientoGenerico), typeof(string), typeof(ReporteCard), new PropertyMetadata(default(string)));

    public string MedioConocimientoGenerico
    {
        get => (string)GetValue(MedioConocimientoGenericoProperty);
        set => SetValue(MedioConocimientoGenericoProperty, value);
    }

    public static readonly DependencyProperty MedioConocimientoEspecificoProperty = DependencyProperty.Register(
        nameof(MedioConocimientoEspecifico), typeof(string), typeof(ReporteCard),
        new PropertyMetadata(default(string)));

    public string MedioConocimientoEspecifico
    {
        get => (string)GetValue(MedioConocimientoEspecificoProperty);
        set => SetValue(MedioConocimientoEspecificoProperty, value);
    }

    public static readonly DependencyProperty TipoReporteProperty = DependencyProperty.Register(
        nameof(TipoReporte), typeof(string), typeof(ReporteCard), new PropertyMetadata(default(string)));

    public string TipoReporte
    {
        get => (string)GetValue(TipoReporteProperty);
        set => SetValue(TipoReporteProperty, value);
    }

    public static readonly DependencyProperty FechaCreacionProperty = DependencyProperty.Register(
        nameof(FechaCreacion), typeof(DateTime), typeof(ReporteCard), new PropertyMetadata(default(DateTime)));

    public DateTime FechaCreacion
    {
        get => (DateTime)GetValue(FechaCreacionProperty);
        set => SetValue(FechaCreacionProperty, value);
    }

    public static readonly DependencyProperty EstadoProperty = DependencyProperty.Register(
        nameof(Estado), typeof(string), typeof(ReporteCard), new PropertyMetadata(default(string)));

    public string Estado
    {
        get => (string)GetValue(EstadoProperty);
        set => SetValue(EstadoProperty, value);
    }

    public static readonly DependencyProperty AbreviaturaEstadoProperty = DependencyProperty.Register(
        nameof(AbreviaturaEstado), typeof(string), typeof(ReporteCard), new PropertyMetadata(default(string)));

    public string AbreviaturaEstado
    {
        get => (string)GetValue(AbreviaturaEstadoProperty);
        set => SetValue(AbreviaturaEstadoProperty, value);
    }

    public static readonly DependencyProperty EstaGuardadoProperty = DependencyProperty.Register(
        nameof(EstaGuardado), typeof(bool), typeof(ReporteCard), new PropertyMetadata(default(bool)));

    public bool EstaGuardado
    {
        get => (bool)GetValue(EstaGuardadoProperty);
        set => SetValue(EstaGuardadoProperty, value);
    }

    private async void ToggleReport(object sender, RoutedEventArgs e)
    {
        var bt = (Button)sender;
        bt.IsEnabled = false;

        var vm = new ReporteCardViewModel(Id);
        await vm.ToggleReporteCommand.ExecuteAsync(null);
        EstaGuardado = vm.ReporteToggle.EsFavorito;

        bt.IsEnabled = true;
    }
}