
Use Dunamis_SA

GO

CREATE PROCEDURE sp_ListarTipoPago
AS
BEGIN
    SELECT TipoPagoID, Descripcion
    FROM TipoPago
    WHERE Activo = 1;
END
GO


CREATE PROCEDURE sp_RegistrarTipoPago
    @Descripcion VARCHAR(255),
    @Resultado BIT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
AS
BEGIN
    SET @Resultado = 0;
    BEGIN TRY
        INSERT INTO TipoPago (Descripcion, Activo)
        VALUES (@Descripcion, 1);
        SET @Resultado = 1;
        SET @Mensaje = 'Tipo de pago registrado exitosamente.';
    END TRY
    BEGIN CATCH
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END
GO



CREATE PROCEDURE sp_EditarTipoPago
    @TipoPagoID INT,
    @Descripcion VARCHAR(255),
    @Resultado BIT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
AS
BEGIN
    SET @Resultado = 0;
    BEGIN TRY
        UPDATE TipoPago
        SET Descripcion = @Descripcion
        WHERE TipoPagoID = @TipoPagoID AND Activo = 1;
        SET @Resultado = 1;
        SET @Mensaje = 'Tipo de pago actualizado exitosamente.';
    END TRY
    BEGIN CATCH
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END
GO



CREATE PROCEDURE sp_EliminarTipoPago
    @TipoPagoID INT,
    @Resultado BIT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
AS
BEGIN
    SET @Resultado = 0;
    BEGIN TRY
        UPDATE TipoPago
        SET Activo = 0
        WHERE TipoPagoID = @TipoPagoID;
        SET @Resultado = 1;
        SET @Mensaje = 'Tipo de pago eliminado exitosamente.';
    END TRY
    BEGIN CATCH
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END
GO
