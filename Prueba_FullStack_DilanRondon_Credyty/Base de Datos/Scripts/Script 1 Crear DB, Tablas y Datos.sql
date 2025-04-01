-- Verificar y crear la base de datos si no existe
IF DB_ID('Prueba_FullStack_DilanRondon') IS NULL
	CREATE DATABASE Prueba_FullStack_DilanRondon
GO
--Seleccionar la base de datos
USE Prueba_FullStack_DilanRondon
GO

--Tabla para registrar los vehículo ingresados
IF(OBJECT_ID('TVehiculos ','U') IS NULL)
BEGIN
	CREATE TABLE TVehiculos (
		VehiculoID INT NOT NULL PRIMARY KEY IDENTITY(1,1),
		TipoVehiculo VARCHAR(50) NOT NULL,
		DocumentoIdentidad VARCHAR(20) NOT NULL,
		Placa VARCHAR(20) NULL
	)
END	
GO
--Tabla para registrar los tarifas a cobrar por el tiempo de estadia
IF(OBJECT_ID('TTarifas','U') IS NULL)
BEGIN
	CREATE TABLE TTarifas (
		TarifaID INT NOT NULL PRIMARY KEY IDENTITY(1,1),
		TipoVehiculo NVARCHAR(50) NOT NULL,
		PrecioPorMinuto DECIMAL(20,2) NOT NULL
	)
END
GO
--Tabla para registrar los tarifas a cobrar por el tiempo de estadia
IF(OBJECT_ID('TParqueadero','U') IS NULL)
BEGIN
	CREATE TABLE TParqueadero (
		IngresoID INT NOT NULL PRIMARY KEY IDENTITY(1,1),
		VehiculoID INT NULL,
		HoraIngreso DATETIME NOT NULL DEFAULT GETDATE(),
		HoraSalida DATETIME NULL,
		MinutosEstacionado INT NULL,
		ValorPagado DECIMAL(20,2) NULL,
		DescuentoAplicado BIT DEFAULT 0,
		FOREIGN KEY (VehiculoID) 
			REFERENCES TVehiculos(VehiculoID) ON DELETE CASCADE
	)
END
GO
--Tabla para registrar las facturas
IF(OBJECT_ID('TFacturasDescuento','U') IS NULL)
BEGIN
	CREATE TABLE TFacturasDescuento (
		FacturaID INT NOT NULL PRIMARY KEY IDENTITY(1,1),
		IngresoID INT NULL,
		NumeroFactura VARCHAR(50) NULL,
		FOREIGN KEY (IngresoID) 
			REFERENCES TParqueadero(IngresoID) ON DELETE CASCADE
	)
END
GO
--Tabla para registrar los Usuarios
IF(OBJECT_ID('TUsuarios','U') IS NULL)
BEGIN
    CREATE TABLE TUsuarios (
        UsuarioID INT NOT NULL PRIMARY KEY IDENTITY(1,1),
        Nombre VARCHAR(80) NOT NULL,
        DocumentoIdentidad VARCHAR(20) NOT NULL, 
        Email VARCHAR(80) NOT NULL,
        Clave VARCHAR(255) NOT NULL,
        FechaCreacion DATETIME NOT NULL DEFAULT GETDATE()
    )
END
GO


-- Insertar tarifas iniciales
IF NOT EXISTS (SELECT 1 FROM TTarifas)
BEGIN
    INSERT INTO TTarifas (TipoVehiculo, PrecioPorMinuto) VALUES 
    ('Carro', 110),
    ('Moto', 50),
    ('Bicicleta', 10)
END
GO
-- Insertar usuario inicial
IF NOT EXISTS (SELECT 1 FROM TUsuarios)
BEGIN
    INSERT INTO TUsuarios ( Nombre, DocumentoIdentidad, Email, Clave, FechaCreacion) Values
	('Dilan Rondon', '1001173106', 'prueba@prueba.com',	'Dilan123',	GETDATE())
END
GO