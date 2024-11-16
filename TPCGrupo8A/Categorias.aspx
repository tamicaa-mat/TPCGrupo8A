<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Categorias.aspx.cs" Inherits="TPCGrupo8A.Categorias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
        <h1 class="tex-center my-4">Categorías</h1>
    </div>
    <div class="mb-3">
        <button type="button" class="btn-iniciar" style="max-width: 200px" data-bs-toggle="modal" data-bs-target="#modalAgregar">Agregar Categoría</button>
        <asp:Label ID="lblMensaje" runat="server" ForeColor="#cb7298" Visible="false"></asp:Label>
        <asp:Label ID="lblExito" runat="server" visible="false"></asp:Label>

    </div>
    <%-- Modal Categoría Agregar --%>
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
                        <asp:TextBox CssClass="form-control" ID="txtNombreCategoria" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnGuardarCategoria" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnGuardarCategoria_OnClick" />
                </div>
            </div>
        </div>
    </div>

     <div class="mb-3" style="overflow-x: auto">
     <div class="gv-container"></div>

 <asp:GridView ID="GVCategorias" runat="server" AutoGenerateColumns="False" 
              OnRowCommand="GVCategorias_OnRowCommand" 
              OnRowDataBound="GVCategorias_RowDataBound" 
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
    <asp:HiddenField ID="hdnCategoriaId" runat="server" />
    <%-- Modal Editar categoría --%>
    <div class="modal fade" id="modalEditar" tabindex="-1" aria-labelledby="modalEditarLabel" aria-hidden="true" data-bs-backdrop="static" data-bs-keyboard="false">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="modalEditarLabel">Editar Categoría</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="mb-3">
                        <asp:Label CssClass="form-label" ID="lblEditarCategoria" runat="server" Text="Nombre:"></asp:Label>
                        <asp:TextBox CssClass="form-control" ID="txtNombreCategoriaEditar" onkeypress="return event.keyCode != 13;" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnEditarCategoria" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnEditarCategoria_OnClick" />
                </div>
            </div>
        </div>
    </div>
    <!-- Botones de Editar y Eliminar fuera del GridView -->
    <div class="mb-3">
        <button class="animated-button" type="button" onclick="$('#modalEditar').modal('show');">Editar Categoría</button>
        <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger" OnClick="btnEliminar_OnClick" />
    </div>

</asp:Content>
