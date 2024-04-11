using System.Windows;

namespace Cebv;

public interface IWindow
{
    event RoutedEventHandler Loaded;
    void Show();
}