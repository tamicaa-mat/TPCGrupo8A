CREATE DATABASE TPCGRUPO8A;
GO

USE TPCGRUPO8A;
GO

CREATE TABLE Categorias(
    IdCategoria INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(50) NOT NULL
);
GO

CREATE TABLE Marcas(
    IdMarca INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(50) NOT NULL
);
GO

CREATE TABLE Productos (
    IdProducto INT PRIMARY KEY IDENTITY(1,1),
	Codigo VARCHAR(50) NOT NULL UNIQUE,
    Nombre VARCHAR(100) NOT NULL,
    Descripcion VARCHAR(MAX),
    Precio DECIMAL(18,2) NOT NULL,
	Estado BIT NOT NULL DEFAULT 1,
    IdCategoria INT FOREIGN KEY REFERENCES Categorias(IdCategoria),
    IdMarca INT FOREIGN KEY REFERENCES Marcas(IdMarca)
);
GO

CREATE TABLE Imagenes (
    IdImagen INT PRIMARY KEY IDENTITY(1,1),
    IdProducto INT NOT NULL FOREIGN KEY REFERENCES Productos(IdProducto),
    ImagenUrl VARCHAR(1000) NOT NULL
);
GO

CREATE TABLE Usuarios (
    IdUsuario INT PRIMARY KEY IDENTITY(1,1),
    Apellido VARCHAR(100) NOT NULL,
    Nombre VARCHAR(100) NOT NULL,
    Email VARCHAR(150) NOT NULL UNIQUE,
    Contrase�a VARCHAR(150) NOT NULL,
    FechaAlta DATETIME NOT NULL DEFAULT GETDATE(),
    FechaNacimiento DATETIME NULL,
	TipoUsuario INT NOT NULL
);
GO

ALTER TABLE Productos
ADD Stock INT NOT NULL DEFAULT 0;
GO

CREATE TABLE Clientes (
    IdCliente INT PRIMARY KEY IDENTITY(1,1),
	IdUsuario INT FOREIGN KEY REFERENCES Usuarios(IdUsuario),
	Dni VARCHAR(10) NOT NULL,
    Apellido VARCHAR(100) NOT NULL,
    Nombre VARCHAR(100) NOT NULL,
	Direccion VARCHAR(200) NOT NULL,
	Provincia VARCHAR(150) NOT NULL,
	TipoUsuario INT NOT NULL
);
GO

CREATE TABLE Pedidos (
    IdPedido INT PRIMARY KEY IDENTITY(1,1),
    IdCliente INT NOT NULL FOREIGN KEY REFERENCES Clientes(IdCliente),
    IdProducto INT NOT NULL FOREIGN KEY REFERENCES Productos(IdProducto),
    Cantidad INT NOT NULL,
	Monto MONEY,
    FechaPedido DATETIME NOT NULL DEFAULT GETDATE(),
    Estado VARCHAR(50) NOT NULL
);
GO

CREATE TABLE PedidosPorCliente (
    IdPedidoPorCliente INT PRIMARY KEY IDENTITY(1,1),
    IdPedido INT FOREIGN KEY REFERENCES Pedidos(IdPedido),
    IdCliente INT FOREIGN KEY REFERENCES Clientes(IdCliente)
);  
GO

CREATE TABLE Carrito (
    IdCarrito INT PRIMARY KEY IDENTITY(1,1),
    IdCliente INT NOT NULL FOREIGN KEY REFERENCES Clientes(IdCliente)
);
GO

CREATE TABLE DetallesCarrito (
    IdDetalle INT PRIMARY KEY IDENTITY(1,1),
    IdCarrito INT NOT NULL FOREIGN KEY REFERENCES Carrito(IdCarrito),
    IdProducto INT NOT NULL FOREIGN KEY REFERENCES Productos(IdProducto),
    Cantidad INT NOT NULL,
    Precio DECIMAL(18,2) NOT NULL
);
GO

