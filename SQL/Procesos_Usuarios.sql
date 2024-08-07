USE Dunamis_SA
GO

CREATE  PROCEDURE [dbo].[sp_ListarUsuarios]
AS
BEGIN
    SELECT u.UsuarioID, u.Cedula, u.Contrasena,u.RestablecerContraseña, p.Nombre, p.Apellido1, p.Apellido2, r.Rol, u.Activo, u.FechaCreacion, p.CorreoID, c.Correo
    FROM Usuarios u
    JOIN Persona p ON u.Cedula = p.Cedula
    JOIN Roles r ON u.RolID = r.RolID
    JOIN Correo c ON p.CorreoID = c.CorreoID
    WHERE u.Activo = 1;
END
GO



CREATE PROCEDURE sp_RegistrarUsuario
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

        -- Inserción en la tabla Correo
        DECLARE @CorreoID INT;
        INSERT INTO [dbo].[Correo] (Correo, TipoCorreoID, Activo)
        VALUES (@Correo, @TipoCorreoID, 1);

        SET @CorreoID = SCOPE_IDENTITY();

        -- Inserción en la tabla Persona
        INSERT INTO [dbo].[Persona] (Cedula, Nombre, Apellido1, Apellido2, CorreoID, Activo)
        VALUES (@Cedula, @Nombre, @Apellido1, @Apellido2, @CorreoID, 1);

        -- Obtener la fecha actual
        DECLARE @FechaCreacion DATE;
        SET @FechaCreacion = GETDATE();

        -- Inserción en la tabla Usuarios
        INSERT INTO [dbo].[Usuarios] (Contrasena, RestablecerContraseña, Activo, FechaCreacion, Cedula, RolID)
        VALUES (
            @Contrasena,
            1,  -- RestablecerContraseña
            1,  -- Activo
            @FechaCreacion,
            @Cedula,
            @RolID  -- RolID proporcionado por el usuario
        );

        SET @Resultado = 1;
        SET @Mensaje = 'Usuario registrado con éxito.';

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END
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

        -- Borrado lógico del usuario
        UPDATE Usuarios SET Activo = 0 WHERE UsuarioID = @UsuarioID;

        -- Borrado lógico de la persona
        UPDATE Persona SET Activo = 0 WHERE Cedula = @Cedula;

        -- Borrado lógico del correo
        UPDATE Correo SET Activo = 0 WHERE CorreoID = @CorreoID;

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
            RestablecerContraseña = 1
        WHERE UsuarioID = @UsuarioID AND Activo = 1;

        SET @Resultado = 1;
        SET @Mensaje = 'Contraseña restablecida exitosamente.';

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END
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

        -- Actualización en la tabla Correo
        UPDATE [dbo].[Correo]
        SET Correo = @Correo, TipoCorreoID = @TipoCorreoID
        WHERE CorreoID = @CorreoID AND Activo = 1;

        -- Actualización en la tabla Persona
        UPDATE [dbo].[Persona]
        SET Nombre = @Nombre, Apellido1 = @Apellido1, Apellido2 = @Apellido2
        WHERE Cedula = @Cedula AND Activo = 1;

        -- Actualización en la tabla Usuarios
        UPDATE [dbo].[Usuarios]
        SET RolID = @RolID
        WHERE UsuarioID = @UsuarioID AND Activo = 1;

        SET @Resultado = 1;
        SET @Mensaje = 'Usuario editado con éxito.';

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END
GO




