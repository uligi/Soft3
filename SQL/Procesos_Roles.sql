use Dunamis_SA

GO

CREATE PROCEDURE sp_ListarRoles
AS
BEGIN
    SELECT r.RolID, r.Rol, r.PermisoID, p.Descripcion AS TipoRolDescripcion
    FROM Roles r
    INNER JOIN Permisos p ON r.PermisoID = p.PermisoID
    WHERE r.Activo = 1;
END
GO



CREATE PROCEDURE sp_RegistrarRol
    @Rol NVARCHAR(255),
    @PermisoID INT,
    @Resultado BIT OUTPUT,
    @Mensaje NVARCHAR(500) OUTPUT
AS
BEGIN
    SET @Resultado = 0;
    BEGIN TRY
        BEGIN TRANSACTION;
        -- Check if PermisoID exists
        IF EXISTS (SELECT 1 FROM Permisos WHERE PermisoID = @PermisoID AND Activo = 1)
        BEGIN
            INSERT INTO Roles (Rol, PermisoID, Activo) VALUES (@Rol, @PermisoID, 1);
            SET @Resultado = 1;
            SET @Mensaje = 'Rol registrado exitosamente.';
            COMMIT TRANSACTION;
        END
        ELSE
        BEGIN
            SET @Resultado = 0;
            SET @Mensaje = 'PermisoID no existe.';
            ROLLBACK TRANSACTION;
        END
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END
GO



CREATE PROCEDURE sp_EditarRol
    @RolID INT,
    @Rol NVARCHAR(255),
    @PermisoID INT,
    @Resultado BIT OUTPUT,
    @Mensaje NVARCHAR(500) OUTPUT
AS
BEGIN
    SET @Resultado = 0;
    BEGIN TRY
        BEGIN TRANSACTION;
        -- Check if PermisoID exists
        IF EXISTS (SELECT 1 FROM Permisos WHERE PermisoID = @PermisoID AND Activo = 1)
        BEGIN
            UPDATE Roles SET Rol = @Rol, PermisoID = @PermisoID WHERE RolID = @RolID AND Activo = 1;
            SET @Resultado = 1;
            SET @Mensaje = 'Rol actualizado exitosamente.';
            COMMIT TRANSACTION;
        END
        ELSE
        BEGIN
            SET @Resultado = 0;
            SET @Mensaje = 'PermisoID no existe.';
            ROLLBACK TRANSACTION;
        END
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END
GO



CREATE PROCEDURE sp_EliminarRol
    @RolID INT,
    @Resultado BIT OUTPUT,
    @Mensaje NVARCHAR(500) OUTPUT
AS
BEGIN
    SET @Resultado = 0;
    BEGIN TRY
        BEGIN TRANSACTION;
        UPDATE Roles
        SET Activo = 0
        WHERE RolID = @RolID;
        SET @Resultado = 1;
        SET @Mensaje = 'Rol eliminado exitosamente.';
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END
GO







