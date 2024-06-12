using System.Net.Http;
using Cebv.core.domain;

namespace Cebv.features.formulario_cebv.media_filiacion.domain;

public class MediaFiliacionNetwork
{
    private static HttpClient Client => CebvClientHandler.SharedClient;

}