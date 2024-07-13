-- Crear el procedimiento almacenado para registrar un tipo de impuesto
USE Dunamis_SA
GO

CREATE PROCEDURE sp_RegistrarTipoImpuesto
    @Descripcion VARCHAR(255),
    @Resultado INT OUTPUT
AS
BEGIN
    SET @Resultado = 0;
    BEGIN TRY
        BEGIN TRANSACTION
            INSERT INTO TipoImpuesto (Descripcion) VALUES (@Descripcion);
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

-- Crear el procedimiento almacenado para actualizar un tipo de impuesto

USE Dunamis_SA
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
            UPDATE TipoImpuesto SET Descripcion = @Descripcion WHERE TipoImpuestoID = @TipoImpuestoID;
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

-- Crear el procedimiento almacenado para eliminar un tipo de impuesto

USE Dunamis_SA
GO

CREATE PROCEDURE sp_EliminarTipoImpuesto
    @TipoImpuestoID INT,
    @Resultado BIT OUTPUT
AS
BEGIN
    SET @Resultado = 0;
    BEGIN TRY
        BEGIN TRANSACTION
            DELETE FROM TipoImpuesto WHERE TipoImpuestoID = @TipoImpuestoID;
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
