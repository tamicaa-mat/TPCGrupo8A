<%@ Page Title="Gestión de Pedidos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Pedidos.aspx.cs" Inherits="TPCGrupo8A.Pedidos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main class="container">
        <h1 class="text-center my-4">Gestión de Pedidos</h1>

        <!-- Filtro por cliente -->
        <div class="mb-4">
            <div class="row g-3">
                <div class="col-md-6">
                    <label for="cliente" class="form-label">Seleccione Cliente:</label>
                    <select id="cliente" class="form-select" required>
                        <option value="" selected>Seleccione un cliente</option>
                        <option value="1">Cliente 1</option>
                        <option value="2">Cliente 2</option>
                        <option value="3">Cliente 3</option>
                    </select>
                </div>
                <div class="col-md-6 align-self-end">
                    <button type="button" id="btnFiltrar" class="btn btn-primary w-100">Filtrar Pedidos</button>
                </div>
            </div>
        </div>

        <!-- Tabla dinámica -->
        <div class="table-responsive">
            <table id="tablaPedidos" class="table table-bordered table-striped">
                <thead class="table-dark">
                    <tr>
                        <th>Número de Pedido</th>
                        <th>Fecha</th>
                        <th>Cliente</th>
                        <th>Importe</th>
                        <th>Estado</th>
                    </tr>
                </thead>
                <tbody>
                    <!--  pedidos dinámicamente -->
                    <tr>
                        <td colspan="5" class="text-center text-muted">Seleccione un cliente para ver los pedidos</td>
                    </tr>
                </tbody>
            </table>
        </div>
    </main>

    <script>
        // Simulación de datos de pedidos
        const pedidos = [
            { numero: 101, fecha: "2024-11-01", cliente: "1", importe: 1500, estado: "Pendiente" },
            { numero: 102, fecha: "2024-11-03", cliente: "2", importe: 2000, estado: "Completado" },
            { numero: 103, fecha: "2024-11-05", cliente: "1", importe: 800, estado: "Pendiente" },
            { numero: 104, fecha: "2024-11-07", cliente: "3", importe: 1200, estado: "Completado" }
        ];

        const tablaPedidos = document.getElementById("tablaPedidos").querySelector("tbody");
        const btnFiltrar = document.getElementById("btnFiltrar");
        const selectCliente = document.getElementById("cliente");

        // botón Filtrar
        btnFiltrar.addEventListener("click", () => {
            const clienteSeleccionado = selectCliente.value;
            tablaPedidos.innerHTML = ""; // Limpiar la tabla

            if (!clienteSeleccionado) {
                tablaPedidos.innerHTML = `
                    <tr>
                        <td colspan="5" class="text-center text-danger">Por favor, seleccione un cliente.</td>
                    </tr>`;
                return;
            }

            // Filtrar pedidos por cliente seleccionado
            const pedidosFiltrados = pedidos.filter(p => p.cliente === clienteSeleccionado);

            if (pedidosFiltrados.length === 0) {
                tablaPedidos.innerHTML = `
                    <tr>
                        <td colspan="5" class="text-center text-warning">No hay pedidos para este cliente.</td>
                    </tr>`;
                return;
            }

            // Mostrar pedidos en la tabla
            pedidosFiltrados.forEach(pedido => {
                tablaPedidos.innerHTML += `
                    <tr>
                        <td>${pedido.numero}</td>
                        <td>${pedido.fecha}</td>
                        <td>Cliente ${pedido.cliente}</td>
                        <td>$${pedido.importe}</td>
                        <td>${pedido.estado}</td>
                    </tr>`;
            });
        });
    </script>
</asp:Content>
