USE Dunamis_SA
GO

create PROCEDURE sp_ListarClientes
AS
BEGIN
    SELECT u.ClienteID, u.Cedula, p.Nombre, p.Apellido1, p.Apellido2, r.TipoClienteID, u.Activo, p.CorreoID,c.Correo
    FROM Clientes u
    JOIN Persona p ON u.Cedula = p.Cedula
    JOIN TipoCliente r ON u.TipoClienteID = r.TipoClienteID
	JOIN Correo c ON p.CorreoID = c.CorreoID;
END
GO


Create PROCEDURE sp_RegistrarClientes
    @Cedula INT,
    @Nombre VARCHAR(255),
    @Apellido1 VARCHAR(255),
    @Apellido2 VARCHAR(255),
    @Correo VARCHAR(255),
    @TipoCorreoID INT,
    @TipoClienteID INT,
    @Resultado INT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        -- Inserción en la tabla Correo
        DECLARE @CorreoID INT;
        INSERT INTO [dbo].[Correo] (Correo, TipoCorreoID)
        VALUES (@Correo, @TipoCorreoID);

        SET @CorreoID = SCOPE_IDENTITY();

        -- Inserción en la tabla Persona
        INSERT INTO [dbo].[Persona] (Cedula, Nombre, Apellido1, Apellido2, CorreoID)
        VALUES (@Cedula, @Nombre, @Apellido1, @Apellido2, @CorreoID);

        -- Obtener la fecha actual
        DECLARE @FechaCreacion DATE;
        SET @FechaCreacion = GETDATE();

        -- Inserción en la tabla Clientes
        INSERT INTO [dbo].[Clientes] (Activo, Cedula, TipoClienteID)
        VALUES (
            1,  -- Activo
            @Cedula,
            @TipoClienteID  -- TipoClienteID proporcionado por el cliete
        );

        SET @Resultado = 1;
        SET @Mensaje = 'Cliente registrado con éxito.';

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END
GO


CREATE PROCEDURE sp_EliminarCliente
    @ClienteID INT,
    @Resultado BIT OUTPUT,
    @Mensaje NVARCHAR(500) OUTPUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Primero, obtener los IDs relacionados
        DECLARE @Cedula INT;
        DECLARE @CorreoID INT;

        SELECT @Cedula = Cedula
        FROM Clientes
        WHERE ClienteID = @ClienteID;

        IF @Cedula IS NULL
        BEGIN
            SET @Resultado = 0;
            SET @Mensaje = 'Cliente no encontrado.';
            ROLLBACK TRANSACTION;
            RETURN;
        END

        SELECT @CorreoID = CorreoID
        FROM Persona
        WHERE Cedula = @Cedula;

        -- Eliminar la relación del Cliente
        DELETE FROM Clientes WHERE ClienteID = @ClienteID;

        -- Eliminar la persona relacionada
        DELETE FROM Persona WHERE Cedula = @Cedula;

        -- Eliminar el correo relacionado
        DELETE FROM Correo WHERE CorreoID = @CorreoID;

        SET @Resultado = 1;
        SET @Mensaje = 'Cliente eliminado exitosamente.';

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END
GO


CREATE PROCEDURE sp_DesactivarCliente
    @ClienteID INT,
    @Resultado BIT OUTPUT,
    @Mensaje NVARCHAR(500) OUTPUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        UPDATE Clientes
        SET Activo = 0
        WHERE ClienteID = @ClienteID;

        SET @Resultado = 1;
        SET @Mensaje = 'Cliente desactivado exitosamente.';

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END
GO

CREATE PROCEDURE sp_EditarCliente
    @ClienteID INT,
    @Cedula INT,
    @Nombre VARCHAR(255),
    @Apellido1 VARCHAR(255),
    @Apellido2 VARCHAR(255),
    @Correo VARCHAR(255),
    @TipoCorreoID INT,
    @TipoClienteID INT,
    @Resultado INT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        -- Obtener CorreoID asociado a la persona
        DECLARE @CorreoID INT;
        SELECT @CorreoID = CorreoID FROM [dbo].[Persona] WHERE Cedula = @Cedula;

        -- Actualización en la tabla Correo
        UPDATE [dbo].[Correo]
        SET Correo = @Correo, TipoCorreoID = @TipoCorreoID
        WHERE CorreoID = @CorreoID;

        -- Actualización en la tabla Persona
        UPDATE [dbo].[Persona]
        SET Nombre = @Nombre, Apellido1 = @Apellido1, Apellido2 = @Apellido2
        WHERE Cedula = @Cedula;

        -- Actualización en la tabla Cliente
        UPDATE [dbo].[Clientes]
        SET TipoClienteID = @TipoClienteID
        WHERE ClienteID = @ClienteID;

        SET @Resultado = 1;
        SET @Mensaje = 'Cliente editado con éxito.';

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END
GO



