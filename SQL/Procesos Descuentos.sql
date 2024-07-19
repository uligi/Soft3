use Dunamis_SA

GO
CREATE PROCEDURE sp_ActualizarDescuento
    @DescuentoID INT,
    @Porcentaje DECIMAL(5, 2),
    @TipoDescuentoID INT,
    @Resultado INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        UPDATE Descuento
        SET Porcentaje = @Porcentaje,
            TipoDescuentoID = @TipoDescuentoID
        WHERE DescuentoID = @DescuentoID;

        SET @Resultado = @DescuentoID;
    END TRY
    BEGIN CATCH
        SET @Resultado = 0;
    END CATCH
END

use Dunamis_SA

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

use Dunamis_SA

GO
CREATE PROCEDURE sp_ListarDescuentos
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        d.DescuentoID,
        d.Porcentaje,
        d.TipoDescuentoID,
        td.Descripcion as TipoDescuento
    FROM 
        Descuento d
    JOIN 
        TipoDescuento td ON d.TipoDescuentoID = td.TipoDescuentoID;
END


use Dunamis_SA

GO
CREATE PROCEDURE sp_RegistrarDescuento
    @Porcentaje DECIMAL(5, 2),
    @TipoDescuentoID INT,
    @Resultado INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        INSERT INTO Descuento (Porcentaje, TipoDescuentoID)
        VALUES (@Porcentaje, @TipoDescuentoID);

        SET @Resultado = SCOPE_IDENTITY();
    END TRY
    BEGIN CATCH
        SET @Resultado = 0;
    END CATCH
END
