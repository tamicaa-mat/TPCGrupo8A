﻿<%@ Page Title="Tienda" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TPCGrupo8A._Default" %>

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
            <div class="card col-md-4" style="width: 18rem; margin: 10px; border-color: gray; height: 18rem;">
                <div class="image-container shadow" style="margin-bottom: 30px;">
                   
                    <img src='<%# Eval("Imagenes[0].ImagenUrl", "{0}") %>' class="card-img-top img-fluid" alt="Imagen del artículo">
                </div>
                <div class="card-body">
                    <h5 class="card-title"><%# Eval("Nombre") %></h5>
                    
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
                    <label for="inputNombre" class="form-label">Nombre</label>
                    <input type="text" class="txtdatos" id="inputNombre" placeholder="Nombre">
                </div>

                <div class="mb-3">
                    <label for="inputCelular" class="form-label">Celular</label>
                    <input type="tel" class="txtdatos" id="inputCelular" placeholder="">
                </div>
            </div>
            <div class="formulario-2">
                <div class="mb-3">
                    <label for="inputCorreo" class="form-label lbldatos">Correo</label>
                    <input type="email" class="txtdatos" id="inputCorreo" placeholder="nombre@ejemplo.com">
                </div>

                <div class="mb-3">
                    <label for="inputMensaje" class="form-label lbldatos">Mensaje</label>
                    <textarea class=" txtdatos" id="inputMensaje" rows="3" placeholder="Escribe tu mensaje aquí"></textarea>
                </div>
            </div>
            <div class="mb-3">
                <button type="submit" class="button">Enviar</button>
            </div>
        </div>
    </main>
</asp:Content>