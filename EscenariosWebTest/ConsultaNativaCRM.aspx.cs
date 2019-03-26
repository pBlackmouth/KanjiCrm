using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security;
using System.ServiceModel.Description;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tim.Crm.ProveedorServicio;
using Tim.Crm.Base.Entidades;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Discovery;
using Microsoft.Xrm.Sdk.Query;
using PruebasWeb.Modelos;

namespace PruebasWeb
{
    public partial class ConsultaNativaCRM : System.Web.UI.Page
    {
        

        protected void Page_Load(object sender, EventArgs e)
        {
            using (Contexto ctx = new Contexto())
            {
                var service = ctx.ObtenerServicio();

                //Objeto que se utiliza para hacer la consulta.
                QueryExpression query = new QueryExpression();

                //Se define el tipo de entidad, para el query.
                query.EntityName = "account";

                #region COLUMNAS A CONSULTAR

                //Variable que contiene las columnas a ser consultadas.
                ColumnSet cols = new ColumnSet();

                //Ejecuta el delegado especificado para obtener las columnas reuqeridas de consulta.
                cols.AddColumns(new string[] { "accountid", "name", "description", "creditonhold", "creditlimit", "ownerid", "customertypecode", "address1_city", "emailaddress1" });

                //Asigno las columnas requeridas.
                query.ColumnSet = cols;

                #endregion

                #region CONDICIONES
                ConditionExpression condition = new ConditionExpression();
                condition.AttributeName = "address1_city";
                condition.Operator = ConditionOperator.Like;
                condition.Values.Add("%Ovie%");

                ConditionExpression condition2 = new ConditionExpression();
                condition2.AttributeName = "name";
                condition2.Operator = ConditionOperator.Like;
                condition2.Values.Add("%fa%");

                //Obtengo las condiciones de la entidad.
                ConditionExpression[] condiciones = new ConditionExpression[] { condition, condition2 };

                FilterExpression filter = new FilterExpression();

                filter.FilterOperator = LogicalOperator.Or;


                //Agrego las condiciones al filtro.
                filter.Conditions.AddRange(condiciones);

                //Se asigna el filtro a la consulta.
                query.Criteria.AddFilter(filter);


                #endregion

                #region ORDENAMIENTO

                OrderExpression order1 = new OrderExpression();
                OrderExpression order2 = new OrderExpression();

                order1.AttributeName = "name";
                order1.OrderType = OrderType.Ascending;

                order2.AttributeName = "address1_city";
                order2.OrderType = OrderType.Ascending;

                OrderExpression[] ordenamientos = new OrderExpression[] { order1, order2 };

                if (ordenamientos != null)
                {
                    query.Orders.AddRange(ordenamientos);
                }

                #endregion

                #region CONSULTA PAGINADA

                //Implementación de consultas páginadas de más de 5000 registros.
                query.PageInfo = new PagingInfo();

                //Se asigna la cantidad de registros a ser consultados por página.
                query.PageInfo.Count = 5000;

                //Se inicializa la página que será consultada.
                query.PageInfo.PageNumber = 1;

                //para obtener la primera página, pagingCookie debe de ser nulo.
                query.PageInfo.PagingCookie = null;

                #endregion

                // Hace el mapeo de las entidades Entity de CRM a las entidades de TIM.
                EntityCollection listaFinal = new EntityCollection();
                while (true)
                {
                    // Se hace el retrieve.
                    EntityCollection lista = service.RetrieveMultiple(query);



                    // Valido que la lista haya obtenido información.
                    if (lista != null && lista.Entities != null && lista.Entities.Count > 0)
                    {


                        listaFinal.Entities.AddRange(lista.Entities);

                        // Verifica que sea necesario otra consulta para obtener más resultados.
                        if (lista.MoreRecords)
                        {
                            // Incrementa el número de página para obtener la página siguiente.
                            query.PageInfo.PageNumber++;

                            // Asigna a la consulta la Cookie de paginación, devuelta por el resultado actual.
                            query.PageInfo.PagingCookie = lista.PagingCookie;
                        }
                        else
                        {
                            // Si ya no hay más registros que obtener
                            // detiene el ciclo del while.
                            break;
                        }

                    }
                }


                List<Account> cuentas = new List<Account>();

                foreach (Entity en in listaFinal.Entities)
                {
                    Account c = new Account();
                    //"accountid", "name", "description", "creditonhold", 
                    //"creditlimit", "ownerid", "customertypecode", "address1_city", "emailaddress1" 
                    c.CuentaID = (Guid)en["accountid"];
                    c.Nombre = (string)en["name"];
                    c.CuentaConCredito = (bool)en["creditonhold"];
                    c.Propietario = new CrmLookup() { ID = ((EntityReference)en["ownerid"]).Id, Nombre = ((EntityReference)en["ownerid"]).Name };
                    c.TipoDeRelacion = new CrmPicklist() { ID = ((OptionSetValue)en["customertypecode"]).Value };
                    c.DireccionCiudad = (string)en["address1_city"];
                    c.CorreoElectronico = (string)en["emailaddress1"];

                    cuentas.Add(c);
                }

                GridView1.DataSource = cuentas;
                GridView1.DataBind();

            }           
            

        }


    }

    






