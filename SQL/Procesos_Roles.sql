

Create PROCEDURE sp_ListarRoles
AS
BEGIN
    SELECT r.RolID, r.Rol, r.PermisoID, p.Descripcion AS TipoRolDescripcion
    FROM Roles r
    INNER JOIN Permisos p ON r.PermisoID = p.PermisoID
END
GO


USE [Dunamis_SA]
GO
/****** Object:  StoredProcedure [dbo].[sp_ListarPagosPorCliente]    Script Date: 21/07/2024 16:56:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE sp_RegistrarRol
    @Rol NVARCHAR(255),
    @PermisoID INT,
    @Resultado BIT OUTPUT,
    @Mensaje NVARCHAR(500) OUTPUT
AS
BEGIN
    SET @Resultado = 0
    BEGIN TRY
        BEGIN TRANSACTION
        -- Check if PermisoID exists
        IF EXISTS (SELECT 1 FROM Permisos WHERE PermisoID = @PermisoID)
        BEGIN
            INSERT INTO Roles (Rol, PermisoID) VALUES (@Rol, @PermisoID)
            SET @Resultado = 1
            SET @Mensaje = 'Rol registrado exitosamente.'
            COMMIT TRANSACTION
        END
        ELSE
        BEGIN
            SET @Resultado = 0
            SET @Mensaje = 'PermisoID no existe.'
            ROLLBACK TRANSACTION
        END
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION
        SET @Resultado = 0
        SET @Mensaje = ERROR_MESSAGE()
    END CATCH
END
GO


Create PROCEDURE sp_EditarRol
    @RolID INT,
    @Rol NVARCHAR(255),
    @PermisoID INT,
    @Resultado BIT OUTPUT,
    @Mensaje NVARCHAR(500) OUTPUT
AS
BEGIN
    SET @Resultado = 0
    BEGIN TRY
        BEGIN TRANSACTION
        -- Check if PermisoID exists
        IF EXISTS (SELECT 1 FROM Permisos WHERE PermisoID = @PermisoID)
        BEGIN
            UPDATE Roles SET Rol = @Rol, PermisoID = @PermisoID WHERE RolID = @RolID
            SET @Resultado = 1
            SET @Mensaje = 'Rol actualizado exitosamente.'
            COMMIT TRANSACTION
        END
        ELSE
        BEGIN
            SET @Resultado = 0
            SET @Mensaje = 'PermisoID no existe.'
            ROLLBACK TRANSACTION
        END
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION
        SET @Resultado = 0
        SET @Mensaje = ERROR_MESSAGE()
    END CATCH
END
GO


Create PROCEDURE sp_EliminarRol
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






