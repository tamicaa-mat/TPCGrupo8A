<%@ Page Title="Tienda" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TPCGrupo8A._Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <div class="img-card">
            <img src="./assets/fondo.jpeg" alt="fondo" style="width: 100%; height: auto;" />
            <div class="container-btn-primavera">
                <a href="#" class="btn-primavera">OFERTAS DE PRIMAVERA</a>
            </div>
        </div>
        <%-- btn Agregar producucto--%>
        <% if (tipoUsuario == 1)
            {%>
        <div class="container mb-3">
            <a href="FormularioProductosAM.aspx" style="text-decoration:none" class="c-button">
                <span class="c-main">
                    <span class="c-ico"><span class="c-blur"></span><span class="ico-text">+</span></span>
                    Agregar Producto
                </span>
            </a>
        </div>
        <% } %>

        <div class="row">
            <asp:Repeater ID="rptProductos" runat="server">
                <ItemTemplate>
                    <div class="card col-md-4" style="width: 18rem; margin: 10px; border-color: gray;">
                        <div class="image-container">
                            <img src='<%# Eval("Imagenes[0].ImagenUrl") %>' class="card-img-top img-fluid" alt="Imagen del artículo">
                        </div>
                        <div class="card-body">
                            <h5 class="card-title"><%# Eval("Nombre") %></h5>
                            <% if (tipoUsuario != 1)
                                {%>
                            <a href="IniciarSesion.aspx" class="btn btn-primary">Seleccionar</a>
                            <% }
                                else
                                { %>
                                <div class="container d-flex justify-content-between ">
                                    <button class="btn-editar" ><a style="text-decoration:none" href="FormularioProductosAM.aspx">Editar</a></button>
                                    <asp:Button ID="btnEliminar" runat="server" CommandArgument='<%# Eval("ID") %>' OnCommand="btnEliminar_Command" class="delete-button" Text="Eliminar" />
                                   <%--<button class="delete-button" id="btnEliminar" CommandArgument='<%# Eval("ID") %>' OnCommand="btnEliminar_Command">
                                    <svg class="delete-svgIcon" viewBox="0 0 448 512">
                                        <path d="M135.2 17.7L128 32H32C14.3 32 0 46.3 0 64S14.3 96 32 96H416c17.7 0 32-14.3 32-32s-14.3-32-32-32H320l-7.2-14.3C307.4 6.8 296.3 0 284.2 0H163.8c-12.1 0-23.2 6.8-28.6 17.7zM416 128H32L53.2 467c1.6 25.3 22.6 45 47.9 45H346.9c25.3 0 46.3-19.7 47.9-45L416 128z"></path>
                                    </svg>
                                </button>--%>
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