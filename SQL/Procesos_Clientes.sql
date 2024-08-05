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
    WHERE P.ClienteID = @ClienteID AND P.Activo = 1;
END
GO


-- Procedure to register a client
Create PROCEDURE [dbo].[sp_RegistrarCliente]
    @Cedula INT,
    @Nombre NVARCHAR(255),
    @Apellido1 NVARCHAR(255),
    @Apellido2 NVARCHAR(255),
    @Correo NVARCHAR(255),
    @TipoCorreoID INT,
    @TipoClienteID INT,
    @Resultado INT OUTPUT,
    @Mensaje NVARCHAR(500) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        DECLARE @CorreoID INT;

        -- Insert into Correo
        INSERT INTO Correo (Correo, TipoCorreoID, Activo)
        VALUES (@Correo, @TipoCorreoID, 1);

        SET @CorreoID = SCOPE_IDENTITY();

        -- Insert into Persona
        INSERT INTO Persona (Cedula, Nombre, Apellido1, Apellido2, CorreoID, Activo)
        VALUES (@Cedula, @Nombre, @Apellido1, @Apellido2, @CorreoID, 1);

        -- Insert into Clientes
        INSERT INTO Clientes (Cedula, Activo, TipoClienteID)
        VALUES (@Cedula, 1, @TipoClienteID);

        SET @Resultado = 1;
        SET @Mensaje = 'Cliente registrado exitosamente.';
    END TRY
    BEGIN CATCH
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END;


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
        SET  TipoClienteID = @TipoClienteID
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

        -- Borrado lógico en Correo
        UPDATE Correo SET Activo = 0 WHERE CorreoID = @CorreoID;

        -- Borrado lógico en Persona
        UPDATE Persona SET Activo = 0 WHERE Cedula = @Cedula;

        -- Borrado lógico en Clientes
        UPDATE Clientes SET Activo = 0 WHERE ClienteID = @ClienteID;

        SET @Resultado = 1;
        SET @Mensaje = 'Cliente eliminado exitosamente.';
    END TRY
    BEGIN CATCH
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END;
GO


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
    JOIN TipoCliente tc ON c.TipoClienteID = tc.TipoClienteID
    WHERE c.Activo = 1 AND p.Activo = 1 AND co.Activo = 1;
END;
GO


CREATE PROCEDURE [dbo].[sp_ListarClientesparaAdmin]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT c.ClienteID, p.Cedula, p.Nombre, p.Apellido1, p.Apellido2, tc.Descripcion AS TipoCliente,
           c.Activo, c.Fecha, co.Correo AS Correo
    FROM Clientes c
    JOIN Persona p ON c.Cedula = p.Cedula
    JOIN Correo co ON p.CorreoID = co.CorreoID
    JOIN TipoCliente tc ON c.TipoClienteID = tc.TipoClienteID
    
END;
GO


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
        INSERT INTO Pago (Descripcion, TipoPagoID, ClienteID, Activo)
        VALUES (@Descripcion, @TipoPagoID, @ClienteID, 1);

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
        UPDATE Pago SET Activo = 0 WHERE PagoID = @PagoID;
        SET @Resultado = 1;
        SET @Mensaje = 'Pago eliminado correctamente.';
    END TRY
    BEGIN CATCH
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END
GO

CREATE PROCEDURE sp_ListarClientesPorCedula
    @Cedula INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Información del cliente
    SELECT 
        c.ClienteID,
        p.Cedula,
        p.Nombre,
        p.Apellido1,
        p.Apellido2,
        co.Correo AS Correo,
        tc.Descripcion AS TipoCliente,
        c.Activo,
        c.Fecha
    FROM 
        Clientes c
        INNER JOIN Persona p ON c.Cedula = p.Cedula
        INNER JOIN Correo co ON p.CorreoID = co.CorreoID
        INNER JOIN TipoCliente tc ON c.TipoClienteID = tc.TipoClienteID
    WHERE 
        p.Cedula = @Cedula AND c.Activo = 1 AND p.Activo = 1 AND co.Activo = 1;

    -- Teléfonos del cliente
    SELECT 
        t.TelefonoID,
        t.NumeroTelefono,
        tt.Descripcion AS TipoTelefono
    FROM 
        Telefono t
        INNER JOIN TipoTelefono tt ON t.TipoTelefonoID = tt.TipoTelefonoID
    WHERE 
        t.Cedula = @Cedula AND t.Activo = 1;

    -- Direcciones del cliente
    SELECT 
        d.DireccionID,
        d.NombreDireccion,
        d.DireccionDetallada,
        p.Descripcion AS Provincia,
        c.Descripcion AS Canton,
        di.Descripcion AS Distrito
    FROM 
        Direcciones d
        INNER JOIN Provincia p ON d.ProvinciaID = p.ProvinciaID
        INNER JOIN Canton c ON d.CantonID = c.CantonID
        INNER JOIN Distrito di ON d.DistritoID = di.DistritoID
    WHERE 
        d.ClienteID = (SELECT ClienteID FROM Clientes WHERE Cedula = @Cedula) AND d.Activo = 1;
END
GO

CREATE PROCEDURE sp_CambiarClave
    @UsuarioID INT,
    @NuevaClave VARCHAR(255),
    @Resultado BIT OUTPUT,
    @Mensaje NVARCHAR(500) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        -- Actualizar la contraseña y restablecer la bandera de restablecimiento
        UPDATE Usuarios
        SET Contrasena = @NuevaClave, RestablecerContraseña = 0
        WHERE UsuarioID = @UsuarioID;

        -- Verificar si se actualizó alguna fila
        IF @@ROWCOUNT > 0
        BEGIN
            SET @Resultado = 1;
            SET @Mensaje = 'Contraseña cambiada exitosamente.';
        END
        ELSE
        BEGIN
            SET @Resultado = 0;
            SET @Mensaje = 'No se encontró el usuario o la contraseña ya estaba actualizada.';
        END
    END TRY
    BEGIN CATCH
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END;
GO
