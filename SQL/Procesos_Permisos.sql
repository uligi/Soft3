USE [Dunamis_SA]
GO

CREATE PROCEDURE sp_ListarPermisos
AS
BEGIN
    SELECT PermisoID, Descripcion 
    FROM Permisos
    WHERE Activo = 1;
END
GO


CREATE PROCEDURE sp_RegistrarPermiso
    @Descripcion VARCHAR(255),
    @Resultado BIT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
AS
BEGIN
    SET @Resultado = 0;
    SET @Mensaje = '';
    BEGIN TRY
        BEGIN TRANSACTION;
            INSERT INTO Permisos (Descripcion, Activo) VALUES (@Descripcion, 1);
            SET @Resultado = 1;
            SET @Mensaje = 'Permiso registrado exitosamente';
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END
GO


CREATE PROCEDURE sp_EditarPermiso
    @PermisoID INT,
    @Descripcion VARCHAR(255),
    @Resultado BIT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
AS
BEGIN
    SET @Resultado = 0;
    SET @Mensaje = '';
    BEGIN TRY
        BEGIN TRANSACTION;
            UPDATE Permisos SET Descripcion = @Descripcion WHERE PermisoID = @PermisoID;
            SET @Resultado = 1;
            SET @Mensaje = 'Permiso actualizado exitosamente';
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END
GO



CREATE PROCEDURE sp_EliminarPermiso
    @PermisoID INT,
    @Resultado BIT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
AS
BEGIN
    SET @Resultado = 0;
    SET @Mensaje = '';
    BEGIN TRY
        BEGIN TRANSACTION;
            UPDATE Permisos
            SET Activo = 0
            WHERE PermisoID = @PermisoID;
            SET @Resultado = 1;
            SET @Mensaje = 'Permiso eliminado exitosamente';
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END
GO
