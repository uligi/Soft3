use Dunamis_SA

GO

-- Procedimiento para listar direcciones
CREATE PROCEDURE sp_ListarDirecciones
AS
BEGIN
    SELECT d.DireccionID, d.NombreDireccion, d.DireccionDetallada, 
           p.Descripcion AS ProvinciaDescripcion, 
           c.Descripcion AS CantonDescripcion, 
           dt.Descripcion AS DistritoDescripcion,
           d.ProvinciaID, d.CantonID, d.DistritoID, d.ClienteID
    FROM Direcciones d
    INNER JOIN Provincia p ON d.ProvinciaID = p.ProvinciaID
    INNER JOIN Canton c ON d.CantonID = c.CantonID
    INNER JOIN Distrito dt ON d.DistritoID = dt.DistritoID
    WHERE d.Activo = 1;
END
GO


-- Procedimiento para registrar una nueva dirección
CREATE PROCEDURE sp_RegistrarDireccion
    @Direccion NVARCHAR(255),
    @DireccionDetallada NVARCHAR(255),
    @ProvinciaID INT,
    @CantonID INT,
    @DistritoID INT,
    @ClienteID INT,
    @Resultado INT OUTPUT,
    @Mensaje NVARCHAR(500) OUTPUT
AS
BEGIN
    BEGIN TRY
        INSERT INTO Direcciones (NombreDireccion, DireccionDetallada, ProvinciaID, CantonID, DistritoID, ClienteID, Activo)
        VALUES (@Direccion, @DireccionDetallada, @ProvinciaID, @CantonID, @DistritoID, @ClienteID, 1);
        SET @Resultado = SCOPE_IDENTITY();
        SET @Mensaje = 'Dirección registrada correctamente';
    END TRY
    BEGIN CATCH
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END
GO

-- Procedimiento para editar una dirección existente
CREATE PROCEDURE sp_EditarDireccion
    @DireccionID INT,
    @Direccion NVARCHAR(255),
    @DireccionDetallada NVARCHAR(255),
    @ProvinciaID INT,
    @CantonID INT,
    @DistritoID INT,
    @ClienteID INT,
    @Resultado INT OUTPUT,
    @Mensaje NVARCHAR(500) OUTPUT
AS
BEGIN
    BEGIN TRY
        UPDATE Direcciones
        SET NombreDireccion = @Direccion,
            DireccionDetallada = @DireccionDetallada,
            ProvinciaID = @ProvinciaID,
            CantonID = @CantonID,
            DistritoID = @DistritoID,
            ClienteID = @ClienteID
        WHERE DireccionID = @DireccionID;
        SET @Resultado = @DireccionID;
        SET @Mensaje = 'Dirección actualizada correctamente';
    END TRY
    BEGIN CATCH
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END
GO

-- Procedimiento para eliminar una dirección
CREATE PROCEDURE sp_EliminarDireccion
    @DireccionID INT,
    @Resultado BIT OUTPUT,
    @Mensaje NVARCHAR(500) OUTPUT
AS
BEGIN
    BEGIN TRY
        UPDATE Direcciones
        SET Activo = 0
        WHERE DireccionID = @DireccionID;
        SET @Resultado = 1;
        SET @Mensaje = 'Dirección eliminada correctamente';
    END TRY
    BEGIN CATCH
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END
GO

-- Procedimiento para listar direcciones por cliente
CREATE PROCEDURE sp_ListarDireccionesPorCliente
    @ClienteID INT
AS
BEGIN
    SELECT d.DireccionID, d.NombreDireccion, d.DireccionDetallada, 
           p.Descripcion AS ProvinciaDescripcion, 
           c.Descripcion AS CantonDescripcion, 
           dt.Descripcion AS DistritoDescripcion,
           d.ProvinciaID, d.CantonID, d.DistritoID, d.ClienteID
    FROM Direcciones d
    INNER JOIN Provincia p ON d.ProvinciaID = p.ProvinciaID
    INNER JOIN Canton c ON d.CantonID = c.CantonID
    INNER JOIN Distrito dt ON d.DistritoID = dt.DistritoID
    WHERE d.ClienteID = @ClienteID AND d.Activo = 1;
END
GO
