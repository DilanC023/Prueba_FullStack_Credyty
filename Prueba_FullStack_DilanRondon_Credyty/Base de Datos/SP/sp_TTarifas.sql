IF OBJECT_ID('sp_TTarifas', 'P') IS NOT NULL 
BEGIN
    DROP PROCEDURE sp_TTarifas
END
GO

CREATE PROCEDURE sp_TTarifas
    @TarifaID INT = 0,
    @TipoVehiculo VARCHAR(50) = NULL,
    @PrecioPorMinuto DECIMAL(20,2) = 0,
    @MensajeError VARCHAR(255)  = null OUTPUT,
    @Opcion VARCHAR(10) -- A - Adicionar
						-- M - Modificar
						-- E - Eliminar
						-- ID - Consultar Usuario
WITH RECOMPILE AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION

        IF @Opcion = 'A'
        BEGIN
            INSERT INTO TTarifas (TipoVehiculo, PrecioPorMinuto)
            VALUES (@TipoVehiculo, @PrecioPorMinuto)
        END
        ELSE IF @Opcion = 'M'
        BEGIN
            IF EXISTS (SELECT 1 FROM TTarifas WHERE TarifaID = @TarifaID)
            BEGIN
                UPDATE TTarifas 
                SET TipoVehiculo = @TipoVehiculo, PrecioPorMinuto = @PrecioPorMinuto
                WHERE TarifaID = @TarifaID
            END
            ELSE
            BEGIN
                SET @MensajeError = 'Tarifa no encontrada'
                ROLLBACK TRANSACTION
                RETURN
            END
        END
        ELSE IF @Opcion = 'E'
        BEGIN
            IF EXISTS (SELECT 1 FROM TTarifas WHERE TarifaID = @TarifaID)
            BEGIN
                DELETE FROM TTarifas WHERE TarifaID = @TarifaID
            END
            ELSE
            BEGIN
                SET @MensajeError = 'Tarifa no encontrada'
                ROLLBACK TRANSACTION
                RETURN
            END
        END
        ELSE IF @Opcion = 'ID'
        BEGIN
            SELECT * FROM TTarifas WHERE (TarifaID = @TarifaID OR @TarifaID IS NULL)
        END
        ELSE
        BEGIN
            SET @MensajeError = 'Opción no válida'
            ROLLBACK TRANSACTION
            RETURN
        END

        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION
        SET @MensajeError = ERROR_MESSAGE()
    END CATCH
END
GO
