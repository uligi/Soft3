USE Dunamis_SA
GO

CREATE PROCEDURE sp_ListarCanton
AS
BEGIN
    SELECT 
        c.CantonID, 
        c.Descripcion, 
        c.ProvinciaID, 
        p.Descripcion AS ProvinciaDescripcion
    FROM 
        Canton c
    INNER JOIN 
        Provincia p ON c.ProvinciaID = p.ProvinciaID
    WHERE
        c.Activo = 1;
END
GO


CREATE PROCEDURE sp_RegistrarCanton
    @Descripcion NVARCHAR(255),
    @ProvinciaID INT,
    @Resultado INT OUTPUT,
    @Mensaje NVARCHAR(500) OUTPUT
AS
BEGIN
    SET @Resultado = 0;
    SET @Mensaje = '';

    BEGIN TRY
        BEGIN TRANSACTION;

        INSERT INTO Canton (Descripcion, ProvinciaID, Activo)
        VALUES (@Descripcion, @ProvinciaID, 1);

        SET @Resultado = SCOPE_IDENTITY();

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END
GO



CREATE PROCEDURE sp_EditarCanton
    @CantonID INT,
    @Descripcion NVARCHAR(255),
    @ProvinciaID INT,
    @Resultado INT OUTPUT,
    @Mensaje NVARCHAR(500) OUTPUT
AS
BEGIN
    SET @Resultado = 0;
    SET @Mensaje = '';

    BEGIN TRY
        BEGIN TRANSACTION;

        UPDATE Canton
        SET Descripcion = @Descripcion,
            ProvinciaID = @ProvinciaID,
            Activo = 1 -- Asegurando que se mantenga activo al editar
        WHERE CantonID = @CantonID;

        SET @Resultado = @CantonID;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END
GO



USE Dunamis_SA
GO
CREATE PROCEDURE sp_EliminarCanton
    @CantonID INT,
    @Resultado BIT OUTPUT,
    @Mensaje NVARCHAR(500) OUTPUT
AS
BEGIN
    SET @Resultado = 0;
    SET @Mensaje = '';

    BEGIN TRY
        BEGIN TRANSACTION;

        -- Borrado lógico de los Distritos relacionados
        UPDATE Distrito
        SET Activo = 0
        WHERE CantonID = @CantonID;

        -- Borrado lógico del Cantón
        UPDATE Canton
        SET Activo = 0
        WHERE CantonID = @CantonID;

        SET @Resultado = 1;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END
GO


USE Dunamis_SA
GO
CREATE PROCEDURE sp_ListarCantonesPorProvincia
    @ProvinciaID INT
AS
BEGIN
    SELECT 
        CantonID, 
        Descripcion 
    FROM 
        Canton 
    WHERE 
        ProvinciaID = @ProvinciaID
        AND Activo = 1;
END
GO
