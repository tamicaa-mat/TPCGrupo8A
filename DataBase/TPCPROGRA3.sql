CREATE DATABASE TPCGRUPO8A;
GO

USE TPCGRUPO8A;
GO

----------------------------------------- TABLAS -----------------------------------------

CREATE TABLE Categorias(
    IdCategoria INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(50) NOT NULL,
	Estado BIT  NOT NULL DEFAULT '1'
);
GO

CREATE TABLE Marcas(
    IdMarca INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(50) NOT NULL,
	Estado BIT  NOT NULL DEFAULT '1'
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
    IdMarca INT FOREIGN KEY REFERENCES Marcas(IdMarca),
	Stock INT NOT NULL DEFAULT 0
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
    Contraseña VARCHAR(150) NOT NULL,
    FechaAlta DATETIME NOT NULL DEFAULT GETDATE(),
    FechaNacimiento DATETIME NULL,
	TipoUsuario INT NOT NULL
);
GO

CREATE TABLE Pedidos (
    IdPedido INT PRIMARY KEY IDENTITY(1,1),
    IdUsuario INT NOT NULL FOREIGN KEY REFERENCES Usuarios(IdUsuario),
	MontoTotal MONEY NULL,
    Fecha DATETIME NULL DEFAULT GETDATE(),
    Estado VARCHAR(50) NOT NULL
);
GO

CREATE TABLE Clientes (
    IdCliente INT PRIMARY KEY IDENTITY(1,1),
	IdUsuario INT FOREIGN KEY REFERENCES Usuarios(IdUsuario),
	IdPedido INT FOREIGN KEY REFERENCES Pedidos(IdPedido),
	Direccion VARCHAR(200) NOT NULL,
	Telefono INT NOT NULL,
	TipoUsuario INT NOT NULL
);
GO

