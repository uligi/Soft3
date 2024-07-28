-- Crear el procedimiento almacenado para registrar un tipo de impuesto
USE Dunamis_SA
GO

CREATE PROCEDURE sp_ListarTipoImpuestos
AS
BEGIN
    SELECT TipoImpuestoID, Descripcion, Activo
    FROM TipoImpuesto
    WHERE Activo = 1
END
GO


CREATE PROCEDURE sp_RegistrarTipoImpuesto
    @Descripcion VARCHAR(255),
    @Resultado INT OUTPUT
AS
BEGIN
    SET @Resultado = 0;
    BEGIN TRY
        BEGIN TRANSACTION
            INSERT INTO TipoImpuesto (Descripcion, Activo) VALUES (@Descripcion, 1);
            SET @Resultado = SCOPE_IDENTITY();
        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION
        SET @Resultado = 0;
        RAISERROR('Error en el procedimiento RegistrarTipoImpuesto', 16, 1);
    END CATCH
END
GO



CREATE PROCEDURE sp_ActualizarTipoImpuesto
    @TipoImpuestoID INT,
    @Descripcion VARCHAR(255),
    @Resultado INT OUTPUT
AS
BEGIN
    SET @Resultado = 0;
    BEGIN TRY
        BEGIN TRANSACTION
            UPDATE TipoImpuesto SET Descripcion = @Descripcion WHERE TipoImpuestoID = @TipoImpuestoID AND Activo = 1;
            SET @Resultado = @TipoImpuestoID;
        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION
        SET @Resultado = 0;
        RAISERROR('Error en el procedimiento ActualizarTipoImpuesto', 16, 1);
    END CATCH
END
GO


CREATE PROCEDURE sp_EliminarTipoImpuesto
    @TipoImpuestoID INT,
    @Resultado BIT OUTPUT
AS
BEGIN
    SET @Resultado = 0;
    BEGIN TRY
        BEGIN TRANSACTION
            UPDATE TipoImpuesto SET Activo = 0 WHERE TipoImpuestoID = @TipoImpuestoID;
            SET @Resultado = 1;
        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION
        SET @Resultado = 0;
        RAISERROR('Error en el procedimiento EliminarTipoImpuesto', 16, 1);
    END CATCH
END
GO
