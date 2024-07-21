use Dunamis_SA

GO

CREATE PROCEDURE sp_ListarPagosPorCliente
    @ClienteID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT P.PagoID, P.Descripcion, TP.Descripcion AS TipoPago
    FROM Pago P
    INNER JOIN TipoPago TP ON P.TipoPagoID = TP.TipoPagoID
    WHERE P.ClienteID = @ClienteID;
END
GO


-- Procedure to register a client
CREATE PROCEDURE [dbo].[sp_RegistrarCliente]
    @Cedula INT,
    @Nombre NVARCHAR(255),
    @Apellido1 NVARCHAR(255),
    @Apellido2 NVARCHAR(255),
    @Correo NVARCHAR(255),
    @TipoCorreoID INT,
    @TipoClienteID INT,
    @Activo BIT,
    @Resultado INT OUTPUT,
    @Mensaje NVARCHAR(500) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        DECLARE @CorreoID INT;

        -- Insert into Correo
        INSERT INTO Correo (Correo, TipoCorreoID)
        VALUES (@Correo, @TipoCorreoID);

        SET @CorreoID = SCOPE_IDENTITY();

        -- Insert into Persona
        INSERT INTO Persona (Cedula, Nombre, Apellido1, Apellido2, CorreoID)
        VALUES (@Cedula, @Nombre, @Apellido1, @Apellido2, @CorreoID);

        -- Insert into Clientes
        INSERT INTO Clientes (Cedula, Activo, TipoClienteID)
        VALUES (@Cedula, @Activo, @TipoClienteID);

        SET @Resultado = 1;
        SET @Mensaje = 'Cliente registrado exitosamente.';
    END TRY
    BEGIN CATCH
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END;
go


-- Procedure to update a client
CREATE PROCEDURE [dbo].[sp_ActualizarCliente]
    @ClienteID INT,
    @Cedula INT,
    @Nombre NVARCHAR(255),
    @Apellido1 NVARCHAR(255),
    @Apellido2 NVARCHAR(255),
    @Correo NVARCHAR(255),
    @TipoCorreoID INT,
    @TipoClienteID INT,
    @Activo BIT,
    @Resultado INT OUTPUT,
    @Mensaje NVARCHAR(500) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        DECLARE @CorreoID INT;

        -- Update Correo
        SELECT @CorreoID = CorreoID FROM Persona WHERE Cedula = @Cedula;

        UPDATE Correo
        SET Correo = @Correo, TipoCorreoID = @TipoCorreoID
        WHERE CorreoID = @CorreoID;

        -- Update Persona
        UPDATE Persona
        SET Nombre = @Nombre, Apellido1 = @Apellido1, Apellido2 = @Apellido2
        WHERE Cedula = @Cedula;

        -- Update Clientes
        UPDATE Clientes
        SET Activo = @Activo, TipoClienteID = @TipoClienteID
        WHERE ClienteID = @ClienteID;

        SET @Resultado = 1;
        SET @Mensaje = 'Cliente actualizado exitosamente.';
    END TRY
    BEGIN CATCH
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END;
go

-- Procedure to delete a client
CREATE PROCEDURE [dbo].[sp_EliminarCliente]
    @ClienteID INT,
    @Resultado BIT OUTPUT,
    @Mensaje NVARCHAR(500) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        DECLARE @Cedula INT;
        DECLARE @CorreoID INT;

        SELECT @Cedula = Cedula FROM Clientes WHERE ClienteID = @ClienteID;
        SELECT @CorreoID = CorreoID FROM Persona WHERE Cedula = @Cedula;

        -- Delete from Correo
        DELETE FROM Correo WHERE CorreoID = @CorreoID;

        -- Delete from Persona
        DELETE FROM Persona WHERE Cedula = @Cedula;

        -- Delete from Clientes
        DELETE FROM Clientes WHERE ClienteID = @ClienteID;

        SET @Resultado = 1;
        SET @Mensaje = 'Cliente eliminado exitosamente.';
    END TRY
    BEGIN CATCH
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END;
go

-- Procedure to activate/deactivate a client
CREATE PROCEDURE [dbo].[sp_ActivarDesactivarCliente]
    @ClienteID INT,
    @Activo BIT,
    @Resultado BIT OUTPUT,
    @Mensaje NVARCHAR(500) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        UPDATE Clientes
        SET Activo = @Activo
        WHERE ClienteID = @ClienteID;

        SET @Resultado = 1;
        SET @Mensaje = 'Estado del cliente actualizado exitosamente.';
    END TRY
    BEGIN CATCH
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END;
go

-- Procedure to list clients
CREATE PROCEDURE [dbo].[sp_ListarClientes]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT c.ClienteID, p.Cedula, p.Nombre, p.Apellido1, p.Apellido2, tc.Descripcion AS TipoCliente,
           c.Activo, c.Fecha, co.Correo AS Correo
    FROM Clientes c
    JOIN Persona p ON c.Cedula = p.Cedula
    JOIN Correo co ON p.CorreoID = co.CorreoID
    JOIN TipoCliente tc ON c.TipoClienteID = tc.TipoClienteID;
END;
go

CREATE PROCEDURE sp_RegistrarPago
    @Descripcion NVARCHAR(255),
    @TipoPagoID INT,
    @ClienteID INT,
    @Resultado INT OUTPUT,
    @Mensaje NVARCHAR(500) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        INSERT INTO Pago (Descripcion, TipoPagoID, ClienteID)
        VALUES (@Descripcion, @TipoPagoID, @ClienteID);

        SET @Resultado = SCOPE_IDENTITY();
        SET @Mensaje = 'Pago registrado correctamente.';
    END TRY
    BEGIN CATCH
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END
GO

CREATE PROCEDURE sp_EditarPago
    @PagoID INT,
    @Descripcion NVARCHAR(255),
    @TipoPagoID INT,
    @ClienteID INT,
    @Resultado BIT OUTPUT,
    @Mensaje NVARCHAR(500) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        UPDATE Pago
        SET Descripcion = @Descripcion,
            TipoPagoID = @TipoPagoID,
            ClienteID = @ClienteID
        WHERE PagoID = @PagoID;

        SET @Resultado = 1;
        SET @Mensaje = 'Pago editado correctamente.';
    END TRY
    BEGIN CATCH
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END
GO

CREATE PROCEDURE sp_EliminarPago
    @PagoID INT,
    @Resultado BIT OUTPUT,
    @Mensaje NVARCHAR(500) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        DELETE FROM Pago WHERE PagoID = @PagoID;
        SET @Resultado = 1;
        SET @Mensaje = 'Pago eliminado correctamente.';
    END TRY
    BEGIN CATCH
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END
GO