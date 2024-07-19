USE Dunamis_SA
GO

CREATE PROCEDURE sp_ListarDistrito
AS
BEGIN
    SELECT 
        d.DistritoID, 
        d.Descripcion, 
        d.CantonID, 
        c.Descripcion AS CantonDescripcion
    FROM 
        Distrito d
    INNER JOIN 
        Canton c ON d.CantonID = c.CantonID;
END
GO

CREATE PROCEDURE sp_RegistrarDistrito
    @Descripcion NVARCHAR(255),
    @CantonID INT,
    @Resultado INT OUTPUT,
    @Mensaje NVARCHAR(500) OUTPUT
AS
BEGIN
    SET @Resultado = 0;
    SET @Mensaje = '';

    BEGIN TRY
        BEGIN TRANSACTION;

        INSERT INTO Distrito (Descripcion, CantonID)
        VALUES (@Descripcion, @CantonID);

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

CREATE PROCEDURE sp_EditarDistrito
    @DistritoID INT,
    @Descripcion NVARCHAR(255),
    @CantonID INT,
    @Resultado INT OUTPUT,
    @Mensaje NVARCHAR(500) OUTPUT
AS
BEGIN
    SET @Resultado = 0;
    SET @Mensaje = '';

    BEGIN TRY
        BEGIN TRANSACTION;

        UPDATE Distrito
        SET Descripcion = @Descripcion,
            CantonID = @CantonID
        WHERE DistritoID = @DistritoID;

        SET @Resultado = @DistritoID;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END
GO

CREATE PROCEDURE sp_EliminarDistrito
    @DistritoID INT,
    @Resultado BIT OUTPUT,
    @Mensaje NVARCHAR(500) OUTPUT
AS
BEGIN
    SET @Resultado = 0;
    SET @Mensaje = '';

    BEGIN TRY
        BEGIN TRANSACTION;

        DELETE FROM Distrito
        WHERE DistritoID = @DistritoID;

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
