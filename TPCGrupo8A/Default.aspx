﻿<%@ Page Title="Tienda" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TPCGrupo8A._Default" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


    <main>
      
        <div class="img-card">
            <img src="./assets/fondo.jpeg" alt="fondo" style="width: 100%; height: auto;" />
            <div class="container-btn-primavera">
                <a href="#" class="btn-primavera">OFERTAS DE PRIMAVERA</a>
            </div>
        </div>

        <div class="container-productos">
            <h3 id="titulo-productos" style="text-align: center; margin-top: 25px;">-- LO NUEVO --</h3>
            <div class="container text-center">
                <div class="row">
                    <div class="col">
                        <img src="./assets/error.jpg" alt="prueba" height="205px" />
                    </div>
                    <div class="col">
                        <img src="./assets/error.jpg" alt="prueba" height="205px" />
                    </div>
                    <div class="col">
                        <img src="./assets/error.jpg" alt="prueba" height="205px" />
                    </div>
                </div>
            </div>
        </div>
        <hr />
        <div class="container-contacto" id="contacto">
            <h4>CONTACTANOS</h4>
            <div class="formulario-1">
            <div class="mb-3">
                <label for="inputNombre" class="form-label">Nombre</label>
                <input type="text" class="form-control" id="inputNombre" placeholder="Nombre">
            </div>

            <div class="mb-3">
                <label for="inputCelular" class="form-label">Celular</label>
                <input type="tel" class="form-control" id="inputCelular" placeholder="">
            </div>
            </div>
            <div class="formulario-2">
            <div class="mb-3">
                <label for="inputCorreo" class="form-label">Correo</label>
                <input type="email" class="form-control" id="inputCorreo" placeholder="nombre@ejemplo.com">
            </div>

            <div class="mb-3">
                <label for="inputMensaje" class="form-label">Mensaje</label>
                <textarea class="form-control" id="inputMensaje" rows="3" placeholder="Escribe tu mensaje aquí"></textarea>
            </div>
            </div>
            <div class="mb-3">
                <button type="submit" class="btn btn-primary">Enviar</button>
            </div>
            
        </div>
    </main>
</asp:Content>


