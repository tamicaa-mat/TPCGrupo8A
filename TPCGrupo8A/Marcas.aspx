﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Marcas.aspx.cs" Inherits="TPCGrupo8A.Marcas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
        <h1 class="tex-center my-4">Marcas</h1>
    </div>
    <div class="mb-3">
        <button type="button" class="btn-iniciar" style="max-width: 200px" data-bs-toggle="modal" data-bs-target="#modalAgregar">Agregar Marca</button>
        <asp:Label ID="lblMensaje" runat="server" ForeColor="#cb7298" Visible="false"></asp:Label>
        <asp:Label ID="lblExito" runat="server" Visible="false"></asp:Label>
    </div>
    <%-- Modal Marca Agregar --%>
    <div class="modal fade" id="modalAgregar" tabindex="-1" aria-labelledby="modalAgregarLabel" aria-hidden="true" data-bs-backdrop="static" data-bs-keyboard="false">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="modalAgregarLabel">Agregar Nueva Marca</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="mb-3">
                        <asp:Label CssClass="form-label" ID="lblNombreMarca" runat="server" Text="Nombre:"></asp:Label>
                        <asp:TextBox CssClass="form-control" ID="txtNombreMarca" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>

                    <asp:Button ID="btnGuardarMarca" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnGuardarMarca_OnClick" />

                </div>
            </div>
        </div>
    </div>

    <div class="mb-3" style="overflow-x: auto">

        <div class="gv-container"></div>
        <asp:GridView ID="GVMarca" runat="server" AutoGenerateColumns="False"
            OnRowCommand="GVMarca_OnRowCommand"
            OnRowDataBound="GVMarca_RowDataBound"
            DataKeyNames="ID" CssClass="list-categorias">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="ID" Visible="false" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                <%--<asp:ButtonField ButtonType="Button" CommandName="Seleccionar" Text="✔️" />--%>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="btnSeleccionar" runat="server" CommandName="Seleccionar"
                            CommandArgument='<%# Container.DataItemIndex %>' Text="✓"
                            CssClass="btn-transparente" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:ButtonField ButtonType="Button" CommandName="Habilitar" Text="Habilitar" />
            </Columns>
        </asp:GridView>
    </div>

    <asp:HiddenField ID="hdnMarcaId" runat="server" />
    <%-- Modal Editar Marca --%>
    <div class="modal fade" id="modalEditar" tabindex="-1" aria-labelledby="modalEditarLabel" aria-hidden="true" data-bs-backdrop="static" data-bs-keyboard="false">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="modalEditarLabel">Editar Marca</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="mb-3">
                        <asp:Label CssClass="lbldatos" ID="lblEditarMarca" runat="server" Text="Nombre:"></asp:Label>
                        <asp:TextBox CssClass="txtdatos" ID="txtNombreMarcaEditar" onkeypress="return event.keyCode != 13;" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnEditarMarca" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnEditarMarca_OnClick" />
                </div>
            </div>
        </div>
    </div>
    <!-- Botones de Editar y Eliminar fuera del GridView -->
    <div class="mb-3">
        <button class="animated-button" type="button" onclick="$('#modalEditar').modal('show');">Editar Marca</button>
        <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger" OnClick="btnEliminar_Click" />
    </div>
</asp:Content>
