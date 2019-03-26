<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConsultarElementos.aspx.cs" Inherits="PruebasWeb.ConsultarElementos" %>

<!doctype html>
<html lang="en" ng-app="phonecatApp">
<head>
    <meta charset="utf-8">
    <title>Google Phone Gallery</title>

    <script src="Recursos/Scripts/Libs/angular.min.js"></script>
    <script>
        var principalApp = angular.module('phonecatApp', []);

        principalApp.controller("ContactController",
              function ($scope, $http) {
                  $http.get("Recursos/Archivos/Contactos.json").success(function (data) {
                      $scope.contacts = data;
                  }).error(function (response) {
                      alert(response);
                  });

                  $scope.orderProp = 'Indice';

              }
          );
    </script>
    <style>
        #wrapper {
            width: 600px;
            position: absolute;
        }

        .DataTable {
            width: 100%;
        }

        .DataTable tr.item {
          color: #1f2092;
        }

        .DataTable tr.item:nth-child(odd) {
          color: #1f2092;
          background-color: #DDDDDD;
        }

        thead {
            background-color: black;
            color: white;
        }

        th {
            text-align: left;
            text-indent: 5px;
        }

        tfoot {
            background-color: black;
            color: white;
        }

            tfoot td {
                text-align: right;
                padding-right: 5px;
            }

        #headerRow {
            display: inline-block;
            width: 100%;
        }

        #headerRow {
            background-color: #4800ff;
            height: 25px;
            color: white;
            font-weight: bold;
        }

        #searchDiv {
            float: right;
            position: relative;
            padding-right: 2px;
            margin: auto;
            z-index: 100;
        }

        #orderDiv {
            position: relative;
            padding-left: 2px;
        }

        .tdIndice {
            width: 30px;
        }

        .tdPicture {
            width: 100px;
        }

        .imgSize {
            width: 100px;
            height: 133px;
        }
        .tdEstatus
        {
            text-align:right;
            padding-right: 5px;
        }
    </style>
</head>
<body ng-controller="ContactController">
    <form id="form1" runat="server">
        <div id="wrapper">
            <div id="headerRow">
                <div id="searchDiv">
                    Search:
                        <input ng-model="query">
                </div>
                <div id="orderDiv">
                    Sort by:
                        <select ng-model="orderProp">
                            <option value="Nombres">Nombre</option>
                            <option value="Indice">Indice</option>
                        </select>
                </div>
            </div>
            <table class="DataTable">
                <thead>
                    <tr>
                        <th>Contactos</th>                        
                    </tr>
                </thead>
                <tbody>
                    <tr ng-repeat="contact in contacts | filter:query | orderBy:orderProp" class="item">
                        <td>
                            <table style="width: 100%;" id="repeaterTable">
                                <tr>
                                    <td class="tdIndice" rowspan="6">{{contact.Indice}}</td>
                                    <td class="tdPicture" rowspan="6">
                                        <img alt="" class="imgSize" src="Recursos/Imagenes/Men0{{contact.Indice}}.jpg
                            " /></td>
                                    <td><b>Guid:</b> {{contact.Id}}</td>
                                </tr>
                                <tr>
                                    <td><b>Nombre:</b> {{contact.Nombres}} {{contact.Apellidos}}</td>
                                </tr>
                                <tr>
                                    <td><b>Teléfono: </b>{{contact.Telefono}}</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td><b>Estatus:</b> {{contact.Estatus}}</td>
                                </tr>
                                
                                
                            </table>
                        </td>
                    </tr>
                </tbody>
                <tfoot>
                    <tr>
                        <td colspan="5">Orderer by: {{orderProp}}</td>
                    </tr>
                </tfoot>
            </table>
        </div>






    </form>
</body>
</html>
