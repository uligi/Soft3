use Dunamis_SA

go

CREATE PROCEDURE sp_RegistrarTipoCorreo
    @Descripcion VARCHAR(45),
    @Resultado INT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        INSERT INTO TipoCorreo (Descripcion)
        VALUES (@Descripcion);

        SET @Resultado = SCOPE_IDENTITY();
        SET @Mensaje = 'Tipo de Correo registrado exitosamente.';
    END TRY
    BEGIN CATCH
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END
GO

use Dunamis_SA

go

CREATE PROCEDURE sp_EditarTipoCorreo
    @TipoCorreoID INT,
    @Descripcion VARCHAR(45),
    @Resultado BIT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        UPDATE TipoCorreo
        SET Descripcion = @Descripcion
        WHERE TipoCorreoID = @TipoCorreoID;

        SET @Resultado = 1;
        SET @Mensaje = 'Tipo de Correo actualizado exitosamente.';
    END TRY
    BEGIN CATCH
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END
GO

use Dunamis_SA

go


CREATE PROCEDURE sp_EliminarTipoCorreo
    @TipoCorreoID INT,
    @Resultado BIT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        DELETE FROM TipoCorreo
        WHERE TipoCorreoID = @TipoCorreoID;

        SET @Resultado = 1;
        SET @Mensaje = 'Tipo de Correo eliminado exitosamente.';
    END TRY
    BEGIN CATCH
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END
GO
