<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CarritoPago.aspx.cs" Inherits="TPCGrupo8A.CarritoPago" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .btn-color {
            background-color: #FA8072;
            color: white;
        }

            .btn-color:hover {
                background-color: #E57264;
                color: white;
            }

        .carrito-container {
            border: 1px solid #ddd;
            border-radius: 5px;
            padding: 15px;
            margin-bottom: 20px;
        }

        .carrito-header {
            font-weight: bold;
            font-size: 1.2em;
        }

        .carrito-item {
            display: flex;
            justify-content: space-between;
            padding: 10px 0;
        }

        .carrito-total {
            font-weight: bold;
            font-size: 1.2em;
            text-align: right;
        }
    </style>

    <h2 class="mb-4">Carrito de Productos</h2>

<div class="container mt-5">
    <div class="row">
        <div class="col-md-12 rpt-carrito">
            <div class="scroll-container"> <%-- scroll --%>
                <asp:Repeater ID="RepeaterCarrito" runat="server">
                    <ItemTemplate>
                        <div class="carrito-item">
                            <div class="repeater-imagen">
                                <img src='<%# Eval("Imagenes[0].ImagenUrl", "{0}") %>' class="producto-img" alt="Imagen del artículo">
                            </div>
                            <div class="producto-info">
                                <asp:Label ID="lblNombre" runat="server" Text='<%# Eval("producto.Nombre") %>' CssClass="producto-nombre" />
                                <asp:Label ID="lblPrecio" runat="server" Text='<%# "Precio: $" + Eval("PrecioUnitario") %>' CssClass="producto-precio" />
                            </div>
                            <div class="input-group">
                                <label for="txtCantidad">Cantidad:</label>
                                <asp:TextBox CssClass="form-control spinner-cantidad" ID="txtCantidad" runat="server" Text='<%# Eval("Cantidad") %>' TextMode="Number" Min="1" Max='<%# Eval("Stock") %>' AutoPostBack="true" OnTextChanged="ActualizarTotal" />
                                <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="false" Text="Error: Cantidad no válida." />
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <div class="totalCarrito mt-5">
                <h3>Total Carrito:<asp:Label ID="totalCarritoLabel" runat="server" Text='<%#" $" + Eval("Total") %>'/></h3>
            </div>
        </div>
    </div>
</div>



        <div class="container">
            <div class="row justify-content-center align-items-center">
        <div class="col" style="margin: 5px">
            <asp:Button ID="btnConfirmarPago" CssClass="btn btn-color btn-block" Text="Completar la Compra" OnClick="btnConfirmarPago_Click" runat="server" />
        </div>
        <div class="col" style="margin: 5px">
            <asp:Button ID="btnSeleccionarOtro" runat="server" CssClass="btn btn-color btn-block" Text="Selecciona otro artículo" OnClick="btnSeleccionarOtro_Click" UseSubmitBehavior="false" />
        </div>
                </div>
            </div>
</asp:Content>
