use Dunamis_SA
go

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

        INSERT INTO TiposDeCarga (Nombre, Descripcion, TarifaPorKilo, FechaRegistro, Activo)
        VALUES (@Nombre, @Descripcion, @TarifaPorKilo, GETDATE(), 1);

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
        WHERE TiposDeCargaID = @TiposDeCargaID AND Activo = 1;

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

        UPDATE TiposDeCarga
        SET Activo = 0
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
