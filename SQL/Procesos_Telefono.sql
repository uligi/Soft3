use Dunamis_SA

GO

-- Procedimiento para listar tel�fonos
CREATE PROCEDURE sp_ListarTelefonos
AS
BEGIN
    SELECT t.TelefonoID, t.NumeroTelefono, t.Cedula, t.TipoTelefonoID, tt.Descripcion AS TipoTelefonoDescripcion
    FROM Telefono t
    INNER JOIN TipoTelefono tt ON t.TipoTelefonoID = tt.TipoTelefonoID
    WHERE t.Activo = 1;
END
GO

-- Procedimiento para registrar tel�fono
CREATE PROCEDURE sp_RegistrarTelefono
    @NumeroTelefono VARCHAR(45),
    @Cedula INT,
    @TipoTelefonoID INT,
    @Resultado INT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
AS
BEGIN
    BEGIN TRY
        INSERT INTO Telefono (NumeroTelefono, Cedula, TipoTelefonoID, Activo)
        VALUES (@NumeroTelefono, @Cedula, @TipoTelefonoID, 1);

        SET @Resultado = SCOPE_IDENTITY();
        SET @Mensaje = 'Tel�fono registrado exitosamente.';
    END TRY
    BEGIN CATCH
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END
GO

-- Procedimiento para editar tel�fono
CREATE PROCEDURE sp_EditarTelefono
    @TelefonoID INT,
    @NumeroTelefono VARCHAR(45),
    @Cedula INT,
    @TipoTelefonoID INT,
    @Resultado INT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
AS
BEGIN
    BEGIN TRY
        UPDATE Telefono
        SET NumeroTelefono = @NumeroTelefono, Cedula = @Cedula, TipoTelefonoID = @TipoTelefonoID
        WHERE TelefonoID = @TelefonoID AND Activo = 1;

        SET @Resultado = @TelefonoID;
        SET @Mensaje = 'Tel�fono actualizado exitosamente.';
    END TRY
    BEGIN CATCH
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END
GO

-- Procedimiento para eliminar tel�fono
CREATE PROCEDURE sp_EliminarTelefono
    @TelefonoID INT,
    @Resultado BIT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
AS
BEGIN
    BEGIN TRY
        UPDATE Telefono
        SET Activo = 0
        WHERE TelefonoID = @TelefonoID;

        SET @Resultado = 1;
        SET @Mensaje = 'Tel�fono eliminado exitosamente.';
    END TRY
    BEGIN CATCH
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END
GO



-- Procedimiento para listar tel�fonos por cliente
CREATE PROCEDURE sp_ListarTelefonosPorCliente
    @ClienteID INT
AS
BEGIN
    SELECT t.TelefonoID, t.NumeroTelefono, t.Cedula, t.TipoTelefonoID, tt.Descripcion AS TipoTelefono
    FROM Telefono t
    INNER JOIN TipoTelefono tt ON t.TipoTelefonoID = tt.TipoTelefonoID
    WHERE t.Cedula = @ClienteID AND t.Activo = 1;
END
GO
