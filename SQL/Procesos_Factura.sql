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
