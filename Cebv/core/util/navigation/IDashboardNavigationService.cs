using Wpf.Ui;
using Wpf.Ui.Controls;

namespace Cebv.core.util.navigation;

public interface IDashboardNavigationService
{
    /// <summary>
    /// Lets you navigate to the selected page based on its type. Should be used with <see cref="IPageService"/>.
    /// </summary>
    /// <param name="pageType"><see langword="Type"/> of the page.</param>
    /// <returns><see langword="true"/> if the operation succeeds. <see langword="false"/> otherwise.</returns>
    bool Navigate(Type pageType);

    /// Clears the navigation stack, effectively resetting the navigation history.
    void ClearNavigationStack();

    /// <summary>
    /// Lets you navigate to the selected page based on its type, Should be used with <see cref="IPageService"/>.
    /// </summary>
    /// <param name="pageType"><see langword="Type"/> of the page.</param>
    /// <param name="dataContext">DataContext <see cref="object"/></param>
    /// <param name="caller"></param>
    /// <returns><see langword="true"/> if the operation succeeds. <see langword="false"/> otherwise.</returns>
    bool Navigate(Type pageType, object? dataContext);

    /// <summary>
    /// Lets you navigate to the selected page based on it's tag. Should be used with <see cref="IPageService"/>.
    /// </summary>
    /// <param name="pageIdOrTargetTag">Id or tag of the page.</param>
    /// <returns><see langword="true"/> if the operation succeeds. <see langword="false"/> otherwise.</returns>
    bool Navigate(string pageIdOrTargetTag);

    /// <summary>
    /// Lets you navigate to the selected page based on it's tag. Should be used with <see cref="IPageService"/>.
    /// </summary>
    /// <param name="pageType"> Tipo de pagina a la que se desea navegar. </param>
    /// <param name="dataContext"> DataContext que se le asignara a la Page despues de ser creada e instanciada, esto puede llevar a dos instancias de dicho viewmodel. <see cref="object"/></param>
    /// <param name="caller"></param>
    /// <param name="callerName"> Se sugiere agregar en navegacion de Page a Page. Se necesita para controlar correctamente el estado del reporte. </param>
    /// <returns><see langword="true"/> if the operation succeeds. <see langword="false"/> otherwise.</returns>
    bool Navigate(Type pageType, object? dataContext, Type caller = null);

    /// <summary>
    /// Synchronously adds an element to the navigation stack and navigates current navigation Frame to the
    /// </summary>
    /// <param name="pageType">Type of control to be synchronously added to the navigation stack</param>
    /// <returns><see langword="true"/> if the operation succeeds. <see langword="false"/> otherwise.</returns>
    bool NavigateWithHierarchy(Type pageType);

    /// <summary>
    /// Synchronously adds an element to the navigation stack and navigates current navigation Frame to the
    /// </summary>
    /// <param name="pageType">Type of control to be synchronously added to the navigation stack</param>
    /// <param name="dataContext">DataContext <see cref="object"/></param>
    /// <returns><see langword="true"/> if the operation succeeds. <see langword="false"/> otherwise.</returns>
    bool NavigateWithHierarchy(Type pageType, object? dataContext);

    /// <summary>
    /// Provides direct access to the control responsible for navigation.
    /// </summary>
    /// <returns>Instance of the <see cref="INavigationView"/> control.</returns>
    INavigationView GetNavigationControl();

    /// <summary>
    /// Lets you attach the control that represents the <see cref="INavigationView"/>.
    /// </summary>
    /// <param name="navigation">Instance of the <see cref="INavigationView"/>.</param>
    void SetNavigationControl(INavigationView navigation);

    /// <summary>
    /// Lets you attach the service that delivers page instances to <see cref="INavigationView"/>.
    /// </summary>
    /// <param name="pageService">Instance of the <see cref="IPageService"/>.</param>
    void SetPageService(IPageService pageService);

    /// <summary>
    /// Navigates the NavigationView to the previous journal entry.
    /// </summary>
    /// <returns><see langword="true"/> if the operation succeeds. <see langword="false"/> otherwise.</returns>
    bool GoBack();
}