CREATE TRIGGER EliminarProductoYRelaciones ON Productos
INSTEAD OF DELETE
AS
BEGIN
    BEGIN TRY
        IF @@TRANCOUNT = 0
            BEGIN TRANSACTION

        DECLARE @IDsProductos TABLE (ID INT);
        INSERT INTO @IDsProductos (ID)
        SELECT IDPRODUCTO FROM DELETED;

        DELETE FROM Imagenes 
        WHERE IdProducto IN (SELECT ID FROM @IDsProductos);

        DELETE FROM Productos 
        WHERE IDPRODUCTO IN (SELECT ID FROM @IDsProductos);

        IF @@TRANCOUNT > 0
            COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        PRINT 'Error: Ocurri� un problema al eliminar el producto y sus im�genes.';
    END CATCH
END;
GO

CREATE PROCEDURE EliminacionLogicaProducto @idProducto INT
AS 
BEGIN
	BEGIN TRY 
		UPDATE Productos
		SET Estado = 0
		WHERE IdProducto = @idProducto;
		PRINT 'PRODUCTO ELIMINADO (ELIMINACI�N LOGICA)'
	END TRY
	BEGIN CATCH
		PRINT 'ERROR: NO SE PUDO ELIMINAR EL PRODUCTO'
	END CATCH
END;
GO

ALTER TABLE Clientes
ADD Telefono VARCHAR(20) NOT NULL DEFAULT 'Sin especificar'
GO
 -----------------------------8/11 ----------------------------
ALTER TABLE Marcas
ADD Estado BIT  NOT NULL DEFAULT '1'
GO

ALTER TABLE Categorias
ADD Estado BIT  NOT NULL DEFAULT '1'
GO

---------------------PARA ELIMINAR MARCAS Y AGREGAR--------------------
CREATE PROCEDURE SP_EliminacionLogicaMarcas(@IDMARCA INT)
AS
BEGIN
    BEGIN TRY 
        BEGIN TRANSACTION

        -- Verificar si la marca con el ID proporcionado existe
        IF EXISTS(SELECT 1 FROM Marcas WHERE IdMarca = @IDMARCA)
        BEGIN
            -- Actualizar el estado de la marca para realizar la eliminaci�n l�gica
            UPDATE Marcas 
            SET Estado = 0  
            WHERE IdMarca = @IDMARCA;

            PRINT 'Marca eliminada l�gicamente con �xito.';
        END
        ELSE
        BEGIN
            PRINT 'ERROR: No hay una marca asociada a ese ID';
        END

        -- Confirmar la transacci�n si no hubo problemas
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH 
        -- Deshacer la transacci�n en caso de error y levantar un error personalizado
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        RAISERROR ('ERROR: No se pudo eliminar la marca', 16, 1);
    END CATCH
END;
GO

--------------trigger para insertar una marca y poner el estado en 1
 
CREATE TRIGGER Al_Agregar_Marca ON Marcas

AFTER INSERT 
AS
BEGIN

DECLARE @IDMARCAAGREGADA INT;

SELECT @IDMARCAAGREGADA=IdMarca FROM INSERTED

 UPDATE Marcas 
            SET Estado = 1  
            WHERE IdMarca = @IDMARCAAGREGADA;

END
GO
-------------------------PARA ELIMINAR Y AGREGAR CATEGORIAS----------------------------

CREATE PROCEDURE SP_EliminacionLogicaCategorias(@IDCATEGORIA INT)
AS
BEGIN
    BEGIN TRY 
        BEGIN TRANSACTION

        -- Verificar si la marca con el ID proporcionado existe
        IF EXISTS(SELECT 1 FROM Categorias WHERE IdCategoria = @IDCATEGORIA)
        BEGIN
            -- Actualizar el estado de la marca para realizar la eliminaci�n l�gica
            UPDATE Categorias 
            SET Estado = 0  
            WHERE IdCategoria = @IDCATEGORIA;

            PRINT 'Categoria eliminada l�gicamente con �xito.';
        END
        ELSE
        BEGIN
            PRINT 'ERROR: No hay una Categoria asociada a ese ID';
        END

        -- Confirmar la transacci�n si no hubo problemas
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH 
        -- Deshacer la transacci�n en caso de error y levantar un error personalizado
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        RAISERROR ('ERROR: No se pudo eliminar la Categoria', 16, 1);
    END CATCH
