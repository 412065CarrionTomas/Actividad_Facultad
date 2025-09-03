USE Actividad1_5
--------STORED PROCEDURE AVANZADO--------------
--insertar facutra con su detalle

CREATE PROCEDURE sp_InsertarFactura
	@FECHA DATE,
	@formaPagoID INT,
	@cliente VARCHAR(100),
	@articuloID INT,
	@cantidad INT
AS
BEGIN
	SET NOCOUNT ON;
	-- 1. Insertamos la factura
    INSERT INTO Facturas (fecha, formaPagoID, cliente)
    VALUES (@fecha, @formaPagoID, @cliente);

    -- 2. Recuperamos el ID de la última factura insertada
    DECLARE @nroFactura INT;
    SET @nroFactura = SCOPE_IDENTITY();

    -- 3. Insertamos el detalle
    INSERT INTO DetalleFactura (nroFactura, articuloID, cantidad)
    VALUES (@nroFactura, @articuloID, @cantidad);
END;

-- consultar facturas por cliente
CREATE PROCEDURE sp_FacturasPorCliente
    @cliente VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT f.nroFactura, f.fecha, fp.descripcion AS FormaPago, a.descripcion AS Articulo, d.cantidad
    FROM Facturas f
    INNER JOIN FormaPago fp ON f.formaPagoID = fp.formaPagoID
    INNER JOIN DetalleFactura d ON f.nroFactura = d.nroFactura
    INNER JOIN Articulo a ON d.articuloID = a.articuloID
    WHERE f.cliente = @cliente;
END;

--------STORED PROCEDURE CRUD-------------
-- READ (uno o todos)
CREATE PROCEDURE sp_FormaPago_Get
    @formaPagoID INT = NULL
AS
BEGIN
    IF @formaPagoID IS NULL
        SELECT * FROM FormaPago;
    ELSE
        SELECT * FROM FormaPago WHERE formaPagoID = @formaPagoID;
END;
GO



-- DELETE
CREATE PROCEDURE sp_FormaPago_Delete
    @formaPagoID INT
AS
BEGIN
    DELETE FROM FormaPago WHERE formaPagoID = @formaPagoID;
END;
GO
------------------Articulo-----------------------
-- READ
CREATE OR ALTER PROCEDURE sp_Articulo_Get
    @articuloID INT = NULL
AS
BEGIN
    IF @articuloID IS NULL
        SELECT * FROM Articulo;
    ELSE
        SELECT * FROM Articulo WHERE articuloID = @articuloID;
END;
GO

--UPSERT--
CREATE PROCEDURE SP_GUARDAR_ARTICULO
    @articuloID INT = NULL,
    @descripcion NVARCHAR(100)  
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM Articulo WHERE articuloID = @articuloID)
    BEGIN
        UPDATE Articulo
        SET descripcion = @descripcion
        WHERE articuloID = @articuloID;
    END
    ELSE
    BEGIN
        INSERT INTO Articulo (descripcion)
        VALUES (@descripcion);
        
    END
END

-- DELETE
CREATE OR ALTER PROCEDURE sp_Articulo_Delete
    @articuloID INT
AS
BEGIN
    DELETE FROM Articulo WHERE articuloID = @articuloID;
END;
GO
------------------Factura-----------------------


-- READ
CREATE OR ALTER PROCEDURE sp_Factura_Get
    @nroFactura INT = NULL
AS
BEGIN
    IF @nroFactura IS NULL
        SELECT * FROM Facturas;
    ELSE
        SELECT * FROM Facturas WHERE nroFactura = @nroFactura;
END;
GO



-- DELETE
CREATE OR ALTER PROCEDURE sp_Factura_Delete
    @nroFactura INT
AS
BEGIN
    DELETE FROM Facturas WHERE nroFactura = @nroFactura;
END;
GO
------------------DetalleFactura-----------------------

-- READ
CREATE OR ALTER PROCEDURE sp_DetalleFactura_Get
    @nroDetalle INT = NULL
AS
BEGIN
    IF @nroDetalle IS NULL
        SELECT * FROM DetalleFactura;
    ELSE
        SELECT * FROM DetalleFactura WHERE nroDetalle = @nroDetalle;
END;
GO

-- DELETE
CREATE OR ALTER PROCEDURE sp_DetalleFactura_Delete
    @nroDetalle INT
AS
BEGIN
    DELETE FROM DetalleFactura WHERE nroDetalle = @nroDetalle;
