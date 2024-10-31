<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Perfil.aspx.cs" Inherits="TPCGrupo8A.Perfil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

   <div class="container">
    
    <div class="row justify-content-center">
        <div class="col-auto">
            <div class="nav nav-pills mb-3" id="pills-tab" role="tablist">
                <button class="nav-link active" id="pills-home-tab" data-bs-toggle="pill" data-bs-target="#pills-home" type="button" role="tab" aria-controls="pills-home" aria-selected="true">Perfil</button>
                
                <button class="nav-link" id="pills-disabled-tab" data-bs-toggle="pill" data-bs-target="#pills-disabled" type="button" role="tab" aria-controls="pills-disabled" aria-selected="false" disabled>Disabled</button>
                <button class="nav-link" id="pills-messages-tab" data-bs-toggle="pill" data-bs-target="#pills-messages" type="button" role="tab" aria-controls="pills-messages" aria-selected="false">Messages</button>
                <button class="nav-link" id="pills-settings-tab" data-bs-toggle="pill" data-bs-target="#pills-settings" type="button" role="tab" aria-controls="pills-settings" aria-selected="false">Settings</button>
            </div>
        </div>
    </div>

   
    <div class="tab-content" id="pills-tabContent">
        <div class="tab-pane fade show active" id="pills-home" role="tabpanel" aria-labelledby="pills-home-tab">
            
            
            <main id="perfil">

                <h1>Perfil</h1>
                <div class="datos">
                    <label for="txtEmail">Email:</label>
                    <asp:TextBox ID="TextEmail" runat="server" TextMode="Email" Placeholder="Ingrese su correo electrónico" CssClass="textbox"></asp:TextBox>
                </div>
                <div class="datos">
                    <label for="txtNombre">Nombre:</label>
                    <asp:TextBox ID="TextNombre" runat="server" Placeholder="Ingrese su nombre" CssClass="textbox"></asp:TextBox>
                </div>
                <div class="datos">
                    <label for="txtApellido">Apellido:</label>
                    <asp:TextBox ID="TextApellido" runat="server" Placeholder="Ingrese su apellido" CssClass="textbox"></asp:TextBox>
                </div>
                <div class="datos">
                    <label for="txtFechaNacimiento">Fecha de Nacimiento:</label>
                    <asp:TextBox ID="TextFechaNacimiento" runat="server" Placeholder="Ingrese su apellido" TextMode="Date" CssClass="textbox"></asp:TextBox>
                </div>

                <h2>Cambiar contraseña</h2>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="datos">
                            <asp:Button ID="btnCambiarContrasenia" runat="server" Text="Cambiar Contraseña" OnClick="btnCambiarContraseniaOnClick" CssClass="btn-bd-primary-Personalizado-violeta active " />
                        </div>
                        <div id="CambiarContrasenia" runat="server" visible="false">
                            <div class="datos">
                                <label for="txtContraseniaActual">Contraseña Actual:</label>
                                <asp:TextBox ID="TextContraseniaActual" runat="server" Placeholder="Ingrese su contraseña actual" TextMode="Password" CssClass="textbox"></asp:TextBox>
                            </div>
                            <div class="datos">
                                <label for="txtContraseniaNueva">Nueva Contraseña:</label>
                                <asp:TextBox ID="TextContraseniaNueva" runat="server" CssClass="textbox" TextMode="Password" Placeholder="Ingrese nueva contraseña"></asp:TextBox>
                            </div>
                            <div class="datos">
                                <label for="txtContraseniaNuevaConf">Confirmar Nueva Contraseña:</label>
                                <asp:TextBox ID="TextContraseniaNuevaConf" runat="server" CssClass="textbox" TextMode="Password" Placeholder="Confirme su nueva contraseña"></asp:TextBox>
                            </div>
                            <asp:Button ID="btnGuardarNuevaCont" runat="server" Text="Guardar" OnClick="btnGuardarNuevaContOnClick" CssClass="btn-bd-primary-Personalizado-violeta" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

                <div class="datos">
                    <h2>Domicilios</h2>
                    <label for="txtDireccion">Domicilios:</label>
                    <asp:TextBox ID="TextDireccion" runat="server" CssClass="textbox" Placeholder="Ingrese su dirección"></asp:TextBox>
                </div>

                <asp:Button ID="btnGuardar" runat="server" Text="Guardar Cambios" CssClass="btn-bd-primary-Personalizado-violeta" OnClick="btnGuardarPerfilOnClick" />
                <%--//tendra una opcion de editar perfil y ahi guardara los cambios de ser necesario--%>
            </main>
        </div>
        
        <div class="tab-pane fade" id="pills-disabled" role="tabpanel" aria-labelledby="pills-disabled-tab">
            <h1>Disabled</h1>
            
        </div>
        <div class="tab-pane fade" id="pills-messages" role="tabpanel" aria-labelledby="pills-messages-tab">
            <h1>Messages</h1>
            
        </div>
        <div class="tab-pane fade" id="pills-settings" role="tabpanel" aria-labelledby="pills-settings-tab">
            <h1>Settings</h1>
           
        </div>
    </div>
</div>



   
</asp:Content>