END;
GO

CREATE TRIGGER Al_Agregar_Categoria ON Categorias

AFTER INSERT 
AS
BEGIN

DECLARE @IDCATEGORIAAGREGADA INT;

SELECT @IDCATEGORIAAGREGADA=IdCategoria FROM INSERTED

 UPDATE Categorias 
            SET Estado = 1  
            WHERE IdCategoria= @IDCATEGORIAAGREGADA;

END
GO
  
INSERT INTO Categorias (Nombre)
VALUES 
    ('Remeras'),
    ('Camisas'),
    ('Pantalones'),
    ('Camperas');
GO

INSERT INTO Marcas (Nombre)
VALUES 
    ('Zara'),
    ('H&M'),
    ('Lacoste');
GO

UPDATE Marcas
	SET Nombre = 'PepeGrillo'
	WHERE IdMarca = 1;

UPDATE Marcas
	SET Nombre = 'AliciaEnLasMaravillas'
	WHERE IdMarca = 2;
GO

INSERT INTO Productos(Codigo, Nombre, Descripcion, Precio, IdCategoria, IdMarca)
VALUES 
('REM001','Remera Lisa','Cuello Redondo',23663,1,1),
('REM002','Remera 96','Negra con rayas en las mangas',56000,1,2),
('CAM001','Camisa Lisa','Algodon',63000,2,1),
('CAM002','Camisa Lisa','Algodon 90% 10% licra blanca',23663,2,1),
('PAN001','Pantalon','Impermeable con bolsillos cargo',86000,3,2),
('PAN002','Pantalon','Bolsillos con cierre y botones',95660,3,2);
GO

INSERT INTO Imagenes (IdProducto, ImagenUrl)
VALUES 
(1, 'https://http2.mlstatic.com/D_NQ_NP_805457-MLA51076405822_082022-O.webp'),
(2, 'https://http2.mlstatic.com/D_Q_NP_2X_782562-MLA74088243434_012024-E.webp'),
(3, 'https://http2.mlstatic.com/D_Q_NP_2X_833143-MLA70824430319_082023-E.webp'),
(4, 'https://http2.mlstatic.com/D_NQ_NP_601108-MLA31013770285_062019-O.webp'),
(5, 'https://http2.mlstatic.com/D_NQ_NP_637482-MLA79955935763_102024-O.webp'),
(6, 'https://http2.mlstatic.com/D_Q_NP_2X_619261-MLA31356897283_072019-E.webp');
GO

SET DATEFORMAT 'YMD';
INSERT INTO Usuarios (Apellido, Nombre, Email, Contrase�a, TipoUsuario)
VALUES 
('Administrador', 'Admin', 'administrador@email.com', 'admin123', 1);
GO

SET DATEFORMAT 'YMD';
INSERT INTO Usuarios (Apellido, Nombre, Email, Contrase�a, FechaAlta, FechaNacimiento, TipoUsuario)
VALUES 
('Simpson', 'Homero', 'h.simpson@email.com', 'homer123', DEFAULT, '1980-05-12', 0);
GO

UPDATE Productos SET Stock = 7 WHERE IdProducto = 1;
UPDATE Productos SET Stock = 12 WHERE IdProducto = 2;
UPDATE Productos SET Stock = 5 WHERE IdProducto = 3;
UPDATE Productos SET Stock = 10 WHERE IdProducto = 4;
UPDATE Productos SET Stock = 3 WHERE IdProducto = 5;
UPDATE Productos SET Stock = 15 WHERE IdProducto = 6;
GO

