using System;
using System.Collections.Generic;
using System.Reflection;
using Tim.Crm.Base.Entidades;
using Microsoft.Xrm.Sdk;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using System.Text;
using System.Xml;

namespace Tim.Crm.Base.Logica
{
    /// <summary>
    /// 
    /// </summary>
    public static class Extensiones
    {

        /// <summary>
        /// 
        /// </summary>
        static Extensiones()
        {
            //AppDomain currentDomain = AppDomain.CurrentDomain;
            //currentDomain.AssemblyResolve += new ResolveEventHandler(Handlers.ResolveAssemblyEventHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="coleccion"></param>
        /// <returns></returns>
        public static List<T> TransformarEnListadoDe<T>(this EntityCollection coleccion) where T : EntidadCrm
        {
            List<T> lista = null;

            if (coleccion != null)
            {
                try
                {
                    Type type = typeof(T);
                    ConstructorInfo ctor = type.GetConstructor(new[] { typeof(Entity) });
                    //ConstructorEntidadCrm delegado = new ConstructorEntidadCrm(ctor.Invoke);

                    if (coleccion != null)
                    {
                        lista = new List<T>();

                        foreach (Entity entidad in coleccion.Entities)
                        {
                            lista.Add((T)ctor.Invoke(new object[] { entidad }));
                        }
                    }
                }
                catch (Exception ex)
                {
                    try
                    {

                        lista = new List<T>();

                        foreach (Entity entidad in coleccion.Entities)
                        {
                            T item = (T)Activator.CreateInstance(typeof(T), new object[] { entidad });
                            lista.Add(item);
                        }
                    }
                    catch (Exception ex2)
                    {
                        //TODO:Implementar error personalizado.

                    }
                }
            }

            return lista;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public static T TransformarEn<T>(this Entity entidad)
        {
            T item = default(T);

            if (entidad != null)
            {
                Type type = typeof(T);
                ConstructorInfo ctor = type.GetConstructor(new[] { typeof(Entity) });
                item = (T)ctor.Invoke(new object[] { entidad });
            }

            return item;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lista"></param>
        /// <returns></returns>
        public static String FormatoJSON<T>(this List<T> lista) where T : EntidadCrm
        {
            String json = null;

            if (lista != null)
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                json = js.Serialize(lista);
            }

            return json;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public static String FormatoJSON<T>(this T item) where T : EntidadCrm
        {
            String json = null;

            if (item != null)
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                json = js.Serialize(item);
            }

            return json;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lista"></param>
        /// <returns></returns>
        public static String FormatoXML<T>(this List<T> lista, bool ConFormato = false) where T : EntidadCrm
        {
            String strXML = null;

            if (lista != null)
            {

                /************************************************************************
                //Se eliminó el uso de System.IO por que no es permitido en los Plugins o WA en Sandbox
                //Usando System.IO
                XmlSerializer SerializerObj = new XmlSerializer(lista.GetType());

                using (StringWriter stringWriter = new StringWriter())
                {
                    //// Create a new file stream to write the serialized object to a file
                    SerializerObj.Serialize(stringWriter, lista);
                    strXML = stringWriter.ToString();
                }
                ************************************************************************/

                XmlSerializer SerializerObj = new XmlSerializer(lista.GetType());

                StringBuilder sb = new StringBuilder();
                XmlWriterSettings xmlSettings = null;

                xmlSettings = new XmlWriterSettings() { Indent = ConFormato, OmitXmlDeclaration = true };

                using (XmlWriter xmlw = XmlWriter.Create(sb, xmlSettings))
                {
                    SerializerObj.Serialize(xmlw, lista);
                    strXML = sb.ToString();
                }

                strXML = strXML.Replace(" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", "").Trim();

            }

            return strXML;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public static String FormatoXML<T>(this T item, bool ConFormato = false) where T : EntidadCrm
        {
            String strXML = null;

            if (item != null)
            {

                /************************************************************************
                //Se eliminó el uso de System.IO por que no es permitido en los Plugins o WA en Sandbox
                //Usando System.IO
                XmlSerializer SerializerObj = new XmlSerializer(item.GetType());

                using (StringWriter stringWriter = new StringWriter())
                {
                    //// Create a new file stream to write the serialized object to a file
                    SerializerObj.Serialize(stringWriter, item);
                    strXML = stringWriter.ToString();
                }
                ************************************************************************/

                XmlSerializer SerializerObj = new XmlSerializer(item.GetType());

                StringBuilder sb = new StringBuilder();
                XmlWriterSettings xmlSettings = null;

                xmlSettings = new XmlWriterSettings() { Indent = ConFormato, OmitXmlDeclaration = true };

                using (XmlWriter xmlw = XmlWriter.Create(sb, xmlSettings))
                {
                    SerializerObj.Serialize(xmlw, item);
                    strXML = sb.ToString();
                }

                strXML = strXML.Replace(" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", "").Trim();

            }

            return strXML;
        }

        public static string JSON(this List<CrmPicklist> lista)
        {
            string json = "[";

            if (lista != null)
            {
                int index = 0;
                foreach (CrmPicklist item in lista)
                {
                    if (index == 0)
                        json += string.Format("{{ \"Id\":\"{0}\",\"Texto\":\"{1}\" }}", item.ID, item.Nombre);
                    else
                        json += string.Format(",{{ \"Id\":\"{0}\",\"Texto\":\"{1}\" }}", item.ID, item.Nombre);
                    index++;
                }
            }

            json += "]";

            return json;
        }





    }
}
