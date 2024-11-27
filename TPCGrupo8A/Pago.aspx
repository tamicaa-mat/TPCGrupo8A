<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Pago.aspx.cs" Inherits="TPCGrupo8A.Pago" %>
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

    <h2 class="mb-4">Finalizar Compra</h2>

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
    <div class="container flex">
        <form>
            <div class="form-group">
    <label for="nombre">Nombre</label>
    <input type="text" class="form-control" id="nombre" placeholder="Ingresa tu nombre">
</div>
<div class="form-group">
    <label for="apellido">Apellido</label>
    <input type="text" class="form-control" id="apellido" placeholder="Ingresa tu apellido">
</div>
<div class="form-group">
    <label for="telefono">Teléfono</label>
    <input type="tel" id="phone" name="phone" pattern="[0-9]{3}-[0-9]{3}-[0-9]{4}" class="form-control" placeholder="Ingresa tu número de teléfono">
</div>
<div class="form-group">
    <label for="direccion">Dirección</label>
    <input type="text" class="form-control" id="direccion" placeholder="Ingresa tu dirección">
</div>
<div class="form-group grid-container row">
    <label for="metodoPago">Método de pago</label>
    <select class="form-control" id="metodoPago">
        <option value="credito">Tarjeta de crédito/débito</option>
        <option value="transferencia">Transferencia</option>
        <option value="efectivo">Efectivo</option>
    </select>
    <div class="col-6">
        <input type="file">
    </div>
</div>
<div class="form-check">
    <input type="checkbox" class="form-check-input" id="terminos">
    <label class="form-check-label" for="terminos">Acepto los términos y condiciones</label>
</div>

        </form>
    </div>



            <div class="container">
                <div class="row justify-content-center align-items-center">
        <div class="col" style="margin: 5px">
            <asp:Button ID="btnVolver2" CssClass="btn btn-color btn-block" Text="Volver al Carrito" OnClick="btnVolver2_Click" runat="server" />
        </div>
        <div class="col" style="margin: 5px">
            <asp:Button ID="btnPagar" runat="server" CssClass="btn btn-color btn-block" Text="Realizar Pago" OnClick="btn_finalizarCompra" UseSubmitBehavior="false" />
            </div>
             </div>
        </div>





</asp:Content>  
  


