IF OBJECT_ID('sp_TUsuarios', 'P') IS NOT NULL 
BEGIN
    DROP PROCEDURE sp_TUsuarios
END
GO

CREATE PROCEDURE sp_TUsuarios
    @UsuarioID INT = 0,
    @Nombre VARCHAR(80) = NULL,
    @DocumentoIdentidad VARCHAR(20) = NULL,
    @Email VARCHAR(80) = NULL,
    @Clave VARCHAR(255) = NULL,
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
            INSERT INTO TUsuarios (Nombre, DocumentoIdentidad, Email, Clave, FechaCreacion)
            VALUES (@Nombre, @DocumentoIdentidad, @Email, @Clave, GETDATE())
        END
        ELSE IF @Opcion = 'M'
        BEGIN
            IF EXISTS (SELECT 1 FROM TUsuarios WHERE UsuarioID = @UsuarioID)
            BEGIN
                UPDATE TUsuarios SET 
				Nombre = @Nombre,
				Email = @Email
                WHERE UsuarioID = @UsuarioID
            END
            ELSE
            BEGIN
                SET @MensajeError = 'Usuario no encontrado'
                ROLLBACK TRANSACTION
                RETURN
            END
        END
        ELSE IF @Opcion = 'E'
        BEGIN
            IF EXISTS (SELECT 1 FROM TUsuarios WHERE UsuarioID = @UsuarioID)
            BEGIN
                DELETE FROM TUsuarios WHERE UsuarioID = @UsuarioID
            END
            ELSE
            BEGIN
                SET @MensajeError = 'Usuario no encontrado'
                ROLLBACK TRANSACTION
                RETURN
            END
        END
        ELSE IF @Opcion = 'ID'
        BEGIN
            SELECT UsuarioID, Nombre, DocumentoIdentidad, Email, Clave, FechaCreacion 
			FROM TUsuarios 
			WHERE (UsuarioID = @UsuarioID OR @UsuarioID IS NULL)
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
