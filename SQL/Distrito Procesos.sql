USE [Dunamis_SA]
GO
/****** Object:  StoredProcedure [dbo].[sp_ActualizarTipoDescuento]    Script Date: 13/07/2024 15:52:19 ******/



Create PROCEDURE sp_ListarDistrito
AS
BEGIN
    SELECT d.DistritoID, d.Descripcion, c.CantonID, c.Descripcion AS CantonDescripcion, p.ProvinciaID, p.Descripcion AS ProvinciaDescripcion
    FROM Distrito d
    INNER JOIN Canton c ON d.CantonID = c.CantonID
    INNER JOIN Provincia p ON c.ProvinciaID = p.ProvinciaID;
END;

USE [Dunamis_SA]
GO
/****** Object:  StoredProcedure [dbo].[sp_ActualizarTipoDescuento]    Script Date: 13/07/2024 15:52:19 ******/


Create PROCEDURE sp_RegistrarDistrito
    @Descripcion VARCHAR(255),
    @CantonID INT,
    @Resultado INT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION
        INSERT INTO Distrito (Descripcion, CantonID) VALUES (@Descripcion, @CantonID);
        SET @Resultado = SCOPE_IDENTITY();
        SET @Mensaje = 'Distrito registrado correctamente.';
        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END;

USE [Dunamis_SA]
GO
/****** Object:  StoredProcedure [dbo].[sp_ActualizarTipoDescuento]    Script Date: 13/07/2024 15:52:19 ******/


Create PROCEDURE sp_EditarDistrito
    @DistritoID INT,
    @Descripcion VARCHAR(255),
    @CantonID INT,
    @Resultado INT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION
        UPDATE Distrito SET Descripcion = @Descripcion, CantonID = @CantonID WHERE DistritoID = @DistritoID;
        SET @Resultado = @DistritoID;
        SET @Mensaje = 'Distrito actualizado correctamente.';
        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END;

USE [Dunamis_SA]
GO
/****** Object:  StoredProcedure [dbo].[sp_ActualizarTipoDescuento]    Script Date: 13/07/2024 15:52:19 ******/



Create PROCEDURE sp_EliminarDistrito
    @DistritoID INT,
    @Resultado BIT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION
        DELETE FROM Distrito WHERE DistritoID = @DistritoID;
        SET @Resultado = 1;
        SET @Mensaje = 'Distrito eliminado correctamente.';
        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END;

