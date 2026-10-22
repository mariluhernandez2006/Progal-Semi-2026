USE master;
GO

IF DB_ID('USTR025826') IS NULL
BEGIN
    CREATE DATABASE USTR025826;
END
GO

USE USTR025826;
GO

-- CREAR TABLA DE USUARIOS
IF OBJECT_ID('dbo.usuarios_USTR025826', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.usuarios_USTR025826
    (
        idUsuario INT IDENTITY(1,1) PRIMARY KEY,
        usuario VARCHAR(35) NOT NULL UNIQUE,
        clave VARCHAR(35) NOT NULL,
        nombre VARCHAR(65) NOT NULL,
        direccion VARCHAR(85) NULL,
        telefono VARCHAR(9) NULL
    );
END
GO

-- CREAR TABLA DE ALUMNOS
IF OBJECT_ID('dbo.alumnos', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.alumnos
    (
        idAlumno INT IDENTITY(1,1) PRIMARY KEY,
        codigo VARCHAR(25) NOT NULL,
        nombre VARCHAR(100) NOT NULL,
        direccion VARCHAR(120) NULL,
        telefono VARCHAR(20) NULL,
        email VARCHAR(120) NULL
    );
END
GO

-- CREAR TABLA DE MATERIAS
IF OBJECT_ID('dbo.materias', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.materias
    (
        idMateria INT IDENTITY(1,1) PRIMARY KEY,
        codigo VARCHAR(25) NOT NULL,
        nombre VARCHAR(100) NOT NULL,
        uv INT NOT NULL
    );
END
GO

-- INSERTAR USUARIO ADMINISTRADOR
IF NOT EXISTS
(
    SELECT 1
    FROM dbo.usuarios_USTR025826
    WHERE usuario = 'USTR025826'
)
BEGIN
    INSERT INTO dbo.usuarios_USTR025826
    (usuario, clave, nombre, direccion, telefono)
    VALUES
    ('USTR025826', 'pastel#123', 'Administrador', 'Usulutan', '70000000');
END
GO

-- INSERTAR SEGUNDO USUARIO
IF NOT EXISTS
(
    SELECT 1
    FROM dbo.usuarios_USTR025826
    WHERE usuario = 'usuario02'
)
BEGIN
    INSERT INTO dbo.usuarios_USTR025826
    (usuario, clave, nombre, direccion, telefono)
    VALUES
    ('usuario02', 'Clave123', 'Usuario Dos', 'Usulutan', '70000001');
END
GO

-- INSERTAR TERCER USUARIO
IF NOT EXISTS
(
    SELECT 1
    FROM dbo.usuarios_USTR025826
    WHERE usuario = 'usuario03'
)
BEGIN
    INSERT INTO dbo.usuarios_USTR025826
    (usuario, clave, nombre, direccion, telefono)
    VALUES
    ('usuario03', 'Clave456', 'Usuario Tres', 'Usulutan', '70000002');
END
GO

-- VERIFICAR BASE DE DATOS
SELECT DB_NAME() AS BaseDeDatosActual;
GO

-- VERIFICAR USUARIOS
SELECT * FROM dbo.usuarios_USTR025826;
GO

-- VERIFICAR TABLAS
SELECT TABLE_NAME
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_TYPE = 'BASE TABLE';
GO
