IF OBJECT_ID('vw_CalculoTarifa', 'V') IS NOT NULL
	DROP VIEW vw_CalculoTarifa
GO
CREATE VIEW vw_CalculoTarifa 
AS
SELECT 
    tp.IngresoID, tp.VehiculoID, tp.HoraIngreso, tp.HoraSalida, tp.DescuentoAplicado,
    tv.Placa,
    DATEDIFF(MINUTE, tp.HoraIngreso, ISNULL(tp.HoraSalida, GETDATE())) AS MinutosEstacionados,
    tt.PrecioPorMinuto,
    DATEDIFF(MINUTE, tp.HoraIngreso, ISNULL(tp.HoraSalida, GETDATE())) * tt.PrecioPorMinuto AS ValorBruto,
    -- Aplicar descuento del 30% si corresponde
    CASE 
        WHEN tp.DescuentoAplicado = 1 
        THEN (DATEDIFF(MINUTE, tp.HoraIngreso, ISNULL(tp.HoraSalida, GETDATE())) * tt.PrecioPorMinuto) * 0.7	
        ELSE DATEDIFF(MINUTE, tp.HoraIngreso, ISNULL(tp.HoraSalida, GETDATE())) * tt.PrecioPorMinuto
    END AS ValorFinal
FROM TParqueadero tp
INNER JOIN TVehiculos tv ON tp.VehiculoID = tv.VehiculoID
INNER JOIN TTarifas tt ON tv.TipoVehiculo = tt.TipoVehiculo
GO