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



 --agrego productos a la bd
 INSERT INTO Productos(Codigo, Nombre, Descripcion, Precio, IdCategoria, IdMarca,Stock)
VALUES 
('REM003','Remera Lisa',' Verde Cuello Redondo',$23663,1,1,10),
('REM004','Remera Naranja','Remera naranja,cuello redondo, algodón',$56000,1,2,5),
('CAM003','Camisa','Camisa de algodón 100%, blanca rayas negras',$63000,2,1,3),
('CAM004','Camisa','Camisa de licra 10% y algodón 90% rosa',$23663,2,1,4),
('PAN003','Pantalon','Pantalón jogger azul algodón,bolsillos y cordón',$86000,3,2,6),
('PAN004','Pantalon','Pantalón jogger gris claro,bolsillos y cordón',$95660,3,2,2)
GO
INSERT INTO Imagenes (IdProducto, ImagenUrl)
VALUES 
(7, 'https://http2.mlstatic.com/D_NQ_NP_869993-MLA50771783931_072022-O.webp'),  
(8, 'https://http2.mlstatic.com/D_NQ_NP_712215-MLA50787394463_072022-O.webp'),
(9, 'https://http2.mlstatic.com/D_NQ_NP_880439-MLA79794285038_102024-O.webp'),
(10, 'https://http2.mlstatic.com/D_NQ_NP_681299-MLA72721107221_112023-O.webp'),
(11, 'https://http2.mlstatic.com/D_NQ_NP_975990-MLA69552048974_052023-O.webp'),
(12, 'https://http2.mlstatic.com/D_NQ_NP_634469-MLA77029249022_062024-O.webp')
GO

 INSERT INTO Productos(Codigo, Nombre, Descripcion, Precio, IdCategoria, IdMarca)
VALUES 
('REM005','Remera Lisa','Cuello Redondo, algodon 90%',$23663,1,3),
('REM006','Remera Lacoste','Nueva Temporada',$126000,1,3),
('CAM005','Camisa Lacoste','Camisa de algodon ',$33000,2,3),
('CAM006','Camisa Lacoste','Camisa cuadrille de invierno',$23663,2,3),
('PAN005','Pantalon','Pantalon algodon verde',$81200,3,3),
('PAN006','Pantalon','Bolsillos con cierre y botones',$156560,3,3)
GO
INSERT INTO Imagenes (IdProducto, ImagenUrl)
VALUES 
(13, 'https://http2.mlstatic.com/D_NQ_NP_984496-MLA43491010868_092020-O.webp'),
(14, 'https://http2.mlstatic.com/D_NQ_NP_654564-MLA43486966389_092020-O.webp'),
(15, 'https://http2.mlstatic.com/D_NQ_NP_633055-MLA43150078496_082020-O.webp'),
(16, 'https://http2.mlstatic.com/D_NQ_NP_640105-MLA43149870463_082020-O.webp'),
(17, 'https://http2.mlstatic.com/D_NQ_NP_634003-MLA80174982005_102024-O.webp'),
(18, 'https://http2.mlstatic.com/D_NQ_NP_634469-MLA77029249022_062024-O.webp')
GO
--debo cambiar la descripcion de las productos con los siguientes codigos
UPDATE Productos SET Descripcion = 'Remera de algodón con cuello redondo color verde, algodon' WHERE Codigo = 'REM003';
UPDATE Productos SET Descripcion = 'Remera naranja,cuello redondo, algodón' WHERE Codigo = 'REM004';
UPDATE Productos SET Descripcion = 'Camisa de algodón 100%, blanca rayas negras' WHERE Codigo = 'CAM003';
UPDATE Productos SET Descripcion = 'Camisa de licra 10% y algodón 90% rosa' WHERE Codigo = 'CAM004';
UPDATE Productos SET Descripcion = 'Pantalón jogger azul algodón,bolsillos y cordón' WHERE Codigo = 'PAN003';
UPDATE Productos SET Descripcion = 'Pantalón jogger gris claro,bolsillos y cordón' WHERE Codigo = 'PAN004';
GO

UPDATE Productos SET Stock = 7 WHERE IdProducto = 7;
UPDATE Productos SET Stock = 12 WHERE IdProducto = 8;
UPDATE Productos SET Stock = 5 WHERE IdProducto = 9;
UPDATE Productos SET Stock = 10 WHERE IdProducto = 10;
UPDATE Productos SET Stock = 3 WHERE IdProducto = 11;
UPDATE Productos SET Stock = 15 WHERE IdProducto = 12;
UPDATE Productos SET Stock = 13 WHERE IdProducto = 13;
UPDATE Productos SET Stock = 12 WHERE IdProducto = 14;
UPDATE Productos SET Stock = 5 WHERE IdProducto = 15;
UPDATE Productos SET Stock = 10 WHERE IdProducto = 16;
UPDATE Productos SET Stock = 3 WHERE IdProducto = 17;
UPDATE Productos SET Stock = 15 WHERE IdProducto = 18;

DELETE FROM Imagenes 
WHERE IdImagen >= 7 
GO

DELETE FROM Productos 
WHERE IdProducto >= 7 
GO
--reinicio los id-----------------------------------------------------------------------------------------------
DELETE FROM Imagenes;
DELETE FROM Productos;

DBCC CHECKIDENT ('Productos', RESEED, 0);
DBCC CHECKIDENT ('Imagenes', RESEED, 0);

