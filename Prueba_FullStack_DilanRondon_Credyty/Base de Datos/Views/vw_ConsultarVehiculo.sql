IF OBJECT_ID('vw_ConsultarVehiculo', 'V') IS NOT NULL 
    DROP VIEW vw_ConsultarVehiculo
GO

CREATE VIEW vw_ConsultarVehiculo 
AS
	SELECT 
    tv.VehiculoID, tv.Placa, tv.TipoVehiculo, tv.DocumentoIdentidad,
    tp.IngresoID, tp.HoraIngreso, tp.HoraSalida, tp.ValorPagado, tp.DescuentoAplicado,
	tt.PrecioPorMinuto
	FROM TVehiculos tv
	LEFT JOIN TParqueadero tp ON tv.VehiculoID = tp.VehiculoID
	LEFT JOIN TTarifas tt ON tv.TipoVehiculo = tt.TipoVehiculo
GO