END;
GO
------------
create PROCEDURE [dbo].[SP_GUARDAR_FORMAPAGO]
    @formaPagoID INT = NULL,
    @descripcion NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM FormaPago WHERE formaPagoID = @formaPagoID)
    BEGIN
        UPDATE FormaPago
        SET descripcion = @descripcion
        WHERE formaPagoID = @formaPagoID;

        RETURN 2; -- actualización
    END
    ELSE
    BEGIN
        INSERT INTO FormaPago (descripcion)
        VALUES (@descripcion);

        RETURN 1; -- inserción
    END
END

-----------------
create PROCEDURE [dbo].[SP_GUARDAR_FACTURA]
    @nroFactura INT = NULL,
    @fecha DATE,
    @formaPagoID INT,
    @cliente NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM Facturas WHERE nroFactura = @nroFactura)
    BEGIN
        UPDATE Facturas
        SET fecha = @fecha,
            formaPagoID = @formaPagoID,
            cliente = @cliente
        WHERE nroFactura = @nroFactura;

        RETURN 2; -- actualización
    END
    ELSE
    BEGIN
        INSERT INTO Facturas (fecha, formaPagoID, cliente)
        VALUES (@fecha, @formaPagoID, @cliente);

        RETURN 1; -- inserción
    END
END
------------------
create PROCEDURE [dbo].[SP_GUARDAR_DETALLEFACTURA]
    @nroDetalle INT = NULL,
    @nroFactura INT,
    @articuloID INT,
    @cantidad INT
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM DetalleFactura WHERE nroDetalle = @nroDetalle)
    BEGIN
        UPDATE DetalleFactura
        SET nroFactura = @nroFactura,
            articuloID = @articuloID,
            cantidad = @cantidad
        WHERE nroDetalle = @nroDetalle;

        RETURN 2; -- actualización
    END
    ELSE
    BEGIN
        INSERT INTO DetalleFactura (nroFactura, articuloID, cantidad)
        VALUES (@nroFactura, @articuloID, @cantidad);

        RETURN 1; -- inserción
    END
END
-----------------------



--------INSERT----------------
-- 1. Insertar formas de pago
INSERT INTO FormaPago (descripcion) VALUES ('Efectivo');
INSERT INTO FormaPago (descripcion) VALUES ('Tarjeta de Crédito');
INSERT INTO FormaPago (descripcion) VALUES ('Tarjeta de Débito');
INSERT INTO FormaPago (descripcion) VALUES ('Transferencia Bancaria');
INSERT INTO FormaPago (descripcion) VALUES ('Cheque');

-- 2. Insertar artículos
INSERT INTO Articulo (descripcion) VALUES ('Laptop');
INSERT INTO Articulo (descripcion) VALUES ('Mouse inalámbrico');
INSERT INTO Articulo (descripcion) VALUES ('Teclado mecánico');
INSERT INTO Articulo (descripcion) VALUES ('Monitor 24 pulgadas');
INSERT INTO Articulo (descripcion) VALUES ('Impresora multifunción');
INSERT INTO Articulo (descripcion) VALUES ('Auriculares Bluetooth');
INSERT INTO Articulo (descripcion) VALUES ('Disco Duro Externo 1TB');
INSERT INTO Articulo (descripcion) VALUES ('Memoria USB 64GB');
INSERT INTO Articulo (descripcion) VALUES ('Silla ergonómica');
INSERT INTO Articulo (descripcion) VALUES ('Mochila para laptop');

-- 3. Insertar 20 facturas
INSERT INTO Facturas (fecha, formaPagoID, cliente) VALUES ('2023-01-15', 1, 'Juan Pérez');
INSERT INTO Facturas (fecha, formaPagoID, cliente) VALUES ('2023-01-16', 2, 'María López');
INSERT INTO Facturas (fecha, formaPagoID, cliente) VALUES ('2023-01-18', 3, 'Carlos Sánchez');
INSERT INTO Facturas (fecha, formaPagoID, cliente) VALUES ('2023-01-20', 1, 'Ana Torres');
INSERT INTO Facturas (fecha, formaPagoID, cliente) VALUES ('2023-02-01', 4, 'Luis Gómez');
INSERT INTO Facturas (fecha, formaPagoID, cliente) VALUES ('2023-02-05', 5, 'Fernanda Díaz');
INSERT INTO Facturas (fecha, formaPagoID, cliente) VALUES ('2023-02-07', 2, 'Jorge Herrera');
INSERT INTO Facturas (fecha, formaPagoID, cliente) VALUES ('2023-02-08', 1, 'Claudia Ríos');
INSERT INTO Facturas (fecha, formaPagoID, cliente) VALUES ('2023-02-10', 3, 'Pedro Castillo');
INSERT INTO Facturas (fecha, formaPagoID, cliente) VALUES ('2023-02-12', 4, 'Sofía Martínez');
INSERT INTO Facturas (fecha, formaPagoID, cliente) VALUES ('2023-02-14', 2, 'Ricardo Morales');
INSERT INTO Facturas (fecha, formaPagoID, cliente) VALUES ('2023-02-15', 1, 'Valentina Cruz');
INSERT INTO Facturas (fecha, formaPagoID, cliente) VALUES ('2023-02-16', 5, 'Gabriel Romero');
INSERT INTO Facturas (fecha, formaPagoID, cliente) VALUES ('2023-02-18', 3, 'Andrea Silva');
INSERT INTO Facturas (fecha, formaPagoID, cliente) VALUES ('2023-02-20', 2, 'Manuel Castro');
INSERT INTO Facturas (fecha, formaPagoID, cliente) VALUES ('2023-02-22', 1, 'Isabel Reyes');
INSERT INTO Facturas (fecha, formaPagoID, cliente) VALUES ('2023-02-24', 4, 'Tomás Fernández');
INSERT INTO Facturas (fecha, formaPagoID, cliente) VALUES ('2023-02-25', 2, 'Natalia Vega');
INSERT INTO Facturas (fecha, formaPagoID, cliente) VALUES ('2023-02-27', 3, 'Francisco Medina');
INSERT INTO Facturas (fecha, formaPagoID, cliente) VALUES ('2023-02-28', 1, 'Camila Ortiz');

