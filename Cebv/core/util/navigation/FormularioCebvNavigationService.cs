using Wpf.Ui;
using Wpf.Ui.Controls;

namespace Cebv.core.util.navigation;
// TODO: Este codigo esta copiado de lepoco/wpfui, aun debe ser adaptado a nuestro proyecto.
public partial class FormularioCebvNavigationService : IFormularioCebvNavigationService
{
        /// <summary>
    /// Locally attached service provider.
    /// </summary>
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// Locally attached page service.
    /// </summary>
    private IPageService? _pageService;

    /// <summary>
    /// Gets or sets the control representing navigation.
    /// </summary>
    protected INavigationView? NavigationControl { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="NavigationService"/> class.
    /// </summary>
    /// <param name="serviceProvider">Service provider providing page instances.</param>
    public FormularioCebvNavigationService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    /// <inheritdoc />
    public INavigationView GetNavigationControl()
    {
        return NavigationControl ?? throw new ArgumentNullException(nameof(NavigationControl));
    }

    /// <inheritdoc />
    public void SetNavigationControl(INavigationView navigation)
    {
        Console.WriteLine(navigation);
        NavigationControl = navigation;

        if (_pageService == null) return;
        NavigationControl.SetPageService(_pageService);

        //NavigationControl.SetServiceProvider(_serviceProvider);
    }

    /// <inheritdoc />
    public void SetPageService(IPageService pageService)
    {
        if (NavigationControl == null)
        {
            _pageService = pageService;

            return;
        }

        ThrowIfPageServiceIsNull();

        NavigationControl.SetPageService(_pageService!);
    }

    /// <inheritdoc />
    public bool Navigate(Type pageType)
    {
        ThrowIfNavigationControlIsNull();

        return NavigationControl!.Navigate(pageType);
    }

    /// <inheritdoc />
    public bool Navigate(Type pageType, object? dataContext)
    {
        ThrowIfNavigationControlIsNull();

        return NavigationControl!.Navigate(pageType, dataContext);
    }

    /// <inheritdoc />
    public bool Navigate(string pageTag)
    {
        ThrowIfNavigationControlIsNull();

        return NavigationControl!.Navigate(pageTag);
    }

    /// <inheritdoc />
    public bool Navigate(string pageTag, object? dataContext)
    {
        ThrowIfNavigationControlIsNull();

        return NavigationControl!.Navigate(pageTag, dataContext);
    }

    /// <inheritdoc />
    public bool GoBack()
    {
        ThrowIfNavigationControlIsNull();

        return NavigationControl!.GoBack();
    }

    /// <inheritdoc />
    public bool NavigateWithHierarchy(Type pageType)
    {
        ThrowIfNavigationControlIsNull();

        return NavigationControl!.NavigateWithHierarchy(pageType);
    }

    /// <inheritdoc />
    public bool NavigateWithHierarchy(Type pageType, object? dataContext)
    {
        ThrowIfNavigationControlIsNull();

        return NavigationControl!.NavigateWithHierarchy(pageType, dataContext);
    }

    protected void ThrowIfNavigationControlIsNull()
    {
        if (NavigationControl is null)
        {
            throw new ArgumentNullException(nameof(NavigationControl));
        }
    }

    protected void ThrowIfPageServiceIsNull()
    {
        if (_pageService is null)
        {
            throw new ArgumentNullException(nameof(_pageService));
        }
    }
}