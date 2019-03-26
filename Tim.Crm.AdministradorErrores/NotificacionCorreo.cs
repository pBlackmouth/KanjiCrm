using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using Tim.Crm.Base.Entidades;
using Tim.Crm.Base.Entidades.Excepciones;
using Tim.Crm.Base.Entidades.Interfases;
using Tim.Crm.Base.Entidades.Sistema;
using Tim.Crm.Base.Logica;

namespace Tim.Crm.AdministradorErrores
{
    /// <summary>
    /// 
    /// </summary>
    public class NotificacionCorreo : IRegistroError
    {
        ///Gris
        /// <summary>
        /// Propiedad que se utiliza para tener acceso a los métodos de la clase de registro de errores.
        /// </summary>
        private RegistroError registrar = null;


        ///Gris
        /// <summary>
        /// Propiedad utilizada para tener acceso a los métodos de la clase de TIMException.
        /// </summary>
        private TIMException Ex = null;
        

        ///Gris
        /// <summary>
        /// Propiedad que permite el acceso a los métodos y propiedades de la entidad de eventos del sistema de CRM.
        /// </summary>
        private EventoSistema Evento = null;
        
        /// <summary>
        /// 
        /// </summary>
        private AdaptadorCLN Adaptador = null;

        /// <summary>
        /// 
        /// </summary>
        private List<String> ListaMarcadores = new List<string>() { "[NUMEROERROR]", "[MENSAJEERROR]", "[REFERENTEA]", "[DESCRIPCION]", "[TIMSTAK]", "[STAK]" };


        ///Gris
        /// <summary>
        /// Método utilizado para obetner el evento del sistema con el id enviado como parámetro.
        /// </summary>
        /// <param name="EventoSistemaID">Identificador del evento del sistema.</param>
        /// <param name="Ex">TIMException</param>
        /// <param name="Adaptador">AdaptadorCLN</param>
        public NotificacionCorreo(Guid EventoSistemaID, TIMException Ex, AdaptadorCLN Adaptador)
        {
            this.Ex = Ex;
            this.Adaptador = Adaptador;

            registrar = new RegistroError();

            EventoSistema ev = new EventoSistema() { ID = EventoSistemaID };
            Evento = Adaptador.ObtenerElemento<EventoSistema>(ev);
        }


        ///Gris
        /// <summary>
        /// Método utilizado  para enviar un correo electrónico por cada alerta de error encontrada.
        /// </summary>
        /// <returns>Respuesta si fue completado el envió de correo electrónico.</returns>
        public Respuesta EnviarErrores()
        {
            Respuesta resp = new Respuesta();
            List<Respuesta> respuestas = new List<Respuesta>();

            PlantillaCorreoEventoSistema plantilla = null;
            String Asunto = String.Empty;
            String Descripcion = String.Empty;

            try
            {

                List<AlertaError> alertas = AlertarErrores();

                foreach (AlertaError alerta in alertas)
                {
                    if (alerta.PlantillaDeCorreo != null)
                    {
                        plantilla = new PlantillaCorreoEventoSistema()
                        {
                            ID = alerta.PlantillaDeCorreo.ID
                        };

                        plantilla = Adaptador.ObtenerElemento<PlantillaCorreoEventoSistema>(plantilla);
                    }

                    if (alerta.Usuario != null)
                    {
                        if (plantilla != null)
                        {
                            Asunto = ReemplazarMarcadores(plantilla.Asunto);
                            Descripcion = ReemplazarMarcadores(plantilla.Descripcion);
                        }
                        else
                        {
                            Asunto = Ex.Message;
                            Descripcion = string.Format("{0}\r\n\f\r\n\f{1}[No hay plantilla definida para este Error]", Ex.Descripcion);
                        }



                        CrmActivityParty de = new CrmActivityParty();
                        de.Agregar(alerta.Propietario);

                        CrmActivityParty para = new CrmActivityParty();
                        para.Agregar(alerta.Usuario);

                        CorreoElectronico correo = new CorreoElectronico();
                        correo.De = de;
                        correo.Para = para;
                        correo.Asunto = Asunto;
                        correo.Descripcion = WebUtility.HtmlEncode(Descripcion);

                        Respuesta localResp = Adaptador.Guardar(correo);
                        Respuesta envioResp = new Respuesta();

                        if (localResp.Completado.Value)
                        {
                            envioResp = Adaptador.Util.EnviarCorreoElectronico(localResp.ID.Value);
                        }

                        respuestas.Add(localResp);
                        respuestas.Add(envioResp);

                        foreach (Respuesta r in respuestas)
                        {
                            if (r.Completado.Value == false)
                            {
                                resp.Error = string.Format("{0}\r\n", r.Error);
                            }
                        }

                        if (resp.Error == "")
                            resp.Completado = true;

                    }

                }
            }
            catch (TIMException ex)
            {
                NotificacionCorreoException tex = new NotificacionCorreoException(ex);
                RegistrarError(tex);
            }
            catch (Exception ex)
            {
                NotificacionCorreoException tex = new NotificacionCorreoException(ex);
                RegistrarError(tex);
            }

            return resp;
        }


