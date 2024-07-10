# Soft_3
CREATE DATABASE DUNAMIS_SA;
GO

USE DUNAMIS_SA;
GO

CREATE TABLE TipoImpuesto (
    TipoImpuestoID INT PRIMARY KEY IDENTITY(1,1),
    Descripcion VARCHAR(255) NOT NULL
);




CREATE TABLE TipoDescuento (
    TipoDescuentoID INT PRIMARY KEY IDENTITY(1,1),
    Descripcion VARCHAR(255) NOT NULL
);




CREATE TABLE TipoDeCarga (
    TipoDeCargaID INT PRIMARY KEY IDENTITY(1,1),
    Descripcion VARCHAR(255) NOT NULL,
    TarifaPorKilo DECIMAL(10, 2) NOT NULL
);




CREATE TABLE TipoCliente (
    TipoClienteID INT PRIMARY KEY IDENTITY(1,1),
    Descripcion VARCHAR(45) NOT NULL
);




CREATE TABLE TipoRol (
    TipoRolID INT PRIMARY KEY IDENTITY(1,1),
    Descripcion VARCHAR(45) NOT NULL
);



CREATE TABLE TipoTelefono (
    TipoTelefonoID INT PRIMARY KEY IDENTITY(1,1),
    Descripcion VARCHAR(255) NOT NULL
);




CREATE TABLE TipoCorreo (
    TipoCorreoID INT PRIMARY KEY IDENTITY(1,1),
    Descripcion VARCHAR(255) NOT NULL
);



CREATE TABLE TipoPago (
    TipoPagoID INT PRIMARY KEY IDENTITY(1,1),
    Tipo VARCHAR(255) NOT NULL
);




