use Dunamis_SA

GO

CREATE PROCEDURE sp_ListarImpuestos
AS
BEGIN
    SELECT i.ImpuestoID, i.Porcentaje, i.TipoImpuestoID, t.Descripcion
    FROM Impuesto i
    INNER JOIN TipoImpuesto t ON i.TipoImpuestoID = t.TipoImpuestoID
    WHERE i.Activo = 1;
END
GO



CREATE PROCEDURE sp_RegistrarImpuesto
    @Porcentaje DECIMAL(5,2),
    @TipoImpuestoID INT,
    @Resultado INT OUTPUT,
    @Mensaje NVARCHAR(500) OUTPUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        INSERT INTO Impuesto (Porcentaje, TipoImpuestoID, Activo)
        VALUES (@Porcentaje, @TipoImpuestoID, 1);

        SET @Resultado = SCOPE_IDENTITY();
        SET @Mensaje = 'Impuesto registrado exitosamente.';

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END
GO



CREATE PROCEDURE sp_EditarImpuesto
    @ImpuestoID INT,
    @Porcentaje DECIMAL(5,2),
    @TipoImpuestoID INT,
    @Resultado BIT OUTPUT,
    @Mensaje NVARCHAR(500) OUTPUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        UPDATE Impuesto
        SET Porcentaje = @Porcentaje, TipoImpuestoID = @TipoImpuestoID
        WHERE ImpuestoID = @ImpuestoID;

        SET @Resultado = 1;
        SET @Mensaje = 'Impuesto actualizado exitosamente.';

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END
GO



CREATE PROCEDURE sp_EliminarImpuesto
    @ImpuestoID INT,
    @Resultado BIT OUTPUT,
    @Mensaje NVARCHAR(500) OUTPUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        UPDATE Impuesto
        SET Activo = 0
        WHERE ImpuestoID = @ImpuestoID;

        SET @Resultado = 1;
        SET @Mensaje = 'Impuesto eliminado exitosamente.';

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END
GO


CREATE PROCEDURE sp_ListarTiposImpuesto
AS
BEGIN
    SELECT TipoImpuestoID, Descripcion
    FROM TipoImpuesto
    WHERE Activo = 1;
END
GO
