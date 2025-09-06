-------AGREGAR ULTIMAS ACTUALIZACIONES MINIMAS A LOS SP ( SIN REALIZAR) -----------------

--CREATE DATABASE ActividadFacultad
--use ActividadFactultad

--CREACION DE TABLAS
create table Formas_Pagos(
formaPagoID INT IDENTITY(1,1) PRIMARY KEY,
nombreFormaPago varchar(100)
);

create table Articulos(
articuloID INT IDENTITY(1,1) PRIMARY KEY,
nombreArticulo varchar(100),
precioUnitario decimal(12,2)
);

create table Facturas(
nroFactura INT IDENTITY(1,1) PRIMARY KEY,
fecha date,
formaPagoID int,
cliente varchar(100)
CONSTRAINT FK_forma_pago FOREIGN KEY (formaPagoID)
	REFERENCES Formas_Pagos(formaPagoID)
);

create table DetalleFactura(
nroDetalle INT IDENTITY(1,1) PRIMARY KEY,
nroFactura int,
articuloID int,
cantidad int
CONSTRAINT fk_nroFactura FOREIGN KEY (nroFactura)
	REFERENCES Facturas(nroFactura),
CONSTRAINT fk_articulo FOREIGN KEY (articuloID)
	REFERENCES Articulos(articuloID)
);



--------INSERT----------------
-- 1. Insertar formas de pago
INSERT INTO Formas_Pagos (nombreFormaPago) VALUES ('Efectivo');
INSERT INTO Formas_Pagos (nombreFormaPago) VALUES ('Tarjeta de Crédito');
INSERT INTO Formas_Pagos (nombreFormaPago) VALUES ('Tarjeta de Débito');
INSERT INTO Formas_Pagos (nombreFormaPago) VALUES ('Transferencia Bancaria');
INSERT INTO Formas_Pagos (nombreFormaPago) VALUES ('Cheque');

-- 2. Insertar artículos
INSERT INTO Articulos (nombreArticulo) VALUES ('Laptop');
INSERT INTO Articulos (nombreArticulo) VALUES ('Mouse inalámbrico');
INSERT INTO Articulos (nombreArticulo) VALUES ('Teclado mecánico');
INSERT INTO Articulos (nombreArticulo) VALUES ('Monitor 24 pulgadas');
INSERT INTO Articulos (nombreArticulo) VALUES ('Impresora multifunción');
INSERT INTO Articulos (nombreArticulo) VALUES ('Auriculares Bluetooth');
INSERT INTO Articulos (nombreArticulo) VALUES ('Disco Duro Externo 1TB');
INSERT INTO Articulos (nombreArticulo) VALUES ('Memoria USB 64GB');
INSERT INTO Articulos (nombreArticulo) VALUES ('Silla ergonómica');
INSERT INTO Articulos (nombreArticulo) VALUES ('Mochila para laptop');

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


---STORED PROCEDURE
-----------------------------------------------------------------FORMA DE PAGO-----------------------
---- READ (uno o todos)
CREATE PROCEDURE sp_FormaPago_Get
    @formaPagoID INT = NULL
AS
BEGIN
    IF @formaPagoID IS NULL
        SELECT * FROM Formas_Pagos;
    ELSE
        SELECT * FROM Formas_Pagos WHERE formaPagoID = @formaPagoID;
END;

---- DELETE
CREATE PROCEDURE sp_FormaPago_Delete
    @formaPagoID INT
AS
BEGIN
    DELETE FROM Formas_Pagos WHERE formaPagoID = @formaPagoID;
END;

---- UPSERT
create PROCEDURE [dbo].[sp_formaPago_UPSERT]
    @formaPagoID INT = NULL,
    @descripcion NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM Formas_Pagos WHERE formaPagoID = @formaPagoID)
    BEGIN
        UPDATE Formas_Pagos
        SET nombreFormaPago = @descripcion
        WHERE formaPagoID = @formaPagoID;

        RETURN 2; -- actualización
    END
    ELSE
    BEGIN
        INSERT INTO Formas_Pagos (nombreFormaPago)
        VALUES (@descripcion);

        RETURN 1; -- inserción
    END
END

