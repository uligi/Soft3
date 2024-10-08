USE [Dunamis_SA]
GO


Create PROCEDURE [dbo].[sp_RegistrarCotizacionCarga]
    @Cedula INT,
    @Peso DECIMAL(10,2),
    @TiposDeCargaID INT,
    @DireccionID INT,
    @Total DECIMAL(10,2),
    @TotalDescuento DECIMAL(10,2),
    @TotalImpuesto DECIMAL(10,2),
    @TotalPagar DECIMAL(10,2),
    @DescuentoID INT
AS
BEGIN
    -- Declarar variables para almacenar el ID del cliente
    DECLARE @ClienteID INT;

    BEGIN TRY
        -- Iniciar transacción
        BEGIN TRANSACTION;

        -- Obtener el ClienteID a partir de la cédula
        SELECT @ClienteID = ClienteID
        FROM Clientes
        WHERE Cedula = @Cedula;

        -- Insertar la nueva cotización de carga en la tabla CotizarCarga
        INSERT INTO CotizarCarga (Peso, Fecha, TotalImpuesto, TotalDescuento, Total, TotalPagar, TiposDeCargaID, ClienteID, DireccionID, Activo, CargaFacturada, DescuentoID)
        VALUES (@Peso, GETDATE(), @TotalImpuesto, @TotalDescuento, @Total, @TotalPagar, @TiposDeCargaID, @ClienteID, @DireccionID, 1, 0, @DescuentoID);

        -- Confirmar la transacción
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- En caso de error, deshacer la transacción
        ROLLBACK TRANSACTION;
        -- Lanza de nuevo el error para manejarlo externamente
        THROW;
    END CATCH
END;
GO


USE [Dunamis_SA]
GO


CREATE PROCEDURE [dbo].[sp_ListarCotizacionesCarga]
AS
BEGIN
    SELECT 
        c.CotizarCargaID,
        p.Cedula,
        p.Nombre,
        p.Apellido1,
        p.Apellido2,
        co.Correo,
        tc.Descripcion AS TipoCarga,
        c.Fecha,
        d.NombreDireccion
    FROM CotizarCarga c
    INNER JOIN Clientes cl ON c.ClienteID = cl.ClienteID
    INNER JOIN Persona p ON cl.Cedula = p.Cedula
    INNER JOIN Correo co ON p.CorreoID = co.CorreoID
    INNER JOIN TiposDeCarga tc ON c.TiposDeCargaID = tc.TiposDeCargaID
    INNER JOIN Direcciones d ON c.DireccionID = d.DireccionID
    WHERE c.Activo = 1
END
GO


CREATE PROCEDURE [dbo].[sp_ActualizarCotizacionCarga]
    @CotizaCargaID INT,
    @Cedula INT,
    @Peso DECIMAL(10,2),
    @TiposDeCargaID INT,
    @DireccionID INT,
    @Total DECIMAL(10,2),
    @TotalDescuento DECIMAL(10,2),
    @TotalImpuesto DECIMAL(10,2),
    @TotalPagar DECIMAL(10,2),
    @DescuentoID INT
AS
BEGIN
    DECLARE @ClienteID INT;
    SELECT @ClienteID = ClienteID FROM Clientes WHERE Cedula = @Cedula;

    UPDATE CotizarCarga
    SET 
        Peso = @Peso,
        TiposDeCargaID = @TiposDeCargaID,
        DireccionID = @DireccionID,
        Total = @Total,
        TotalDescuento = @TotalDescuento,
        TotalImpuesto = @TotalImpuesto,
        TotalPagar = @TotalPagar,
        DescuentoID = @DescuentoID,
        ClienteID = @ClienteID
    WHERE CotizarCargaID = @CotizaCargaID;
END
GO



CREATE PROCEDURE sp_EliminarCotizacionCarga
    @CotizaCargaID INT
AS
BEGIN
    UPDATE CotizarCarga
    SET Activo = 0
    WHERE CotizarCargaID = @CotizaCargaID;
END
GO


USE [Dunamis_SA]
GO
CREATE PROCEDURE sp_ObtenerCotizacionPorID
    @CotizaCargaID INT
AS
BEGIN
    SELECT 
        c.CotizarCargaID,
        p.Cedula,
        p.Nombre,
        p.Apellido1,
        p.Apellido2,
        cor.Correo,
        d.DireccionID,
        t.TelefonoID,
        t.NumeroTelefono,
        c.Peso,
        tc.TiposDeCargaID,
        tc.TarifaPorKilo,
        c.Total,
        c.TotalDescuento,
        c.TotalImpuesto,
        c.TotalPagar
    FROM CotizarCarga c
    INNER JOIN Clientes cl ON c.ClienteID = cl.ClienteID
    INNER JOIN Persona p ON cl.Cedula = p.Cedula
    INNER JOIN Correo cor ON p.CorreoID = cor.CorreoID
    INNER JOIN Direcciones d ON c.ClienteID = cl.ClienteID
    LEFT JOIN Telefono t ON p.Cedula = t.Cedula
    INNER JOIN TiposDeCarga tc ON c.TiposDeCargaID = tc.TiposDeCargaID
    WHERE c.CotizarCargaID = @CotizaCargaID;
END
GO

