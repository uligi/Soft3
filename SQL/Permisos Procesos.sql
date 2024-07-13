USE [Dunamis_SA]
GO
/****** Object:  StoredProcedure [dbo].[sp_ListarPermisos]    Script Date: 13/07/2024 13:11:09 ******/


Create PROCEDURE sp_ListarPermisos
AS
BEGIN
    SELECT PermisoID, Descripcion FROM Permisos
END
GO

USE [Dunamis_SA]
GO
/****** Object:  StoredProcedure [dbo].[sp_ListarPermisos]    Script Date: 13/07/2024 13:11:09 ******/


Create PROCEDURE sp_RegistrarPermiso
    @Descripcion VARCHAR(255),
    @Resultado BIT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
AS
BEGIN
    SET @Resultado = 0
    SET @Mensaje = ''
    BEGIN TRY
        BEGIN TRANSACTION
            INSERT INTO Permisos (Descripcion) VALUES (@Descripcion)
            SET @Resultado = 1
            SET @Mensaje = 'Permiso registrado exitosamente'
        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION
        SET @Resultado = 0
        SET @Mensaje = ERROR_MESSAGE()
    END CATCH
END
GO


USE [Dunamis_SA]
GO
/****** Object:  StoredProcedure [dbo].[sp_ListarPermisos]    Script Date: 13/07/2024 13:11:09 ******/


Create PROCEDURE sp_EditarPermiso
    @PermisoID INT,
    @Descripcion VARCHAR(255),
    @Resultado BIT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
AS
BEGIN
    SET @Resultado = 0
    SET @Mensaje = ''
    BEGIN TRY
        BEGIN TRANSACTION
            UPDATE Permisos SET Descripcion = @Descripcion WHERE PermisoID = @PermisoID
            SET @Resultado = 1
            SET @Mensaje = 'Permiso actualizado exitosamente'
        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION
        SET @Resultado = 0
        SET @Mensaje = ERROR_MESSAGE()
    END CATCH
END
GO



USE [Dunamis_SA]
GO
/****** Object:  StoredProcedure [dbo].[sp_ListarPermisos]    Script Date: 13/07/2024 13:11:09 ******/


Create PROCEDURE sp_EliminarPermiso
    @PermisoID INT,
    @Resultado BIT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
AS
BEGIN
    SET @Resultado = 0
    SET @Mensaje = ''
    BEGIN TRY
        BEGIN TRANSACTION
            DELETE FROM Permisos WHERE PermisoID = @PermisoID
            SET @Resultado = 1
            SET @Mensaje = 'Permiso eliminado exitosamente'
        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION
        SET @Resultado = 0
        SET @Mensaje = ERROR_MESSAGE()
    END CATCH
END
GO