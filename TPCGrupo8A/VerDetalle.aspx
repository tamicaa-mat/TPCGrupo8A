<%@ Page Title="Ver Detalle" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VerDetalle.aspx.cs" Inherits="TPCGrupo8A.VerDetalle" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<div class="container">
        <h1 class="text-center my-4">Detalle del Pedido</h1>

        <!-- Aquí se mostrarán los detalles del pedido -->
        <div  id="detallePedido" runat="server">
            <!-- Los detalles se inyectarán desde el código detrás -->
        </div>
    <a class="animated-button" href="Pedidos">Volver</a>
    </div>

</asp:Content>
