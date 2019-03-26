using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tim.Crm.Base.Entidades.Excepciones;

namespace Tim.Crm.Base.Entidades
{
    /// <summary>
    /// 
    /// </summary>
    public class CrmActivityParty
    {

        /// <summary>
        /// 
        /// </summary>
        public CrmActivityParty()
        {
            Referencias = null;
        }

        /// <summary>
        /// 
        /// </summary>
        public List<CrmLookup> Referencias { get; set; }


        /// <summary>
        /// Constructor que permite agregar un CrmLookup directamente.
        /// </summary>
        /// <param name="Referencia">Tipo de dato CrmLookup.</param>
        public void Agregar(CrmLookup Referencia)
        {
            if (Referencia.ID == null || Referencia.ID == Guid.Empty)
                throw new ParametroVacioNuloException("El parámetro Referencia.ID en el método Agregar de CrmActivityParty no puede ser Nulo o Guid.Empty ");
         
            if (String.IsNullOrEmpty(Referencia.TipoEntidad))
                throw new ParametroVacioNuloException("El parámetro Referencia.TipoEntidad en el método Agregar de CrmActivityParty no puede ser nulo o vacío.");

            if (Referencias == null)
            {
                Referencias = new List<CrmLookup>();
            }

            Referencias.Add(Referencia);
        }


        /// <summary>
        /// Constructor que permite agregar una referencia directamente con el ID y el nombre de esquema de la entidad.
        /// </summary>
        /// <param name="IdReferencia">ID de referencia de la entidad relacionada.</param>
        /// <param name="NombreEsquemaReferencia">Nombre de esquema de la entidad relacionada.</param>
        public void Agregar(Guid IdReferencia, String NombreEsquemaReferencia)
        {
            if (String.IsNullOrEmpty(NombreEsquemaReferencia))
                throw new ParametroVacioNuloException("El parámetro NombreEsquemaReferencia en el método Agregar de CrmActivityParty no puede ser nulo o vacío.");

            if (IdReferencia == Guid.Empty)
                throw new ParametroVacioNuloException("El parámetro IdReferencia en el método Agregar de CrmActivityParty no puede ser Guid.Empty");
            
            if (Referencias == null)
            {
                Referencias = new List<CrmLookup>();
            }
            
            CrmLookup item = new CrmLookup() { ID = IdReferencia, TipoEntidad = NombreEsquemaReferencia };
            Referencias.Add(item);

        }

    }
}
