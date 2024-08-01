using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Wpf.Ui.Appearance;

namespace Cebv.features.dashboard.presentation;

public partial class Inicio : Page
{
    private readonly DispatcherTimer _timer;
    
    public Inicio()
    {
        InitializeComponent();
        
        _timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(1)
        };
        _timer.Tick += UpdateClock;
        _timer.Start();

        UpdateClock(this, EventArgs.Empty);
    }
    
    private void UpdateClock(object sender, EventArgs e)
    {
        TimeTextBlock.Text = DateTime.Now.ToString("HH:mm");
        DateTextBlock.Text = DateTime.Now.ToString("dddd, MMMM d, yyyy");
    }

    private void Light(object sender, RoutedEventArgs e)
    {
        SetApplicationTheme(ApplicationTheme.Dark);
    }

    private void Dark(object sender, RoutedEventArgs e)
    {
        SetApplicationTheme(ApplicationTheme.Light);
    }
    
    private void SetApplicationTheme(ApplicationTheme theme)
    {
        var app = Application.Current as App;
        app?.SetTheme(theme);
    }
}