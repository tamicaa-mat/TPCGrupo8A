<%@ Page Title="Tienda" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TPCGrupo8A._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <navbar class="navbar-dropdown" style="background-color: #e3f2fd; padding: 0; display: flex; justify-content: center;">
        <div class="btn-group" >

            <button class="dropdown-menu-dark" type="button" data-bs-toggle="dropdown" aria-expanded="false">
                Categorías
            </button>
            <ul class="dropdown-menu" style="background-color: #273746">
                <li><a class="desplegable-item" href="#">Opcion 1</a></li>
                <li><a class="desplegable-item" href="#">Opcion 2</a></li>
                <li><a class="desplegable-item" href="#">Opcion 3</a></li>
            </ul>
        </div>

        <div class="btn-group">
            <button class="dropdown-menu-dark" type="button" data-bs-toggle="dropdown" aria-expanded="false">
                Calzados
            </button>
            <ul class="dropdown-menu" style="background-color: #273746">
                <li><a class="desplegable-item" href="#">Opcion 1</a></li>
                <li><a class="desplegable-item" href="#">Opcion 2</a></li>
                <li><a class="desplegable-item" href="#">Opcion 3</a></li>
            </ul>
        </div>
    </navbar>
    
        <div class="img-card">
        <img src="./assets/fondo.jpeg" alt="fondo" style="width: 100%; height: auto;" />
        <div class="container-btn-primavera">   
        <a href="#"  class="btn-primavera">OFERTAS DE PRIMAVERA</a>
        </div>    
        </div>


   
        <div class="container-productos">
        
        <h3 id="titulo-productos" style="text-align: center; margin-top: 25px;">-- LO NUEVO --</h3>

            <div class="container text-center">
                <div class="row">
                    <div class="col">
                        <img src="./assets/error.jpg" alt="prueba" height="205px"/>
                    </div>
                    <div class="col">
                        <img src="./assets/error.jpg" alt="prueba" height="205px"/>
                    </div>
                    <div class="col">
                        <img src="./assets/error.jpg" alt="prueba" height="205px"/>
                    </div>
                </div>
            </div>

        </div>
    

</asp:Content>
