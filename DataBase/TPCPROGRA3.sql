CREATE DATABASE TPCGRUPO8A;
GO

USE TPCGRUPO8A;
GO

CREATE TABLE Categorias(
    IdCategoria INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(50) NOT NULL,
)
GO

CREATE TABLE Marcas(
    IdMarca INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(50) NOT NULL,
)
GO

CREATE TABLE Productos (
    IdProducto INT PRIMARY KEY IDENTITY(1,1),
	Codigo	VARCHAR(50) NOT NULL UNIQUE,
    Nombre VARCHAR(100) NOT NULL,
    Descripcion VARCHAR(MAX),
    Precio DECIMAL(18,2) NOT NULL,
    IdCategoria INT FOREIGN KEY REFERENCES Categorias(IdCategoria),
    IdMarca INT FOREIGN KEY REFERENCES Marcas(IdMarca),
)
GO

INSERT INTO Categorias (Nombre)
VALUES 
    ('Remeras'),
    ('Camisas'),
    ('Pantalones'),
    ('Camperas')
GO

INSERT INTO Marcas (Nombre)
VALUES 
    ('Zara'),
    ('H&M'),
    ('Lacoste')
GO

CREATE TABLE Imagenes (
    IdImagen INT PRIMARY KEY IDENTITY(1,1),
    IdProducto INT NOT NULL FOREIGN KEY REFERENCES Productos(IdProducto),
    ImagenUrl VARCHAR(1000) NOT NULL,
)
GO

UPDATE Marcas
	SET Nombre = 'PepeGrillo'
	WHERE IdMarca = 1
UPDATE Marcas
	SET Nombre = 'AliciaEnLasMaravillas'
	WHERE IdMarca = 2
GO

INSERT INTO Productos(Codigo, Nombre, Descripcion, Precio, IdCategoria, IdMarca)
VALUES 
('REM001','Remera Lisa','Cuello Redondo',$23663,1,1),
('REM002','Remera 96','Negra con rayas en las mangas',$56000,1,2),
('CAM001','Camisa Lisa','Algodon',$63000,2,1),
('CAM002','Camisa Lisa','Algodon 90% 10% licra blanca',$23663,2,1),
('PAN001','Pantalon','Impermeable con bolsillos cargo',$86000,3,2),
('PAN002','Pantalon','Bolsillos con cierre y botones',$95660,3,2)
GO

INSERT INTO Imagenes (IdProducto, ImagenUrl)
VALUES 
(1, 'https://http2.mlstatic.com/D_NQ_NP_805457-MLA51076405822_082022-O.webp'),
(2, 'https://http2.mlstatic.com/D_Q_NP_2X_782562-MLA74088243434_012024-E.webp'),
(3, 'https://http2.mlstatic.com/D_Q_NP_2X_833143-MLA70824430319_082023-E.webp'),
(4, 'https://http2.mlstatic.com/D_NQ_NP_601108-MLA31013770285_062019-O.webp'),
(5, 'https://http2.mlstatic.com/D_NQ_NP_637482-MLA79955935763_102024-O.webp'),
(6, 'https://http2.mlstatic.com/D_Q_NP_2X_619261-MLA31356897283_072019-E.webp')
GO

CREATE TABLE Usuarios (
    IdUsuario INT PRIMARY KEY IDENTITY(1,1),
    Apellido VARCHAR(100) NOT NULL,
    Nombre VARCHAR(100) NOT NULL,
    Email VARCHAR(150) NOT NULL UNIQUE,
    Contraseña VARCHAR(150) NOT NULL,
    FechaAlta DATETIME NOT NULL DEFAULT GETDATE(),
    FechaNacimiento DATETIME NULL,
	TipoUsuario INT NOT NULL
);
GO

SET DATEFORMAT 'YMD';
INSERT INTO Usuarios (Apellido, Nombre, Email, Contraseña, TipoUsuario)
VALUES 
('Administrador', 'Admin', 'administrador@email.com', 'admin123', 1)
GO

SET DATEFORMAT 'YMD';
INSERT INTO Usuarios (Apellido, Nombre, Email, Contraseña, FechaAlta, FechaNacimiento, TipoUsuario)
VALUES 
('Simpson', 'Homero', 'h.simpson@email.com', 'homer123', DEFAULT, '1980-05-12', 0)
GO

----nuevas tablas bd para administrador 30/10/24
--agrego stock a tabla productos

ALTER TABLE Productos
ADD Stock INT NOT NULL DEFAULT 0;

UPDATE Productos SET Stock = 7 WHERE IdProducto = 1;
UPDATE Productos SET Stock = 12 WHERE IdProducto = 2;
UPDATE Productos SET Stock = 5 WHERE IdProducto = 3;
UPDATE Productos SET Stock = 10 WHERE IdProducto = 4;
UPDATE Productos SET Stock = 3 WHERE IdProducto = 5;
UPDATE Productos SET Stock = 15 WHERE IdProducto = 6;

--agrego tabla clientes
CREATE TABLE Clientes (
    IdCliente INT PRIMARY KEY IDENTITY(1,1),
	IdUsuario INT FOREIGN KEY REFERENCES Usuarios(IdUsuario),
	Dni VARCHAR(10) NOT NULL,
    Apellido VARCHAR(100) NOT NULL,
    Nombre VARCHAR(100) NOT NULL,
	Direccion VARCHAR(200) NOT NULL,
	Provincia VARCHAR (150) NOT NULL,
	TipoUsuario INT NOT NULL
  
)
 --agrego tabla pedidos
 CREATE TABLE Pedidos (
    IdPedido INT PRIMARY KEY IDENTITY(1,1),
    IdCliente INT NOT NULL FOREIGN KEY REFERENCES Clientes(IdCliente),
    IdProducto INT NOT NULL FOREIGN KEY REFERENCES Productos(IdProducto),
    Cantidad INT NOT NULL,
	Monto MONEY,
    FechaPedido DATETIME NOT NULL DEFAULT GETDATE(),
    Estado VARCHAR(50) NOT NULL
)
 --agrego tabla pedidos por cliente
 CREATE TABLE PedidosPorCliente (
  IdPedidoPorCliente INT PRIMARY KEY IDENTITY(1,1),
  IdPedido INT FOREIGN KEY REFERENCES Pedidos(IdPedido),
  IdCliente INT FOREIGN KEY REFERENCES Clientes(IdCliente)
 )  