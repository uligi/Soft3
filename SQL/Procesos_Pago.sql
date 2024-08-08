Use Dunamis_SA

GO

CREATE PROCEDURE sp_RegistrarPago
    @Descripcion NVARCHAR(255),
    @TipoPagoID INT,
    @ClienteID INT,
    @Activo BIT,
    @Resultado INT OUTPUT,
    @Mensaje NVARCHAR(500) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        INSERT INTO Pago (Descripcion, TipoPagoID, ClienteID, Activo)
        VALUES (@Descripcion, @TipoPagoID, @ClienteID, @Activo);
        
        SET @Resultado = SCOPE_IDENTITY();
        SET @Mensaje = 'Pago registrado correctamente';
    END TRY
    BEGIN CATCH
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END
GO
