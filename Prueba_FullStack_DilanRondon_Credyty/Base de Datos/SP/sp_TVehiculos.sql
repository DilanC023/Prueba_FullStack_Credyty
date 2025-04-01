IF OBJECT_ID('sp_TVehiculos', 'P') IS NOT NULL 
BEGIN
    DROP PROCEDURE sp_TVehiculos
END
GO

CREATE PROCEDURE sp_TVehiculos
    @VehiculoID INT OUTPUT,
    @TipoVehiculo VARCHAR(50) = NULL,
    @Placa VARCHAR(20) = NULL,
    @DocumentoIdentidad VARCHAR(20) = NULL,
    @MensajeError VARCHAR(255)  = null OUTPUT,
    @Opcion VARCHAR(10) -- A - Adicionar
                        -- M - Modificar
                        -- E - Eliminar
                        -- ID - Consultar por ID
                        -- CP - Consultar por Placa
WITH RECOMPILE 
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION

        IF @Opcion = 'A'
        BEGIN
            INSERT INTO TVehiculos (TipoVehiculo, Placa, DocumentoIdentidad)
            VALUES (@TipoVehiculo, @Placa, @DocumentoIdentidad)
            
            -- Capturar el ID generado
            SET @VehiculoID = SCOPE_IDENTITY()
        END
        ELSE IF @Opcion = 'M'
        BEGIN
            IF EXISTS (SELECT 1 FROM TVehiculos WHERE VehiculoID = @VehiculoID)
            BEGIN
                UPDATE TVehiculos 
                SET TipoVehiculo = @TipoVehiculo, 
                    Placa = @Placa
                WHERE VehiculoID = @VehiculoID
            END
            ELSE
            BEGIN
                SET @MensajeError = 'Vehículo no encontrado'
                RETURN
            END
        END
        ELSE IF @Opcion = 'E'
        BEGIN
            IF EXISTS (SELECT 1 FROM TVehiculos WHERE VehiculoID = @VehiculoID)
            BEGIN
                DELETE FROM TVehiculos WHERE VehiculoID = @VehiculoID
            END
            ELSE
            BEGIN
                SET @MensajeError = 'Vehículo no encontrado'
                RETURN
            END
        END
        ELSE IF @Opcion = 'ID'
        BEGIN
            SELECT VehiculoID, TipoVehiculo, Placa, DocumentoIdentidad
            FROM TVehiculos 
            WHERE (@VehiculoID = 0 OR VehiculoID = @VehiculoID)
        END
        ELSE IF @Opcion = 'CP'
        BEGIN
            SELECT VehiculoID, TipoVehiculo, Placa, DocumentoIdentidad
            FROM TVehiculos 
            WHERE (@Placa IS NOT NULL AND Placa = @Placa)
        END
        ELSE
        BEGIN
            SET @MensajeError = 'Opción no válida'
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
