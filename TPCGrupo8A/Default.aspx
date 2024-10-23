<%@ Page Title="Tienda" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TPCGrupo8A._Default" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


    <main>
      
        <div class="img-card">
            <img src="./assets/fondo.jpeg" alt="fondo" style="width: 100%; height: auto;" />
            <div class="container-btn-primavera">
                <a href="#" class="btn-primavera">OFERTAS DE PRIMAVERA</a>
            </div>
        </div>

        <div class="container-productos">
            <h3 id="titulo-productos" style="text-align: center; margin-top: 25px;">-- LO NUEVO --</h3>
            <div class="container text-center">
                <div class="row">
                    <div class="col">
                        <img src="./assets/error.jpg" alt="prueba" height="205px" />
                    </div>
                    <div class="col">
                        <img src="./assets/error.jpg" alt="prueba" height="205px" />
                    </div>
                    <div class="col">
                        <img src="./assets/error.jpg" alt="prueba" height="205px" />
                    </div>
                </div>
            </div>
        </div>
    </main>
</asp:Content>


