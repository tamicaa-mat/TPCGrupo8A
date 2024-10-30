<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Categorias.aspx.cs" Inherits="TPCGrupo8A.Categorias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
    <h1 class="tex-center my-4">Categorías</h1>
    </div>
    <div class="mb-3">
        <asp:Button ID="btnAgregar" runat="server" Text="Agregar Categoría" CssClass="btn btn-primary" OnClick="btnAgregar_OnClick" />
    </div>

    <asp:Repeater ID="RptCategorias" runat="server">
        <ItemTemplate>
            <div class="list-group">
                <button type="button" class="list-group-item list-group-item-action">
                    <%# Eval("Nombre") %>
                </button>
            </div>
        </ItemTemplate>
    </asp:Repeater>

    <div class="mt-3">
        <asp:Button ID="btnEditar" runat="server" Text="Editar" CssClass="btn btn-primary" OnClick="btnEditar_OnClick" />
        <asp:Button ID="btnBorrar" runat="server" Text="Borrar" CssClass="btn btn-danger" OnClick="btnBorrar_OnClick" />
    </div>
</asp:Content>
