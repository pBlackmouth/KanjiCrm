using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tim.Crm.Base.Logica;
using Tim.Crm.Base.Entidades;
using Tim.Crm.Base.Entidades.Enumeraciones;
using Tim.Crm.Base.Entidades.Excepciones;
using Tim.Crm.Base.Entidades.Interfases;
using Tim.Crm.Base.Entidades.Sistema;
using System.Net;

namespace Tim.Crm.AdministradorErrores
{
    ///Gris
    /// <summary>
    /// Delegado para menejo de reporte de error.
    /// </summary>
    /// <param name="Ex"></param>
    public delegate void ReportarErrorDelegado(Exception Ex);

    /// <summary>
    /// 
    /// </summary>
    public static class Observador
    {
        /// <summary>
        /// 
        /// </summary>
        public static event ReportarErrorDelegado EventoError;

        /// <summary>
        /// 
        /// </summary>
        private static IObservador claseObservador;

        /// <summary>
        /// 
        /// </summary>
        private static Tema asuntoRelacionado = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ClaseObservador"></param>
        public static void RegistrarObservador(IObservador ClaseObservador)
        {
            claseObservador = ClaseObservador;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="adaptador"></param>
        /// <param name="Ex"></param>
        /// <param name="tipo"></param>
        /// <returns></returns>
        public static bool GuardarError(this AdaptadorCLN adaptador, TIMException Ex, eTipoEventoSistema tipo = eTipoEventoSistema.Error)
        {
            bool Completado = false;
            Respuesta resp = new Respuesta();

            try
            {
                EventoSistema evento = ObtenerEventoSistema(Ex, adaptador);

                if (evento != null)
                {
                    evento.TipoEventoSistema = new CrmPicklist() { ID = (int)tipo };
                    resp = adaptador.Guardar(evento);

                    if(resp.Completado.Value && resp.ID != null)
                    {
                        evento.ID = resp.ID;

                        AsociarArticuloBC asociar = new AsociarArticuloBC(adaptador);
                        asociar.GuardarArticuloBC(evento, Constantes.ClavePlantillaSolucionProblemasTIM);
                    }

                    if (!(Ex is EnviarCorreoEventoSistemaException))
                    {
                        if (resp.Completado.Value)
                        {
                            NotificacionCorreo notificar = new NotificacionCorreo(resp.ID.Value, Ex, adaptador);
                            resp = notificar.EnviarErrores();

                            if (resp.Error != String.Empty)
                                throw new EnviarCorreoEventoSistemaException(resp.Error);
                        }
                    }

                    if (claseObservador != null)
                    {
                        EventoError += new ReportarErrorDelegado(claseObservador.ReportarError);
                    }
                }
            }
            catch (TIMException tex)
            {
                adaptador.ReintentarGuardarError(tex);
            }
            catch (Exception ex)
            {
                EnviarCorreoEventoSistemaException tex = new EnviarCorreoEventoSistemaException(ex);
                adaptador.ReintentarGuardarError(tex);
            }

            return Completado;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="adaptador"></param>
        /// <param name="Ex"></param>
        /// <returns></returns>
        private static bool ReintentarGuardarError(this AdaptadorCLN adaptador, TIMException Ex )
        {
            bool Completado = false;
            Respuesta resp = new Respuesta();

            try
            {
                EventoSistema evento = ObtenerEventoSistema(Ex, adaptador);
                if (evento != null)
                {
                    resp = adaptador.Guardar(evento);

                    if (resp.Completado.Value && resp.ID != null)
                    {
                        evento.ID = resp.ID;
                        AsociarArticuloBC asociar = new AsociarArticuloBC(adaptador);
                        asociar.GuardarArticuloBC(evento, Constantes.ClavePlantillaSolucionProblemasTIM);
                    }

                    if (!(Ex is EnviarCorreoEventoSistemaException))
                    {
                        if (resp.Completado.Value)
                        {
                            NotificacionCorreo notificar = new NotificacionCorreo(resp.ID.Value, Ex, adaptador);
                            resp = notificar.EnviarErrores();

                            if (resp.Error != String.Empty)
                                throw new EnviarCorreoEventoSistemaException(resp.Error);
                        }
                    }

                    if (claseObservador != null)
                    {
                        EventoError += new ReportarErrorDelegado(claseObservador.ReportarError);
                    }

                }
            }
            catch (Exception ex)
            {
                //TODO: Implementar registro de errores en almacenamiento local. (BD, Archivos).
                GuardarErrorLocalmente(ex, false);
            }

            return Completado;
        }

        /// <summary>
        /// Crea un objeto de tipo EventoSistema a partir de un objeto Exception.
        /// </summary>
        /// <param name="Ex">Excepción de la cual se genera el evento de sistema.</param>
        /// <param name="adaptador">Provee la funcionalidad necesaria para obtener y guardar información en CRM.</param>
        private static EventoSistema ObtenerEventoSistema(TIMException Ex, AdaptadorCLN adaptador)
        {
            //TODO: Implementar registro de erroresCRM. (EventosSistema, Temas, MensajesAdmin).
            EventoSistema item = null;

            if (Ex != null)
            {
                item = new EventoSistema();
                Ex.ObtenerInfoEventoSistema(ref item, adaptador);
                item.TIMStack = string.Format("{0} \r\n {1}", Ex.PilaDeDescripciones, Ex.PilaDeMetodos);
                item.Stack = Ex.StackTrace;
                item.Fecha = DateTime.Now;
            }

            return item;
        }

        /// <summary>
        /// Crea un objeto de tipo EventoSistema a partir de un objeto Exception.
        /// </summary>
        /// <param name="Ex">Excepción de la cual se genera el evento de sistema.</param>
        /// <param name="evento">Parámetro de salida, que devuelve el Evento de sistema generado.</param>
        /// <param name="adaptador">Provee la funcionalidad necesaria para obtener y guardar información en CRM.</param>
        private static void ObtenerInfoEventoSistema(this Exception Ex, ref EventoSistema evento, AdaptadorCLN adaptador)
        {
            Respuesta resp = new Respuesta();


            if (adaptador != null)
            {

                try
                {
                    if (Ex is TIMException && !((TIMException)Ex).EsExcepcionMensajeError)
                    {

                        //La profundidad 1 (Categoría) es la excepción que se tomará para el Tema y la profundidad 0 será el MensajeError
                        if (((TIMException)Ex).Profundidad == 1 || ((TIMException)Ex).Profundidad == 0)
                        {

                            //Se convierte la excepción obtenida desde los parámetros a una de tipo TIMException.
                            TIMException tex = ((TIMException)Ex);

                            //Se obtiene la información relacionada al origen del error, tomado como categoría del error.
                            evento.Metodo = tex.Metodo;
                            evento.Archivo = tex.ArchivoOrigen;
                            evento.ReferenteA = Ex.Message;
                            evento.Descripcion = String.Format("{0} - {1}", tex.Message, tex.Detalle);
                            
                            


                            //En este tema se guarda la categoría del error.
                            Tema asunto = new Tema()
                            {
                                Titulo = string.Format("{0} - {1}", tex.EventoID.ToString(), Ex.Message),
                                Descripcion = Ex.Message
                            };

                            //Se busca que exista este tema en CRM.
                            asuntoRelacionado = ObtenerAsunto(asunto, adaptador);

                            //Si no existe el asunto, se crea.
                            if (asuntoRelacionado == null)
                            {
                                //Se obtiene el tema raíz, que actua como agrupador.
                                Tema temaRaiz = ObtenerTemaRaiz(adaptador);

                                //Solo si se obtiene el tema raíz se crea el asunto (Categoría)
                                if (temaRaiz != null)
                                {
                                    //Se obtiene el ID del tema padre.
                                    asunto.TemaPrimario = new CrmLookup() { ID = temaRaiz.ID };

                                    //Se guarda el asunto.
                                    resp = adaptador.Guardar(asunto);

                                    //Si se guardó correctamente, se obtiene el ID del asunto para asociarlo al Evento del sistema.
                                    if (resp.Completado.Value)
                                    {
                                        asuntoRelacionado = asunto;
                                        asuntoRelacionado.ID = resp.ID;
                                    }
                                    else
                                    {
                                        throw new RegistroErrorIncompletoException(string.Format("No fué posible guardar el Tema {0} para el registro de error.", asunto.Descripcion));
                                    }
                                }
                                else
                                {
                                    throw new RegistroErrorIncompletoException(string.Format("No fué posible guardar el Tema {0} para el registro de error.", asunto.Descripcion));
                                }
                            }

                        }

                        //Profundida != 0 son todos los errores consecuencia del error origen.
                        if (((TIMException)Ex).Profundidad != 0)
                        {
                            ObtenerInfoEventoSistema(Ex.InnerException, ref evento, adaptador);
                        }
                        else
                        {
                            ((TIMException)Ex).EsExcepcionMensajeError = true;
                            ObtenerInfoEventoSistema(Ex, ref evento, adaptador);
                        }
                    }
                    else
                    {   //Esta sección es relacionado a la profundidad 0, que es el origen del error.

                        MensajeError msgError = null;

                        if (asuntoRelacionado != null)
                        {

                            //Este es el mensaje de error que se guardará.
                            msgError = new MensajeError()
                            {
                                CodigoError = Ex.HResult.ToString(),
                                Descripcion = Ex.Message,
                                Asunto = new CrmLookup() { ID = asuntoRelacionado.ID }

                            };


                            //Se busca que este mensaje de error ya se haya creado.
                            MensajeError mensajeRelacionado = ObtenerMensajeError(msgError, adaptador);

                            //Si el mensaje de error no existe, se crea.
                            if (mensajeRelacionado == null)
                            {

                                msgError.Asunto = new CrmLookup() { ID = asuntoRelacionado.ID };
                                msgError.EnviarNotificacion = false;
                                resp = adaptador.Guardar(msgError);

                                if (resp.Completado.Value)
                                {
                                    evento.MensajeError = new CrmLookup() { ID = resp.ID };
                                }
                                else
                                {
                                    throw new RegistroErrorIncompletoException("No fué posible guardar el MensajeError para el registro de error.");
                                }
                            }
                            else
                            {
                                evento.MensajeError = new CrmLookup() { ID = mensajeRelacionado.ID };

                            }
                        }
                        else
                        {
                            throw new RegistroErrorIncompletoException("No fué posible guardar el MensajeError para el registro de error.");
                        }


                    }
                }
                catch (TIMException ex)
                {
                    adaptador.GuardarError(ex);
                }
            }
            else
            {
                if (Ex is ConexionCRMException)
                { //Esta sección es exclusiva para cuando la falla es la conexión de CRM y se requiere guardar el error localmente en disco.
                    ConexionCRMException tex = ((ConexionCRMException)Ex);
                    //Se obtiene la información relacionada al origen del error, tomado como categoría del error.
                    evento.Metodo = tex.Metodo;
                    evento.Archivo = tex.ArchivoOrigen;
                    evento.ReferenteA = Ex.Message;
                    evento.TIMStack = string.Format("{0} \r\n {1}", tex.PilaDeDescripciones, tex.PilaDeMetodos);
                    evento.Stack = tex.StackTrace;
                    evento.Descripcion = String.Format("{0} - {1}", tex.Descripcion, tex.Message);

                }
                else
                {
                    GuardarErrorLocalmente(new Exception("El adaptador para la conexión a CRM es nulo"), true);
                }
            }


        }


        ///Gris
        /// <summary>
        /// Almacena en  la base de datos local el error.
        /// </summary>
        /// <param name="Ex"></param>
        /// <param name="AdaptadorNuloCausaDesconocida"></param>
        /// <returns>Respuesta de proceso completado o no.</returns>
        private static Respuesta GuardarErrorLocalmente(Exception Ex, bool AdaptadorNuloCausaDesconocida)
        {
            //TODO: Implementar registro de errores en almacenamiento local. (BD, Archivos).
            Respuesta resp = new Respuesta();
            ConexionCRMException tex = new ConexionCRMException("Error al tratar de guardar el error en CRM.", Ex);
            EventoSistema evento = null;
            if (!AdaptadorNuloCausaDesconocida)
            {
                evento = ObtenerEventoSistema(tex, null);
            }

            if (claseObservador != null)
            {
                EventoError += new ReportarErrorDelegado(claseObservador.ReportarError);
            }

            return resp;
        }



        ///Gris
        /// <summary>
        /// Obtiene el asunto de uan lista de asuntos del adaptador.
        /// </summary>
        /// <param name="asunto"></param>
        /// <param name="adaptador"></param>
        /// <returns></returns>
        private static Tema ObtenerAsunto(Tema asunto, AdaptadorCLN adaptador)
        {
            Tema item = null;
            List<Tema> asuntos = adaptador.ObtenerListado<Tema>(asunto);

            if (asuntos != null && asuntos.Count > 0)
            {
                item = asuntos[0];
            }

            return item;

        }


        ///Gris
        /// <summary>
        /// Obtiene una instancia  del mensaje de error.
        /// </summary>
        /// <param name="msgError"></param>
        /// <param name="adaptador"></param>
        /// <returns>Mensaje de error</returns>
        private static MensajeError ObtenerMensajeError(MensajeError msgError, AdaptadorCLN adaptador)
        {
            MensajeError item = null;
            List<MensajeError> mensajes = adaptador.ObtenerListado<MensajeError>(msgError);

            if (mensajes != null && mensajes.Count > 0)
            {
                item = mensajes[0];
            }

            return item;

        }


        ///Gris
        /// <summary>
        /// Método que obtiene el tema raiz de mensajes de error generales. 
        /// </summary>
        /// <param name="adaptador"></param>
        /// <returns>Tema</returns>
        private static Tema ObtenerTemaRaiz(AdaptadorCLN adaptador)
        {
            Tema item = null;
            Respuesta resp = new Respuesta();

            Tema asunto = new Tema()
            {
                Titulo = "Mensajes de error generales",
                Descripcion = "Contiene todos los mensajes de error generales para las implementaciones hechas por TI-M"
            };

            List<Tema> asuntos = adaptador.ObtenerListado<Tema>(asunto);

            if (asuntos != null && asuntos.Count > 0)
            {
                item = asuntos[0];
            }
            else
            {
                resp = adaptador.Guardar(asunto);

                if (resp.Completado.Value)
                {
                    asunto.ID = resp.ID;
                    item = asunto;
                }
            }

            return item;
        }






    }
}
