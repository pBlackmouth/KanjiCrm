using System;
namespace Tim.Crm.Base.Entidades
{
    /// <summary>
    /// 
    /// </summary>
    public class Respuesta : Entidad
    {

        /// <summary>
        /// 
        /// </summary>
        public Respuesta()
        {
            Completado = false;
            ID = null;
            Error = String.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool? Completado { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid? ID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool ShouldSerializeID()
        {
            return ID.HasValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool ShouldSerializeCompletado()
        {
            return Completado.HasValue;
        }

    }
}
