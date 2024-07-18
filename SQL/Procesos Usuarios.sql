USE Dunamis_SA
GO

create PROCEDURE sp_ListarUsuarios
AS
BEGIN
    SELECT u.UsuarioID, u.Cedula, p.Nombre, p.Apellido1, p.Apellido2, r.Rol, u.Activo, u.FechaCreacion, p.CorreoID,c.Correo
    FROM Usuarios u
    JOIN Persona p ON u.Cedula = p.Cedula
    JOIN Roles r ON u.RolID = r.RolID
	JOIN Correo c ON p.CorreoID = c.CorreoID;
END
GO




use Dunamis_SA

Go

Create PROCEDURE sp_RegistrarUsuario
    @Cedula INT,
    @Nombre VARCHAR(255),
    @Apellido1 VARCHAR(255),
    @Apellido2 VARCHAR(255),
    @Correo VARCHAR(255),
    @TipoCorreoID INT,
    @Contrasena NVARCHAR(255),
    @RolID INT,
    @Resultado INT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        -- Inserci�n en la tabla Correo
        DECLARE @CorreoID INT;
        INSERT INTO [dbo].[Correo] (Correo, TipoCorreoID)
        VALUES (@Correo, @TipoCorreoID);

        SET @CorreoID = SCOPE_IDENTITY();

        -- Inserci�n en la tabla Persona
        INSERT INTO [dbo].[Persona] (Cedula, Nombre, Apellido1, Apellido2, CorreoID)
        VALUES (@Cedula, @Nombre, @Apellido1, @Apellido2, @CorreoID);

        -- Obtener la fecha actual
        DECLARE @FechaCreacion DATE;
        SET @FechaCreacion = GETDATE();

        -- Inserci�n en la tabla Usuarios
        INSERT INTO [dbo].[Usuarios] (Contrasena, RestablecerContrase�a, Activo, FechaCreacion, Cedula, RolID)
        VALUES (
            CONVERT(varchar(255), HASHBYTES('SHA2_256', @Contrasena), 2),
            1,  -- RestablecerContrase�a
            1,  -- Activo
            @FechaCreacion,
            @Cedula,
            @RolID  -- RolID proporcionado por el usuario
        );

        SET @Resultado = 1;
        SET @Mensaje = 'Usuario registrado con �xito.';

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END
GO



USE Dunamis_SA
GO

USE Dunamis_SA
GO

CREATE PROCEDURE sp_EliminarUsuario
    @UsuarioID INT,
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
        FROM Usuarios
        WHERE UsuarioID = @UsuarioID;

        IF @Cedula IS NULL
        BEGIN
            SET @Resultado = 0;
            SET @Mensaje = 'Usuario no encontrado.';
            ROLLBACK TRANSACTION;
            RETURN;
        END

        SELECT @CorreoID = CorreoID
        FROM Persona
        WHERE Cedula = @Cedula;

        -- Eliminar la relaci�n del usuario
        DELETE FROM Usuarios WHERE UsuarioID = @UsuarioID;

        -- Eliminar la persona relacionada
        DELETE FROM Persona WHERE Cedula = @Cedula;

        -- Eliminar el correo relacionado
        DELETE FROM Correo WHERE CorreoID = @CorreoID;

        SET @Resultado = 1;
        SET @Mensaje = 'Usuario eliminado exitosamente.';

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END
GO





USE Dunamis_SA
GO

CREATE PROCEDURE sp_DesactivarUsuario
    @UsuarioID INT,
    @Resultado BIT OUTPUT,
    @Mensaje NVARCHAR(500) OUTPUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        UPDATE Usuarios
        SET Activo = 0
        WHERE UsuarioID = @UsuarioID;

        SET @Resultado = 1;
        SET @Mensaje = 'Usuario desactivado exitosamente.';

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END
GO




USE Dunamis_SA
GO

CREATE PROCEDURE sp_RestablecerContrasena
    @UsuarioID INT,
    @Contrasena NVARCHAR(255),
    @Resultado BIT OUTPUT,
    @Mensaje NVARCHAR(500) OUTPUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        UPDATE Usuarios
        SET Contrasena = CONVERT(varchar(255), HASHBYTES('SHA2_256', @Contrasena), 2),
            RestablecerContrase�a = 1
        WHERE UsuarioID = @UsuarioID;

        SET @Resultado = 1;
        SET @Mensaje = 'Contrase�a restablecida exitosamente.';

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END
GO



USE Dunamis_SA
GO

CREATE PROCEDURE sp_EditarUsuario
    @UsuarioID INT,
    @Cedula INT,
    @Nombre VARCHAR(255),
    @Apellido1 VARCHAR(255),
    @Apellido2 VARCHAR(255),
    @Correo VARCHAR(255),
    @TipoCorreoID INT,
    @RolID INT,
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

        -- Actualizaci�n en la tabla Correo
        UPDATE [dbo].[Correo]
        SET Correo = @Correo, TipoCorreoID = @TipoCorreoID
        WHERE CorreoID = @CorreoID;

        -- Actualizaci�n en la tabla Persona
        UPDATE [dbo].[Persona]
        SET Nombre = @Nombre, Apellido1 = @Apellido1, Apellido2 = @Apellido2
        WHERE Cedula = @Cedula;

        -- Actualizaci�n en la tabla Usuarios
        UPDATE [dbo].[Usuarios]
        SET RolID = @RolID
        WHERE UsuarioID = @UsuarioID;

        SET @Resultado = 1;
        SET @Mensaje = 'Usuario editado con �xito.';

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END
GO



