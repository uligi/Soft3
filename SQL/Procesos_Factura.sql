use Dunamis_SA
Go

CREATE PROCEDURE sp_ObtenerDatosFactura
    @CotizarCargaID INT,
    @UsuarioID INT
AS
BEGIN
    SELECT 
		pp.Cedula as CedulaCliente,
		pp.Nombre as NombreCliente,
		pp.Apellido1 as Apellido1Cliente,
		pp.Apellido2 as apellido2Cliente,
		coc.Correo as CorreoCliente,
        cc.CotizarCargaID,
		cc.Total as SubTotalGravado,
		cc.TotalDescuento,
		cc.TotalPagar as TotalComprobante,
		cc.TotalImpuesto,
        tc.Nombre AS TipoCarga,
        tc.TarifaPorKilo as PrecioPorPeso,
        cc.Peso As Cantidad,
		u.UsuarioID,
        p.Nombre + ' ' + p.Apellido1 + ' ' + p.Apellido2 AS Representante
    FROM CotizarCarga cc
    INNER JOIN TiposDeCarga tc ON cc.TiposDeCargaID = tc.TiposDeCargaID
	Inner join Clientes c on c.ClienteID = cc.ClienteID
    INNER JOIN Usuarios u ON u.UsuarioID = @UsuarioID
    INNER JOIN Persona p ON p.Cedula = u.Cedula  
	inner join Persona pp on pp.Cedula = c.Cedula
	inner join Correo coc on coc.CorreoID = pp.CorreoID
    WHERE cc.CotizarCargaID = @CotizarCargaID
END
go


CREATE PROCEDURE [dbo].[sp_RegistrarFactura]
    @CotizarCargaID INT,
    @UsuarioID INT,
    @PrecioPorPeso DECIMAL(10,2),
    @Cantidad INT,
    @SubTotalGravado DECIMAL(10,2),
    @TotalSinDescuento DECIMAL(10,2),
    @TotalConDescuento DECIMAL(10,2),
    @TotalImpuesto DECIMAL(10,2),
    @TotalComprobante DECIMAL(10,2)
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Insertar la nueva factura en la tabla DetallesDeFactura
        INSERT INTO DetallesDeFactura (
            CotizarCargaID,
            UsuarioID,
            PrecioPorPeso,
            Cantidad,
            SubTotalGravado,
            TotalSinDescuento,
            TotalConDescuento,
            TotalImpuesto,
            TotalComprobante,
            Activo,
            FechaEmision
        )
        VALUES (
            @CotizarCargaID,
            @UsuarioID,
            @PrecioPorPeso,
            @Cantidad,
            @SubTotalGravado,
            @TotalSinDescuento,
            @TotalConDescuento,
            @TotalImpuesto,
            @TotalComprobante,
            1, -- Activo siempre será 1
            GETDATE()
        );

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO


CREATE PROCEDURE [dbo].[sp_ListarFacturas]
AS
BEGIN
    SELECT 
        df.DetalleFacturaID,
        df.CotizarCargaID,
        df.SubTotalGravado,
        df.FechaEmision,
        df.TotalSinDescuento,
        df.TotalConDescuento,
        df.TotalImpuesto,
        df.TotalComprobante,
        df.PrecioPorPeso,
        df.Cantidad,
        df.UsuarioID,
        df.Activo,
        cc.TiposDeCargaID,
        tc.Nombre as NombreCarga,
        c.Cedula,
        p.Nombre as NombreCliente,
        p.Apellido1 as Apellido1Cliente,
        p.Apellido2 as Apellido2Cliente,
        cc.DescuentoID,
        d.Porcentaje as PorcentajeDescuento,
        td.Descripcion as TipoDescuento,
        pp.Nombre + ' ' + pp.Apellido1 + ' ' + pp.Apellido2 AS Representante
    FROM DetallesDeFactura df
    INNER JOIN CotizarCarga cc ON cc.CotizarCargaID = df.CotizarCargaID
    INNER JOIN Usuarios u ON u.UsuarioID = df.UsuarioID
    INNER JOIN Persona pp ON pp.Cedula = u.Cedula
    INNER JOIN Clientes c ON c.ClienteID = cc.ClienteID
    INNER JOIN TiposDeCarga tc ON tc.TiposDeCargaID = cc.TiposDeCargaID
    INNER JOIN Persona p ON p.Cedula = c.Cedula
    INNER JOIN Descuento d ON d.DescuentoID = cc.DescuentoID
    INNER JOIN TipoDescuento td ON td.TipoDescuentoID = d.TipoDescuentoID
END;
GO

use Dunamis_SA
Go

CREATE PROCEDURE [dbo].[sp_ObtenerFacturaPorID]
    @DetalleFacturaID INT
AS
BEGIN
    SELECT 
        df.DetalleFacturaID,
        df.CotizarCargaID,
        df.SubTotalGravado,
        df.FechaEmision,
        df.TotalSinDescuento,
        df.TotalConDescuento,
        df.TotalImpuesto,
        df.TotalComprobante,
        df.PrecioPorPeso,
        df.Cantidad,
        df.UsuarioID,
        df.Activo,
        cc.TiposDeCargaID,
		cor.Correo,
        tc.Nombre AS NombreCarga,
        c.Cedula,
        p.Nombre AS NombreCliente,
        p.Apellido1 AS Apellido1Cliente,
        p.Apellido2 AS Apellido2Cliente,
        cc.DescuentoID,
        d.Porcentaje AS PorcentajeDescuento,
        td.Descripcion AS TipoDescuento,
        pp.Nombre + ' ' + pp.Apellido1 + ' ' + pp.Apellido2 AS Representante
    FROM DetallesDeFactura df
    INNER JOIN CotizarCarga cc ON cc.CotizarCargaID = df.CotizarCargaID
    INNER JOIN Usuarios u ON u.UsuarioID = df.UsuarioID
    INNER JOIN Persona pp ON pp.Cedula = u.Cedula
    INNER JOIN Clientes c ON c.ClienteID = cc.ClienteID
    INNER JOIN TiposDeCarga tc ON tc.TiposDeCargaID = cc.TiposDeCargaID
    INNER JOIN Persona p ON p.Cedula = c.Cedula
	INNER JOIN Correo cor ON cor.CorreoID = p.CorreoID
    INNER JOIN Descuento d ON d.DescuentoID = cc.DescuentoID
    INNER JOIN TipoDescuento td ON td.TipoDescuentoID = d.TipoDescuentoID
    WHERE df.DetalleFacturaID = @DetalleFacturaID
END;
GO
