-- Crear procedimientos almacenados
use Dunamis_SA

Go
-- Listar Roles
CREATE PROCEDURE sp_ListarRoles
AS
BEGIN
    SELECT r.RolID, r.Rol, r.TipoRolID, p.Descripcion AS TipoRolDescripcion
    FROM Roles r
    INNER JOIN Permisos p ON r.TipoRolID = p.PermisoID
END
GO

use Dunamis_SA

Go
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
        INSERT INTO Roles (Rol, TipoRolID) VALUES (@Rol, @TipoRolID)
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

use Dunamis_SA

Go
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
        UPDATE Roles SET Rol = @Rol, TipoRolID = @TipoRolID WHERE RolID = @RolID
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

use Dunamis_SA

Go
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


