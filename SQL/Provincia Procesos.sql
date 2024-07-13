use Dunamis_SA

GO

CREATE PROCEDURE sp_ListarProvincias
AS
BEGIN
    SELECT ProvinciaID, Descripcion
    FROM Provincia
END
GO

use Dunamis_SA

GO

CREATE PROCEDURE sp_RegistrarProvincia
    @Descripcion VARCHAR(255),
    @Resultado BIT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
AS
BEGIN
    BEGIN TRY
        INSERT INTO Provincia (Descripcion)
        VALUES (@Descripcion)

        SET @Resultado = 1
        SET @Mensaje = 'Provincia registrada correctamente.'
    END TRY
    BEGIN CATCH
        SET @Resultado = 0
        SET @Mensaje = ERROR_MESSAGE()
    END CATCH
END
GO


use Dunamis_SA

GO


CREATE PROCEDURE sp_EditarProvincia
    @ProvinciaID INT,
    @Descripcion VARCHAR(255),
    @Resultado BIT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
AS
BEGIN
    BEGIN TRY
        UPDATE Provincia
        SET Descripcion = @Descripcion
        WHERE ProvinciaID = @ProvinciaID

        SET @Resultado = 1
        SET @Mensaje = 'Provincia actualizada correctamente.'
    END TRY
    BEGIN CATCH
        SET @Resultado = 0
        SET @Mensaje = ERROR_MESSAGE()
    END CATCH
END
GO


use Dunamis_SA

GO

CREATE PROCEDURE sp_EliminarProvincia
    @ProvinciaID INT,
    @Resultado BIT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
AS
BEGIN
    BEGIN TRY
        DELETE FROM Provincia
        WHERE ProvinciaID = @ProvinciaID

        SET @Resultado = 1
        SET @Mensaje = 'Provincia eliminada correctamente.'
    END TRY
    BEGIN CATCH
        SET @Resultado = 0
        SET @Mensaje = ERROR_MESSAGE()
    END CATCH
END
GO
