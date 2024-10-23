using System.Collections.ObjectModel;
using System.Reflection;
using Cebv.core.modules.persona.data;
using Cebv.core.util.reporte.viewmodels;
using Cebv.features.formulario_cebv.circunstancias_desaparicion.presentation;
using Cebv.features.formulario_cebv.condiciones_vulnerabilidad.presentation;
using Cebv.features.formulario_cebv.contexto.data;
using Cebv.features.formulario_cebv.contexto.presentation;
using Cebv.features.formulario_cebv.datos_complementarios.presentation;
using Cebv.features.formulario_cebv.datos_de_localizacion.presentation;
using Cebv.features.formulario_cebv.datos_del_reporte.presentation;
using Cebv.features.formulario_cebv.desaparicion_forzada.presentation;
using Cebv.features.formulario_cebv.media_filiacion_complementaria.presentation;
using Cebv.features.formulario_cebv.persona_desaparecida.presentation;


namespace Cebv.core.util;

public class ListEmptyElements
{
    public static List<string> EnlistarElementosVacios(object objeto)
    {
        PropertyInfo[] properties = objeto.GetType().GetProperties();
        var emptyElements = new List<string>();

        foreach (var property in properties)
        {
            if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() 
                == typeof(ObservableCollection<>)) continue;
            
            var value = property.GetValue(objeto);
            if (value is null || (value is string str && string.IsNullOrEmpty(str)))
            {
                emptyElements.Add(property.Name);
            }
        }

