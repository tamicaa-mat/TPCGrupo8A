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
                    <div class="card col-md-4" style="width: 18rem; margin: 10px; border-color: gray; height: 18rem;">
                        <div class="image-container" style="margin-bottom: 30px;">
                            <img src='<%# Eval("Imagenes[0].ImagenUrl") %>' class="card-img-top img-fluid" alt="Imagen del artículo">
                        </div>
                        <div class="card-body">
                            <h5 class="card-title"><%# Eval("Nombre") %></h5>
                            <% if (tipoUsuario != 1)
                                {%>
                            <a href="IniciarSesion.aspx" class="btn-iniciar">Seleccionar</a>
                            <% }
                                else
                                { %>
                                <div class="container d-flex justify-content-between ">
                                    <asp:Button CssClass="btn-editar" ID="btnEditar"  runat="server" CommandArgument='<%# Eval("ID") %>' OnCommand="btnEditar_Command"  Text="Editar" />
                                    <asp:Button ID="btnEliminar" runat="server" CommandArgument='<%# Eval("ID") %>' OnCommand="btnEliminar_Command" CssClass="delete-button" Text="Eliminar" />
                                  
                                </div>
                            <%  } %>
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
                    <input type="text" class="form-control" id="inputNombre" placeholder="Nombre">
                </div>

                <div class="mb-3">
                    <label for="inputCelular" class="form-label">Celular</label>
                    <input type="tel" class="form-control" id="inputCelular" placeholder="">
                </div>
            </div>
            <div class="formulario-2">
                <div class="mb-3">
                    <label for="inputCorreo" class="form-label">Correo</label>
                    <input type="email" class="form-control" id="inputCorreo" placeholder="nombre@ejemplo.com">
                </div>

                <div class="mb-3">
                    <label for="inputMensaje" class="form-label">Mensaje</label>
                    <textarea class="form-control" id="inputMensaje" rows="3" placeholder="Escribe tu mensaje aquí"></textarea>
                </div>
            </div>
            <div class="mb-3">
                <button type="submit" class="btn btn-primary">Enviar</button>
            </div>
        </div>
    </main>
</asp:Content>