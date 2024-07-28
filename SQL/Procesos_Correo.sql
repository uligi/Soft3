

use Dunamis_SA

Go

CREATE PROCEDURE sp_ListarCorreos
AS
BEGIN
    SET NOCOUNT ON;

    SELECT c.CorreoID, c.Correo, c.TipoCorreoID, tc.Descripcion AS DescripcionTipoCorreo
    FROM Correo c
    INNER JOIN TipoCorreo tc ON c.TipoCorreoID = tc.TipoCorreoID
    WHERE c.Activo = 1;
END
GO



CREATE PROCEDURE sp_RegistrarCorreo
    @Correo VARCHAR(255),
    @TipoCorreoID INT,
    @Resultado INT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        INSERT INTO Correo (Correo, TipoCorreoID, Activo)
        VALUES (@Correo, @TipoCorreoID, 1);

        SET @Resultado = SCOPE_IDENTITY();
        SET @Mensaje = 'Correo registrado correctamente.';
    END TRY
    BEGIN CATCH
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END
GO



CREATE PROCEDURE sp_EditarCorreo
    @CorreoID INT,
    @Correo VARCHAR(255),
    @TipoCorreoID INT,
    @Resultado BIT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        UPDATE Correo
        SET Correo = @Correo,
            TipoCorreoID = @TipoCorreoID
        WHERE CorreoID = @CorreoID;

        SET @Resultado = 1;
        SET @Mensaje = 'Correo actualizado correctamente.';
    END TRY
    BEGIN CATCH
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END
GO




CREATE PROCEDURE sp_EliminarCorreo
    @CorreoID INT,
    @Resultado BIT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        UPDATE Correo
        SET Activo = 0
        WHERE CorreoID = @CorreoID;

        SET @Resultado = 1;
        SET @Mensaje = 'Correo eliminado correctamente.';
    END TRY
    BEGIN CATCH
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END
GO