        return emptyElements;
    }
    
    public static Dictionary<string, object?> GetCondicionesVulneravilidad(Desaparecido desaparecido, CondicionesVulnerabilidadViewModel condicionnes)
    {
        return new Dictionary<string, object?>
        {
            {"Tipo sanguineo", desaparecido.Persona.Salud?.TipoSangre},
            {"Factor Rhesus", desaparecido.Persona.Salud?.FactorRhesus},
            {"Condicion", condicionnes.CondicionSalud},
            {"Indole", condicionnes.IndoleSalud},
            {"Tratamiento", condicionnes.Tratamiento},
            {"Observaciones", condicionnes.Observaciones},
            {"En transito a EUA", desaparecido.Persona.ContextoSocial?.EstaTransitoEstadosUnidos},
            {"Situacion migratoria", desaparecido.Persona.ContextoSocial?.SituacionMigratoria},
            {"Descripcion proceso migratorio", desaparecido.Persona.ContextoSocial?.DescripcionProcesoMigratorio},
            {"Pertenencia grupal o etnica", desaparecido.Persona.EnfoqueDiferenciado?.PertenenciaGrupalEtnica},
            {"Enfoque diferenciado", condicionnes.EnfoqueDiferenciado},
            {"Descripcion vuneralidad", desaparecido.Persona.EnfoqueDiferenciado?.DescripcionVulnerabilidad},
            {"Informacion relevante busqueda", desaparecido.Persona.EnfoqueDiferenciado?.InformacionRelevanteBusqueda},
            {"¿Esta embarazada?", desaparecido.Persona.Embarazo?.EstaEmbarazada},
            {"Meses de embarazo", desaparecido.Persona.Embarazo?.Meses}
        };
    }

    public static Dictionary<string, object?> GetCircunstanciaDesaparicion(Reporte reporte, CircunstanciaDesaparicionViewModel circunstancia,
    Hipotesis? hipotesisPrimaria, Hipotesis? hipotesisSecundaria)
    {
        return new Dictionary<string, object?>
        {
            { "Fecha desaparicion", reporte.HechosDesaparicion?.FechaDesaparicion },
            {"Hora desaparicion", reporte.HechosDesaparicion?.HoraDesaparicion},
            {"Fecha percato", reporte.HechosDesaparicion?.FechaPercato},
            {"Hora percato", reporte.HechosDesaparicion?.HoraPercato},
            {"Aclaracion de la fecha y hpra de los hechos", reporte.HechosDesaparicion?.AclaracionesFechaHechos},
            {"Fecha desaparicion aproximada", reporte.HechosDesaparicion?.FechaDesaparicionCebv},
            {"Fecha de percato aproximada", reporte.HechosDesaparicion?.FechaPercatoCebv},
            {"Tipo domicilio", circunstancia.TiposDomicilio},
            {"Calle", reporte.HechosDesaparicion?.Direccion.Calle},
            {"Numero exterior", reporte.HechosDesaparicion?.Direccion.NumeroExterior},
            {"Numero interior", reporte.HechosDesaparicion?.Direccion.NumeroInterior},
            {"Colonia", reporte.HechosDesaparicion?.Direccion.Colonia},
            {"Codigo postal", reporte.HechosDesaparicion?.Direccion.CodigoPostal},
            {"Estado", circunstancia.EstadoSelected},
            {"Municipio", circunstancia.MunicipioSelected},
            {"Localidad", reporte.HechosDesaparicion?.Direccion.Asentamiento},
            {"Entre calle 1", reporte.HechosDesaparicion?.Direccion.Calle1},
            {"Entre calle 2", reporte.HechosDesaparicion?.Direccion.Calle2},
            {"Tramo carretero", reporte.HechosDesaparicion?.Direccion.TramoCarretero},
            {"Referencia", reporte.HechosDesaparicion?.Direccion.Referencia},
            {"Amenaza - Cambio comportamiento", reporte.HechosDesaparicion?.AmenazaCambioComportamiento},
            {"Descripcion de la situacion de amenaza", reporte.HechosDesaparicion?.DescripcionAmenazaCambioComportamiento},
            {"Contador desapariciones", reporte.HechosDesaparicion?.ContadorDesapariciones},
            {"Situacion previa", reporte.HechosDesaparicion?.SituacionPrevia},
            {"Folios previos", circunstancia.Folios},
            {"Informacion relevante", reporte.HechosDesaparicion?.InformacionRelevante},
            {"Descripcion hechos desaparicion", reporte.HechosDesaparicion?.HechosDesaparicionNarracion},
            {"Sintesis desaparicion", reporte.HechosDesaparicion?.SintesisDesaparicion},
            {"Posible hipotesis desaparicion 1", hipotesisPrimaria?.TipoHipotesis},
            {"Circunstancia inicial 1", hipotesisPrimaria?.TipoHipotesis?.Circunstancia?.Id},
            {"Posible hipotesis desaparicion 2", hipotesisSecundaria?.TipoHipotesis},
            {"Circunstancia inicial 2", hipotesisSecundaria?.TipoHipotesis?.Circunstancia?.Id},
            {"Sitio inicial", reporte.HechosDesaparicion?.Sitio},
            {"Area que codifica", reporte.HechosDesaparicion?.AreaAsignaSitio},
            {"¿Desaparecio acompañado?", reporte.HechosDesaparicion?.DesaparecioAcompanado},
            {"Personas en el mismo evento", reporte.HechosDesaparicion?.PersonasMismoEvento},
            {"Expedientes", reporte.Expedientes.Count}
        };
    }

    public static Dictionary<string, object?> GetContexto(Reporte reporte, Familiar familiar, Desaparecido desaparecido, 
        ContextoViewModel contexto, Amistad amistad)
    {
        return new Dictionary<string, object?>
        {
            {"Numero de personas con las que vive", reporte.Desaparecidos[0].Persona.ContextoFamiliar?.NumeroPersonasVive },
            {"Nombre pariente", familiar.Nombre},
            {"Parentesco", familiar.Parentesco},
            {"¿Ha ejercido violencia?", familiar.HaEjercidoViolencia},
            {"¿Es un familair cercano?", familiar.EsFamiliarCercano},
            {"Observaciones", familiar.Observaciones},
            {"¿Donde trabaja?", desaparecido.Persona.ContextoEconomico?.DondeTrabaja},
            {"Años laborando", desaparecido.Persona.ContextoEconomico?.AntiguedadTrabajo},
            {"¿Le gusta su trabajo?", desaparecido.Persona.ContextoEconomico?.GustaTrabajo},
            {"¿Ha manifestado intención de trabajar fuera de la ciudad?", desaparecido.Persona.ContextoEconomico?.DeseaTrabajoForaneo},
            {"A donde", desaparecido.Persona.ContextoEconomico?.UbicacionTrabajoForaneo},
            {"Violencia por parte de algun compañero del trabajo", desaparecido.Persona.ContextoEconomico?.ViolenciaLaboral},
            {"Por parte de quien", desaparecido.Persona.ContextoEconomico?.ViolentadorLaboral},
            {"Deudas", desaparecido.Persona.ContextoEconomico?.TieneDeudas},
            {"Monto aproximado deuda", desaparecido.Persona.ContextoEconomico?.MontoDeuda},
            {"A quien le debe", desaparecido.Persona.ContextoEconomico?.DeudaCon},
            {"Pasatiempo", contexto.Pasatiempos},
            {"Clubes u organiacion a las que pertenece", contexto.Club},
            {"Amistad - Nombew", amistad.Nombre},
            {"Amistad - Apellido paterno", amistad.ApellidoPaterno},
            {"Amistad - Apellido materno", amistad.ApellidoMaterno},
            {"Amistad - Apodo", amistad.Apodo},
            {"Amistad - Telefono", amistad.Telefono},
            {"Amistad - Tipo red social", amistad.TipoRedSocial},
            {"Amistad - Red social", amistad.UsuarioRedSocial},
            {"Amistad - Observaciones red social", amistad.ObservacionesRedSocial}
        };
    }

    public static Dictionary<string, object?> GetControlOgpi(Reporte reporte)
    {
        return new Dictionary<string, object?>
        { 
            {"Fecha codificacion", reporte.ControlOgpi?.FechaCodificacion},
            {"Nombre de quien codifico", reporte.ControlOgpi?.NombreCodificador},
            {"Observaciones generales", reporte.ControlOgpi?.Observaciones},
            {"No. Tarjeta OGPI", reporte.ControlOgpi?.NumeroTarjeta},
            {"Folio FUB", reporte.ControlOgpi?.FolioFub},
            {"Autoridad que ingresa FUB", reporte.ControlOgpi?.AutoridadIngresaFub},
            {"Estatus de persona en RNPDNO", reporte.ControlOgpi?.EstatusRndpno}
        };
    }

    public static Dictionary<string, object?> GetDatosComplementarios(Reporte reporte, DatoComplementarioViewModel datoComplementario)
    {
        return new Dictionary<string, object?>
        {
            { "Calle", reporte.DatoComplementario?.Direccion.Calle },
            { "Calle", reporte.DatoComplementario?.Direccion.Calle },
            { "Numero exterior", reporte.DatoComplementario?.Direccion.NumeroExterior },
            { "Numero interior", reporte.DatoComplementario?.Direccion.NumeroInterior },
            { "Colonia", reporte.DatoComplementario?.Direccion.Colonia },
            { "Codigo postal", reporte.DatoComplementario?.Direccion.CodigoPostal },
            { "Estado", datoComplementario.EstadoSelected },
            { "Municipio", datoComplementario.MunicipioSelected },
            { "Localidad", reporte.DatoComplementario?.Direccion.Asentamiento },
            { "Entre calle 1", reporte.DatoComplementario?.Direccion.Calle1 },
            { "Entre calle 2", reporte.DatoComplementario?.Direccion.Calle2 },
            { "Tramo carretero", reporte.DatoComplementario?.Direccion.TramoCarretero },
            {"Referencia", reporte.DatoComplementario?.Direccion.Referencia}
        };
    }

    public static Dictionary<string, object?> GetDatosLocalizacion(DatosLocalizacionViewModel datosLocalizacion,
    Desaparecido desaparecido, Hipotesis? hipotesisPrimaria, Hipotesis? hipotesisSecundaria)
    {
        return new Dictionary<string, object?>
        {
            {"¿La persona fue localizada con vida", datosLocalizacion.LocalizadoVivo},
            {"Estatus preliminar", desaparecido.EstatusPreliminar},
            {"Fecha de estatus preliminar", desaparecido.FechaEstatusPreliminar},
            {"Fecha localizacion", desaparecido.Localizacion?.FechaLocalizacion},
            {"Estatus formalizado", desaparecido.EstatusFormalizado},
            {"Fecha de formalizacion del estatus", desaparecido.FechaEstatusFormalizado},
            {"Fecha de actualizacion en sistema", desaparecido.FechaCapturaEstatusFormalizado},
            {"Sintesis localizacion", desaparecido.Localizacion?.SintesisLocalizacion},
            {"Descripcion de la condicion psicofisica de lozalizacion de la persona", desaparecido.Localizacion?.DescripcionCondicionPersona},
            {"Fecha del hallazgo", desaparecido.Localizacion?.FechaHallazgo},
            {"Fecha identificacion del cuerpo", desaparecido.Localizacion?.FechaIdentificacionFamiliar},
            {"Descripcion causas fallecimiento", desaparecido.Localizacion?.DescripcionCausasFallecimiento},
            {"Estado", datosLocalizacion.EstadoSelected},
            {"Municipio", desaparecido.Localizacion?.MunicipioLocalizacion},
            {"Hipotesis localizacion 1", hipotesisPrimaria?.TipoHipotesis},
            {"Circunstancia final 1", hipotesisPrimaria?.TipoHipotesis?.Circunstancia?.Id},
            {"Hipotesis localizacion 2", hipotesisSecundaria?.TipoHipotesis},
            {"Circunstancia final 2", hipotesisSecundaria?.TipoHipotesis?.Circunstancia?.Id},
            {"Sitio final", desaparecido.Localizacion?.Sitio},
            {"Area que codifica", desaparecido.Localizacion?.Area},
            {"Hipotesis DEB", desaparecido.Localizacion?.HipotesisDeb},
            {"Observaciones sobre la actualización de estatus", desaparecido.ObservacionesActualizacionEstatus},
        };
    }

    public static Dictionary<string, object?> GetDatosReporte(Reporte reporte, DatosReporteViewModel datosReporte, Reportante reportante)
    {
        return new Dictionary<string, object?>
        {
            {"Fecha de creacion", reporte.FechaCreacion},
            {"Medio conocimineto generico", datosReporte.TipoMedio},
            {"Medio cnocimiento especifico", reporte.MedioConocimiento},
            {"Estado de donde proviene el reporte", reporte.Estado},
            {"Institucion origen", reporte.InstitucionOrigen},
            {"Informacion exclusiva busqueda", reportante.InformacionExclusivaBusqueda},
            {"Publicacion boletin", reportante.PublicacionBoletin},
        };
    }

    public static Dictionary<string, object?> GetDesaparicionForzada(Reporte reporte, DesaparicionForzadaViewModel desaparicionForzada)
    {
        return new Dictionary<string, object?>
        {
            {"¿Sufrio desaparicion forzada?", reporte.DesaparicionForzada?.DesaparecioAutoridad },
            {"Autoridad", reporte.DesaparicionForzada?.Autoridad},
            {"Describa la situacion", reporte.DesaparicionForzada?.DescripcionAutoridad},
            {"Sufrio desaparicion por particulares", reporte.DesaparicionForzada?.DesaparecioParticular},
            {"Particular", reporte.DesaparicionForzada?.Particular},
            {"Describa la situacion", reporte.DesaparicionForzada?.DescripcionParticular},
            {"Método de captura", reporte.DesaparicionForzada?.MetodoCaptura},
            {"Observaciones metodo captura", reporte.DesaparicionForzada?.DescripcionMetodoCaptura},
            {"Medio captura", reporte.DesaparicionForzada?.MedioCaptura},
            {"Observaciones metodo captura", reporte.DesaparicionForzada?.DescripcionMedioCaptura},
            {"Detencion previa o extorsion", reporte.DesaparicionForzada?.DetencionPreviaExtorsion},
            {"Observaciones de la detencion", reporte.DesaparicionForzada?.DescripcionDetencionPreviaExtorsion},
            {"¿Ha sido avistado?", reporte.DesaparicionForzada?.HaSidoAvistado},
            {"¿Donde?", reporte.DesaparicionForzada?.DondeHaSidoAvistado},
            {"Nombre(s) / apodo(s)", desaparicionForzada.Nombre},
            {"Sexo", desaparicionForzada.Sexo},
            {"Ultimo status perpetrador", desaparicionForzada.EstatusPerpetrador},
            {"Descripcion", desaparicionForzada.PerpetradorDescripcion},
            {"Descripcion grupo perpetrador", reporte.DesaparicionForzada?.DescripcionGrupoPerpetrador},
            {"¿Se suscitaron otros delitos antes o después de la desaparición?", reporte.DesaparicionForzada?.DelitosDesaparicion},
            {"Especifique cuales", reporte.DesaparicionForzada?.DescripcionDelitosDesaparicion}
        };
    }

    public static Dictionary<string, object?> GetFolioExpediente(Reporte reporte, Desaparecido desaparecido)
    {
        return new Dictionary<string, object?>
        {
            {"Estado", reporte.Estado?.Nombre},
            {"Municipio", reporte.HechosDesaparicion?.Direccion.Asentamiento?.Municipio?.Nombre},
            {"Numero personas mismo evento", reporte.HechosDesaparicion?.PersonasMismoEvento},
            {"Fecha desaparicion", reporte.HechosDesaparicion?.FechaDesaparicion},
            {"Zona del estado", reporte.ZonaEstado?.Nombre},
            {"Terminación de la entidad (SC, SDF)", reporte.Estado?.AbreviaturaCebv},
            {"Tipo folio", reporte.TipoReporte},
            {"Tipo desaparicion", reporte.TipoDesaparicion},
            {"Area que atendera", reporte.AreaAtiende},
            {"Nombre de quien asigno folio", desaparecido.Folios?.User?.NombreCompleto},
            {"Area a la cambio el expediente", reporte.ExpedienteFisico?.AreaReceptora},
            {"Fecha cambio de area", reporte.ExpedienteFisico?.FechaCambioArea},
            {"Quien solicita el prestamo", reporte.ExpedienteFisico?.SolicitanteExpediente},
            {"Fecha prestamo", reporte.ExpedienteFisico?.FechaPrestamo},
            {"Fecha devolucion", reporte.ExpedienteFisico?.FechaDevolucion},
            {"Observaciones", reporte.ExpedienteFisico?.Observaciones}
        };
    }

    public static Dictionary<string, object?> GetInstrumentoJuridico(DocumentoLegal? carpetaInvestigacion, DocumentoLegal? amparoBuscador, 
        DocumentoLegal? recomedacionDerechos, Desaparecido desaparecido)
    {
        return new Dictionary<string, object?>
        {
            {"Especifica si hay o existe carpeta de investigación (CI).", carpetaInvestigacion?.EsOficial},
            {"Número de CI, IM o reporte FGE", carpetaInvestigacion?.NumeroDocumento},
            {"Dónde radica la CI, IM o reporte FGE", carpetaInvestigacion?.DondeRadica},
            {"Nombre servidor(a) público", carpetaInvestigacion?.NombreServidorPublico},
            {"Fecha de recepción de CI, IM o reporte FGE", carpetaInvestigacion?.FechaRecepcion},
            {"Especifica si hay o existe amparo buscador", amparoBuscador?.EsOficial},
            {"Número de amparo buscador", amparoBuscador?.NumeroDocumento},
            {"Dónde radica el amparo buscador", amparoBuscador?.DondeRadica},
            {"Nombre del juez", amparoBuscador?.NombreServidorPublico},
            {"Fecha recepción", amparoBuscador?.FechaRecepcion},
            {"Especifica si hay o existe recomendación de derechos humanos", recomedacionDerechos?.EsOficial},
            {"Número de recomendación derechos humanos", recomedacionDerechos?.NumeroDocumento},
            {"Dónde radica la recomendación derechos humanos", recomedacionDerechos?.DondeRadica},
            {"Nombre servidor(a) público", recomedacionDerechos?.NombreServidorPublico},
            {"Fecha recepción de recomendación DH", recomedacionDerechos?.FechaRecepcion},
            {"Declaracion especial ausencia", desaparecido.DeclaracionEspecialAusencia},
            {"Accion urgente", desaparecido.AccionUrgente},
            {"Dictamen", desaparecido.Dictamen},
            {"Carpeta de investigacion nivel federal", desaparecido.CiNivelFederal},
            {"Otro derecho humano", desaparecido.OtroDerechoHumano}
        };
    }

    public static Dictionary<string, object?> GetMediaFiliacion(Desaparecido desaparecido)
    {
        return new Dictionary<string, object?>
        {
            { "Estatura", desaparecido.Persona.Salud?.EstaturaCentimetros},
            { "Peso", desaparecido.Persona.Salud?.PesoKilogramos},
            { "Complexion", desaparecido.Persona.Salud?.Complexion},
            { "Color piel", desaparecido.Persona.Salud?.ColorPiel},
            { "Forma cara", desaparecido.Persona.Salud?.FormaCara},
            { "Color ojos", desaparecido.Persona.Ojos?.ColorOjos},
            { "Forma ojos", desaparecido.Persona.Ojos?.FormaOjos},
            { "Tamaño ojos", desaparecido.Persona.Ojos?.TamanoOjos},
            { "Otra especificaciónojos", desaparecido.Persona.Ojos?.EspecificacionesOjos},
            {"Calvicie", desaparecido.Persona.Cabello?.Calvicie},
            { "Color cabello", desaparecido.Persona.Cabello?.ColorCabello},
            {"Tamaño cabello", desaparecido.Persona.Cabello?.TamanoCabello},
            {"Tipo cabello", desaparecido.Persona.Cabello?.TipoCabello},
            {"Cualquier otras especificacion cabello", desaparecido.Persona.Cabello?.EspecificacionesCabello},
            {"Cejas", desaparecido.Persona.VelloFacial?.Cejas},
            {"Cualquier otrsa especificacion cejas", desaparecido.Persona.VelloFacial?.EspecificacionesCejas},
            {"Bigote", desaparecido.Persona.VelloFacial?.TieneBigote},
            {"Cualquier otra especificacion bigote", desaparecido.Persona.VelloFacial?.EspescificacionesBigote},
            {"Barba", desaparecido.Persona.VelloFacial?.TieneBarba},
            {"Cualquier otra especificacion barba", desaparecido.Persona.VelloFacial?.EspecificacionesBarba},
            {"Forma nariz", desaparecido.Persona.Nariz?.TipoNariz},
            {"Cualquier otra especificacion nariz", desaparecido.Persona.Nariz?.EspesificacionesNariz},
            {"Tamaño boca", desaparecido.Persona.Boca?.TamanoBoca},
            {"Tamaño labios", desaparecido.Persona.Boca?.TamanoLabios},
            {"Cualquir otra especificacion boca", desaparecido.Persona.Boca?.EspecificacionesBoca},
            {"Tamaño orejas", desaparecido.Persona.Orejas?.TamanoOrejas},
            {"Forma orejas", desaparecido.Persona.Orejas?.FormaOrejas},
            {"Cualquier otra especificacion orejas", desaparecido.Persona.Orejas?.EspesificacionesOrejas}
        };
    }

    public static Dictionary<string, object?> GetMediaFiliacionComplementaria(Desaparecido desaparecido, 
        MediaFiliacionComplementariaViewModel mediaFiliacionComplementaria)
    {
        return new Dictionary<string, object?>
        {
            {"Ausencia dental", desaparecido.Persona.MediaFiliacionComplementaria?.TieneAusenciaDental},
            {"Especifique ausencia dental", desaparecido.Persona.MediaFiliacionComplementaria?.DescripcionAusenciaDental},
            {"Tratamiento dental", desaparecido.Persona.MediaFiliacionComplementaria?.TieneTratamientoDental},
            {"Especifique tratamiento dental", desaparecido.Persona.MediaFiliacionComplementaria?.DescripcionTratamientoDental},
            {"Tipo menton", desaparecido.Persona.MediaFiliacionComplementaria?.TipoMenton},
            {"Especificacion menton", desaparecido.Persona.MediaFiliacionComplementaria?.EspecificacionesMenton},
            {"Tipo intervencion", mediaFiliacionComplementaria.TipoIntervencion},
            {"Tipo intervencion descripcion", mediaFiliacionComplementaria.TipoIntervencionDescripcion},
            {"Enfermedad piel", mediaFiliacionComplementaria.EnfermedadPiel},
            {"Enfermedad piel descripcion", mediaFiliacionComplementaria.EnfermedadPielDescripcion},
            {"Observaciones", desaparecido.Persona.MediaFiliacionComplementaria?.ObservacionesGenerales}
        };
    }

    public static Dictionary<string, object?> GetDesaparecido(Desaparecido desaparecido,
        DesaparecidoViewModel desaparecidoViewModel, Direccion direccion, Pseudonimo pseudonimo, OcupacionPersona? ocupacionPrincipal,
        OcupacionPersona? ocupacionSecundaria)
    {
        return new Dictionary<string, object?>
        {
            { "Nombre", desaparecido.Persona.Nombre },
            { "Apellido paterno", desaparecido.Persona.ApellidoPaterno },
            { "Apellido materno", desaparecido.Persona.ApellidoMaterno },
            { "Identidad resguardada", desaparecido.IdentidadResguardada },
            { "Pseudónimo(s) nombre(s)", pseudonimo.Nombre },
            { "Pseudónimo(s) primer apellido", pseudonimo.ApellidoPaterno },
            { "Pseudónimo(s) segundo apellido", pseudonimo.ApellidoMaterno },
            { "Apodo", desaparecido.Persona.Apodo },
            { "Sexo", desaparecido.Persona.Sexo },
            { "Genero", desaparecido.Persona.Genero },
            { "Fecha aproximada", desaparecidoViewModel.FechaAproximada },
            { "Fecha nacimiento", desaparecido.Persona.FechaNacimiento },
            { "Edad", desaparecidoViewModel.EdadAnos },
            { "Edad", desaparecidoViewModel.EdadMeses },
            { "Edad", desaparecidoViewModel.EdadDias },
            { "Lugar de nacimiento", desaparecido.Persona.LugarNacimiento },
            { "Nacionalidad", desaparecido.Persona.Nacionalidades[0] },
            { "RFC", desaparecido.Persona.Rfc },
            { "CURP", desaparecido.Persona.Curp },
            { "Observaciones CURP", desaparecido.Persona.ObservacionesCurp },
            { "Mismo domicilio que la perosna reportante", desaparecidoViewModel.EsMismoDomicilioReportante },
            { "Calle", direccion.Calle },
            { "Numero exterior", direccion.NumeroExterior },
            { "Numero interior", direccion.NumeroInterior },
            { "Colonia", direccion.Colonia },
            { "Codigo postal", direccion.CodigoPostal },
            { "Estado", desaparecidoViewModel.EstadoSelected },
            { "Municipio", desaparecidoViewModel.MunicipioSelected },
            { "Localidad", direccion.Asentamiento },
            { "Entre calle 1", direccion.Calle1 },
            { "Entre calle 2", direccion.Calle2 },
            { "Tramo carretero", direccion.TramoCarretero },
            { "Referencia", direccion.Referencia },
            { "Telefono movil", desaparecidoViewModel.NoTelefonoMovil },
            { "Compañia", desaparecidoViewModel.CompaniaTelefonicaSelected },
            { "Observaciones movil", desaparecidoViewModel.ObservacionesMovil },
            {"Telefono fijo", desaparecidoViewModel.NoTelefonoFijo},
            {"ObseRVACIONES", desaparecidoViewModel.ObservacionesFijo},
            {"Correo electronico", desaparecidoViewModel.UsuarioCorreo},
            {"Observaciones", desaparecidoViewModel.ObservacionesCorreo},
            {"Tipo red social", desaparecidoViewModel.TipoRedSocialSelected},
            {"Usuario", desaparecidoViewModel.UsuarioRedSocial},
            {"Observaciones", desaparecidoViewModel.ObservacionesRedSocial},
            {"Descripcion ocupacion principal", ocupacionPrincipal?.Observaciones},
            {"Codigo genereal princiapal", desaparecidoViewModel.TipoOcupacionPrincipal},
            {"Codigo especifico principal", ocupacionPrincipal?.Ocupacion},
            {"Descripcion ocupacion secundaria", ocupacionSecundaria?.Observaciones},
            {"Codigo general secundaria", desaparecidoViewModel.TipoOcupacionSecundaria},
            {"cODIGO ESPECIFICO SECUNDARIA", ocupacionSecundaria?.Ocupacion},
            { "¿Habla español?", desaparecido.Persona.HablaEspanhol },
            { "Nacionalidad", desaparecido.Persona.Estudios?.SabeLeerEscribir },
            { "Religion", desaparecido.Persona.Religion },
            { "Escolaridad", desaparecido.Persona.Estudios?.Escolaridad },
            { "Avance escolaridad", desaparecido.Persona.Estudios?.EstatusEscolaridad },
            { "Otras especificaciones ocupación", desaparecido.Persona.ContextoEconomico?.OtrasEspecificacionesOcupacion },
            { "Estado conyugal", desaparecido.Persona.ContextoFamiliar?.EstadoConyugal },
            { "Estado civil", desaparecido.Persona.ContextoFamiliar?.NombreParejaConyugue },
        };
    }
    

    public static List<string> GetEmptyElements(Dictionary<string, object?> propertyDictionary)
    {
        return propertyDictionary
            .Where(kv => kv.Value is null || string.IsNullOrWhiteSpace(kv.Value.ToString()) || (kv.Value is int intValue && intValue == 0))
            .Select(kv => kv.Key)
            .ToList();
    }
}