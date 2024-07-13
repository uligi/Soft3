-- Crear la base de datos
CREATE DATABASE Dunamis_SA;
GO

USE Dunamis_SA;
GO

-- Crear la tabla Provincia
CREATE TABLE Provincia (
    ProvinciaID INT IDENTITY(1,1) PRIMARY KEY,
    Descripcion VARCHAR(255) NOT NULL
);
GO

-- Crear la tabla Canton
CREATE TABLE Canton (
    CantonID INT IDENTITY(1,1) PRIMARY KEY,
    Descripcion VARCHAR(255) NOT NULL,
    ProvinciaID INT NOT NULL,
    FOREIGN KEY (ProvinciaID) REFERENCES Provincia(ProvinciaID)
);
GO

-- Crear la tabla Distrito
CREATE TABLE Distrito (
    DistritoID INT IDENTITY(1,1) PRIMARY KEY,
    Descripcion VARCHAR(255) NOT NULL,
    CantonID INT NOT NULL,
    FOREIGN KEY (CantonID) REFERENCES Canton(CantonID)
);
GO



-- Crear la tabla TipoTelefono
CREATE TABLE TipoTelefono (
    TipoTelefonoID INT IDENTITY(1,1) PRIMARY KEY,
    Descripcion VARCHAR(45) NOT NULL
);
GO

-- Crear la tabla TipoCorreo
CREATE TABLE TipoCorreo (
    TipoCorreoID INT IDENTITY(1,1) PRIMARY KEY,
    Descripcion VARCHAR(45) NOT NULL
);
GO

-- Crear la tabla Persona
CREATE TABLE Persona (
    Cedula INT PRIMARY KEY,
    Nombre VARCHAR(255) NOT NULL,
    Apellido1 VARCHAR(255) NOT NULL,
	Apellido2 VARCHAR(255) NOT NULL,
    CorreoID INT NOT NULL,
    FOREIGN KEY (CorreoID) REFERENCES TipoCorreo(TipoCorreoID)
);
GO

-- Crear la tabla Telefono
CREATE TABLE Telefono (
    TelefonoID INT IDENTITY(1,1) PRIMARY KEY,
    NumeroTelefono VARCHAR(45) NOT NULL,
    Cedula INT NOT NULL,
    TipoTelefonoID INT NOT NULL,
    FOREIGN KEY (Cedula) REFERENCES Persona(Cedula),
    FOREIGN KEY (TipoTelefonoID) REFERENCES TipoTelefono(TipoTelefonoID)
);
GO

-- Crear la tabla Correo
CREATE TABLE Correo (
    CorreoID INT IDENTITY(1,1) PRIMARY KEY,
    Correo VARCHAR(255) NOT NULL,
    Cedula INT NOT NULL,
    TipoCorreoID INT NOT NULL,
    FOREIGN KEY (Cedula) REFERENCES Persona(Cedula),
    FOREIGN KEY (TipoCorreoID) REFERENCES TipoCorreo(TipoCorreoID)
);
GO

-- Crear la tabla TipoCliente
CREATE TABLE TipoCliente (
    TipoClienteID INT IDENTITY(1,1) PRIMARY KEY,
    Descripcion VARCHAR(255) NOT NULL
);
GO

-- Crear la tabla Clientes
CREATE TABLE Clientes (
    ClienteID INT IDENTITY(1,1) PRIMARY KEY,
    Activo BIT NOT NULL,
    Fecha DATE DEFAULT GETDATE(),
    Cedula INT NOT NULL,
    TipoClienteID INT NOT NULL,
    DireccionID INT NOT NULL,
    FOREIGN KEY (Cedula) REFERENCES Persona(Cedula),
    FOREIGN KEY (TipoClienteID) REFERENCES TipoCliente(TipoClienteID)
    
);
GO

-- Crear la tabla TipoPago
CREATE TABLE TipoPago (
    TipoPagoID INT IDENTITY(1,1) PRIMARY KEY,
    Descripcion VARCHAR(255) NOT NULL
);
GO

-- Crear la tabla Pago
CREATE TABLE Pago (
    PagoID INT IDENTITY(1,1) PRIMARY KEY,
    Descripcion VARCHAR(255) NOT NULL,
    TipoPagoID INT NOT NULL,
    FOREIGN KEY (TipoPagoID) REFERENCES TipoPago(TipoPagoID)
);
GO

-- Crear la tabla Direcciones
CREATE TABLE Direcciones (
    DireccionID INT IDENTITY(1,1) PRIMARY KEY,
    Direccion VARCHAR(255) NOT NULL,
    DireccionDetallada VARCHAR(255),
    ProvinciaID INT NOT NULL,
    CantonID INT NOT NULL,
    DistritoID INT NOT NULL,
	ClienteID INT NOT NULL,
    FOREIGN KEY (ClienteID) REFERENCES Clientes(ClienteID),
    FOREIGN KEY (ProvinciaID) REFERENCES Provincia(ProvinciaID),
    FOREIGN KEY (CantonID) REFERENCES Canton(CantonID),
    FOREIGN KEY (DistritoID) REFERENCES Distrito(DistritoID)
);
GO

-- Crear la tabla TipoImpuesto
CREATE TABLE TipoImpuesto (
    TipoImpuestoID INT IDENTITY(1,1) PRIMARY KEY,
    Descripcion VARCHAR(255) NOT NULL
);
GO

