using System;

namespace Tim.Crm.Base.Entidades
{
    public class EntidadRelacionada
    {
        public Object EntidadRelacion { get; set; }

        public string AliasEntidadRelacion { get; set; }

        public string AtributoOrigenRelacion { get; set; }
        public string AtributoDestinoRelacion { get; set; }

        public Microsoft.Xrm.Sdk.Query.JoinOperator TipoRelacion { get; set; }

        //public DataCollection<LinkEntity> LinkEntities { get; }
    }
}
