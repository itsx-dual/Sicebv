using CommunityToolkit.Mvvm.ComponentModel;

namespace Cebv.features.formulario_cebv.testing.presentation;

public partial class TestingViewModel : ObservableObject
{
    [ObservableProperty] public TimeSpan _time;

    public TestingViewModel()
    {
        Time = new TimeSpan(10, 30, 0);
    }
}