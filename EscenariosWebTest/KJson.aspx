<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KJson.aspx.cs" Inherits="PruebasWeb.KJson" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Content/bootstrap.css" rel="stylesheet" />
    <script src="Recursos/Scripts/Libs/jquery-2.1.1.min.js"></script>
    <script src="Recursos/Scripts/Tim/Reflection.js"></script>

    <script>
        $(function () {


            var cuenta;

            $.ajax({
                dataType: 'json',
                type: 'POST',
                url: "KJson.aspx/ObtenerCuenta",
                data: '{ "CuentaID": "36194BF8-B9A3-E311-940A-00155D01082E", "kJSON": "true" }',
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    cuenta = JSON.parse(data.d);
                },
                error: function (data) {
                    console.log("failed");
                }
            })
            .then(function () {
                var propsCuenta = new Reflector(cuenta).getProperties();
                $(propsCuenta).each(function () {
                    if (cuenta.hasOwnProperty(this.Name))
                        if(this.TipoEntidad == null)
                            AddMessage(this.Name + ", \t value: " + this.Value + ", type: " + this.Type + ", Display: " + cuenta.NombreParaMostrar[this.Name] + ", NombreDeEsquema: " + cuenta.NombreEsquemaCrm[this.Name]);
                        else
                            AddMessage(this.Name + ", \t value: " + this.Value + ", type: " + this.Type + ",TipoEntidad: " + this.TipoEntidad + ", Display: " + cuenta.NombreParaMostrar[this.Name] + ", NombreDeEsquema: " + cuenta.NombreEsquemaCrm[this.Name]);
                })
            });


        });


        function AddMessage(message) {
            $("#lista").append("<li class='list-group-item'>" + message + "</li>");
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container">
        <ul id="lista" class="list-group"></ul>
    </div>
    </form>
</body>
</html>
