use Dunamis_SA

GO

CREATE PROCEDURE sp_ListarTipoTelefono
AS
BEGIN
    SELECT TipoTelefonoID, Descripcion 
    FROM TipoTelefono
    WHERE Activo = 1;
END
GO


CREATE PROCEDURE sp_RegistrarTipoTelefono
    @Descripcion VARCHAR(45),
    @Resultado BIT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
AS
BEGIN
    BEGIN TRY
        INSERT INTO TipoTelefono (Descripcion, Activo) 
        VALUES (@Descripcion, 1);
        SET @Resultado = 1;
        SET @Mensaje = 'Tipo de Teléfono registrado correctamente.';
    END TRY
    BEGIN CATCH
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END
GO




CREATE PROCEDURE sp_EditarTipoTelefono
    @TipoTelefonoID INT,
    @Descripcion VARCHAR(45),
    @Resultado BIT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
AS
BEGIN
    BEGIN TRY
        UPDATE TipoTelefono 
        SET Descripcion = @Descripcion 
        WHERE TipoTelefonoID = @TipoTelefonoID AND Activo = 1;
        SET @Resultado = 1;
        SET @Mensaje = 'Tipo de Teléfono actualizado correctamente.';
    END TRY
    BEGIN CATCH
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END
GO



CREATE PROCEDURE sp_EliminarTipoTelefono
    @TipoTelefonoID INT,
    @Resultado BIT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
AS
BEGIN
    BEGIN TRY
        UPDATE TipoTelefono
        SET Activo = 0
        WHERE TipoTelefonoID = @TipoTelefonoID;
        SET @Resultado = 1;
        SET @Mensaje = 'Tipo de Teléfono eliminado correctamente.';
    END TRY
    BEGIN CATCH
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END
GO

