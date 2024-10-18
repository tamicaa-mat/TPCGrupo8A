<%@ Page Title="Tienda" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TPCGrupo8A._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <navbar  class="navbar-dropdown" style="background-color: #e3f2fd; padding: 0; display: flex; justify-content: center;">
    <div class="btn-group" style="padding: 10px">
        <button class="dropdown-menu-dark" type="button" data-bs-toggle="dropdown" aria-expanded="false">
            Categorías
        </button>
        <ul class="dropdown-menu" style="background-color:#273746">
            <li><a class="dropdown-item" href="#">Opcion 1</a></li>
            <li><a class="dropdown-item" href="#">Opcion 2</a></li>
            <li><a class="dropdown-item" href="#">Opcion 3</a></li>
        </ul>
    </div>
    <div class="btn-group" style="padding: 10px">
    <button class="dropdown-menu-dark" type="button" data-bs-toggle="dropdown" aria-expanded="false">
        Calzados
    </button>
    <ul class="dropdown-menu" style="background-color:#273746">
        <li><a class="dropdown-item" href="#">Opcion 1</a></li>
        <li><a class="dropdown-item" href="#">Opcion 2</a></li>
        <li><a class="dropdown-item" href="#">Opcion 3</a></li>
    </ul>
</div>
        </navbar>
    <hr />
        <div class="container-productos">
        
        <h3 id="titulo-productos" style="text-align: center; margin-top: 25px;">TITULO SECCION</h3>
        
        </div>
    

</asp:Content>
