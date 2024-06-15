using System.IO;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;

namespace Cebv.features.formulario_cebv.comunicacion_difusion.presentation;

public partial class GeneracionBoletinViewModel : ObservableObject
{
    /**
     * Path de las imagenes seleccionadas
     */
    [ObservableProperty]
    private string[]? _openedFilePath = [];
    
    [RelayCommand]
    private void OnOpenFile()
    {
        OpenFileDialog openFileDialog =
            new()
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
                Multiselect = true
            };

        if (openFileDialog.ShowDialog() != true)
        {
            return;
        }

        if (!File.Exists(openFileDialog.FileName))
        {
            return;
        }

        OpenedFilePath = openFileDialog.FileNames;
    }
}