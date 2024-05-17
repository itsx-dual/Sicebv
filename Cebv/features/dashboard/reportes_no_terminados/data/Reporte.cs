namespace Cebv.features.dashboard.reportes_no_terminados.data;

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Datum
    {
        public int id { get; set; }
        public bool esta_terminado { get; set; }
        public TipoReporte tipo_reporte { get; set; }
        public int area_atiende_id { get; set; }
        public MedioConocimiento medio_conocimiento { get; set; }
        public Estado estado { get; set; }
        public int zona_estado_id { get; set; }
        public int hipotesis_oficial_id { get; set; }
        public string tipo_desaparicion { get; set; }
        public object fecha_localizacion { get; set; }
        public object sintesis_localizacion { get; set; }
        public List<Reportante> reportantes { get; set; }
        public List<Desaparecido> desaparecidos { get; set; }
        public DateTime fecha_creacion { get; set; }
        public DateTime fecha_actualizacion { get; set; }
    }

    public class Desaparecido
    {
        public int id { get; set; }
        public int reporte_id { get; set; }
        public Persona persona { get; set; }
        public EstatusRpdno estatus_rpdno { get; set; }
        public EstatusCebv estatus_cebv { get; set; }
        public string clasificacion_persona { get; set; }
        public bool habla_espanhol { get; set; }
        public bool sabe_leer { get; set; }
        public bool sabe_escribir { get; set; }
        public string url_boletin { get; set; }
        public bool declaracion_especial_ausencia { get; set; }
        public bool accion_urgente { get; set; }
        public bool dictamen { get; set; }
        public bool ci_nivel_federal { get; set; }
        public object otro_derecho_humano { get; set; }
        public object folio_cebv { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }

    public class Estado
    {
        public string id { get; set; }
        public string nombre { get; set; }
        public string abreviatura_inegi { get; set; }
        public string abreviatura_cebv { get; set; }
    }

    public class EstatusCebv
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string abreviatura { get; set; }
    }

    public class EstatusRpdno
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string abreviatura { get; set; }
    }

    public class Genero
    {
        public int id { get; set; }
        public string nombre { get; set; }
    }

    public class Link
    {
        public string url { get; set; }
        public string label { get; set; }
        public bool active { get; set; }
        public string first { get; set; }
        public string last { get; set; }
        public object prev { get; set; }
        public object next { get; set; }
    }

    public class MedioConocimiento
    {
        public int id { get; set; }
        public TipoMedio tipo_medio { get; set; }
        public string nombre { get; set; }
    }

    public class Meta
    {
        public int current_page { get; set; }
        public int from { get; set; }
        public int last_page { get; set; }
        public List<Link> links { get; set; }
        public string path { get; set; }
        public int per_page { get; set; }
        public int to { get; set; }
        public int total { get; set; }
    }

    public class Nacionalidade
    {
        public int id { get; set; }
        public string nombre { get; set; }
    }

    public class Persona
    {
        public int id { get; set; }
        public string lugar_nacimiento_id { get; set; }
        public string nombre { get; set; }
        public string apellido_paterno { get; set; }
        public string apellido_materno { get; set; }
        public string pseudonimo_nombre { get; set; }
        public string pseudonimo_apellido_paterno { get; set; }
        public string pseudonimo_apellido_materno { get; set; }
        public DateTime fecha_nacimiento { get; set; }
        public string curp { get; set; }
        public string observaciones_curp { get; set; }
        public string rfc { get; set; }
        public string ocupacion { get; set; }
        public object sexo { get; set; }
        public Genero genero { get; set; }
        public List<object> apodos { get; set; }
        public List<object> nacionalidades { get; set; }
    }

    public class Reportante
    {
        public int id { get; set; }
        public int reporte_id { get; set; }
        public Persona persona { get; set; }
        public int parentesco_id { get; set; }
        public bool denuncia_anonima { get; set; }
        public bool informacion_consentimiento { get; set; }
        public bool informacion_exclusiva_busqueda { get; set; }
        public bool publicacion_registro_nacional { get; set; }
        public bool publicacion_boletin { get; set; }
        public bool pertenencia_colectivo { get; set; }
        public string nombre_colectivo { get; set; }
        public string informacion_relevante { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }

    public class Reporte
    {
        public List<Datum> data { get; set; }
        //public Links links { get; set; }
        public Meta meta { get; set; }
    }

    public class TipoMedio
    {
        public int id { get; set; }
        public string nombre { get; set; }
    }

    public class TipoReporte
    {
        public int id { get; set; }
        public string nombre { get; set; }
    }