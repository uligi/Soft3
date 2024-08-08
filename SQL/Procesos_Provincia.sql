USE Dunamis_SA
GO

CREATE PROCEDURE sp_ListarProvincias
AS
BEGIN
    SELECT ProvinciaID, Descripcion
    FROM Provincia
    WHERE Activo = 1;
END
GO


CREATE PROCEDURE sp_RegistrarProvincia
    @Descripcion VARCHAR(255),
    @Resultado BIT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
AS
BEGIN
    BEGIN TRY
        INSERT INTO Provincia (Descripcion, Activo)
        VALUES (@Descripcion, 1);

        SET @Resultado = 1;
        SET @Mensaje = 'Provincia registrada correctamente.';
    END TRY
    BEGIN CATCH
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END
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
        WHERE ProvinciaID = @ProvinciaID;

        SET @Resultado = 1;
        SET @Mensaje = 'Provincia actualizada correctamente.';
    END TRY
    BEGIN CATCH
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END
GO


CREATE PROCEDURE sp_EliminarProvincia
    @ProvinciaID INT,
    @Resultado BIT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Borrado lógico de Distritos relacionados
        UPDATE Distrito
        SET Activo = 0
        WHERE CantonID IN (SELECT CantonID FROM Canton WHERE ProvinciaID = @ProvinciaID);

        -- Borrado lógico de Cantones relacionados
        UPDATE Canton
        SET Activo = 0
        WHERE ProvinciaID = @ProvinciaID;

        -- Borrado lógico de la Provincia
        UPDATE Provincia
        SET Activo = 0
        WHERE ProvinciaID = @ProvinciaID;

        SET @Resultado = 1;
        SET @Mensaje = 'Provincia eliminada correctamente.';
        
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END
GO