INSERT INTO Productos(Codigo, Nombre, Descripcion, Precio, IdCategoria, IdMarca,Stock)
VALUES 
('REM001','Remera Lisa','Cuello Redondo',$23663,1,1,10),
('REM002','Remera 96','Negra con rayas en las mangas',$56000,1,2,8),
('CAM001','Camisa Lisa','Algodon',$63000,2,1,5),
('CAM002','Camisa Lisa','Algodon 90% 10% licra blanca',$23663,2,1,3),
('PAN001','Pantalon','Impermeable con bolsillos cargo',$86000,3,2,2),
('PAN002','Pantalon','Bolsillos con cierre y botones',$95660,3,2,4),
('REM003','Remera Lisa',' Verde Cuello Redondo',$23663,1,1,10),
('REM004','Remera Naranja','Remera naranja,cuello redondo, algodón',$56000,1,2,5),
('CAM003','Camisa','Camisa de algodón 100%, blanca rayas negras',$63000,2,1,3),
('CAM004','Camisa','Camisa de licra 10% y algodón 90% rosa',$23663,2,1,4),
('PAN003','Pantalon','Pantalón jogger azul algodón,bolsillos y cordón',$86000,3,2,6),
('PAN004','Pantalon','Pantalón jogger gris claro,bolsillos y cordón',$95660,3,2,2),
('REM005','Remera Lisa','Cuello Redondo, algodon 90%',$23663,1,3,5),
('REM006','Remera Lacoste','Nueva Temporada',$126000,1,3,6),
('CAM005','Camisa Lacoste','Camisa de algodon ',$33000,2,3,2),
('CAM006','Camisa Lacoste','Camisa cuadrille de invierno',$23663,2,3,3),
('PAN005','Pantalon','Pantalon algodon verde',$81200,3,3,9),
('PAN006','Pantalon','Bolsillos con cierre y botones',$156560,3,3,4)

GO


INSERT INTO Imagenes (IdProducto, ImagenUrl)
VALUES 
(1, 'https://http2.mlstatic.com/D_NQ_NP_805457-MLA51076405822_082022-O.webp'),
(2, 'https://http2.mlstatic.com/D_Q_NP_2X_782562-MLA74088243434_012024-E.webp'),
(3, 'https://http2.mlstatic.com/D_Q_NP_2X_833143-MLA70824430319_082023-E.webp'),
(4, 'https://http2.mlstatic.com/D_NQ_NP_601108-MLA31013770285_062019-O.webp'),
(5, 'https://http2.mlstatic.com/D_NQ_NP_637482-MLA79955935763_102024-O.webp'),
(6, 'https://http2.mlstatic.com/D_Q_NP_2X_619261-MLA31356897283_072019-E.webp'),
(7, 'https://http2.mlstatic.com/D_NQ_NP_869993-MLA50771783931_072022-O.webp'),  
(8, 'https://http2.mlstatic.com/D_NQ_NP_712215-MLA50787394463_072022-O.webp'),
(9, 'https://http2.mlstatic.com/D_NQ_NP_880439-MLA79794285038_102024-O.webp'),
(10, 'https://http2.mlstatic.com/D_NQ_NP_681299-MLA72721107221_112023-O.webp'),
(11, 'https://http2.mlstatic.com/D_NQ_NP_975990-MLA69552048974_052023-O.webp'),
(12, 'https://http2.mlstatic.com/D_NQ_NP_634469-MLA77029249022_062024-O.webp'),
(13, 'https://http2.mlstatic.com/D_NQ_NP_984496-MLA43491010868_092020-O.webp'),
(14, 'https://http2.mlstatic.com/D_NQ_NP_654564-MLA43486966389_092020-O.webp'),
(15, 'https://http2.mlstatic.com/D_NQ_NP_633055-MLA43150078496_082020-O.webp'),
(16, 'https://http2.mlstatic.com/D_NQ_NP_640105-MLA43149870463_082020-O.webp'),
(17, 'https://http2.mlstatic.com/D_NQ_NP_634003-MLA80174982005_102024-O.webp'),
(18, 'https://http2.mlstatic.com/D_NQ_NP_634469-MLA77029249022_062024-O.webp')

ALTER TABLE Clientes
ADD Telefono VARCHAR(20) NOT NULL DEFAULT 'Sin especificar';

------------------------------------------------trigger para eliminar producto

INSERT INTO Productos(Codigo, Nombre, Descripcion, Precio, IdCategoria, IdMarca,Stock)
VALUES 
('probandotrigger033','Remera Lisa',' Verde Cuello Redondo',$23663,1,1,10)
INSERT INTO Imagenes (IdProducto, ImagenUrl)
VALUES 
(19, 'https://http2.mlstatic.com/D_NQ_NP_805457-MLA51076405822_082022-O.webp')

DELETE FROM Productos WHERE IdProducto=19


CREATE TRIGGER EliminarProductoYRelaciones ON Productos
INSTEAD OF DELETE
AS
BEGIN
    BEGIN TRY
        IF @@TRANCOUNT = 0
            BEGIN TRANSACTION

       
        DECLARE @IDsProductos TABLE (ID INT)
        INSERT INTO @IDsProductos (ID)
        SELECT IDPRODUCTO FROM DELETED


        DELETE FROM Imagenes 
        WHERE IdProducto IN (SELECT ID FROM @IDsProductos)

        DELETE FROM Productos 
        WHERE IDPRODUCTO IN (SELECT ID FROM @IDsProductos)

    
        IF @@TRANCOUNT > 0
            COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
   
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        PRINT 'Error: Ocurrió un problema al eliminar el producto y sus imágenes.';
    END CATCH
END
