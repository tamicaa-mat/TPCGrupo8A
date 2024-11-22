<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="TPCGrupo8A.Error" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">



    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css">
<link href="site.css" rel="stylesheet" />
<div class="d-flex justify-content-center align-items-center vh-100">
    <div class="text-center p-5 rounded shadow-lg" style="background-color: #f8d7da; max-width: 500px;">
        <div class="containerExit mb-3">
            <i class="fa fa-times-circle-o" aria-hidden="true" style="font-size: 3rem; color: #dc3545;"></i>
            <asp:Label ID="MensajeError" CssClass="textM display-4" runat="server" Text="Ocurrió un error inesperado."></asp:Label>
        </div>
        <div>
            <a href="Default.aspx" class="link-primary btn btn-link" style="text-decoration: none; font-size: 1.25rem;">Volver</a>
        </div>
    </div>
</div>














</asp:Content>
