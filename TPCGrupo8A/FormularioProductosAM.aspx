<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormularioProductosAM.aspx.cs" Inherits="TPCGrupo8A.FormularioProductosAM" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main class="container">
        <div>
            <h1>Productos</h1>
        </div>
        <div class="mb-3 ">
            <asp:Label CssClass="form-label lbldatos" ID="lblCodigo" runat="server" Text="Codigo:"></asp:Label>
            <asp:TextBox CssClass="txtdatos" ID="txtCodigo" runat="server" placeholder="Código del Producto"></asp:TextBox>
            <asp:Label ID="lblErrorCodigo" runat="server" Forecolor="red" Visible="false" Enabled="false"></asp:Label>
        </div>
        <div class="mb-3">
            <asp:Label CssClass="form-label lbldatos" ID="lblNombre" runat="server" Text="Nombre:"></asp:Label>
            <asp:TextBox CssClass="txtdatos" ID="txtNombre" runat="server" placeholder="Nombre"></asp:TextBox>
        </div>
        <div class="mb-3">
            <asp:Label CssClass="form-label lbldatos "  ID="lblDescripcion" runat="server" Text="Descripción:"></asp:Label>
            <asp:TextBox CssClass="txtdatos" ID="txtDescripcion" runat="server" placeholder="Descripción"></asp:TextBox>
        </div>
        <div class="input-group mb-3">
            <span class="input-group-text lbldato">$</span>
            <asp:TextBox CssClass="form-control " ID="txtPrecio" runat="server" TextMode="Number"></asp:TextBox>
            <span class="input-group-text">.00</span>
            <asp:Label ID="lblErrorPrecio" runat="server" Forecolor="red" Visible="false"></asp:Label>
        </div>
        
        <div class="input-group mb-3 lbldatos">
            <asp:Label cssClass="lbldatos" ID="lblStock" runat="server" Text="Stock:"></asp:Label>
            <asp:TextBox CssClass="txtdatos" ID="txtStock" runat="server" TextMode="Number"  Step="1"></asp:TextBox>
            <asp:Label ID="lblErrorStock" runat="server" Forecolor="red" visible="false"></asp:Label>
        </div>
        <!-- Desplegable de Marcas -->
        <div class="btn-group mb-3 lbldatos" >
            <asp:Label ID="lblMarca" runat="server" Text="Marca:"></asp:Label>
            <asp:DropDownList ID="ddlMarcas" runat="server" CssClass=" txtdatos" AutoPostBack="True" OnSelectedIndexChanged="ddlMarcas_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:TextBox ID="txtMarca" runat="server" CssClass="form-control" Placeholder="Seleccione una marca" ReadOnly="True" visible="false"/>
            <asp:Label ID="lblErrorMarca" runat="server" Forecolor="red" visible="false"></asp:Label>
        </div>
        <!-- Desplegable de Categorías-->
        <div class="btn-group mb-3 lbldatos">
            <asp:Label ID="lblCategoria" runat="server" Text="Categoría:"></asp:Label>
            <asp:DropDownList ID="ddlCategorias" runat="server" CssClass=" txtdatos" AutoPostBack="True" OnSelectedIndexChanged="ddlCategorias_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:TextBox ID="txtCategoria" runat="server" CssClass="form-control" Placeholder="Seleccione una Categoría" ReadOnly="True" Visible="false" />
            <asp:Label ID="lblErrorCategoria" runat="server" Forecolor="red" visible="false"></asp:Label>
        </div>
        <div class="mb-3">
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" CssClass="btn-iniciar" />
        </div>
        <asp:Label ID="lblExito" runat="server" visible="false"></asp:Label>
        <%-- Imagenes EN PROCESO--%>
        <asp:Panel ID="pnlImagenes" runat="server" Visible="false">
            <h3>Imágenes del Producto</h3>

            <asp:TextBox ID="txtImagenUrl" runat="server" Placeholder="URL de la imagen" CssClass="form-control"/>
            <asp:Button ID="btnAgregarImagen" runat="server" Text="Agregar Imagen" OnClick="btnAgregarImagen_Click" CssClass="btn-iniciar" />
            <asp:Repeater ID="rptImagenes" runat="server">
                <ItemTemplate>
                    <div class="card" style="width: 150px; display: inline-block; margin: 10px;">
                        <asp:Image ID="imgProducto" runat="server" ImageUrl='<%# Eval("ImagenUrl") %>' CssClass="card-img-top" Width="100%" />
                        <div class="card-body">
                            <asp:Button ID="btnEliminarImagen" runat="server" Text="Eliminar" CssClass="btn btn-danger btn-sm"  CommandName="EliminarImagen" CommandArgument='<%# Eval("Id") %>' OnCommand="btnEliminarImagen_Command" />
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </asp:Panel>
    </main>
</asp:Content>
