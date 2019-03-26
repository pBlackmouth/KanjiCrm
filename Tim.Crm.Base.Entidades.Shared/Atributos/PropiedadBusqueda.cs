using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tim.Crm.Base.Entidades.Atributos
{
  
    /// <summary>
    /// Create custom attribute to be assigned to class members
    /// </summary>
    [AttributeUsage(AttributeTargets.Class |
        AttributeTargets.Constructor |
        AttributeTargets.Field |
        AttributeTargets.Method |
        AttributeTargets.Property,
        AllowMultiple = true)]
    public class PropiedadBusqueda : System.Attribute
    {
     
        /// <summary>
        /// Campo que indica  si la propuedad es propiedad de búsqueda o no.
        /// </summary>
        public bool _EsPropiedadBusqueda = true;
        

      
        /// <summary>
        /// Constructor de la clase para la propiedad de Búsqueda.
        /// </summary>
        public PropiedadBusqueda()
        {
            
        }


        /// <summary>
        /// Propiedad de la clase que indica si la propiedad es de búsqueda.
        /// Las propiedades de búsqueda sirven para hacer una búsqueda de un campo de tipo EntityReference y no se
        /// conoce el ID de dicho campo, por lo que se utiliza este campo para buscar un registro por un campo distinto al ID.              
        /// </summary>
        /// <example>
        /// El ejemplo muestra como atributo es utilizado en campos que suelen ser utilizados para integraciones con otro sistema donde por ejemplo el otro sistema envia la clave del registro
        /// dado que no cuentan con el ID de Dynamics CRM y después esa clave es pasada a un método para hacer la búsqueda del registro por clave.
        /// Consideremos las clases Contacto y Cuenta.
        ///<code>
        /// [Serializable]
        /// [NombreEsquemaCrm("contact")]
        /// public partial class Contacto : EntidadCrm
        /// {
        ///     
        ///     public Contacto()
        ///     {
        ///         Inicializar();
        ///     }
        /// 
        ///     public Contacto(Entity entity) 
        ///         : base(entity)
        ///     {
        ///     }
        /// 
        ///     private void Inicializar()
        ///     {
        ///         ContactoID = null;
        ///         Nombres = null;
        ///         Apellidos = null;
        ///         NombreCompania = null;
        ///         Compania = null;
        ///     }
        ///     
        ///     [IdentificadorCRM]
        ///     [NombreEsquemaCrm("contactid")]
        ///     public Guid? ContactoID { get; set; }
        ///     
        ///     [NombreEsquemaCrm("firstname")]
        ///     public String Nombres { get; set; }
        ///     
        ///     [NombreEsquemaCrm("lastname")]
        ///     public String Apellidos { get; set; }   
        ///     
        ///     [PropiedadBusqueda]
        ///     public String NombreCompania { get; set; }
        ///     
        ///     [XmlIgnore]
        ///     [JsonIgnore]
        ///     [NombreEsquemaCrm("account")]
        ///     [TipoDatoCrm(eTipoDatoCRM.EntityReference, "parentcustomerid")]
        ///     public CrmLookup Compania { get; set; }
        ///
        /// } 
        /// 
        /// [Serializable]
        /// [NombreEsquemaCrm("account")]
        /// public partial class Cuenta : EntidadCrm
        /// {
        ///    
        ///     public Cuenta()
        ///     {
        ///         Inicializar();
        ///     }
        /// 
        ///     public Cuenta(Entity entity) 
        ///         : base(entity)
        ///     {
        ///     }
        /// 
        ///     private void Inicializar()
        ///     {
        ///         CuentaID = null;
        ///         Nombre = null;
        ///     }
        /// 
        ///     [IdentificadorCRM]
        ///     [NombreEsquemaCrm("accountid")]
        ///     public Guid? CuentaID { get; set; }
        ///     
        ///     [NombreEsquemaCrm("name")]
        ///     public string Nombre { get; set; }
        /// 
        /// }
        ///</code>
        /// Tomando en cuenta la clase Prospecto y suponiendo que estamos trabajando con una integración con algún sistema de terceros,
        /// donde el convenio de comunicación es enviar una cadena de texto serializada en la variable <remarks>ContactoSerializado</remarks>
        /// 
        /// <remarks>ContactoSerializado</remarks> = "&lt;Contacto&gt;&lt;Nombres&gt;Javier&lt;/Nombres&gt;&lt;Apellidos&gt;Alanís&lt;/Apellidos&gt;&lt;NombreCompania&gt;Aerolíneas lejanas (ejemplo)&lt;/NombreCompania&gt;&lt;/Contacto&gt;"
        /// 
        /// Esta variable es pasada como parámetro al método <remarks>GuardarProspecto</remarks>
        /// <code>
        /// [WebService(Namespace = "http://tempuri.org/")]
        /// [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
        /// [System.ComponentModel.ToolboxItem(false)]
        /// public class WsPrueba : System.Web.Services.WebService
        /// {
        /// 
        ///     [WebMethod]
        ///     public string GuardarContacto(string ContactoSerializado)
        ///     {
        ///         using(Contexto ctx = new Contexto(Constantes.ConfiguracionActual))
        ///         {
        ///             //Convierte en objeto la cadena serializada.
        ///             Contacto contacto = new Contacto(ContactoSerializado);
        ///             
        ///             //Hace la búsqueda de la Compañia por nombre.
        ///             if (!string.IsNullOrEmpty(contacto.NombreCompania))
        ///             {
        ///                 contacto.Compania = ObtenerCuentaPorNombre(contacto.NombreCompania, ctx);
        ///             }
        ///             
        ///             ctx.Guardar(contacto);
        ///         }
        ///     }
        ///     
        ///     private Cuenta ObtenerCuentaPorNombre(string NombreCuenta, Contexto ctx)
        ///     {
        ///         Cuenta item = null;
        ///         List&lt;Cuenta&gt; listaTemp = null;
        ///         
        ///         if (!string.IsNullOrEmpty(NombreCuenta))
        ///         {
        ///             Cuenta cuenta = new Cuenta()
        ///             {
        ///                 Nombre = NombreCuenta
        ///             };
        ///             
        ///             listaTemp = ctx.ObtenerListado&lt;Cuenta&gt;(cuenta);
        ///             
        ///             if (listaTemp != null && listaTemp.Count > 0)
        ///             {
        ///                 item = listaTemp[0];
        ///             }                   
        ///         }
        ///         
        ///         return item;
        ///     }
        /// 
        /// }
        /// </code>
        /// </example>
        public bool EsPropiedadBusqueda { get { return _EsPropiedadBusqueda; } }
    }
}
