using Cebv.core.util.reporte.viewmodels;

namespace Cebv.core.util.reporte;

/// <summary>
/// Interfaz <c>IReporteService</c>
/// </summary>
public interface IReporteService
{
    /// <summary>
    /// Obtiene el reporte actual.
    /// </summary>
    /// <returns>El objeto <see cref="Reporte"/> si existe, de lo contrario null.</returns>
    Reporte GetReporte();
    
    /// <summary>
    /// Sincroniza el reporte actual con el servicio web externo de forma asíncrona.
    /// </summary>
    /// <returns>
    /// Una tarea que representa la operación de sincronización y que, al completarse, devuelve el <see cref="Reporte"/> sincronizado,
    /// null si no se logro sincronizar de manera exitosa.
    /// </returns>
    /// <remarks>
    /// Este método se encarga de realizar la comunicación con el servicio web, 
    /// enviar los datos necesarios y recibir la respuesta actualizada.
    /// </remarks>
    Task<Reporte> Sync();
    
    /// <summary>
    /// Recarga un reporte específico desde el servicio web externo de forma asíncrona.
    /// </summary>
    /// <param name="id">El identificador único del reporte a recargar.</param>
    /// <returns>Una tarea que representa la operación de recarga y que, al completarse, devuelve el <see cref="Reporte"/> recargado.</returns>
    /// <remarks>
    /// Este método realiza una solicitud al servicio web para obtener los datos del reporte 
    /// correspondiente al identificador proporcionado.
    /// </remarks>
    Task<Reporte> Reload(int id);
    
    /// <summary>
    /// Borra el contenido del <see cref="Reporte"/> actual.
    /// </summary>
    /// <returns>El objeto Reporte vacío.</returns>
    Reporte ClearReporte();
    
    /// <summary>
    /// Obtiene el estado actual del <see cref="Reporte"/>.
    /// </summary>
    /// <returns>El estado del reporte, representado por el enum <see cref="EstadoReporte"/>.</returns>
    EstadoReporte GetStatusReporte();
    
    /// <summary>
    /// Establece el estado del reporte.
    /// </summary>
    /// <param name="estado">El nuevo estado del reporte, utilizando el enum <see cref="EstadoReporte"/>.</param>
    void SetStatusReporte(EstadoReporte estado);
    
    /// <summary>
    /// Crea los folios de los desaparecidos del reporte.
    /// </summary>
    /// <returns>True si los folios fueron correctamente asignados, False si no se asignaron.</returns>
    Task<bool> SetFolios();
    
    /// <summary>
    /// Obtiene el identificador único del reporte actual.
    /// </summary>
    /// <returns>El identificador del reporte, o 0 si no hay reporte cargado.</returns>
    int GetReporteId();
    
    /// <summary>
    /// Verifica si hay un reporte cargado actualmente.
    /// </summary>
    /// <returns>True si hay un reporte, False si no lo hay.</returns>
    bool HayReporte();
}