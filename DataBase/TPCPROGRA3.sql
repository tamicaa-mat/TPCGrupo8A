CREATE DATABASE TPCGRUPO8A;
GO


USE TPCGRUPO8A;
GO


CREATE TABLE Talla (
    IdTalla INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(50) NOT NULL
)


CREATE TABLE Categoria(
    IdCategoria INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(50) NOT NULL,
	Stock INT NOT NULL
)


CREATE TABLE Color (
    IdColor INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(50) NOT NULL
)


CREATE TABLE Producto (
    IdProducto INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(100) NOT NULL,
    Descripcion VARCHAR(MAX),
    Precio DECIMAL(18,2) NOT NULL,
    IdCategoria INT FOREIGN KEY REFERENCES Categoria(IdCategoria),
    IdColor INT FOREIGN KEY REFERENCES Color(IdColor),
    IdTalla INT FOREIGN KEY REFERENCES Talla(IdTalla)
)

CREATE TABLE Cliente (
   IdCliente INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(100) NOT NULL,
    Apellido VARCHAR(100) NOT NULL,
    Email VARCHAR(255) NOT NULL,
    Telefono VARCHAR(20),
    Direccion VARCHAR(255)
)


CREATE TABLE Carrito (
    IdCarrito INT PRIMARY KEY IDENTITY(1,1),
    IdCliente INT FOREIGN KEY REFERENCES Cliente(IdCliente),
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE()
)

CREATE TABLE CarritoProducto (
    IdCarrito INT FOREIGN KEY REFERENCES Carrito(IdCarrito),
    IdProducto INT FOREIGN KEY REFERENCES Producto(IdProducto),
    Cantidad INT NOT NULL,
    PRIMARY KEY (IdCarrito , IdProducto)
)


CREATE TABLE Pago (
    IdPago INT PRIMARY KEY IDENTITY(1,1),
    Metodo VARCHAR(50) NOT NULL,
    Monto DECIMAL(18,2) NOT NULL,
    Fecha DATETIME NOT NULL DEFAULT GETDATE()
)


CREATE TABLE Pedido (
    IdPedido INT PRIMARY KEY IDENTITY(1,1),
    Fecha DATETIME NOT NULL DEFAULT GETDATE(),
    Estado VARCHAR(50) NOT NULL,
    IdCliente INT FOREIGN KEY REFERENCES Cliente(IdCliente),
    IdPago INT FOREIGN KEY REFERENCES Pago(IdPago)
)


CREATE TABLE PedidoProducto (
    IdPedido INT FOREIGN KEY REFERENCES Pedido(IdPedido),
    IdProducto INT FOREIGN KEY REFERENCES Producto(IdProducto),
    Cantidad INT NOT NULL,
    Precio DECIMAL(18,2) NOT NULL,
    PRIMARY KEY (IdPedido, IdProducto)
)

INSERT INTO Categoria (Nombre,Stock)
VALUES 
    ('Remeras',100),
    ('Camisas',100),
    ('Pantalones',100),
    ('Camperas',100)