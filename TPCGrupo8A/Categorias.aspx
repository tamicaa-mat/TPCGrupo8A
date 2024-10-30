<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Categorias.aspx.cs" Inherits="TPCGrupo8A.Categorias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
    <h1 class="tex-center my-4">Categorías</h1>
    </div>
    <div class="mb-3">
        <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#modalAgregar">Agregar Categoría</button>
    </div>
    <%--MODAL--%>
    <div class="modal fade" id="modalAgregar" tabindex="-1" aria-labelledby="modalAgregarLabel" aria-hidden="true" data-bs-backdrop="static" data-bs-keyboard="false">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="modalAgregarLabel">Agregar Nueva Categoría</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                        <div class="mb-3">
                            <asp:Label CssClass="form-label" ID="lblNombreCategoria" runat="server" Text="Nombre:"></asp:Label>
                            <asp:TextBox CssClass="form-control" ID="txtNombreCategoria" runat="server" required=""></asp:TextBox> 
                        </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnGuardarCategoria" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnGuardarCategoria_OnClick" />
                </div>
            </div>
        </div>
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
