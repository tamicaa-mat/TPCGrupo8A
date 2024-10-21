<%@ Page Title="Iniciar Sesion" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IniciarSesion.aspx.cs" Inherits="TPCGrupo8A.IniciarSesion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


     <main class="main-registro">
    <h3 class="h3-iniciar" style="text-align: center; margin-top:10px;">INICIAR SESION</h3>
    <div class="form-container" style="display: flex; align-items: center; flex-direction: column; border: 2px solid; margin: 0 auto; width: 300px; padding: 10px; border-radius: 10px; border-color: darkviolet;">     
        <p>E-mail: <input class = "form"  id="email-registro" type="email"  placeholder="Ingrese e-mail" style="display: flex; justify-content: left; border-radius: 10px; padding: 5px; border: 1px solid; border-color: darkviolet;"></p>
        <p>Clave: <input class = "form" id="pass-registro" type="password" style="display: flex; justify-content: left; border-radius: 10px; padding: 5px; border: 1px solid; border-color: darkviolet;" ></p>
        
        <input class = "form-envia" id="envia" name="envia" type="submit" style="padding: 5px;border-radius: 10px;color: white; background-color: darkviolet; border-style: none;" Value="Iniciar Sesion">
        <p style="padding: 0; margin: 0;">o</p>
        <a href="#" style="margin: 0; padding: 0;">Registrarse</a>
    </div>
</main>
</asp:Content>