---------------------------03/11------------
INSERT INTO Productos(Codigo, Nombre, Descripcion, Precio, IdCategoria, IdMarca,Stock)
VALUES 
('REM003','Remera Lisa',' Verde Cuello Redondo',$23663,1,1,10),
('REM004','Remera Naranja','Remera naranja,cuello redondo, algod�n',$56000,1,2,5),
('CAM003','Camisa','Camisa de algod�n 100%, blanca rayas negras',$63000,2,1,3),
('CAM004','Camisa','Camisa de licra 10% y algod�n 90% rosa',$23663,2,1,4),
('PAN003','Pantalon','Pantal�n jogger azul algod�n,bolsillos y cord�n',$86000,3,2,6),
('PAN004','Pantalon','Pantal�n jogger gris claro,bolsillos y cord�n',$95660,3,2,2),
('REM005','Remera Lisa','Cuello Redondo, algodon 90%',$23663,1,3,5),
('REM006','Remera Lacoste','Nueva Temporada',$126000,1,3,6),
('CAM005','Camisa Lacoste','Camisa de algodon ',$33000,2,3,2),
('CAM006','Camisa Lacoste','Camisa cuadrille de invierno',$23663,2,3,3),
('PAN005','Pantalon','Pantalon algodon verde',$81200,3,3,9),
('PAN006','Pantalon','Bolsillos con cierre y botones',$156560,3,3,4)
GO

INSERT INTO Imagenes (IdProducto, ImagenUrl)
VALUES 
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
GO

--------------para insertar clientes en la bd al comprar------------------------------20/11
ALTER TABLE Clientes
ADD Email NVARCHAR(100); --no puede ser null esta validado en backend


ALTER PROCEDURE sp_InsertarCliente
    @Email NVARCHAR(100),
    @Nombre VARCHAR(100),
    @Apellido VARCHAR(100),
    @Direccion VARCHAR(200),
    @Telefono VARCHAR(20)
AS
BEGIN
      DECLARE @IdUsuario INT;

    SELECT @IdUsuario = IdUsuario FROM Usuarios WHERE Email = @Email;

    IF @IdUsuario IS NULL
    BEGIN
        PRINT 'Usuario no encontrado';
        RAISERROR('No se encontr� un usuario con ese email.', 16, 1);
        RETURN;
    END

    PRINT 'Usuario encontrado, procediendo a insertar...';

    INSERT INTO Clientes (IdUsuario, Nombre, Apellido, Direccion, Email, Telefono)
    VALUES (@IdUsuario, @Nombre, @Apellido, @Direccion, @Email, @Telefono);

    PRINT 'Inserci�n completada';
END

INSERT INTO Clientes (IdUsuario, Nombre, Apellido, Direccion, Email, Telefono, TipoUsuario)
VALUES (2, 'Homero', 'Simpson', 'Calle Falsa 123', 'h.simpson@email.com', '1234567890', 0);

CREATE TABLE Pedidos (
    IdPedido INT PRIMARY KEY IDENTITY(1,1),
    IdUsuario INT NOT NULL FOREIGN KEY REFERENCES Usuarios(IdUsuario),
    IdProducto INT NOT NULL FOREIGN KEY REFERENCES Productos(IdProducto),
    Cantidad INT NOT NULL,
	Monto MONEY,
    FechaPedido DATETIME NOT NULL DEFAULT GETDATE(),
    Estado VARCHAR(50) NOT NULL
);
GO

INSERT INTO Pedidos (IdUsuario, IdProducto, Cantidad, Monto, Estado)
VALUES (2, 1, 3, 125, 'enProceso');

INSERT INTO Productos(Codigo, Nombre, Descripcion, Precio, IdCategoria, IdMarca,Stock)
VALUES 
('REM008','PRUEBASTOCK',' Verde Cuello Redondo',$23663,1,1,1)