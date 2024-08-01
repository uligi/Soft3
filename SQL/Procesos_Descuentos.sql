CREATE PROCEDURE sp_ActualizarDescuento
    @DescuentoID INT,
    @Porcentaje DECIMAL(5, 2),
    @MontoMinimo DECIMAL(10, 2),
    @MontoMaximo DECIMAL(10, 2),
    @TipoDescuentoID INT,
    @Resultado INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        UPDATE Descuento
        SET Porcentaje = @Porcentaje,
            MontoMinimo = @MontoMinimo,
            MontoMaximo = @MontoMaximo,
            TipoDescuentoID = @TipoDescuentoID
        WHERE DescuentoID = @DescuentoID;

        SET @Resultado = @DescuentoID;
    END TRY
    BEGIN CATCH
        SET @Resultado = 0;
    END CATCH
END
GO

CREATE PROCEDURE sp_EliminarDescuento
    @DescuentoID INT,
    @Resultado BIT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        DELETE FROM Descuento
        WHERE DescuentoID = @DescuentoID;

        SET @Resultado = 1;
    END TRY
    BEGIN CATCH
        SET @Resultado = 0;
    END CATCH
END
GO

CREATE PROCEDURE sp_ListarDescuentos
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        d.DescuentoID,
        d.Porcentaje,
        d.MontoMinimo,
        d.MontoMaximo,
        d.TipoDescuentoID,
        td.Descripcion as TipoDescuento
    FROM 
        Descuento d
    JOIN 
        TipoDescuento td ON d.TipoDescuentoID = td.TipoDescuentoID;
END
GO

CREATE PROCEDURE sp_RegistrarDescuento
    @Porcentaje DECIMAL(5, 2),
    @MontoMinimo DECIMAL(10, 2),
    @MontoMaximo DECIMAL(10, 2),
    @TipoDescuentoID INT,
    @Resultado INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        INSERT INTO Descuento (Porcentaje, MontoMinimo, MontoMaximo, TipoDescuentoID)
        VALUES (@Porcentaje, @MontoMinimo, @MontoMaximo, @TipoDescuentoID);

        SET @Resultado = SCOPE_IDENTITY();
    END TRY
    BEGIN CATCH
        SET @Resultado = 0;
    END CATCH
END
GO
