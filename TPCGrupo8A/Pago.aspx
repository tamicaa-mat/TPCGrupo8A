<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Pago.aspx.cs" Inherits="TPCGrupo8A.Pago" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <div class="container mt-5">
    <h2 class="mb-4">Gestión de Pago</h2>
    <form>
        <!-- Nombre Completo -->
        <div class="mb-3">
            <label for="nombreCompleto" class="form-label">Nombre Completo</label>
            <input type="text" class="form-control" id="nombreCompleto" placeholder="Ingrese su nombre completo" required>
        </div>

        <!-- Dirección -->
        <div class="mb-3">
            <label for="direccion" class="form-label">Dirección</label>
            <input type="text" class="form-control" id="direccion" placeholder="Ingrese su dirección" required>
        </div>

        <!-- Teléfono -->
        <div class="mb-3">
            <label for="telefono" class="form-label">Teléfono</label>
            <input type="tel" class="form-control" id="telefono" placeholder="Ingrese su número de teléfono" required>
        </div>

        <!-- Email -->
        <div class="mb-3">
            <label for="email" class="form-label">Correo Electrónico</label>
            <input type="email" class="form-control" id="email" placeholder="Ingrese su correo electrónico" required>
        </div>

        <!-- Métodos de Pago -->
        <div class="mb-4">
            <label class="form-label">Método de Pago</label>
            <div class="form-check mb-3">
                <input class="form-check-input" type="checkbox" id="debito">
                <label class="form-check-label" for="debito">Débito</label>
                <input type="text" class="form-control mt-2" placeholder="Número de transacción (opcional)">
            </div>
            <div class="form-check mb-3">
                <input class="form-check-input" type="checkbox" id="credito">
                <label class="form-check-label" for="credito">Crédito</label>
                <input type="text" class="form-control mt-2" placeholder="Número de transacción (opcional)">
            </div>
            <div class="form-check mb-3">
                <input class="form-check-input" type="checkbox" id="efectivo">
                <label class="form-check-label" for="efectivo">Efectivo</label>
            </div>
            <div class="form-check mb-3">
                <input class="form-check-input" type="checkbox" id="transferencia">
                <label class="form-check-label" for="transferencia">Transferencia</label>
                <input type="text" class="form-control mt-2" placeholder="Número de transacción">
                <label class="form-label mt-2">Imagen del comprobante</label>
                <input type="file" class="form-control">
            </div>
        </div>

        <!-- Botones -->
        <div class="d-flex justify-content-between">
            <button type="button" class="btn btn-secondary">Volver</button>
            <button type="submit" class="btn" style="background-color: salmon; color: white;">Finalizar Compra</button>
        </div>
    </form>
</div>








</asp:Content>
