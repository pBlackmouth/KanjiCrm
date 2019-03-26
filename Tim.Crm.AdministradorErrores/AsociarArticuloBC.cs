using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Tim.Crm.Base.Entidades;
using Tim.Crm.Base.Entidades.Enumeraciones;
using Tim.Crm.Base.Entidades.Excepciones;
using Tim.Crm.Base.Entidades.Interfases;
using Tim.Crm.Base.Entidades.Sistema;
using Tim.Crm.Base.Logica;

namespace Tim.Crm.AdministradorErrores
{
    /// <summary>
    /// 
    /// </summary>
    public class AsociarArticuloBC : IRegistroError
    {
        ///Gris
        /// <summary>
        /// Propiedad utilizada para acceder a  los métodos de la clase de  registro de errores.
        /// </summary>
        private RegistroError registrar = null;

        /// <summary>
        /// 
        /// </summary>
        private AdaptadorCLN Adaptador = null;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Adaptador"></param>
        public AsociarArticuloBC(AdaptadorCLN Adaptador)
        {
            this.Adaptador = Adaptador;
            registrar = new RegistroError();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Evento"></param>
        /// <param name="ClavePlantilla"></param>
        /// <returns></returns>
        public Respuesta GuardarArticuloBC(EventoSistema Evento, String ClavePlantilla)
        {
            Respuesta resp = new Respuesta();

            MensajeError mensaje = null;
            Tema asunto = null;
            ArticuloBC articulo = null;

            PlantillaArticuloBC plantilla = null;

            try
            {
                //Valida que el Evento no sea nulo.
                if (Evento != null)
                {
                    //Valida que el mensaje de error no sea nulo.
                    if (Evento.MensajeError != null)
                    {
                        //Busca un articulo de KB donde ya se haya asociado este MensajeError
                        articulo = ObtenerArticuloBC(Evento.MensajeError.ID.Value);

                        //Si el articulo es diferente de nulo es que si existe articulo y solo se asocia.
                        if (articulo != null)
                        {
                            //Instancia de Actualización de Evento de sistema.
                            EventoSistema evento = new EventoSistema()
                            {
                                ID = Evento.ID,
                                ArticuloBC = new CrmLookup(articulo.ID.Value)
                            };

                            //Se actualiza el Evento de Sistema y se asocia el Articulo.
                            Adaptador.Guardar(evento);
                        }
                        else // De lo contrario se genera uno nuevo y después se asocia.
                        {
                            //Se busca el mensaje de error asociado al Evento de sistema.
                            mensaje = ObtenerMensajeError(Evento.MensajeError.ID.Value);

                            //Si se encontró el MensajeError, se busca el Asunto asociado.
                            if (mensaje != null && mensaje.Asunto != null)
                            {
                                asunto = ObtenerTema(mensaje.Asunto.ID.Value);

                                //Busca la plantilla de Articulo.
                                plantilla = ObtenerPlantillaArticuloBCPorClave(ClavePlantilla);

                                if(plantilla != null)
                                {
                                    String estructuraXML = plantilla.EstructuraXML;
                                    int noSecciones = ObtenerNumeroSeccionesEstructuraXML(estructuraXML);

                                    if(noSecciones > 0)
                                    {
                                        StringBuilder sb = new StringBuilder();

                                        for(int i = 0; i < noSecciones; i++)
                                        {
                                            sb.AppendFormat(Constantes.XMLPlantillaArticuloBC_Nodos, i);
                                        }

                                        string XMLArticulo = sb.ToString();

                                        if (!string.IsNullOrEmpty(XMLArticulo))
                                        {
                                            ArticuloBC art = new ArticuloBC()
                                            {
                                                Titulo = String.Format("{0} - {1}", string.IsNullOrEmpty(mensaje.CodigoError) ? "0000" : mensaje.CodigoError, mensaje.Descripcion),
                                                Asunto = new CrmLookup(asunto.ID.Value),
                                                XMLArticulo = String.Format(Constantes.XMLPlantillaArticuloBC_Principal, XMLArticulo),
                                                PlantillaArticulo = new CrmLookup(plantilla.ID.Value),
                                                MensajeError = new CrmLookup(mensaje.ID.Value),
                                                Idioma = plantilla.Idioma
                                            };

                                            resp = new Respuesta();
                                            resp = Adaptador.Guardar(art);

                                            if(resp.Completado.Value && resp.ID != null)
                                            {

                                                //Instancia de Actualización de Evento de sistema.
                                                EventoSistema evento = new EventoSistema()
                                                {
                                                    ID = Evento.ID,
                                                    ArticuloBC = new CrmLookup(resp.ID.Value)
                                                };

                                                //Se actualiza el Evento de Sistema y se asocia el Articulo.
                                                Adaptador.Guardar(evento);
                                            }

                                        }
                                     
                                    }

                                }
                            }                            
                        }
                    }

                }
            }
            catch(Exception ex)
            {
                AsociarArticuloBCException tex = new AsociarArticuloBCException(ex);
                registrar.Error(tex);
            }

            return resp;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MensajeErrorID"></param>
        /// <returns></returns>
        private MensajeError ObtenerMensajeError(Guid MensajeErrorID)
        {
            
            MensajeError item = null;

            try
            {
                MensajeError elem = new MensajeError()
                {
                    ID = MensajeErrorID
                };

                item = Adaptador.ObtenerElemento<MensajeError>(elem);
            }
            catch (Exception ex)
            {
                AsociarArticuloBCException tex = new AsociarArticuloBCException(ex);
                registrar.Error(tex);
            }

            return item;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TemaID"></param>
        /// <returns></returns>
        private Tema ObtenerTema(Guid TemaID)
        {
            Tema item = null;

            try
            {
                Tema elem = new Tema()
                {
                    ID = TemaID
                };

                item = Adaptador.ObtenerElemento<Tema>(elem);
            }
            catch (Exception ex)
            {
                AsociarArticuloBCException tex = new AsociarArticuloBCException(ex);
                registrar.Error(tex);
            }

            return item;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MensajeErrorID">Identificador del mensaje de error.</param>
        /// <returns>Articulo de la base del conocimiento</returns>
        private ArticuloBC ObtenerArticuloBC(Guid MensajeErrorID)
        {
            ArticuloBC item = null;
            List<ArticuloBC> lista = null;

            try
            {
                ArticuloBC elem = new ArticuloBC()
                {
                    MensajeError = new CrmLookup(MensajeErrorID)
                };

                lista = Adaptador.ObtenerListado<ArticuloBC>(elem);


                if (lista != null && lista.Count > 0)
                {
                    item = lista[0];
                }
            }
            catch (Exception ex)
            {
                AsociarArticuloBCException tex = new AsociarArticuloBCException(ex);
                registrar.Error(tex);
            }

            return item;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Clave"></param>
        /// <returns></returns>
        private PlantillaArticuloBC ObtenerPlantillaArticuloBCPorClave(String Clave)
        {
            PlantillaArticuloBC item = null;
            List<PlantillaArticuloBC> lista = null;

            try
            {
                lista = Adaptador.ObtenerListado<PlantillaArticuloBC>();

                if (lista != null && lista.Count > 0)
                {
                    item = lista.Find(p => p.Titulo.Contains(Clave));
                }
            }
            catch (Exception ex)
            {
                AsociarArticuloBCException tex = new AsociarArticuloBCException(ex);
                registrar.Error(tex);
            }

            return item;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public int ObtenerNumeroSeccionesEstructuraXML(String xml)
        {
            int secciones = 0;

            XmlDocument docXml = new XmlDocument();
            docXml.LoadXml(xml);

            var nodes = docXml.SelectNodes("/kbarticle/sections/section[@type='edit']");

            if (nodes != null)
                secciones = nodes.Count;

            foreach(XmlNode xn in nodes)
            {
                string texto = xn.InnerText;
            }

            return secciones;
        }


        ///Gris
        /// <summary>
        /// Método para registrar un error con mensaje.
        /// </summary>
        /// <param name="Mensaje">Mensaje del error</param>
        /// <param name="Ex">Excepción generada.</param>
        /// <param name="EventoID">El identificador del evento.</param>
        /// <param name="NumeroLinea">Númeo de línea donde se genero el error.</param>
        /// <param name="Metodo">Nombre del método donde se genero el error.</param>
        /// <param name="ArchivoOrigen">Nombre del archivo donde se generó el error.</param>
        public void RegistrarError(string Mensaje, Exception Ex, int EventoID = 1000,
            [CallerLineNumber] int NumeroLinea = 0,
            [CallerMemberName] string Metodo = "",
            [CallerFilePath] string ArchivoOrigen = "")
        {
            registrar.Error(Mensaje, Ex);
        }


        ///Gris
        /// <summary>
        /// Método utilizado para regsitar un error.
        /// </summary>
        /// <param name="Ex">Excepción generada.</param>
        /// <param name="EventoID">Identificador del evento.</param>
        /// <param name="NumeroLinea">Línea en la cúal ocurrió el error.</param>
        /// <param name="Metodo">Nombre del método en donde ocurrió el error.</param>
        /// <param name="ArchivoOrigen">Nombre del archivo de origen de donde proviene el error.</param>
        public void RegistrarError(Exception Ex,
            int EventoID = 1000,
            [CallerLineNumber] int NumeroLinea = 0,
            [CallerMemberName] string Metodo = "",
            [CallerFilePath] string ArchivoOrigen = "")
        {
            registrar.Error(Ex);
        }
    }
}
