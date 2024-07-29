using System.Windows.Controls;
using System.Windows.Threading;

namespace Cebv.features.dashboard.presentation;

public partial class Widget1 : UserControl
{
    private readonly DispatcherTimer _timer;
    public Widget1()
    {
        InitializeComponent();
        
        _timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(1)
        };
        _timer.Tick += UpdateClock;
        _timer.Start();

        UpdateClock(this, EventArgs.Empty); // Initialize the clock immediately
    }
    
    private void UpdateClock(object sender, EventArgs e)
    {
        TimeTextBlock.Text = DateTime.Now.ToString("HH:mm");
        DateTextBlock.Text = DateTime.Now.ToString("dddd, MMMM d, yyyy");
    }
}