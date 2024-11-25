<%@ Page Title="Gestión de Pedidos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Pedidos.aspx.cs" Inherits="TPCGrupo8A.Pedidos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main class="container">
        <h1 class="text-center my-4">Gestión de Pedidos</h1>

        <!-- Filtro por estado -->
        <div class="mb-4">
            <div class="row g-3">
                <div class="col-md-6">
                    <asp:DropDownList ID="DropDownEstado" runat="server" CssClass="form-select">
                        <asp:ListItem Text="Seleccione un estado" Value="" />
                        <asp:ListItem Text="Pendiente" Value="Pendiente" />
                        <asp:ListItem Text="Entregado" Value="Entregado" />
                    </asp:DropDownList>
                </div>
                <div class="col-md-6 align-self-end">
                    <asp:Button ID="BtnFiltrarPedidos" runat="server" Text="Filtrar pedidos por estado" OnClick="BtnFiltrarPedidos_Click" CssClass="btn-iniciar" />
                </div>
            </div>
        </div>

        <!-- Repeater para mostrar los pedidos -->
        <asp:Repeater ID="RepeaterPedidos" runat="server">
            <HeaderTemplate>
                <table class="table table-bordered table-striped">
                    <thead class="table-dark">
                        <tr>
                            <th>Número de Pedido</th>
                            <th>Fecha</th>
                            <th>Cliente</th>
                            <th>Importe</th>
                            <th>Estado</th>
                            <th>Cambiar Estado</th>
                        </tr>
                    </thead>
                    <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td><asp:Label ID="lblNumeroPedido" runat="server" Text='<%# Eval("Numero") %>'></asp:Label></td>
                    <td><%# Eval("Fecha", "{0:yyyy-MM-dd}") %></td>
                    <td><%# Eval("Cliente") %></td>
                    <td><%# Eval("Importe", "{0:C}") %></td>
                    <td><%# Eval("Estado") %></td>
                    <td>
                    <asp:Button ID="BtnCambiarEstado" runat="server" Text='<%# Eval("Estado").ToString() == "Pendiente" ? "Marcar como Entregado" : "Marcar como Pendiente" %>' CssClass="animated-button" OnClick="BtnCambiarEstado_Click" />
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                    </tbody>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </main>
</asp:Content>
