using CommunityToolkit.Mvvm.ComponentModel;

namespace Cebv.features.dashboard.filtro_busqueda.Presentation;

public class ApplyFIlter : ObservableObject
{
    private readonly FiltroBusquedaViewModel _viewModel;
    
    public ApplyFIlter(FiltroBusquedaViewModel viewModel)
    {
        _viewModel = viewModel;
    }
    
    /*
     * Queda pendiente la implementación de la función AplicandoFiltros, ya que no se pasa el valor correctamente.
     */
    
    public async Task<string> AplicandoFiltros()
    {
        var filtros = new List<string>();

        if (_viewModel.NombreDesaparecido != null)
        {
            filtros.Add($"[nombreCompleto_desaparecido]={_viewModel.NombreDesaparecido}");
        }
        if (_viewModel.PseudonimoDesaparecido != null)
        {
            filtros.Add($"[pseudonimoCompleto_desaparecido]={_viewModel.PseudonimoDesaparecido}");
        }
        if (_viewModel.NombreReportante != null)
        {
            filtros.Add($"[nombreCompleto_reportante]={_viewModel.NombreReportante}");
        }
        if (_viewModel.PseudonimoReportante != null)
        {
            filtros.Add($"[pseudonimoCompleto_reportante]={_viewModel.PseudonimoReportante}");
        }
        if (_viewModel.TipoReporteSelected != null)
        {
            filtros.Add($"[tipo_reporte_id]={_viewModel.TipoReporteSelected.Id}");
        }
        if (_viewModel.AreaSelected != null)
        {
            filtros.Add($"[area_atiende_id]={_viewModel.AreaSelected.Id}");
        }
        if (_viewModel.TipoMedioSelected != null)
        {
            filtros.Add($"[tipo_medio_id]={_viewModel.TipoMedioSelected.Id}");
        }
        if (_viewModel.MedioSelected != null)
        {
            filtros.Add($"[medio_conocimiento_id]={_viewModel.MedioSelected.Id}");
        }
        if (_viewModel.EstadoSelected != null)
        {
            filtros.Add($"[estado_id]={_viewModel.EstadoSelected.Id}");
        }
        if (_viewModel.ZonaEstadoSelected != null)
        {
            filtros.Add($"[zona_estado_id]={_viewModel.ZonaEstadoSelected.Id}");
        }
        if (_viewModel.LugarNacimientoReportanteSelected != null)
        {
            filtros.Add($"[reportante/lugar_nacimiento_id]={_viewModel.LugarNacimientoReportanteSelected.Id}");
        }
        if (_viewModel.EscolaridadReportanteSelected != null)
        {
            filtros.Add($"[reportante/escolaridad_id]={_viewModel.EscolaridadReportanteSelected.Id}");
        }
        if (_viewModel.SexoReportanteSelected != null)
        {
            filtros.Add($"[reportante/sexo_id]={_viewModel.SexoReportanteSelected.Id}");
        }
        if (_viewModel.GeneroReportanteSelected != null)
        {
            filtros.Add($"[reportante/genero_id]={_viewModel.GeneroReportanteSelected.Id}");
        }
        if (_viewModel.NacionalidadReportanteSelected != null)
        {
            filtros.Add($"[reportante/nacionalidad_id]={_viewModel.NacionalidadReportanteSelected.Id}");
        }
        if (_viewModel.ReligionReportanteSelected != null)
        {
            filtros.Add($"[reportante/religion_id]={_viewModel.ReligionReportanteSelected.Id}");
        }
        if (_viewModel.LenguaReportanteSelected != null)
        {
            filtros.Add($"[reportante/lengua_id]={_viewModel.LenguaReportanteSelected.Id}");
        }
        if (_viewModel.ParentescoReportanteSelected != null)
        {
            filtros.Add($"[reportante/parentesco_id]={_viewModel.ParentescoReportanteSelected.Id}");
        }
        if (_viewModel.ColectivoReportanteSelected != null)
        {
            filtros.Add($"[reportante/colectivo_id]={_viewModel.ColectivoReportanteSelected.Id}");
        }
        if (_viewModel.EstadoConyugalReportanteSelected != null)
        {
            filtros.Add($"[reportante/estado_conyugal_id]={_viewModel.EstadoConyugalReportanteSelected.Id}");
        }
        if (_viewModel.PublicacionRegistroNacional != null)
        {
            filtros.Add($"[publicacion_registro_nacional]={_viewModel.PublicacionRegistroNacional}");
        }
        if (_viewModel.PublicacionBoletin != null)
        {
            filtros.Add($"[publicacion_boletin]={_viewModel.PublicacionBoletin}");
        }
        if (_viewModel.DescripcionExtorsion != null)
        {
            filtros.Add($"[descripcion_extorsion]={_viewModel.DescripcionExtorsion}");
        }
        if (_viewModel.DescripcionDondeProviene != null)
        {
            filtros.Add($"[descripcion_donde_proviene]={_viewModel.DescripcionDondeProviene}");
        }
        if (_viewModel.NoTelefonoReportante != null)
        {
            filtros.Add($"[reportante/telefono]={_viewModel.NoTelefonoReportante}");
        }
        if (_viewModel.CompañiaTelefonicaReportanteSelected != null)
        {
            filtros.Add($"[reportante/telefono/compania_id]={_viewModel.CompañiaTelefonicaReportanteSelected.Id}");
        }
        if (_viewModel.EsMovilReportante != null)
        {
            filtros.Add($"[reportante/telefono/es_movil]={_viewModel.EsMovilReportante}");
        }
        if (_viewModel.InformacionConsentimiento != null)
        {
            filtros.Add($"[informacion_consentimiento]={_viewModel.InformacionConsentimiento}");
        }
        if (_viewModel.InformacionExclusivaBusqueda != null)
        {
            filtros.Add($"[informacion_exclusiva_busqueda]={_viewModel.InformacionExclusivaBusqueda}");
        }
        if (_viewModel.InformacionRelevante != null)
        {
            filtros.Add($"[informacion_relevante]={_viewModel.InformacionRelevante}");
        }
        if (_viewModel.FechaNacimientoReportante != null)
        {
            filtros.Add($"[reportante/fecha_nacimiento]={_viewModel.FechaNacimientoReportante}");
        }
        if (_viewModel.DenunciaAnonima != null)
        {
            filtros.Add($"[reportante/denuncia_anonima]={_viewModel.DenunciaAnonima}");
        }
        if (_viewModel.CurpReportante != null)
        {
            filtros.Add($"[reportante/curp]={_viewModel.CurpReportante}");
        }
        if (_viewModel.PertenenciaColectivo != null)
        {
            filtros.Add($"[reportante/pertenencia_colectivo]={_viewModel.PertenenciaColectivo}");
        }
        if (_viewModel.RfcReportante != null)
        {
            filtros.Add($"[reportante/rfc]={_viewModel.RfcReportante}");
        }
        if (_viewModel.EdadEstimada != null)
        {
            filtros.Add($"[edad_estimada]={_viewModel.EdadEstimada}");
        }
        if (_viewModel.EstatusEscolaridadReportanteSelectede != null)
        {
            filtros.Add($"[reportante/nivel_escolaridad]={_viewModel.EstatusEscolaridadReportanteSelectede.Nombre}");
        }
        if (_viewModel.NumeroPersonasVive != null)
        {
            filtros.Add($"[reportante/numero_personas_vive]={_viewModel.NumeroPersonasVive}");
        }
        if (_viewModel.LugarNacimientoDesaparecidoSelected != null)
        {
            filtros.Add($"[desaparecido/lugar_nacimiento_id]={_viewModel.LugarNacimientoDesaparecidoSelected.Id}");
        }
        if (_viewModel.EscolaridadDesaparecidoSelected != null)
        {
            filtros.Add($"[desaparecido/escolaridad_id]={_viewModel.EscolaridadDesaparecidoSelected.Id}");
        }
        if (_viewModel.SexoDesaparecidoSelected != null)
        {
            filtros.Add($"[desaparecido/sexo_id]={_viewModel.SexoDesaparecidoSelected.Id}");
        }
        if (_viewModel.GeneroDesaparecidoSelected != null)
        {
            filtros.Add($"[desaparecido/genero_id]={_viewModel.GeneroDesaparecidoSelected.Id}");
        }
        if (_viewModel.NacionalidadDesaparecidoSelected != null)
        {
            filtros.Add($"[desaparecido/nacionalidad_id]={_viewModel.NacionalidadDesaparecidoSelected.Id}");
        }
        if (_viewModel.ReligionDesaparecidoSelected != null)
        {
            filtros.Add($"[desaparecido/religion_id]={_viewModel.ReligionDesaparecidoSelected.Id}");
        }
        if (_viewModel.LenguaDesaparecidoSelected != null)
        {
            filtros.Add($"[desaparecido/lengua_id]={_viewModel.LenguaDesaparecidoSelected.Id}");
        }
        if (_viewModel.FechaNacimientoDesaparecido != null)
        {
            filtros.Add($"[desaparecido/fecha_nacimiento]={_viewModel.FechaNacimientoDesaparecido}");
        }
        if (_viewModel.FechaNacimientoAproximadaDesaparecido != null)
        {
            filtros.Add($"[desaparecido/fecha_nacimiento_aproximada]={_viewModel.FechaNacimientoAproximadaDesaparecido}");
        }
        if (_viewModel.FechaNacimientoCebvDesaparecido != null)
        {
            filtros.Add($"[desaparecido/fecha_nacimiento_cebv]={_viewModel.FechaNacimientoCebvDesaparecido}");
        }
        if (_viewModel.DescripcionOcupacionPrincipal != null)
        {
            filtros.Add($"[desaparecido/descripcion_ocupacion_principal]={_viewModel.DescripcionOcupacionPrincipal}");
        }
        if (_viewModel.DescripcionOcupacionSecundaria != null)
        {
            filtros.Add($"[desaparecido/descripcion_ocupacion_secundaria]={_viewModel.DescripcionOcupacionSecundaria}");
        }
        if (_viewModel.NoTelefonoDesaparecido != null)
        {
            filtros.Add($"[desaparecido/telefono]={_viewModel.NoTelefonoDesaparecido}");
        }
        if (_viewModel.CompañiaTelefonicaDesaparecidoSelected != null)
        {
            filtros.Add($"[desaparecido/telefono/compania_id]={_viewModel.CompañiaTelefonicaDesaparecidoSelected.Id}");
        }
        if (_viewModel.EsMovilDesaparecido != null)
        {
            filtros.Add($"[desaparecido/telefono/es_movil]={_viewModel.EsMovilDesaparecido}");
        }
        if (_viewModel.EdadAnos != null)
        {
            filtros.Add($"[desaparecido/edad_anos]={_viewModel.EdadAnos}");
        }
        if (_viewModel.EdadMeses != null)
        {
            filtros.Add($"[desaparecido/edad_meses]={_viewModel.EdadMeses}");
        }
        if (_viewModel.EdadDias != null)
        {
            filtros.Add($"[desaparecido/edad_dias]={_viewModel.EdadDias}");
        }
        if (_viewModel.HablaEspañol != null)
        {
            filtros.Add($"[desaparecido/habla_espanhol]={_viewModel.HablaEspañol}");
        }
        if (_viewModel.SabeLeer != null)
        {
            filtros.Add($"[desaparecido/sabe_leer]={_viewModel.SabeLeer}");
        }
        if (_viewModel.SabeEscribir != null)
        {
            filtros.Add($"[desaparecido/sabe_escribir]={_viewModel.SabeEscribir}");
        }
        if (_viewModel.EstatusRpdno != null)
        {
            filtros.Add($"[estatus_rpdno_id]={_viewModel.EstatusRpdno.Id}");
        }
        if (_viewModel.EstatusCebv != null)
        {
            filtros.Add($"[estatus_cebv_id]={_viewModel.EstatusCebv.Id}");
        }
        if (_viewModel.OcupacionPrincipal != null)
        {
            filtros.Add($"[desaparecido/ocupacion_id]={_viewModel.OcupacionPrincipal.Id}");
        }
        if (_viewModel.TipoOcupacionPrincipalDesaparecidoSelected != null)
        {
            filtros.Add($"[desaparecido/tipo_ocupacion_id]={_viewModel.TipoOcupacionPrincipalDesaparecidoSelected.Id}");
        }
        if (_viewModel.OcupacionSecundaria != null)
        {
            filtros.Add($"[desaparecido/ocupacion_secundaria_id]={_viewModel.OcupacionSecundaria.Id}");
        }
        if (_viewModel.TipoOcupacionSecundariaDesaparecidoSelected != null)
        {
            filtros.Add($"[desaparecido/tipo_ocupacion_secundaria_id]={_viewModel.TipoOcupacionSecundariaDesaparecidoSelected.Id}");
        }
        if (_viewModel.AccionUrgente != null)
        {
            filtros.Add($"[desaparecido/accion_urgente]={_viewModel.AccionUrgente}");
        }
        if (_viewModel.DeclaracionEspecialAusencia != null)
        {
            filtros.Add($"[desaparecido/declaracion_especial_ausencia]={_viewModel.DeclaracionEspecialAusencia}");
        }
        if (_viewModel.Dictamen != null)
        {
            filtros.Add($"[desaparecido/dictamen]={_viewModel.Dictamen}");
        }
        if (_viewModel.CiNivelFederal != null)
        {
            filtros.Add($"[desaparecido/ci_nivel_federal]={_viewModel.CiNivelFederal}");
        }
        if (_viewModel.EstatusEscolaridadDesaparecidoSelected != null)
        {
            filtros.Add($"[desaparecido/nivel_escolaridad]={_viewModel.EstatusEscolaridadDesaparecidoSelected.Nombre}");
        }
        if (_viewModel.CurpDesaparecido != null)
        {
            filtros.Add($"[desaparecido/curp]={_viewModel.CurpDesaparecido}");
        }
        if (_viewModel.RfcDesaparecido != null)
        {
            filtros.Add($"[desaparecido/rfc]={_viewModel.RfcDesaparecido}");
        }
        if (_viewModel.OtroDerechoHumano != null)
        {
            filtros.Add($"[desaparecido/otro_derecho_humano]={_viewModel.OtroDerechoHumano}");
        }
        if (_viewModel.IdentidadResguardada != null)
        {
            filtros.Add($"[desaparecido/identidad_resguardada]={_viewModel.IdentidadResguardada}");
        }
        if (_viewModel.ClasificacionPersona != null)
        {
            filtros.Add($"[desaparecido/clasificacion_persona]={_viewModel.ClasificacionPersona}");
        }
        if (_viewModel.NombreParejaConyugal != null)
        {
            filtros.Add($"[desaparecido/nombre_pareja_conyugue]={_viewModel.NombreParejaConyugal}");
        }
        if (_viewModel.UrlBoletin != null)
        {
            filtros.Add($"[desaparecido/url_boletin]={_viewModel.UrlBoletin}");
        }
        if (_viewModel.BoletinImgPath != null)
        {
            filtros.Add($"[desaparecido/boletin_img_path]={_viewModel.BoletinImgPath}");
        }
        if (_viewModel.FolioCebv != null)
        {
            filtros.Add($"[folio_cebv]={_viewModel.FolioCebv}");
        }
        if (_viewModel.FolioFub != null)
        {
            filtros.Add($"[folio_fub]={_viewModel.FolioFub}");
        }
        if (_viewModel.FechaDesaparicion != null)
        {
            filtros.Add($"[hechoDesaparicion/fecha_desaparicion]={_viewModel.FechaDesaparicion}");
        }
        if (_viewModel.FechaDesaparicionAproximada != null)
        {
            filtros.Add($"[hechoDesaparicion/fecha_desaparicion_aproximada]={_viewModel.FechaDesaparicionAproximada}");
        }
        if (_viewModel.FechaDesaparicionCebv != null)
        {
            filtros.Add($"[hechoDesaparicion/fecha_desaparicion_cebv]={_viewModel.FechaDesaparicionCebv}");
        }
        if (_viewModel.HoraDesaparicion != null)
        {
            filtros.Add($"[hechoDesaparicion/hora_desaparicion]={_viewModel.HoraDesaparicion}");
        }
        if (_viewModel.HechosDesaparicion != null)
        {
            filtros.Add($"[hechoDesaparicion/hechos_desaparicion]={_viewModel.HechosDesaparicion}");
        }
        if (_viewModel.FechaPercato != null)
        {
            filtros.Add($"[hechoDesaparicion/fecha_percato]={_viewModel.FechaPercato}");
        }
        if (_viewModel.FechaPercatoCebv != null)
        {
            filtros.Add($"[hechoDesaparicion/fecha_percato_cebv]={_viewModel.FechaPercatoCebv}");
        }
        if (_viewModel.HoraPercato != null)
        {
            filtros.Add($"[hechoDesaparicion/hora_percato]={_viewModel.HoraPercato}");
        }
        if (_viewModel.AmenazaCambioComportamiento != null)
        {
            filtros.Add($"[hechoDesaparicion/amenaza_cambio_comportamiento]={_viewModel.AmenazaCambioComportamiento}");
        }
        if (_viewModel.DescripcionAmenazaCambioComportamiento != null)
        {
            filtros.Add($"[hechoDesaparicion/descripcion/amenaza_cambio_comportamiento]={_viewModel.DescripcionAmenazaCambioComportamiento}");
        }
        if (_viewModel.SituacionPrevia != null)
        {
            filtros.Add($"[hechoDesaparicion/situacion_previa]={_viewModel.SituacionPrevia}");
        }
        if (_viewModel.AclaracionesFechasHechos != null)
        {
            filtros.Add($"[hechoDesaparicion/aclaraciones_fechas_hechos]={_viewModel.AclaracionesFechasHechos}");
        }
        if (_viewModel.HechoDesaparicionInformacionRelevante != null)
        {
            filtros.Add($"[hechoDesaparicion/informacion_relevante]={_viewModel.HechoDesaparicionInformacionRelevante}");
        }
        if (_viewModel.SintesisDesaparicion != null)
        {
            filtros.Add($"[hechoDesaparicion/sintesis_desaparicion]={_viewModel.SintesisDesaparicion}");
        }
        if (_viewModel.PersonaMismoEvento != null)
        {
            filtros.Add($"[hechoDesaparicion/personas_mismo_evento]={_viewModel.PersonaMismoEvento}");
        }
        if (_viewModel.ContadorDesapariciones != null)
        {
            filtros.Add($"[hechoDesaparicion/contador_desapariciones]={_viewModel.ContadorDesapariciones}");
        }
        if (_viewModel.HipotesisOficialSelected != null)
        {
            filtros.Add($"[hipotesis_oficial_id]={_viewModel.HipotesisOficialSelected.Id}");
        }
        if (_viewModel.HipotesisOficialCircunstanciaSelected != null)
        {
            filtros.Add($"[hipotesis_oficial_circunstancia_id]={_viewModel.HipotesisOficialCircunstanciaSelected.Id}");
        }
        if (_viewModel.HipotesisOficialDescripcion != null)
        {
            filtros.Add($"[hipotesis_oficial_descripcion]={_viewModel.HipotesisOficialDescripcion}");
        }
        if (_viewModel.Estatura != null)
        {
            filtros.Add($"[desaparecido/media_filiacion/estatura]={_viewModel.Estatura}");
        }
        if (_viewModel.Peso != null)
        {
            filtros.Add($"[desaparecido/media_filiacion/peso]={_viewModel.Peso}");
        }
        if (_viewModel.ComplexionSelected != null)
        {
            filtros.Add($"[desaparecido/media_filiacion/complexion_id]={_viewModel.ComplexionSelected.Id}");
        }
        if (_viewModel.ColorPielSelected != null)
        {
            filtros.Add($"[desaparecido/media_filiacion/color_piel_id]={_viewModel.ColorPielSelected.Id}");
        }
        if (_viewModel.ColorOjosSelected != null)
        {
            filtros.Add($"[desaparecido/media_filiacion/color_ojos_id]={_viewModel.ColorOjosSelected.Id}");
        }
        if (_viewModel.ColorCabelloSelected != null)
        {
            filtros.Add($"[desaparecido/media_filiacion/color_cabello_id]={_viewModel.ColorCabelloSelected.Id}");
        }
        if (_viewModel.TamanioCabelloSelected != null)
        {
            filtros.Add($"[desaparecido/media_filiacion/tamano_cabello_id]={_viewModel.TamanioCabelloSelected.Id}");
        }
        if (_viewModel.TipoCabelloSelected != null)
        {
            filtros.Add($"[desaparecido/media_filiacion/tipo_cabello_id]={_viewModel.TipoCabelloSelected.Id}");
        }
        if (_viewModel.RegionCuerpoSelected != null)
        {
            filtros.Add($"[desaparecido/senas_particulares/region_cuerpo_id]={_viewModel.RegionCuerpoSelected.Id}");
        }
        if (_viewModel.ColorRegionCuerpo != null)
        {
            filtros.Add($"[desaparecido/senas_particulares/region_cuerpo/color]={_viewModel.ColorRegionCuerpo}");
        }
        if (_viewModel.VistaSelected != null)
        {
            filtros.Add($"[desaparecido/senas_particulares/vista_id]={_viewModel.VistaSelected.Id}");
        }
        if (_viewModel.LadoSelected != null)
        {
            filtros.Add($"[desaparecido/senas_particulares/lado_id]={_viewModel.LadoSelected.Id}");
        }
        if (_viewModel.ColorLado != null)
        {
            filtros.Add($"[desaparecido/senas_particulares/lado/color]={_viewModel.ColorLado}");
        }
        if (_viewModel.TipoSelected != null)
        {
            filtros.Add($"[desaparecido/senas_particulares/tipo_id]={_viewModel.TipoSelected.Id}");
        }
        if (_viewModel.Cantidad != null)
        {
            filtros.Add($"[desaparecido/senas_particulares/cantidad]={_viewModel.Cantidad}");
        }
        if (_viewModel.Descripcion != null)
        {
            filtros.Add($"[desaparecido/senas_particulares/descripcion]={_viewModel.Descripcion}");
        }
        
        return string.Join("&filter", filtros);
    }
}