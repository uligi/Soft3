

Use Dunamis_SA
GO

CREATE PROCEDURE [dbo].[sp_ObtenerMontosPorTipoDeCarga]
    @TipoCargaID INT
AS
BEGIN
    SELECT 
        tc.Nombre AS TipoCarga,
        SUM(df.TotalComprobante) AS MontoTotal
    FROM DetallesDeFactura df
    INNER JOIN CotizarCarga cc ON cc.CotizarCargaID = df.CotizarCargaID
    INNER JOIN TiposDeCarga tc ON tc.TiposDeCargaID = cc.TiposDeCargaID
    WHERE tc.TiposDeCargaID = @TipoCargaID
    GROUP BY tc.Nombre
END;
GO


Use Dunamis_SA
GO

CREATE PROCEDURE [dbo].[sp_ObtenerMontosPorDescuento]
    @DescuentoID INT
AS
BEGIN
    SELECT 
        td.Descripcion,
        SUM(df.TotalConDescuento) AS MontoTotal
    FROM 
        DetallesDeFactura df
    INNER JOIN 
        CotizarCarga cc ON cc.CotizarCargaID = df.CotizarCargaID
    INNER JOIN 
        Descuento d ON d.DescuentoID = cc.DescuentoID
    INNER JOIN 
        TipoDescuento td ON td.TipoDescuentoID = d.TipoDescuentoID
    WHERE 
        td.TipoDescuentoID = @DescuentoID
    GROUP BY 
        td.Descripcion
END;
GO

Use Dunamis_SA
GO
CREATE PROCEDURE [dbo].[sp_ObtenerMontosPorPeriodo]
    @FechaInicio DATETIME,
    @FechaFin DATETIME
AS
BEGIN
    SELECT 
        CONVERT(VARCHAR, df.FechaEmision, 23) as FechaEmision,
        SUM(df.TotalComprobante) as MontoTotal
    FROM 
        DetallesDeFactura df
    WHERE 
        df.FechaEmision >= @FechaInicio AND df.FechaEmision < DATEADD(DAY, 1, @FechaFin)
    GROUP BY 
        CONVERT(VARCHAR, df.FechaEmision, 23)
    ORDER BY 
        CONVERT(VARCHAR, df.FechaEmision, 23);
END;
GO