-- 4. Insertar detalles de facturas
INSERT INTO DetalleFactura (nroFactura, articuloID, cantidad) VALUES (1, 1, 1); -- Juan compra Laptop
INSERT INTO DetalleFactura (nroFactura, articuloID, cantidad) VALUES (2, 2, 2); -- María compra 2 Mouses
INSERT INTO DetalleFactura (nroFactura, articuloID, cantidad) VALUES (3, 3, 1);
INSERT INTO DetalleFactura (nroFactura, articuloID, cantidad) VALUES (4, 4, 1);
INSERT INTO DetalleFactura (nroFactura, articuloID, cantidad) VALUES (5, 5, 1);
INSERT INTO DetalleFactura (nroFactura, articuloID, cantidad) VALUES (6, 6, 2);
INSERT INTO DetalleFactura (nroFactura, articuloID, cantidad) VALUES (7, 7, 1);
INSERT INTO DetalleFactura (nroFactura, articuloID, cantidad) VALUES (8, 8, 3);
INSERT INTO DetalleFactura (nroFactura, articuloID, cantidad) VALUES (9, 9, 1);
INSERT INTO DetalleFactura (nroFactura, articuloID, cantidad) VALUES (10, 10, 1);
INSERT INTO DetalleFactura (nroFactura, articuloID, cantidad) VALUES (11, 2, 1);
INSERT INTO DetalleFactura (nroFactura, articuloID, cantidad) VALUES (12, 3, 2);
INSERT INTO DetalleFactura (nroFactura, articuloID, cantidad) VALUES (13, 1, 1);
INSERT INTO DetalleFactura (nroFactura, articuloID, cantidad) VALUES (14, 6, 1);
INSERT INTO DetalleFactura (nroFactura, articuloID, cantidad) VALUES (15, 7, 2);
INSERT INTO DetalleFactura (nroFactura, articuloID, cantidad) VALUES (16, 4, 1);
INSERT INTO DetalleFactura (nroFactura, articuloID, cantidad) VALUES (17, 5, 1);
INSERT INTO DetalleFactura (nroFactura, articuloID, cantidad) VALUES (18, 9, 1);
INSERT INTO DetalleFactura (nroFactura, articuloID, cantidad) VALUES (19, 10, 1);
INSERT INTO DetalleFactura (nroFactura, articuloID, cantidad) VALUES (20, 8, 2);


----CREACION DE TABLAS
create table Facturas(
nroFactura INT IDENTITY(1,1) PRIMARY KEY,
fecha date,
formaPagoID int,
cliente varchar(100)
CONSTRAINT FK_forma_pago FOREIGN KEY (formaPagoID)
	REFERENCES FormaPago(formaPagoID)
);

create table FormaPago(
formaPagoID INT IDENTITY(1,1) PRIMARY KEY,
descripcion varchar(100)
);

create table DetalleFactura(
nroDetalle INT IDENTITY(1,1) PRIMARY KEY,
nroFactura int,
articuloID int,
cantidad int
CONSTRAINT fk_nroFactura FOREIGN KEY (nroFactura)
	REFERENCES Facturas(nroFactura),
CONSTRAINT fk_articulo FOREIGN KEY (articuloID)
	REFERENCES Articulo(articuloID)
);

create table Articulo(
articuloID INT IDENTITY(1,1) PRIMARY KEY,
descripcion varchar(100)
);