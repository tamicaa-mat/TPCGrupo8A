<%@ Page Title="Iniciar Sesion" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IniciarSesion.aspx.cs" Inherits="TPCGrupo8A.IniciarSesion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <div class="container-inicio">
        <h3 class="h3-inicio">Inicia Sesión</h3>

            <div class="container-datos">
                <asp:Label class="lbldatos" ID="lblEmail" runat="server" Text="EMAIL: "> </asp:Label>
                <asp:TextBox class="txtdatos" ID="txtemail" runat="server"></asp:TextBox>
                
                <asp:Label class="lbldatos" ID="lblPassword" runat="server" Text="CONTRASEÑA: "> </asp:Label>
                <asp:TextBox class="txtdatos" ID="txtpassword" runat="server" TextMode="Password"></asp:TextBox>
                <div class="container-btn-iniciar">
                <a href="#" class="btn-iniciar">Iniciar Sesión</a>
                <p style="text-align: center;">o</p>
                <a href="#" class="btn-registrarse">Registrarse</a>
                </div>

            
        </div>  
    </main>


</asp:Content>
