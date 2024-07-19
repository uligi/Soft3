-- Crear procedimientos almacenados
use Dunamis_SA

Go

CREATE PROCEDURE sp_ListarRoles
AS
BEGIN
    SELECT r.RolID, r.Rol, r.PermisoID, p.Descripcion AS TipoRolDescripcion
    FROM Roles r
    INNER JOIN Permisos p ON r.PermisoID = p.PermisoID
END
GO


-- Registrar Rol
CREATE PROCEDURE sp_RegistrarRol
    @Rol NVARCHAR(255),
    @TipoRolID INT,
    @Resultado BIT OUTPUT,
    @Mensaje NVARCHAR(500) OUTPUT
AS
BEGIN
    SET @Resultado = 0
    BEGIN TRY
        BEGIN TRANSACTION
        INSERT INTO Roles (Rol, PermisoID) VALUES (@Rol, @TipoRolID)
        SET @Resultado = 1
        SET @Mensaje = 'Rol registrado exitosamente.'
        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION
        SET @Resultado = 0
        SET @Mensaje = ERROR_MESSAGE()
    END CATCH
END
GO


-- Editar Rol
CREATE PROCEDURE sp_EditarRol
    @RolID INT,
    @Rol NVARCHAR(255),
    @TipoRolID INT,
    @Resultado BIT OUTPUT,
    @Mensaje NVARCHAR(500) OUTPUT
AS
BEGIN
    SET @Resultado = 0
    BEGIN TRY
        BEGIN TRANSACTION
        UPDATE Roles SET Rol = @Rol, PermisoID = @TipoRolID WHERE RolID = @RolID
        SET @Resultado = 1
        SET @Mensaje = 'Rol actualizado exitosamente.'
        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION
        SET @Resultado = 0
        SET @Mensaje = ERROR_MESSAGE()
    END CATCH
END
GO


-- Eliminar Rol
CREATE PROCEDURE sp_EliminarRol
    @RolID INT,
    @Resultado BIT OUTPUT,
    @Mensaje NVARCHAR(500) OUTPUT
AS
BEGIN
    SET @Resultado = 0
    BEGIN TRY
        BEGIN TRANSACTION
        DELETE FROM Roles WHERE RolID = @RolID
        SET @Resultado = 1
        SET @Mensaje = 'Rol eliminado exitosamente.'
        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION
        SET @Resultado = 0
        SET @Mensaje = ERROR_MESSAGE()
    END CATCH
END
GO


