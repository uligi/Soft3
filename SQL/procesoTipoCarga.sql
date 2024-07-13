USE [Dunamis_SA]
GO

-- Crear el procedimiento almacenado para registrar un tipo de carga
CREATE PROCEDURE sp_RegistrarTipoDeCarga
    @Nombre VARCHAR(255),
    @Descripcion VARCHAR(255),
    @Stock INT,
    @TarifaPorKilo DECIMAL(10, 2),
    @Resultado INT OUTPUT
AS
BEGIN
    SET @Resultado = 0;
    BEGIN TRY
        BEGIN TRANSACTION
            INSERT INTO TiposDeCarga (Nombre, Descripcion, Stock, TarifaPorKilo, FechaRegistro)
            VALUES (@Nombre, @Descripcion, @Stock, @TarifaPorKilo, GETDATE());
            SET @Resultado = SCOPE_IDENTITY();
        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION
        SET @Resultado = 0;
        THROW;
    END CATCH
END
GO


USE [Dunamis_SA]
GO


-- Crear el procedimiento almacenado para actualizar un tipo de carga
CREATE PROCEDURE [dbo].[sp_ActualizarTipoDeCarga]
    @TiposDeCargaID INT,
    @Nombre VARCHAR(255),
    @Descripcion VARCHAR(255),
    @Stock INT,
    @TarifaPorKilo DECIMAL(10, 2),
    @Resultado INT OUTPUT
AS
BEGIN
    SET @Resultado = 0;
    BEGIN TRY
        BEGIN TRANSACTION
            UPDATE TiposDeCarga
            SET Nombre = @Nombre, Descripcion = @Descripcion, Stock = @Stock, TarifaPorKilo = @TarifaPorKilo
            WHERE TiposDeCargaID = @TiposDeCargaID;
            SET @Resultado = @TiposDeCargaID;
        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION
        SET @Resultado = 0;
        THROW;
    END CATCH
END
GO


USE [Dunamis_SA]
GO

-- Crear el procedimiento almacenado para eliminar un tipo de carga
CREATE PROCEDURE sp_EliminarTipoDeCarga
    @TiposDeCargaID INT,
    @Resultado BIT OUTPUT
AS
BEGIN
    SET @Resultado = 0;
    BEGIN TRY
        BEGIN TRANSACTION
            DELETE FROM TiposDeCarga WHERE TiposDeCargaID = @TiposDeCargaID;
            SET @Resultado = 1;
        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION
        SET @Resultado = 0;
        THROW;
    END CATCH
END
GO