    public class Account
    {

        public Guid? CuentaID { get; set; }

        public string Nombre { get; set; }

        
        public string Descripcion { get; set; }


        public bool? CuentaConCredito { get; set; }

   
        public Decimal? LimiteCredito { get; set; }

     
        public CrmLookup Propietario { get; set; }


        public CrmPicklist TipoDeRelacion { get; set; }

      
        public string DireccionCiudad { get; set; }

      
        public string CorreoElectronico { get; set; }
        
    }


    public class Configuration : ICloneable
    {
        public String ConfigurationName;
        public String ServerAddress;
        public String OrganizationName;
        public Uri DiscoveryUri;
        public Uri OrganizationUri;
        public Uri HomeRealmUri = null;
        public ClientCredentials DeviceCredentials = null;
        public ClientCredentials Credentials = null;
        public AuthenticationProviderType EndpointType;
        public String UserPrincipalName;


        //Variables de TIM
        public Boolean UseSSL = false;
        public Boolean UseWindowsAuthentication = false;
        public String UserName;
        public SecureString Password;
        //Fin del bloque TIM

        #region internal members of the class
        internal IServiceManagement<IOrganizationService> OrganizationServiceManagement;
        internal SecurityTokenResponse OrganizationTokenResponse;
        internal Int16 AuthFailureCount = 0;
        #endregion

        public override bool Equals(object obj)
        {
            //Check for null and compare run-time types.
            if (obj == null || GetType() != obj.GetType()) return false;

            Configuration c = (Configuration)obj;

            if (!this.ConfigurationName.Equals(c.ConfigurationName, StringComparison.InvariantCultureIgnoreCase))
                return false;
            if (!this.ServerAddress.Equals(c.ServerAddress, StringComparison.InvariantCultureIgnoreCase))
                return false;
            if (!this.OrganizationName.Equals(c.OrganizationName, StringComparison.InvariantCultureIgnoreCase))
                return false;
            if (this.EndpointType != c.EndpointType)
                return false;
            if (null != this.Credentials && null != c.Credentials)
            {
                if (this.EndpointType == AuthenticationProviderType.ActiveDirectory)
                {

                    if (!this.Credentials.Windows.ClientCredential.Domain.Equals(
                        c.Credentials.Windows.ClientCredential.Domain, StringComparison.InvariantCultureIgnoreCase))
                        return false;
                    if (!this.Credentials.Windows.ClientCredential.UserName.Equals(
                        c.Credentials.Windows.ClientCredential.UserName, StringComparison.InvariantCultureIgnoreCase))
                        return false;

                }
                else if (this.EndpointType == AuthenticationProviderType.LiveId)
                {
                    if (!this.Credentials.UserName.UserName.Equals(c.Credentials.UserName.UserName,
                        StringComparison.InvariantCultureIgnoreCase))
                        return false;
                    if (!this.DeviceCredentials.UserName.UserName.Equals(
                        c.DeviceCredentials.UserName.UserName, StringComparison.InvariantCultureIgnoreCase))
                        return false;
                    if (!this.DeviceCredentials.UserName.Password.Equals(
                        c.DeviceCredentials.UserName.Password, StringComparison.InvariantCultureIgnoreCase))
                        return false;
                }
                else
                {

                    if (!this.Credentials.UserName.UserName.Equals(c.Credentials.UserName.UserName,
                        StringComparison.InvariantCultureIgnoreCase))
                        return false;

                }
            }
            return true;
        }

        public override int GetHashCode()
        {
            int returnHashCode = this.ServerAddress.GetHashCode()
                ^ this.OrganizationName.GetHashCode()
                ^ this.EndpointType.GetHashCode();
            if (null != this.Credentials)
            {
                if (this.EndpointType == AuthenticationProviderType.ActiveDirectory)
                    returnHashCode = returnHashCode
                        ^ this.Credentials.Windows.ClientCredential.UserName.GetHashCode()
                        ^ this.Credentials.Windows.ClientCredential.Domain.GetHashCode();
                else if (this.EndpointType == AuthenticationProviderType.LiveId)
                    returnHashCode = returnHashCode
                        ^ this.Credentials.UserName.UserName.GetHashCode()
                        ^ this.DeviceCredentials.UserName.UserName.GetHashCode()
                        ^ this.DeviceCredentials.UserName.Password.GetHashCode();
                else
                    returnHashCode = returnHashCode
                        ^ this.Credentials.UserName.UserName.GetHashCode();
            }
            return returnHashCode;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }




}