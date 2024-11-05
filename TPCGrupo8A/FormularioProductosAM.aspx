﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormularioProductosAM.aspx.cs" Inherits="TPCGrupo8A.FormularioProductosAM" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main class="container">
        <div>
            <h1>Poductos</h1>
        </div>
        <div class="mb-3">
            <asp:Label CssClass="form-label" ID="lblCodigo" runat="server" Text="Codigo:"></asp:Label>
            <asp:TextBox CssClass="form-control" ID="txtCodigo" runat="server" placeholder="Código del Producto"></asp:TextBox>
        </div>
        <div class="mb-3">
            <asp:Label CssClass="form-label" ID="lblNombre" runat="server" Text="Nombre:"></asp:Label>
            <asp:TextBox CssClass="form-control" ID="txtNombre" runat="server" placeholder="Nombre"></asp:TextBox>
        </div>
        <div class="mb-3">
            <asp:Label CssClass="form-label" ID="lblDescripcion" runat="server" Text="Descripción:"></asp:Label>
            <asp:TextBox CssClass="form-control" ID="txtDescripcion" runat="server" placeholder="Descripción"></asp:TextBox>
        </div>
        <div class="input-group mb-3">
            <span class="input-group-text">$</span>
            <asp:TextBox CssClass="form-control" ID="txtPrecio" runat="server" TextMode="Number"></asp:TextBox>
            <span class="input-group-text">.00</span>
        </div>
        <div class="input-group mb-3">
            <asp:Label ID="lblStock" runat="server" Text="Stock:"></asp:Label>
            <asp:TextBox ID="txtStock" runat="server" TextMode="Number" Min="0" Step="1"></asp:TextBox>
        </div>
        <!-- Desplegable de Marcas -->
        <div class="btn-group mb-3">
            <asp:Label ID="lblMarca" runat="server" Text="Marca:"></asp:Label>
            <asp:DropDownList ID="ddlMarcas" runat="server" CssClass="form-select" AutoPostBack="True" OnSelectedIndexChanged="ddlMarcas_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:TextBox ID="txtMarca" runat="server" CssClass="form-control" Placeholder="Seleccione una marca" ReadOnly="True" />
        </div>
        <!-- Desplegable de Categorías-->
        <div class="btn-group mb-3">
            <asp:Label ID="lblCategoria" runat="server" Text="Categoría:"></asp:Label>
            <asp:DropDownList ID="ddlCategorias" runat="server" CssClass="form-select" AutoPostBack="True" OnSelectedIndexChanged="ddlCategorias_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:TextBox ID="txtCategoria" runat="server" CssClass="form-control" Placeholder="Seleccione una Categoría" ReadOnly="True" />
        </div>

        <%-- Imagenes EN PROCESO--%>
        <%--<asp:Label ID="lblImagenUrl" runat="server" Text="Ingrese el URL de la imagen:" CssClass="form-label"></asp:Label>
        <asp:TextBox ID="txtImagenUrl" runat="server" CssClass="form-control" Placeholder="URL de la imagen" />
        <asp:Button ID="btnAgregarImagen" runat="server" Text="Agregar Imagen" CssClass="btn btn-primary" OnClick="btnAgregarImagen_Click" />--%>

        <div class="mb-3">
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" CssClass="btnGuardar" />
        </div>
        <%-- mostrar las imágenes en formato de cards, ya que en las cards se podran no solo agregar sino tambien eliminar imgs //btn eliminarImg en proceso
     <%--   <asp:Repeater ID="rptImagenes" runat="server">
            <ItemTemplate>
                <div class="card" style="width: 10rem; margin: 5px;">
                    <img src='<%# Eval("ImagenUrl") %>' class="card-img-top" alt="Imagen">
                </div>
            </ItemTemplate>
        </asp:Repeater>--%>
            
        <%--<section class="container g-md-3">
            <div class="form">
                <asp:Label CssClass="form-label" ID="lblCodigo" runat="server" Text="Codigo:"></asp:Label>
                <asp:TextBox ID="txtCodigoProducto" runat="server" CssClass="input" placeholder="Código del Producto"></asp:TextBox>                
                <span class="input-border"></span>
            </div>
            <div class="form">
                <asp:Label CssClass="form-label" ID="lblNombre" runat="server" Text="Nombre:"></asp:Label>
               <asp:TextBox ID="txtNombre" runat="server" CssClass="input"  placeholder="Nombre"></asp:TextBox>
                <span class="input-border"></span>
            </div>
            <div class="form">
                <asp:Label CssClass="form-label" ID="lblDescripcion" runat="server" Text="Descripción:"></asp:Label>
               <asp:TextBox ID="txtDescripcion" runat="server" CssClass="input"  placeholder="Despcipción"></asp:TextBox>
                <span class="input-border"></span>
            </div>
            <div class="form">
                <asp:Label CssClass="form-label" ID="lblPrecio" runat="server" Text="Precio:"></asp:Label>
                <input type="number" placeholder="Precio" step="0.01" min="0" class="input">
            </div>
            <div class="form">
                 <div class="number-left"></div>
                    <asp:Label CssClass="form-label" ID="Label1" runat="server" Text="Precio:"></asp:Label>
                    <input type="number" name="number" class="input number-quantity">
                  <div class="number-right"></div>
            </div>
        </section>--%>
    </main>
</asp:Content>
