use Dunamis_SA

GO
CREATE PROCEDURE sp_RegistrarTipoDeCarga
    @Nombre NVARCHAR(255),
    @Descripcion NVARCHAR(255),
    @TarifaPorKilo DECIMAL(10, 2),
    @Resultado INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET @Resultado = 0;

    BEGIN TRY
        BEGIN TRANSACTION;

        INSERT INTO TiposDeCarga (Nombre, Descripcion, TarifaPorKilo, FechaRegistro)
        VALUES (@Nombre, @Descripcion, @TarifaPorKilo, GETDATE());

        SET @Resultado = SCOPE_IDENTITY();

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        SET @Resultado = 0;
        THROW;
    END CATCH
END;
GO


CREATE PROCEDURE sp_EditarTipoDeCarga
    @TiposDeCargaID INT,
    @Nombre NVARCHAR(255),
    @Descripcion NVARCHAR(255),
    @TarifaPorKilo DECIMAL(10, 2),
    @Resultado BIT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET @Resultado = 0;

    BEGIN TRY
        BEGIN TRANSACTION;

        UPDATE TiposDeCarga
        SET Nombre = @Nombre,
            Descripcion = @Descripcion,
            TarifaPorKilo = @TarifaPorKilo
        WHERE TiposDeCargaID = @TiposDeCargaID;

        SET @Resultado = 1;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        SET @Resultado = 0;
        THROW;
    END CATCH
END;
GO


CREATE PROCEDURE sp_EliminarTipoDeCarga
    @TiposDeCargaID INT,
    @Resultado BIT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET @Resultado = 0;

    BEGIN TRY
        BEGIN TRANSACTION;

        DELETE FROM TiposDeCarga
        WHERE TiposDeCargaID = @TiposDeCargaID;

        SET @Resultado = 1;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        SET @Resultado = 0;
        THROW;
    END CATCH
END;
GO
