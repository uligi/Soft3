
Use Dunamis_SA

GO

CREATE PROCEDURE sp_ListarTipoCliente
AS
BEGIN
    SELECT TipoClienteID, Descripcion
    FROM TipoCliente
END
GO

CREATE PROCEDURE sp_RegistrarTipoCliente
    @Descripcion VARCHAR(255),
    @Resultado BIT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
AS
BEGIN
    SET @Resultado = 0;
    BEGIN TRY
        INSERT INTO TipoCliente (Descripcion)
        VALUES (@Descripcion);
        SET @Resultado = 1;
        SET @Mensaje = 'Tipo de cliente registrado exitosamente.';
    END TRY
    BEGIN CATCH
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END
GO


CREATE PROCEDURE sp_EditarTipoCliente
    @TipoClienteID INT,
    @Descripcion VARCHAR(255),
    @Resultado BIT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
AS
BEGIN
    SET @Resultado = 0;
    BEGIN TRY
        UPDATE TipoCliente
        SET Descripcion = @Descripcion
        WHERE TipoClienteID = @TipoClienteID;
        SET @Resultado = 1;
        SET @Mensaje = 'Tipo de cliente actualizado exitosamente.';
    END TRY
    BEGIN CATCH
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END
GO


CREATE PROCEDURE sp_EliminarTipoCliente
    @TipoClienteID INT,
    @Resultado BIT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
AS
BEGIN
    SET @Resultado = 0;
    BEGIN TRY
        DELETE FROM TipoCliente
        WHERE TipoClienteID = @TipoClienteID;
        SET @Resultado = 1;
        SET @Mensaje = 'Tipo de pago eliminado exitosamente.';
    END TRY
    BEGIN CATCH
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END
GO
