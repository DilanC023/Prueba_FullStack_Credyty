IF OBJECT_ID('sp_TParqueadero', 'P') IS NOT NULL 
    DROP PROCEDURE sp_TParqueadero
GO

CREATE PROCEDURE sp_TParqueadero
    @IngresoID INT = 0,
    @VehiculoID INT = 0,
    @HoraIngreso DATETIME = NULL,
    @HoraSalida DATETIME = NULL,
    @ValorPagado DECIMAL(20,2) = NULL,
    @DescuentoAplicado BIT = NULL,
    @TipoVehiculo VARCHAR(50) = NULL,
    @Placa VARCHAR(20) = NULL,
    @DocumentoIdentidad VARCHAR(20) = NULL,
    @MensajeError VARCHAR(255) = null OUTPUT,
    @Opcion VARCHAR(10) -- A - Adicionar
                        -- M - Modificar
                        -- E - Eliminar
                        -- ID - Consultar por ID
WITH RECOMPILE AS
BEGIN

    BEGIN TRY
        BEGIN TRANSACTION
        
        IF @Opcion = 'A'
        BEGIN
			 -- Llamar al procedimiento para insertar el vehículo y obtener el ID
            EXEC sp_TVehiculos  @VehiculoID OUTPUT, @TipoVehiculo, @Placa, @DocumentoIdentidad, @MensajeError OUTPUT, @Opcion 
            
            -- Verificar si se obtuvo un ID válido antes de continuar
            IF @VehiculoID IS NULL
            BEGIN
                SET @MensajeError = 'No se pudo registrar el vehículo'
                ROLLBACK TRANSACTION
                RETURN
            END
            -- Insertar el ingreso en el parqueadero
            INSERT INTO TParqueadero (VehiculoID, HoraIngreso)
            VALUES (@VehiculoID, GETDATE())
        END
        ELSE IF @Opcion = 'M'
        BEGIN
            IF EXISTS (SELECT 1 FROM TParqueadero WHERE IngresoID = @IngresoID)
            BEGIN
                UPDATE TParqueadero 
                SET HoraSalida = @HoraSalida, 
                    ValorPagado = @ValorPagado, 
                    DescuentoAplicado = @DescuentoAplicado
                WHERE IngresoID = @IngresoID
            END
            ELSE
            BEGIN
                SET @MensajeError = 'Ingreso no encontrado'
                RETURN
            END
        END
        ELSE IF @Opcion = 'E'
        BEGIN
            IF EXISTS (SELECT 1 FROM TParqueadero WHERE IngresoID = @IngresoID)
            BEGIN
                DELETE FROM TParqueadero WHERE IngresoID = @IngresoID
            END
            ELSE
            BEGIN
                SET @MensajeError = 'Ingreso no encontrado'
                RETURN
            END
        END
        ELSE IF @Opcion = 'ID'
        BEGIN
            SELECT * FROM vw_ConsultarVehiculo WHERE (IngresoID = @IngresoID OR @IngresoID IS NULL)
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
