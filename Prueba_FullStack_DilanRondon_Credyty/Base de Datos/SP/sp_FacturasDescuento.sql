IF OBJECT_ID('sp_FacturasDescuento', 'P') IS NOT NULL 
    DROP PROCEDURE sp_FacturasDescuento
GO

CREATE PROCEDURE sp_FacturasDescuento
    @FacturaID INT = NULL,
    @IngresoID INT = NULL,
    @NumeroFactura VARCHAR(50) = NULL,
    @MensajeError NVARCHAR(255)  = null OUTPUT,
    @Opcion VARCHAR(10) -- A - Adicionar
						-- C - Consultar
WITH RECOMPILE AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION

        IF @Opcion = 'A' -- Adicionar factura y aplicar descuento
        BEGIN
            -- Insertar la factura de descuento
            INSERT INTO TFacturasDescuento (IngresoID, NumeroFactura)
            VALUES (@IngresoID, @NumeroFactura)

            -- Obtener el valor con descuento desde la vista
            DECLARE @ValorFinal DECIMAL(10,2)
            SELECT @ValorFinal = ValorFinal 
            FROM vw_CalculoTarifa 
            WHERE IngresoID = @IngresoID

            -- Aplicar el descuento
            IF @ValorFinal IS NOT NULL
            BEGIN
                UPDATE TParqueadero 
                SET 
                    ValorPagado = @ValorFinal,
                    DescuentoAplicado = 1
                WHERE IngresoID = @IngresoID
            END
            ELSE
            BEGIN
                SET @MensajeError = 'No se pudo calcular el valor de la tarifa.'
                ROLLBACK TRANSACTION
                RETURN
            END
        END
        ELSE IF @Opcion = 'C'
        BEGIN
            SELECT * FROM TFacturasDescuento 
            WHERE (FacturaID = @FacturaID OR @FacturaID IS NULL)
        END

        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION
        SET @MensajeError = ERROR_MESSAGE()
    END CATCH
END
GO
