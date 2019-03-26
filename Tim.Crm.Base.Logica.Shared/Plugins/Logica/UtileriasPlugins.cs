using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using Tim.Crm.Base.Entidades;
using Tim.Crm.Base.Entidades.Plugins;

namespace Tim.Crm.Base.Logica
{
    public partial class UtileriasPlugins : Acciones
    {
        public UtileriasPlugins(IOrganizationService service)
        {
            this.service = service;
           
        }

        /// <summary>
        /// Obtiene el ID de un mensaje en el que se registra un Step de un Plugin.
        /// </summary>
        /// <param name="NombreMensaje">Nombre de mensaje. Ej: Create, Update, Delete, Etc...</param>
        /// <returns></returns>
        public SdkMensaje ObtenerSdkMensaje(string NombreMensaje)
        {
            SdkMensaje item = null;

            

            try
            {
                SdkMensaje iBusqueda = new SdkMensaje()
                {

                    Nombre = NombreMensaje
                };

                List<SdkMensaje> lista = ObtenerListado<SdkMensaje>(iBusqueda);

                if (lista != null && lista.Count > 0)
                {
                    item = lista[0];
                }
            }
            catch (Exception ex)
            {
                RegistrarError(ex);
            }

            return item;
        }

        public SdkFiltroMensaje ObtenerSdkFiltroMensaje(string NombreEntidad, Guid SdkMensajeId )
        {
            SdkFiltroMensaje item = null;

            

            try
            {
                SdkFiltroMensaje iBusqueda = new SdkFiltroMensaje()
                {
                    NombreEntidad = NombreEntidad,
                    MensajeSdk = new CrmLookup() { ID = SdkMensajeId }
                };

                List<SdkFiltroMensaje> lista = ObtenerListado<SdkFiltroMensaje>(iBusqueda);

                if (lista != null && lista.Count > 0)
                {
                    item = lista[0];
                }
            }
            catch (Exception ex)
            {
                RegistrarError(ex);
            }


            return item;
        }


        public SdkTipoPlugin ObtenerSdkTipoPlugin(MetadataPaso metadata)
        {
            SdkEnsambladoPlugin sep = null;
            SdkTipoPlugin item = null;

            


            try
            {
                SdkEnsambladoPlugin iBusqueda = new SdkEnsambladoPlugin()
                {
                    NombreLibreria = metadata.NombreLibreria
                };

                List<SdkEnsambladoPlugin> listaTemp = ObtenerListado<SdkEnsambladoPlugin>(iBusqueda);
                if (listaTemp != null && listaTemp.Count > 0)
                {
                    sep = listaTemp[0];
                }

                if (sep != null)
                {
                    SdkTipoPlugin iBusquedaTipo = new SdkTipoPlugin()
                    {
                        LibreriaPlugin = new CrmLookup() { ID = sep.ID },
                        NombrePlugin = metadata.NombreEnsamblado
                    };

                    List<SdkTipoPlugin> lista = ObtenerListado<SdkTipoPlugin>(iBusquedaTipo);
                    if (lista != null && lista.Count > 0)
                    {
                        item = lista[0];
                    }
                }
            }
            catch (Exception ex)
            {
                RegistrarError(ex);

            }


            return item;

        }


        public Respuesta RegistrarStep(string NombreEntidad, MetadataPaso step )
        {
            Respuesta resp = new Respuesta();

            try
            {
                SdkMensaje sdkMsg = ObtenerSdkMensaje(step.MensajeSdk.ToString());
                SdkFiltroMensaje sdkFilterMsg = ObtenerSdkFiltroMensaje(NombreEntidad, sdkMsg.ID.Value);
                SdkTipoPlugin sdkTipo = ObtenerSdkTipoPlugin(step);

                if (sdkMsg != null && sdkFilterMsg != null && sdkTipo != null && sdkMsg.ID != null && sdkFilterMsg.ID != null && sdkTipo.ID != null)
                {
                    SdkMensajeRegistroPaso regStep = new SdkMensajeRegistroPaso();
                    regStep.BorrarAutomaticamente = false;
                    regStep.ModoEjecucion = new CrmPicklist((int)step.ModoEjecucion);
                    regStep.Nombre = step.NombrePaso;
                    regStep.ControladorEventoAsociado = new CrmLookup() { ID = sdkTipo.ID, TipoEntidad = sdkTipo.NombreEsquema };
                    regStep.OrdenEjecucion = step.OrdenEjecucion;
                    regStep.MensajeSdk = new CrmLookup() { ID = sdkMsg.ID, TipoEntidad = sdkMsg.NombreEsquema };
                    regStep.FaseEjecucion = new CrmPicklist((int)step.FaseEjecucion);
                    regStep.EjecutarEn = new CrmPicklist((int)step.AmbitoEjecucion);
                    regStep.FiltroMensajeSdk = new CrmLookup() { ID = sdkFilterMsg.ID, TipoEntidad = sdkFilterMsg.NombreEsquema };

                    List<SdkMensajeRegistroPaso> lista = ObtenerListado<SdkMensajeRegistroPaso>(regStep);


                    if (lista == null)
                        resp = Guardar(regStep);
                    else
                    {
                        resp.Completado = false;
                        resp.Error = "El step ya existe.";
                    }
                }
                else
                {
                    resp.Completado = false;
                    resp.Error = "No se encontró información del step.";
                }

            }
            catch (Exception ex)
            {
                RegistrarError(ex);                
            }

            return resp;
        }

    }
}
