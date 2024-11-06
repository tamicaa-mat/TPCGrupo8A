<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Administrador.aspx.cs" Inherits="TPCGrupo8A.Administrasdor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

     <div class="container">
            <h1>Administrador</h1>
        <div class="row">
            <div class="col-md-4">
                <div class="container-datos">
                    <div class="container-btn-iniciar">
                        <h5 class="card-title">Productos</h5>
                        <a href="Default.aspx" class="btn-iniciar">Productos</a>
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="container-datos">
                    <div class="container-btn-iniciar">
                        <h5 class="card-title">Marcas</h5>
                        <a href="Marcas.aspx" class="btn-iniciar">Marcas</a>
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="container-datos">
                    <div class="container-btn-iniciar">
                        <h5 class="card-title">Categorías</h5>
                        <a href="Categorias.aspx" class="btn-iniciar">Categorías</a>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
