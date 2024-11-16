 <%@ Page Title="Tienda" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TPCGrupo8A._Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <div class="img-card">
            <img src="./assets/fondo.jpeg" alt="fondo" style="width: 100%; height: auto;" />
            
        </div>
        <%-- btn Agregar producucto--%>
        <% if (tipoUsuario == 1)
            {%>
        <div class="container mb-3">
            <a href="FormularioProductosAM.aspx" class="button" style="text-decoration:none">
                Añadir
            </a>
        </div>
        <% } %>

     <div class="row">
    <asp:Repeater ID="rptProductos" runat="server">
        <ItemTemplate>
            <!-- Tarjeta de producto individual -->
         
            <div class="card col-md-4" style="width: 18rem; margin: 10px; border-color: gray;">
                <div class="image-container shadow" style="margin-bottom: 30px;">
                   
                    <img src='<%# Eval("Imagenes[0].ImagenUrl", "{0}") %>' class="card-img-top img-fluid" alt="Imagen del artículo">
                </div>
                <div class="card-body">
                    <h5 class="card-title"><%# Eval("Nombre") %></h5>
                    
                        <p class="card-text"><%# Eval("Descripcion") %></p>
                        <p class="card-text font-weight-bold">$<%# Eval("Precio", "{0:N2}") %></p>

                    <%-- Verifica el tipo de usuario  --%>
                    <% if (tipoUsuario != 1) { %>
                      
                        <asp:LinkButton ID="btnSeleccionar" runat="server" CssClass="animated-button" 
                            CommandArgument='<%# Eval("ID") %>' OnClick="btnSeleccionar_Click">Seleccionar</asp:LinkButton>
                    <% } else { %>
                      
                        <div class="container d-flex justify-content-between">
                            <asp:Button CssClass="btn-editar" ID="btnEditar" runat="server" CommandArgument='<%# Eval("ID") %>' 
                                OnCommand="btnEditar_Command" Text="Editar" />
                            <asp:Button ID="btnEliminar" runat="server" CommandArgument='<%# Eval("ID") %>' 
                                OnCommand="btnEliminar_Command" CssClass="delete-button" Text="Eliminar" />
                        </div>
                    <% } %>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</div>



        <div class="container-contacto" id="contacto">
            <h4>CONTACTANOS</h4>
            <div class="formulario-1">
                <div class="mb-3">
                    <asp:Label ID="lblNombreContacto" runat="server" Text="Nombre" CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="txtNombreContacto" runat="server" CssClass="txtdatos"></asp:TextBox>
                    
                </div>

                <div class="mb-3">
                    <asp:Label ID="lblCelularContacto" runat="server" Text="Celular" CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="txtCelularContacto" runat="server" CssClass="txtdatos"></asp:TextBox>

                </div>
            </div>
            <div class="formulario-2">
                <div class="mb-3">
                    <asp:Label ID="lblCorreoContacto" runat="server" Text="Correo" CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="txtCorreoContacto" runat="server" CssClass="txtdatos"></asp:TextBox>
                    
                </div>

                <div class="mb-3">
                    <asp:Label ID="lblMensajeContacto" runat="server" Text="Mensaje" CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="txtMensajeContacto" textMode="MultiLine" runat ="server" CssClass="txtdatos" placeholder="Escriba su mensaje aquí"></asp:TextBox>
                </div>
            </div>
            <div class="mb-3">
               <asp:Button ID="btnEnviar" CssClass="animated-button" runat="server" Text="Enviar" OnClick="btnEnviar_Click"/>
                <asp:Label ID="lblMensaje" runat="server" CssClass="mensaje-estado" Visible="false"></asp:Label>
            </div>
        </div>
    </main>
</asp:Content>