using System;
namespace Tim.Crm.Base.Entidades.Enumeraciones
{
    /// <summary>
    /// 
    /// </summary>
    public enum eTipoDatoCRM : int
    {
        None,
        EntityReference,
        OptionSetValue,
        Money,
        ActivityParty
    }

    /// <summary>
    /// 
    /// </summary>
    public enum eTipoEventoSistema : int
    {
        Error = 100000000,
        Informativo
    }


    //TODO: CESAR Esta enumeración no estaba definida, probablemente por que no estaba la última versión arriba.
    /// <summary>
    /// 
    /// </summary>
    public enum eEstadoDeArchivo : int 
    { 
        SinCambios,
        Guardado,
        Error,
        NoGuardadoRepetido,
        NoActualizadoRepetido,
        Actualizado,
        Eliminado
    }

    /// <summary>
    /// 
    /// </summary>
    public enum TipoPeticion : int
    {
        ObtenerOrganizaciones,
        ProbarConexion
    }

    /// <summary>
    /// 
    /// </summary>
    public enum eIdioma : int
    {
        EN_EU = 1033,   //0x0409
        ES_MX = 2058,   //0x080A
        ES_ES = 3082    //0x0C0A
    }

    /// <summary>
    /// 
    /// </summary>
    public enum eComparacion : int
    {
        Ninguno,
        Simple,
        Rango,
        RangoDiscreto
    }

    /// <summary>
    /// 
    /// </summary>
    public enum eOperador : int
    {       
        Ninguno = 0,
        Igual,
        MayorQue,
        MayorIgualQue,
        MenorQue,
        MenorIgualQue
    }

    /// <summary>
    /// 
    /// </summary>
    public enum eRango : int
    {
        Ninguno = 0,
        Excluidos,
        IncluidoIzquierda,
        IncluidoDerecha,
        Incluidos
    }

    /// <summary>
    /// 
    /// </summary>
    public enum eTipoRango : int
    {
        Ninguno = 0,
        EntreRango,
        FueraRango
    }

    /// <summary>
    /// 
    /// </summary>
    public enum eTipoObjeto : int
    {
        XML,
        JSON
    }

    /// <summary>
    /// 
    /// </summary>
    public enum eDias
    {
        Habiles,
        Naturales
    }

    public enum eTipoControlWeb
    {
        Etiqueta,
        IdentificadorUnico,
        Texto,
        AreaTexto,
        FechaHora,
        Fecha,
        Hora,
        Correo,
        Telefono,
        Numero,
        Decimal,
        Casilla,
        ListaDesplegable,
        Referencia
    }

    

}
