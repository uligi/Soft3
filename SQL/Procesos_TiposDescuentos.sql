-- Crear el procedimiento almacenado para registrar un tipo de descuento
use Dunamis_SA
GO

CREATE PROCEDURE sp_RegistrarTipoDescuento
    @Descripcion VARCHAR(255),
    @Resultado INT OUTPUT
AS
BEGIN
    SET @Resultado = 0;
    BEGIN TRY
        BEGIN TRANSACTION
            INSERT INTO TipoDescuento (Descripcion, Activo) VALUES (@Descripcion, 1);
            SET @Resultado = SCOPE_IDENTITY();
        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION
        SET @Resultado = 0;
        RAISERROR('Error en el procedimiento RegistrarTipoDescuento', 16, 1);
    END CATCH
END
GO



CREATE PROCEDURE sp_ActualizarTipoDescuento
    @TipoDescuentoID INT,
    @Descripcion VARCHAR(255),
    @Resultado INT OUTPUT
AS
BEGIN
    SET @Resultado = 0;
    BEGIN TRY
        BEGIN TRANSACTION
            UPDATE TipoDescuento SET Descripcion = @Descripcion WHERE TipoDescuentoID = @TipoDescuentoID AND Activo = 1;
            SET @Resultado = @TipoDescuentoID;
        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION
        SET @Resultado = 0;
        RAISERROR('Error en el procedimiento ActualizarTipoDescuento', 16, 1);
    END CATCH
END
GO



CREATE PROCEDURE sp_EliminarTipoDescuento
    @TipoDescuentoID INT,
    @Resultado BIT OUTPUT
AS
BEGIN
    SET @Resultado = 0;
    BEGIN TRY
        BEGIN TRANSACTION
            UPDATE TipoDescuento SET Activo = 0 WHERE TipoDescuentoID = @TipoDescuentoID;
            SET @Resultado = 1;
        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION
        SET @Resultado = 0;
        RAISERROR('Error en el procedimiento EliminarTipoDescuento', 16, 1);
    END CATCH
END
GO