--------------------------------------------------------------ARTICULO-----------------------
---- READ (uno o todos)
CREATE OR ALTER PROCEDURE sp_Articulo_Get
    @articuloID INT = NULL
AS
BEGIN
    IF @articuloID IS NULL
        SELECT * FROM Articulos;
    ELSE
        SELECT * FROM Articulos WHERE articuloID = @articuloID;
END;


---- DELETE
CREATE OR ALTER PROCEDURE sp_Articulo_Delete
    @articuloID INT
AS
BEGIN
    DELETE FROM Articulos WHERE articuloID = @articuloID;
END;

---- UPSERT
CREATE PROCEDURE [dbo].[sp_articulo_UPSERT]
    @articuloID INT = NULL,
    @descripcion NVARCHAR(100)  
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM Articulos WHERE articuloID = @articuloID)
    BEGIN
        UPDATE Articulos
        SET nombreArticulo = @descripcion
        WHERE articuloID = @articuloID;
        RETURN 2; -- actualización
    END
    ELSE
    BEGIN
        INSERT INTO Articulos (nombreArticulo)
        VALUES (@descripcion);
        RETURN 1; -- inserción
    END
END

-----------------------------------------------------------------------------FACTURA-----------------------
---- READ (uno o todos)
CREATE PROCEDURE [dbo].[sp_Factura_Get]
    @nroFactura INT = NULL
AS
BEGIN
    IF @nroFactura IS NULL
        SELECT f.nroFactura,
                f.cliente,
                f.fecha,
                fp.formaPagoID
        FROM Facturas f
        JOIN Formas_Pagos fp ON fp.formaPagoID = f.formaPagoID;
    ELSE
        SELECT f.nroFactura,
                f.cliente,
                f.fecha,
                fp.formaPagoID 
        FROM Facturas f
        JOIN Formas_Pagos fp ON fp.formaPagoID = f.formaPagoID
        WHERE nroFactura = @nroFactura;
END;

---- DELETE
CREATE PROCEDURE sp_Factura_Delete
    @nroFactura INT
AS
BEGIN
    DELETE FROM Facturas WHERE nroFactura = @nroFactura;
END;

---- UPSERT --DUDOSO USO
create PROCEDURE [dbo].[sp_Factura_UPSERT]
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

----------------------------------------------------------------------------------------DETALLE FACTURA-----------------------
---- READ (uno o todos)
create PROCEDURE [dbo].[sp_DetalleFactura_Get]
    @nroDetalle INT = NULL
AS
BEGIN
    IF @nroDetalle IS NULL
        SELECT * 
        FROM DetalleFactura df
        JOIN Articulos a ON a.articuloID = df.articuloID
        JOIN Facturas f ON f.nroFactura = df.nroDetalle;
    ELSE
        SELECT * 
        FROM DetalleFactura df
        JOIN Articulos a ON a.articuloID = df.articuloID
        JOIN Facturas f ON f.nroFactura = df.nroDetalle
        WHERE nroDetalle = @nroDetalle;
END;

---- DELETE
create PROCEDURE [dbo].[sp_DetalleFactura_Delete]
    @nroDetalle INT
AS
BEGIN
    DELETE FROM DetalleFactura WHERE nroDetalle = @nroDetalle;
END;

---- UPSERT --DUDOSO USO
create PROCEDURE [dbo].[sp_DetalleFactura_UPSERT]
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

---------------------------------------------------PARA TRANSACCIONES -----------------------------------------------------
--INSERTAR MAESTRO----------------
create procedure [dbo].[sp_INSERTAR_MAESTRO]
    @cliente varchar(100),
    @formaPagoID int,
    @nroFactura int output
as
begin
    INSERT INTO Facturas(cliente,fecha,formaPagoID) VALUES(@cliente,GETDATE(),@formaPagoID)
    SET @nroFactura=SCOPE_IDENTITY();
END


--INSERTAR DETALLLE------------
CREATE PROCEDURE [dbo].[sp_INSERTAR_ALUMNO]
    @nroFactura int,
    @articuloID int,
    @cantidad int
as
begin
    INSERT INTO DetalleFactura(nroFactura,articuloID,cantidad)values(@nroFactura,@articuloID,@cantidad)
end
