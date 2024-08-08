
Use Dunamis_SA

GO

CREATE PROCEDURE sp_ListarTipoCliente
AS
BEGIN
    SELECT TipoClienteID, Descripcion
    FROM TipoCliente
    WHERE Activo = 1;
END
GO



CREATE PROCEDURE [dbo].[sp_RegistrarTipoCliente]
    @Descripcion VARCHAR(255),
    @Resultado INT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        INSERT INTO TipoCliente (Descripcion, Activo)
        VALUES (@Descripcion, 1);
        
        SET @Resultado = SCOPE_IDENTITY();
        SET @Mensaje = 'Tipo de Cliente registrado exitosamente.';
    END TRY
    BEGIN CATCH
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END
GO



CREATE PROCEDURE [dbo].[sp_EditarTipoCliente]
    @TipoClienteID INT,
    @Descripcion VARCHAR(255),
    @Resultado BIT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        UPDATE TipoCliente
        SET Descripcion = @Descripcion
        WHERE TipoClienteID = @TipoClienteID;
        
        SET @Resultado = 1;
        SET @Mensaje = 'Tipo de Cliente actualizado exitosamente.';
    END TRY
    BEGIN CATCH
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END
GO



CREATE PROCEDURE [dbo].[sp_EliminarTipoCliente]
    @TipoClienteID INT,
    @Resultado BIT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        UPDATE TipoCliente
        SET Activo = 0
        WHERE TipoClienteID = @TipoClienteID;
        
        SET @Resultado = 1;
        SET @Mensaje = 'Tipo de Cliente eliminado exitosamente.';
    END TRY
    BEGIN CATCH
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END
GO

