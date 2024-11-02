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



<div class="container mt-5">
    <h2 class="mb-4">Información de Compra</h2>

    <!--  carrito -->
    <div class="carrito-container">
        <div class="carrito-header">Tu Seleccion de Articulos</div>
        <div class="carrito-item">
            <span>Producto 1</span>
            <span>$10.00</span>
        </div>
        <div class="carrito-item">
            <span>Producto 2</span>
            <span>$15.00</span>
        </div>
        <div class="carrito-item">
            <span>Producto 3</span>
            <span>$20.00</span>
        </div>
        <div class="carrito-total">
            Total: $45.00
        </div>
    </div>

    <!--  datos del comprador -->
    <form>
        <div class="form-group">
            <label for="nombre">Nombre:</label>
            <input type="text" class="form-control" id="nombreCompleto" placeholder="Ingresa tu nombre " required>
        </div>
        <div class="form-group">
        <label for="apellido">Apellido:</label>
        <input type="text" class="form-control" id="apellido" placeholder="Ingresa tu apellido" required>
    </div>
        <div class="form-group">
            <label for="dni">Direccion:</label>
            <input type="text" class="form-control" id="dni" placeholder="Ingresa tu direccion" required>
        </div>
        <div class="form-group">
            <label for="email">Correo Electrónico</label>
            <input type="email" class="form-control" id="email" placeholder="Ingresa tu correo electrónico" required>
        </div>
        <div class="form-group">
            <label for="telefono">Teléfono</label>
            <input type="tel" class="form-control" id="telefono" placeholder="Ingresa tu teléfono" required>
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
        <button type="submit" class="btn btn-color btn-block">Confirmar Pago</button>
    </form>
</div>



</asp:Content>
