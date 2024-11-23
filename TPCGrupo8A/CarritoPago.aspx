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

    <h2 class="mb-4">Información de Compra</h2>

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


   <%-- <!--  datos del comprador -->
    <div class="container mt-5">
        <div class="form-group">
            <label for="nombre">Nombre:</label>
            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Ingresa tu nombre" required=""></asp:TextBox>
        </div>

        <div class="form-group">
            <label for="apellido">Apellido:</label>
            <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" placeholder="Ingresa tu apellido" required=""></asp:TextBox>
        </div>

        <div class="form-group">
            <label for="direccion">Dirección:</label>
            <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" placeholder="Ingresa tu dirección" required=""></asp:TextBox>
        </div>

        <div class="form-group">
            <label for="email">Correo Electrónico:</label>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Ingresa tu correo electrónico" TextMode="Email" required=""></asp:TextBox>
        </div>

        <div class="form-group">
            <label for="telefono">Teléfono:</label>
            <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" placeholder="Ingresa tu teléfono" required=""></asp:TextBox>
        </div>--%>



<%--        <h4 class="mt-4">Opciones de Pago</h4>

        <!-- opciones de pago -->
        <div class="form-check">
            <input class="form-check-input" type="checkbox" value="tarjetaCredito" id="tarjetaCredito">
            <label class="form-check-label" for="tarjetaCredito">
                Tarjeta de Crédito
            </label>
        </div>
        <div class="form-check">
            <input class="form-check-input" type="checkbox" value="tarjetaDebito" id="tarjetaDebito">
            <label class="form-check-label" for="tarjetaDebito">
                Tarjeta de Débito
            </label>
        </div>
        <div class="form-check">
            <input class="form-check-input" type="checkbox" value="paypal" id="paypal">
            <label class="form-check-label" for="paypal">
                PayPal
            </label>
        </div>
        <div class="form-check mb-4">
            <input class="form-check-input" type="checkbox" value="transferencia" id="transferencia">
            <label class="form-check-label" for="transferencia">
                Transferencia Bancaria
            </label>
        </div>--%>

        <!-- boton confirmar pago -->
        <%--   <button type="submit" class="btn btn-color btn-block">Confirmar Pago</button>--%>
        <div style="margin: 5px">
            <asp:Button ID="btnConfirmarPago" CssClass="btn btn-color btn-block" Text="Completar la Compra" OnClick="btnConfirmarPago_Click" runat="server" />
        </div>
        <div style="margin: 5px">
            <asp:Button ID="btnSeleccionarOtro" runat="server" CssClass="btn btn-color btn-block" Text="Selecciona otro artículo" OnClick="btnSeleccionarOtro_Click" UseSubmitBehavior="false" />
        </div>
</asp:Content>
