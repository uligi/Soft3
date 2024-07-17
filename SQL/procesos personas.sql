use Dunamis_SA

GO

CREATE PROCEDURE sp_ListarPersonas
AS
BEGIN
    SET NOCOUNT ON;
    SELECT p.Cedula, p.Nombre, p.Apellido1, p.Apellido2, p.CorreoID, c.Correo, tc.TipoCorreoID, tc.Descripcion as DescripcionTipoCorreo
    FROM Persona p
    INNER JOIN Correo c ON p.CorreoID = c.CorreoID
    INNER JOIN TipoCorreo tc ON c.TipoCorreoID = tc.TipoCorreoID
END





use Dunamis_SA

GO

Create PROCEDURE sp_RegistrarPersona
    @Cedula INT,
    @Nombre VARCHAR(255),
    @Apellido1 VARCHAR(255),
    @Apellido2 VARCHAR(255),
    @CorreoID INT,
    @Resultado INT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        INSERT INTO Persona (Cedula, Nombre, Apellido1, Apellido2, CorreoID)
        VALUES (@Cedula, @Nombre, @Apellido1, @Apellido2, @CorreoID);

        SET @Resultado = SCOPE_IDENTITY();
        SET @Mensaje = 'Persona registrada correctamente.';
    END TRY
    BEGIN CATCH
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END;



use Dunamis_SA

GO

Create PROCEDURE sp_ActualizarPersona
    @Cedula INT,
    @Nombre VARCHAR(255),
    @Apellido1 VARCHAR(255),
    @Apellido2 VARCHAR(255),
    @CorreoID INT,
    @Resultado BIT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        UPDATE Persona
        SET Nombre = @Nombre,
            Apellido1 = @Apellido1,
            Apellido2 = @Apellido2,
            CorreoID = @CorreoID
        WHERE Cedula = @Cedula;

        SET @Resultado = 1;
        SET @Mensaje = 'Persona actualizada correctamente.';
    END TRY
    BEGIN CATCH
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END;


use Dunamis_SA

GO

create PROCEDURE sp_EliminarPersona
    @Cedula INT,
    @Resultado BIT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        DELETE FROM Persona WHERE Cedula = @Cedula;

        SET @Resultado = 1;
        SET @Mensaje = 'Persona eliminada correctamente.';
    END TRY
    BEGIN CATCH
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END;