        ///Gris
        /// <summary>
        /// Método que obtiene las alertas de error con Nombre Todos,  y con el identificador del mensaje de error del evento. 
        /// </summary>
        /// <returns>Lista de alertas de error que cumplen los criterios.</returns>
        private List<AlertaError> AlertarErrores()
        {
            List<AlertaError> lista = null;
            List<AlertaError> listaTemp = null;

            try
            {

                //Instancia de búsqueda para traer todas las AlertasError 
                //donde el nombre sea igual a TODOS
                AlertaError alerta = new AlertaError()
                {
                    Nombre = "TODOS"
                };

                //Se hace la consulta
                listaTemp = Adaptador.ObtenerListado<AlertaError>(alerta);


                //Se verifica que se hayan obtenido Alertas
                if (listaTemp != null)
                {
                    //Se verifica que la lista no sea nula
                    if (lista == null)
                    {
                        lista = new List<AlertaError>();
                    }

                    //Se recorren las alertas obtenidas
                    foreach (AlertaError alert in listaTemp)
                    {
                        //Si la alerta está Activa, se toma en cuenta para enviar el correo
                        if (alert.Activo.Value)
                        {
                            lista.Add(alert);
                        }
                    }
                }


                //Se verifica que el evento de sistema no sea nulo
                if (Evento != null)
                {
                    //Se verifica que haya relacionado un mensaje de error en el evento de sistema.
                    if (Evento.MensajeError != null)
                    {

                        MensajeError mensaje = new MensajeError()
                        {
                            ID = Evento.MensajeError.ID
                        };

                        mensaje = Adaptador.ObtenerElemento<MensajeError>(mensaje);

                        if (mensaje != null)
                        {

                            if (mensaje.EnviarNotificacion.Value)
                            {
                                //Instancia de búsqueda para traer todas las AlertasError 
                                //que contengan ese MensajeError relacionado.
                                alerta = new AlertaError()
                                {
                                    MensajeError = Evento.MensajeError
                                };

                                //Se hace la consulta.
                                listaTemp = Adaptador.ObtenerListado<AlertaError>(alerta);

                                //Se verifica que se hayan obtenido resultados.
                                if (listaTemp != null)
                                {
                                    //Se verifica que la lista no sea nula
                                    if (lista == null)
                                    {
                                        lista = new List<AlertaError>();
                                    }

                                    //Se recorren las alertas obtenidas.
                                    foreach (AlertaError alert in listaTemp)
                                    {
                                        //Si la alerta está Activa, se toma en cuenta para enviar el correo
                                        if (alert.Activo.Value)
                                        {
                                            lista.Add(alert);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

            }
            catch (TIMException ex)
            {
                NotificacionCorreoException tex = new NotificacionCorreoException(ex);
                RegistrarError(tex);
            }
            catch (Exception ex)
            {
                NotificacionCorreoException tex = new NotificacionCorreoException(ex);
                RegistrarError(tex);
            }


            return lista;
        }      

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Texto"></param>
        /// <returns></returns>
        private String ReemplazarMarcadores(String Texto)
        {
            String strRespuesta = Texto;

            try
            {

                if (Evento != null)
                {
                    foreach (String marcador in ListaMarcadores)
                    {
                        bool contieneMarcador = strRespuesta.Contains(marcador);

                        while (contieneMarcador)
                        {
                            switch (marcador)
                            {
                                case "[NUMEROERROR]":
                                    strRespuesta = strRespuesta.Replace(marcador, Evento.MensajeError == null ? "[N/A]" : Evento.MensajeError.Nombre == null ? "[N/A]" : Evento.MensajeError.Nombre);
                                    break;
                                case "[MENSAJEERROR]":
                                    strRespuesta = strRespuesta.Replace(marcador, Ex.ErrorOrigen == null ? "[N/A]\r\n\r\n" : Ex.ErrorOrigen.Message == null ? "[N/A]\r\n\r\n" : Ex.ErrorOrigen.Message + "\r\n");
                                    break;
                                case "[REFERENTEA]":
                                    strRespuesta = strRespuesta.Replace(marcador, Evento.ReferenteA == null ? "[N/A]\r\n\r\n" : Evento.ReferenteA + "\r\n");
                                    break;
                                case "[DESCRIPCION]":
                                    strRespuesta = strRespuesta.Replace(marcador, Evento.Descripcion == null ? "[N/A]\r\n\r\n" : Evento.Descripcion + "\r\n");
                                    break;
                                case "[TIMSTAK]":
                                    strRespuesta = strRespuesta.Replace(marcador, Evento.TIMStack == null ? "[N/A]\r\n\r\n" : Evento.TIMStack + "\r\n");
                                    break;
                                case "[STAK]":
                                    strRespuesta = strRespuesta.Replace(marcador, Evento.Stack == null ? "[N/A]\r\n\r\n" : Evento.Stack + "\r\n");
                                    break;
                                default:
                                    break;
                            }
                            contieneMarcador = strRespuesta.Contains(marcador);
                        }
                    }
                }
                else
                {

                    foreach (String marcador in ListaMarcadores)
                    {
                        bool contieneMarcador = strRespuesta.Contains(marcador);

                        while (contieneMarcador)
                        {
                            switch (marcador)
                            {
                                case "[NUMEROERROR]":
                                    strRespuesta = strRespuesta.Replace(marcador, Ex.ErrorOrigen == null ? "[N/A]" : Ex.ErrorOrigen.HResult.ToString() + "\r\n");
                                    break;
                                case "[MENSAJEERROR]":
                                    strRespuesta = strRespuesta.Replace(marcador, Ex.ErrorOrigen == null ? "[N/A]\r\n\r\n" : string.IsNullOrEmpty(Ex.ErrorOrigen.Message) ? "[N/A]\r\n\r\n" : Ex.ErrorOrigen.Message + "\r\n");
                                    break;
                                case "[REFERENTEA]":
                                    strRespuesta = strRespuesta.Replace(marcador, string.IsNullOrEmpty(Ex.Message) ? "[N/A]\r\n\r\n" : Ex.Message + "\r\n");
                                    break;
                                case "[DESCRIPCION]":
                                    strRespuesta = strRespuesta.Replace(marcador, string.IsNullOrEmpty(Ex.Detalle) ? "[N/A]\r\n\r\n" : Ex.Detalle + "\r\n");
                                    break;
                                case "[TIMSTAK]":
                                    strRespuesta = strRespuesta.Replace(marcador, string.Format("{0} \r\n {1}\r\n\r\n", string.IsNullOrEmpty(Ex.PilaDeDescripciones) ? "[N/A]\r\n\r\n" : Ex.PilaDeDescripciones, string.IsNullOrEmpty(Ex.PilaDeMetodos) ? "[N/A]\r\n\r\n" : Ex.PilaDeMetodos));
                                    break;
                                case "[STAK]":
                                    strRespuesta = strRespuesta.Replace(marcador, string.IsNullOrEmpty(Ex.StackTrace) ? "[N/A]\r\n\r\n" : Ex.StackTrace + "\r\n");
                                    break;
                                default:
                                    break;
                            }
                            contieneMarcador = strRespuesta.Contains(marcador);
                        }
                    }
                }
            }
            catch (TIMException ex)
            {
                NotificacionCorreoException tex = new NotificacionCorreoException(ex);
                RegistrarError(tex);
            }
            catch (Exception ex)
            {
                NotificacionCorreoException tex = new NotificacionCorreoException(ex);
                RegistrarError(tex);
            }

            return strRespuesta;
        }



        ///Gris
        /// <summary>
        /// Comprueba si la cadena contiene los caracteres especificados.
        /// </summary>
        /// <param name="cadena">Cadena a analizar.</param>
        /// <returns>Valor boleano que indica si se contiene XML o no.</returns>
        private Boolean ContieneXML(String cadena)
        {
            Boolean valor = false;
            System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex(@"<([^>]+)>[^<]*</(\1)>");
            valor = r.IsMatch(cadena);
            return valor;
        }


        ///Gris
        /// <summary>
        /// Remueve los marcadores de XML de la cadena.
        /// </summary>
        /// <param name="cadena">String a analizar.</param>
        /// <returns>texto string sin caracteres xml.</returns>
        private String RemoverXML(String cadena)
        {
            string texto = cadena;
            StringBuilder sb = new StringBuilder();
            string primeraParte = null;
            string segundaParte = null;

            if(ContieneXML(cadena))
            {
                int indiceInicio = cadena.IndexOf("<");
                int indiceFin = cadena.LastIndexOf(">");
                int longitud = indiceFin - indiceInicio;

                primeraParte = cadena.Substring(0, indiceInicio);
                segundaParte = cadena.Substring(indiceFin + 1);

                sb.Append(primeraParte);
                sb.Append(segundaParte);

                texto = sb.ToString();
            }

            return texto;
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

        //Gris
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
