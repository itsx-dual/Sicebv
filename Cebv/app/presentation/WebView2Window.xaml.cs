using System.Net.Http;
using System.Windows;
using Cebv.core.domain;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;

namespace Cebv.app.presentation;

public partial class WebView2Window : Window
{
    
    private static HttpClient _client = CebvClientHandler.SharedClient;
    private string _uri;
    public WebView2Window(string uri, string title)
    {
        InitializeComponent();
        _uri = uri;
        Title = title;
    }

    private async void WebViewOnLoaded(object sender, RoutedEventArgs e)
    {
        await WebView.EnsureCoreWebView2Async(); 
        WebView.CoreWebView2.AddWebResourceRequestedFilter("*", CoreWebView2WebResourceContext.All); 
        WebView.CoreWebView2.WebResourceRequested += CoreWebView2OnWebResourceRequested;
        WebView.Source = new Uri(_client.BaseAddress + _uri);
    }

    private void CoreWebView2OnWebResourceRequested(object? sender, CoreWebView2WebResourceRequestedEventArgs e)
    {
        var token = _client.DefaultRequestHeaders.Authorization?.Parameter;
        e.Request.Headers.SetHeader("Authorization", $"Bearer {token}");
    }
}