CREATE TABLE DetallePedido(
	IdDetallePedido INT PRIMARY KEY IDENTITY(1,1),
	IdPedido INT NOT NULL FOREIGN KEY REFERENCES Pedidos(IdPedido),
	IdProducto INT NOT NULL FOREIGN KEY REFERENCES Productos(IdProducto),
	Cantidad INT NULL,
	Preciounitario DECIMAL(18,2) NULL
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

------------------ PROCEDIMIENTOS, TRIGGERS, FUNCIONES, ETC... -----------------------------------------

CREATE TRIGGER EliminarProductoYRelaciones ON Productos --ELIMINA EL PRODUCTO DE FORMA FISICA
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
        PRINT 'Error: Ocurrió un problema al eliminar el producto y sus imágenes.';
    END CATCH
END;
GO

CREATE PROCEDURE EliminacionLogicaProducto @idProducto INT --ELIMINA EL PRODUCTO DE FORMA LOGICA
AS 
BEGIN
	BEGIN TRY 
		UPDATE Productos
		SET Estado = 0
		WHERE IdProducto = @idProducto;
		PRINT 'PRODUCTO ELIMINADO (ELIMINACIÓN LOGICA)'
	END TRY
	BEGIN CATCH
		PRINT 'ERROR: NO SE PUDO ELIMINAR EL PRODUCTO'
	END CATCH
END;
GO

CREATE PROCEDURE SP_EliminacionLogicaMarcas(@IDMARCA INT) --- ELIMINA LA MARCA DE FORMA LOGICA
AS
BEGIN
    BEGIN TRY 
        BEGIN TRANSACTION

        -- Verificar si la marca con el ID proporcionado existe
        IF EXISTS(SELECT 1 FROM Marcas WHERE IdMarca = @IDMARCA)
        BEGIN
            -- Actualizar el estado de la marca para realizar la eliminación lógica
            UPDATE Marcas 
            SET Estado = 0  
            WHERE IdMarca = @IDMARCA;

            PRINT 'Marca eliminada lógicamente con éxito.';
        END
        ELSE
        BEGIN
            PRINT 'ERROR: No hay una marca asociada a ese ID';
        END

        -- Confirmar la transacción si no hubo problemas
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH 
        -- Deshacer la transacción en caso de error y levantar un error personalizado
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        RAISERROR ('ERROR: No se pudo eliminar la marca', 16, 1);
    END CATCH
END;
GO

CREATE TRIGGER Al_Agregar_Marca ON Marcas --INSERTA UNA MARCA NUEVA Y LA PONE EN ESTADO 1 "ACTIVO"

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

CREATE PROCEDURE SP_EliminacionLogicaCategorias(@IDCATEGORIA INT) --ELIMINACION LOGICA DE CATEGORIAS
AS
BEGIN
    BEGIN TRY 
        BEGIN TRANSACTION

        -- Verificar si la marca con el ID proporcionado existe
        IF EXISTS(SELECT 1 FROM Categorias WHERE IdCategoria = @IDCATEGORIA)
        BEGIN
            -- Actualizar el estado de la marca para realizar la eliminación lógica
            UPDATE Categorias 
            SET Estado = 0  
            WHERE IdCategoria = @IDCATEGORIA;

            PRINT 'Categoria eliminada lógicamente con éxito.';
        END
        ELSE
        BEGIN
            PRINT 'ERROR: No hay una Categoria asociada a ese ID';
        END

        -- Confirmar la transacción si no hubo problemas
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH 
        -- Deshacer la transacción en caso de error y levantar un error personalizado
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        RAISERROR ('ERROR: No se pudo eliminar la Categoria', 16, 1);
    END CATCH
END;
GO

CREATE TRIGGER Al_Agregar_Categoria ON Categorias --AGREGA LA CATEGORIA Y LA SETEA EN ESTADO 1 "ACTIVO"

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

---------------------------- INSERTS -----------------------------------------

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
INSERT INTO Usuarios (Apellido, Nombre, Email, Contraseña, TipoUsuario)
VALUES 
('Administrador', 'Admin', 'administrador@email.com', 'admin123', 1);
GO

SET DATEFORMAT 'YMD';
INSERT INTO Usuarios (Apellido, Nombre, Email, Contraseña, FechaAlta, FechaNacimiento, TipoUsuario)
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

INSERT INTO Productos(Codigo, Nombre, Descripcion, Precio, IdCategoria, IdMarca,Stock)
VALUES 
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

INSERT INTO Pedidos (IdUsuario, MontoTotal, Estado)
VALUES (2, 125, 'enProceso');
GO

INSERT INTO Productos(Codigo, Nombre, Descripcion, Precio, IdCategoria, IdMarca,Stock)
VALUES 
('REM008','PRUEBASTOCK',' Verde Cuello Redondo',$23663,1,1,1)
GO

--ALTER PROCEDURE sp_InsertarCliente
--    @Email NVARCHAR(100),
--    @Nombre VARCHAR(100),
--    @Apellido VARCHAR(100),
--    @Direccion VARCHAR(200),
--    @Telefono VARCHAR(20)
--AS
--BEGIN
--      DECLARE @IdUsuario INT;

--    SELECT @IdUsuario = IdUsuario FROM Usuarios WHERE Email = @Email;

--    IF @IdUsuario IS NULL
--    BEGIN
--        PRINT 'Usuario no encontrado';
--        RAISERROR('No se encontró un usuario con ese email.', 16, 1);
--        RETURN;
--    END

--    PRINT 'Usuario encontrado, procediendo a insertar...';

--    INSERT INTO Clientes (IdUsuario, Nombre, Apellido, Direccion, Email, Telefono)
--    VALUES (@IdUsuario, @Nombre, @Apellido, @Direccion, @Email, @Telefono);

--    PRINT 'Inserción completada';
--END

--CREATE TABLE Pedidos (
--    IdPedido INT PRIMARY KEY IDENTITY(1,1),
--    IdUsuario INT NOT NULL FOREIGN KEY REFERENCES Usuarios(IdUsuario),
--    IdProducto INT NOT NULL FOREIGN KEY REFERENCES Productos(IdProducto),
--    Cantidad INT NOT NULL,
--	Monto MONEY,
--    FechaPedido DATETIME NOT NULL DEFAULT GETDATE(),
--    Estado VARCHAR(50) NOT NULL
--);
--GO

ALTER TABLE Clientes
ALTER COLUMN Telefono VARCHAR(20) NOT NULL;

GO


INSERT INTO Clientes (IdUsuario, IdPedido, Direccion, Telefono, TipoUsuario)
VALUES 
(1, NULL, 'Av. Principal 123', 1234567890, 1),
(2, NULL, 'Calle Secundaria 45', 9876543210, 1)

GO

INSERT INTO Pedidos (IdUsuario, MontoTotal, Estado)
VALUES 
(1, 1500.00, 'Pendiente'),
(2, 2000.50, 'Entregado')

GO

UPDATE Clientes
SET IdPedido = Pedidos.IdPedido
FROM Clientes
INNER JOIN Pedidos
ON Clientes.IdUsuario = Pedidos.IdUsuario;

DELETE FROM Pedidos
WHERE IdPedido = 1;
------------------------------------------------------------------ 25/11

ALTER TABLE Clientes
ADD Direccion VARCHAR(255) NULL,
    Telefono VARCHAR(50) NULL;

CREATE TRIGGER trg_InsertCliente
ON Pedidos
AFTER INSERT
AS
BEGIN
    -- Verificar si hay registros insertados
    IF EXISTS (SELECT 1 FROM inserted)
    BEGIN
        -- Insertar datos en la tabla Clientes usando los valores del inserted
        INSERT INTO Clientes (IdUsuario)
        SELECT IdUsuario
        FROM inserted
    END
END;
GO
