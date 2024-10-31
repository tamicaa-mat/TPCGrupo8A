<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Administrador.aspx.cs" Inherits="TPCGrupo8A.Administrasdor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

     <div class="container">
            <h1>Administrador</h1>
        <div class="row">
            <div class="col-md-4">
                <div class="card text-center">
                    <div class="card-body">
                        <h5 class="card-title">Productos</h5>
                        <a href="#" class="btn btn-outline-secondary">Productos</a>
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="card text-center">
                    <div class="card-body">
                        <h5 class="card-title">Marcas</h5>
                        <a href="Marcas.aspx" class="btn btn-outline-secondary">Marcas</a>
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="card text-center">
                    <div class="card-body">
                        <h5 class="card-title">Categorías</h5>
                        <a href="Categorias.aspx" class="btn btn-outline-secondary">Categorías</a>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
