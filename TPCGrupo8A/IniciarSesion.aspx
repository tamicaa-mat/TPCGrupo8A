<%@ Page Title="Iniciar Sesion" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IniciarSesion.aspx.cs" Inherits="TPCGrupo8A.IniciarSesion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <div class="container-inicio">
            <h3 class="h3-inicio">Ingresar</h3>

            <div class="container-datos">
                <asp:Label class="lbldatos" ID="lblEmail" runat="server" Text="EMAIL: "> </asp:Label>
                <asp:TextBox class="txtdatos" ID="txtemail" runat="server" TextMode="Email"></asp:TextBox>

                <asp:Label class="lbldatos" ID="lblPassword" runat="server" Text="CONTRASEÑA: "> </asp:Label>
                <asp:TextBox class="txtdatos" ID="txtpassword" runat="server" TextMode="Password"></asp:TextBox>
                <div class="container-btn-iniciar">
                    <asp:Button ID="ButtonIngresar" CssClass="btn-iniciar" runat="server" OnClick="btnButtonIngresarOnClick" Text="Ingresar" />

                    <%--                <a href="#" class="btn-iniciar">Ingresar</a>--%>
                    <p>o</p>
                    <a href="#" class="btn-registrarse" id="btnregistro" data-bs-toggle="modal" data-bs-target="#modalRegistro">Registrarse</a>
                </div>
            </div>
        </div>
        <!-- Estructura de la ventana modal -->
        <div class="modal fade" id="modalRegistro" tabindex="-1" aria-labelledby="modalRegistroLabel" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="modalRegistroLabel">REGISTRARSE</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">

                        <div class="container-registro">
                           
                            <asp:Label class="lbldatos" ID="lblApellido" runat="server" Text="Apellido: "></asp:Label>
                            <asp:TextBox class="txtdatos" ID="txtApellido" runat="server"></asp:TextBox>

                             <asp:Label class="lbldatos" ID="lblNombre" runat="server" Text="Nombre: "></asp:Label>
                             <asp:TextBox class="txtdatos" ID="txtNombre" runat="server"></asp:TextBox>


                            <asp:Label class="lbldatos" ID="lblEmailRegistro" runat="server" Text="Email: "></asp:Label>
                            <asp:TextBox class="txtdatos" ID="txtEmailRegistro" runat="server" TextMode="Email"></asp:TextBox>

                            <asp:Label class="lbldatos" ID="lblPasswordRegistro" runat="server" Text="Contraseña: "></asp:Label>
                            <asp:TextBox class="txtdatos" ID="txtPasswordRegistro" runat="server" TextMode="Password"></asp:TextBox>


                        </div>
                    </div>
                    <div class="modal-footer">
                      
                     <asp:Button ID="btnRegistrar" runat="server" Text="Registrar" CssClass="btn-iniciar" OnClick="btnRegistrar_On_Click" />


                    </div>
                </div>
            </div>
        </div>
    </main>


</asp:Content>
