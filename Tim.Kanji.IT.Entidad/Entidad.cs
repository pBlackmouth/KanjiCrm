using System;
using Tim.Crm.Base.Entidades.Atributos;
using Tim.Crm.Base.Entidades.Enumeraciones;
using Microsoft.Xrm.Sdk;
using Tim.Crm.Base.Entidades;

namespace $rootnamespace$
{
    /// <summary>
    /// TODO: Descripción de la clase y su uso.
    /// </summary>
    [Serializable]
[NombreEsquemaCrm("Nombre_de_esquema")]
public partial class $safeitemname$ : EntidadCrm
    {

        #region CONSTRUCTORES

        public $safeitemname$()
            : base()
        {
            Inicializar();
}

public $safeitemname$(Guid ID)
            : base()
        {
            Inicializar();
            this.ID = ID;
        }

/// <summary>
/// Constructor que permite hacer el mapeo de una clase Entity de CRM en nuestro objeto.
/// </summary>
/// <param name="entidad"></param>
public $safeitemname$(Entity entidad)
            : base(entidad)
        {

        }

        public $safeitemname$(String ObjectoSerializado, bool ObtenerValoresDesdeString = false)
            : base(ObjectoSerializado, ObtenerValoresDesdeString)
        {

        }

        //TODO: Definición de los constructores adicionales.

        #endregion

        #region PROPIEDADES

        [IdentificadorCRM]
[NombreEsquemaCrm("Nombre_de_esquema")]
public Guid? $safeitemname$ID { get; set; }

        //TODO: Definir las propiedades todos los tipos de datos deben ser nullables, a excepción de String y Objetos.

        #endregion

        #region MÉTODOS PRIVADOS

        private void Inicializar()
{
            $safeitemname$ID = null;
    //TODO: Inicializar todas las propiedades en NULL.
}

#endregion

#region MÉTODOS PÚBLICOS

//TODO: Definición de métodos públicos.

#endregion


#region VISTAS
//Para definir nuevas vistas, es necesario agregar nuevos métodos que hagan una instancia de la entidad en cuestion.
//En cada instancia es necesario inicializar cada propiedad con un valor distito de null para que solo dichas propiedades sean devueltas 
//y después retornar el método AtributosConValor() de la instancia.

public static string[] SoloID()
{
            $safeitemname$ entidad = new $safeitemname$()
            {
                $safeitemname$ID = Guid.Empty
            };

    return entidad.AtributosConValor();
}

//TODO: Definición de otras vistas.

#endregion

#region OPCIONES DE SERIALIZACION
//El método ShouldSerialize se aplica a cada propiedad de la clase que se quiera omitir en el XML resultante,
//es decir, cada vez que se serializa la propiedad y este tiene un valor null se serializar de la siguiente forma
//<$safeitemname$ID xsi:nil="true" />, si esto se quiere evitar y que solo se incluya en el XML resultante
//solo cada propiedad que contenga valor, se debe de crear un método para cada propiedad precedido por ShouldSerialize
//como el método siguiente:

public bool ShouldSerialize$safeitemname$ID()
{
    return $safeitemname$ID.HasValue;
}

        #endregion

    }
}
