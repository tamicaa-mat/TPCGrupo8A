<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="productos.aspx.cs" Inherits="TPCGrupo8A.productos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


  
    
    <asp:Repeater ID="rptProductos" runat="server">
    <ItemTemplate>
        <div class="card column" style="width: 18rem; display: flex; flex-direction: column; justify-content: space-between; margin:10px ;height: 100%; border-color:gray">
    
           <img src='<%# Eval("Imagen.ImagenUrl") %>' class="card-img-top img-fluid img" alt="Imagen del artículo">


            <div class="card-body">
                <h5 class="card-title"><%# Eval("Nombre") %></h5>
                <p class="card-text">
                    <%# Eval("Descripcion") %><br />
                    Precio: $<%# Eval("Precio") %>
                </p>
                <a href="RegistroCliente.aspx" class="btn btn-primary">Seleccionar</a>
            </div> 
        </div>
    </ItemTemplate>
</asp:Repeater>--%>





</asp:Content>

     