-- Crear la tabla Impuesto
CREATE TABLE Impuesto (
    ImpuestoID INT IDENTITY(1,1) PRIMARY KEY,
    Porcentaje DECIMAL(5,2) NOT NULL,
    TipoImpuestoID INT NOT NULL,
    FOREIGN KEY (TipoImpuestoID) REFERENCES TipoImpuesto(TipoImpuestoID)
);
GO

-- Crear la tabla TipoDescuento
CREATE TABLE TipoDescuento (
    TipoDescuentoID INT IDENTITY(1,1) PRIMARY KEY,
    Descripcion VARCHAR(255) NOT NULL
);
GO

-- Crear la tabla Descuento
CREATE TABLE Descuento (
    DescuentoID INT IDENTITY(1,1) PRIMARY KEY,
    Porcentaje DECIMAL(5,2) NOT NULL,
    TipoDescuentoID INT NOT NULL,
    FOREIGN KEY (TipoDescuentoID) REFERENCES TipoDescuento(TipoDescuentoID)
);
GO

-- Crear la tabla TiposDeCarga
CREATE TABLE TiposDeCarga (
    TiposDeCargaID INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(255) NOT NULL,
    Descripcion VARCHAR(255),
    TarifaPorKilo DECIMAL(10,2) NOT NULL,
    FechaRegistro DATETIME DEFAULT GETDATE()
);
GO


-- Crear la tabla CotizarCarga
CREATE TABLE CotizarCarga (
    CotizarCargaID INT IDENTITY(1,1) PRIMARY KEY,
    Peso DECIMAL(10,2) NOT NULL,
    Fecha DATE DEFAULT GETDATE(),
    TiposDeCargaID INT NOT NULL,
    ClienteID INT NOT NULL,
    DireccionID INT NOT NULL,
    ImpuestoID INT NOT NULL,
    DescuentoID INT NOT NULL,
    FOREIGN KEY (TiposDeCargaID) REFERENCES TiposDeCarga(TiposDeCargaID),
    FOREIGN KEY (ClienteID) REFERENCES Clientes(ClienteID),
    FOREIGN KEY (DireccionID) REFERENCES Direcciones(DireccionID),
    FOREIGN KEY (ImpuestoID) REFERENCES Impuesto(ImpuestoID),
    FOREIGN KEY (DescuentoID) REFERENCES Descuento(DescuentoID)
);
GO

-- Crear la tabla DetallesDeFactura
CREATE TABLE DetallesDeFactura (
    DetalleFacturaID INT IDENTITY(1,1) PRIMARY KEY,
    SubTotalGravado DECIMAL(10,2) NOT NULL,
    FechaEmision DATETIME DEFAULT GETDATE(),
    ImpuestoID INT NOT NULL,
    DescuentoID INT NOT NULL,
    TotalSinDescuento DECIMAL(10,2) NOT NULL,
    TotalConDescuento DECIMAL(10,2) NOT NULL,
    TotalImpuesto DECIMAL(10,2) NOT NULL,
    TotalComprobante DECIMAL(10,2) NOT NULL,
    PrecioPorKilo DECIMAL(10,2) NOT NULL,
    Cantidad INT NOT NULL,
    FOREIGN KEY (ImpuestoID) REFERENCES Impuesto(ImpuestoID),
    FOREIGN KEY (DescuentoID) REFERENCES Descuento(DescuentoID)
);
GO

-- Crear la tabla Facturas
CREATE TABLE Facturas (
    FacturaID INT IDENTITY(1,1) PRIMARY KEY,
    Fecha DATE DEFAULT GETDATE(),
    SubTotalGravado DECIMAL(10,2) NOT NULL,
    TotalSinDescuento DECIMAL(10,2) NOT NULL,
    TotalConDescuento DECIMAL(10,2) NOT NULL,
    TotalImpuesto DECIMAL(10,2) NOT NULL,
    TotalComprobante DECIMAL(10,2) NOT NULL,
	TransaccionID VARCHAR(255) NOT NULL,
    ClienteID INT NOT NULL,
    CotizarCargaID INT NOT NULL,
    FOREIGN KEY (ClienteID) REFERENCES Clientes(ClienteID),
    FOREIGN KEY (CotizarCargaID) REFERENCES CotizarCarga(CotizarCargaID)
);
GO

-- Crear la tabla Permisos
CREATE TABLE Permisos (
    PermisoID INT IDENTITY(1,1) PRIMARY KEY,
    Descripcion VARCHAR(255) NOT NULL
);
GO

-- Crear la tabla Roles
CREATE TABLE Roles (
    RolID INT IDENTITY(1,1) PRIMARY KEY,
    Rol VARCHAR(255) NOT NULL,
    TipoRolID INT NOT NULL,
    FOREIGN KEY (TipoRolID) REFERENCES Permisos(PermisoID)
);
GO

-- Crear la tabla Usuarios
CREATE TABLE Usuarios (
    UsuarioID INT IDENTITY(1,1) PRIMARY KEY,
	Contrasena VARCHAR(255) NOT NULL,
    RestablecerContrase�a BIT NOT NULL,
	Activo BIT NOT NULL,
    FechaCreacion DATE DEFAULT GETDATE(),
    Cedula INT NOT NULL,
    RolID INT NOT NULL,
    FOREIGN KEY (Cedula) REFERENCES Persona(Cedula),
    FOREIGN KEY (RolID) REFERENCES Roles(RolID)
);
GO
