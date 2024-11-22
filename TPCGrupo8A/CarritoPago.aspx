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

    <%-- <asp:Repeater ID="RepeaterCarrito" runat="server">
                <ItemTemplate>
                    <div class="producto">
                        <h4><%# Eval("Producto.Nombre") %></h4>
                        <%--<img src='<%# Eval("Imagen.ImagenUrl) %>' alt="Producto" width="100" />--%>
    <%--en proceso--%>
    <%--<asp:Label ID="lblImagenUrl" runat="server" Text='<%# Eval("Producto.imagen") %>' />--%>
    <%--  <p>Precio: $<%# Eval("PrecioUnitario") %></p>

                        <div class="input-group mb-3 lbldatos">
                            <asp:Label CssClass="lbldatos" ID="lblStock" runat="server" Text="Cantidad:">
                                <asp:TextBox CssClass="txtdatos" ID="txtCantidad" runat="server" Text='<%# Eval("Cantidad") %>' TextMode="Number" Step="1" />
                            </asp:Label>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>--%>


    <div class="container mt-5">
        <div class="row">
            <div class="col-md-12">
                <asp:Repeater ID="RepeaterCarrito" runat="server">
                    <itemtemplate>
                        <div class="carrito-item">
                            <div>
                                <asp:Label ID="lblNombre" runat="server" Text='<%# Eval("producto.Nombre") %>' />
                                <asp:Label ID="lblPrecio" runat="server" Text='<%# "Precio: $" + Eval("producto.Precio") %>' />
                            </div>
                            <div class="input-group">
                                <label for="txtCantidad">Cantidad:</label>
                                <asp:TextBox CssClass="form-control" ID="txtCantidad" runat="server" Text='<%# Eval("Cantidad") %>' TextMode="Number" Min="0" Max='<%# Eval("Stock") %>' />
                                <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="false" Text="Error: Cantidad no válida." />
                            </div>
                        </div>
            </div>
        </div>
        </ItemTemplate>
</asp:Repeater>

    </div>
    </div>



    </div>
</div>



            <div class="totalCarrito">
                <h3>Total:</h3>
                <asp:Label ID="totalCarritoLabel" runat="server" Text="Total: $0.00" />
            </div>
    </div>
  

             <div class="totalCarrito mt-4">
                 <h3>Total Carrito: $<span id="totalCarrito" runat="server"></span></h3>
                 <%--     <asp:Button Text="Finalizar Compra" runat="server" OnClick="FinalizarCompra_Click" />--%>
             </div>


    <!--  datos del comprador -->
    <form>
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
        </div>



        <h4 class="mt-4">Opciones de Pago</h4>

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
        </div>

        <!-- boton confirmar pago -->
        <%--   <button type="submit" class="btn btn-color btn-block">Confirmar Pago</button>--%>
        <asp:Button ID="btnConfirmarPago" CssClass="btn btn-color btn-block" Text="Confirmar Pago" OnClick="btnConfirmarPago_Click" runat="server" />




    </form>

    <!-- boton seelcciona otro articulo -->
    <asp:Button ID="btnSeleccionarOtro" runat="server" CssClass="btn btn-color btn-block" Text="Selecciona otro artículo" OnClick="btnSeleccionarOtro_Click" UseSubmitBehavior="false" />





</asp:Content>
