USE master
GO
-- Creacion de una base de datos --
IF exists(SELECT * FROM SysDataBases WHERE name='Obligatorio2015')
BEGIN
	DROP DATABASE Obligatorio2015
END
GO

CREATE DATABASE Obligatorio2015
ON(
	NAME=Obliatorio2015,
	FILENAME='C:\Obligatorio2015.mdf'
)
GO

-- Creacion de tablas--
USE Obligatorio2015
GO

CREATE TABLE Categorias(
Id VARCHAR(3) NOT NULL PRIMARY KEY CHECK ((Len(Id)=3) AND  Id like '[A-Z][A-Z][A-Z]' ),
Nombre VARCHAR(20) NOT NULL UNIQUE CHECK (Len(Nombre)<=20),
Descripcion VARCHAR (50) NOT NULL,
Eliminado BIT NOT NULL default 0
)
go

CREATE TABLE Ciudades(
CodigoDepto CHAR NOT NULL CHECK(Len(CodigoDepto) = 1 AND CodigoDepto LIKE '[A-S]'),
Nombre VARCHAR(20) NOT NULL CHECK (Len(Nombre)<=20),
Eliminado BIT NOT NULL default 0,
PRIMARY KEY (CodigoDepto, Nombre)
)
go

CREATE TABLE Empresas (
Rut VARCHAR(12) NOT NULL PRIMARY KEY CHECK(Len(Rut)=12),
Nombre VARCHAR (25) NOT NULL,
Direccion VARCHAR (50) NOT NULL,
Categoria VARCHAR(3) NOT NULL FOREIGN KEY REFERENCES Categorias(Id),
Departamento CHAR NOT NULL,
NombreCiudad VARCHAR (20) NOT NULL,
FOREIGN KEY(Departamento, NombreCiudad) REFERENCES Ciudades (CodigoDepto, Nombre)
)
go


CREATE TABLE Telefonos(
RutEmpresa VARCHAR(12) FOREIGN KEY REFERENCES Empresas(Rut),
Telefono VARCHAR(10) NOT NULL,
PRIMARY KEY (RutEmpresa, Telefono)
)

go

CREATE TABLE Usuarios (
CI INT NOT NULL PRIMARY KEY CHECK (Len(CI)=8),
Nombre VARCHAR (30) NOT NULL, 
Usuario VARCHAR (30) UNIQUE NOT NULL,
Contrasenia VARCHAR (5) NOT NULL CHECK (Len(Contrasenia)=5)
)
go

CREATE TABLE Administradores (
CI INT NOT NULL FOREIGN KEY REFERENCES Usuarios(CI),
VeListados BIT NOT NULL,
PRIMARY KEY(CI)
)
go

CREATE TABLE Clientes(
CI INT NOT NULL FOREIGN KEY REFERENCES Usuarios(CI),
Edad INT NOT NULL,
PRIMARY KEY(CI)
)
go

CREATE TABLE Visitas (
CICliente INT FOREIGN KEY REFERENCES Clientes(CI),
RutEmpresa VARCHAR(12) FOREIGN KEY REFERENCES Empresas (Rut),
FechaHora DATETIME NOT NULL DEFAULT GETDATE(),
VisitaAceptada BIT NOT NULL DEFAULT 0,
PRIMARY KEY (CICliente, RutEmpresa, FechaHora)
)
go
----------------------------------------SP CATEGORIA---------------------------------------------------

CREATE PROCEDURE AltaCategoria @id VARCHAR(3), @nombre VARCHAR(20), @descripcion VARCHAR(50) AS
BEGIN
	IF (EXISTS(SELECT *	FROM BuscarEnTodasLasCategorias	WHERE @id = Id))
		RETURN -1; --YA EXISTE LA CATEGORIA

	IF (EXISTS(SELECT *	FROM BuscarEnTodasLasCategorias	WHERE Nombre = @nombre ))
		RETURN -2; --YA EXISTE ESE NOMBRE DE CATEGORIA EN LA BASE DE DATOS
	
	INSERT Categorias(Id, Nombre, Descripcion) VALUES(@id, @nombre, @descripcion)	
END
GO 

CREATE PROCEDURE BajaCategoria @id VARCHAR(3) AS
BEGIN
	IF (NOT EXISTS(SELECT * FROM Categorias WHERE Id = @id AND Eliminado = 0))
		RETURN -1 --NO EXISTE LA CATEGORIA

	IF (EXISTS(SELECT Empresas.* FROM Empresas WHERE Categoria = @id))
		UPDATE Categorias SET Eliminado = 1 WHERE Id = @id  --SI TIENE ASOCIADO UNA EMPRESA LA CATEGORIA SE DA BAJA LOGICA
	ELSE
		DELETE Categorias WHERE Id = @id --SI NO TIENE NINGUNA EMPRESA ASOCIADA SE DA BAJA FISICA
END
GO

CREATE PROCEDURE ModificarCategoria @id VARCHAR(3), @nombre VARCHAR(20), @descripcion VARCHAR(50) AS
BEGIN
	IF (NOT EXISTS(SELECT *	FROM Categorias	WHERE Id = @id AND Eliminado = 0))
		RETURN -1; --NO EXISTE LA CATEGORIA

	IF (EXISTS(SELECT * FROM Categorias WHERE Id <> @id and Nombre = @nombre))
		return -2; --YA EXISTE ESE NOMBRE EN LA BASE DE DATOS PARA OTRA CATEGORIA

	IF (EXISTS(SELECT * FROM Categorias WHERE Id = @id and Nombre = @nombre))
		UPDATE Categorias SET Descripcion = @descripcion WHERE Id = @id --SOLO MODIFICA DESCRIPCION EN CASO QUE EL NOMBRE INGRESADO Y EL ID SEAN EL MISMO
		
	UPDATE Categorias SET Nombre = @nombre, Descripcion = @descripcion WHERE Id = @id
END
GO

CREATE PROCEDURE ListarCategorias AS
BEGIN
	SELECT * FROM Categorias WHERE Eliminado = 0
END
GO

CREATE PROCEDURE BuscarCategoria  @id VARCHAR(3) AS
BEGIN
	SELECT * FROM Categorias WHERE Id = @id AND Eliminado = 0
END
GO

CREATE VIEW BuscarEnTodasLasCategorias as
	SELECT * FROM Categorias 
GO

CREATE PROCEDURE BuscarCategoriaSinFiltro  @id VARCHAR(3) AS
BEGIN
	SELECT * FROM BuscarEnTodasLasCategorias WHERE Id = @id
END
GO

CREATE PROCEDURE ListarCategoriasXCiudad @ciudad VARCHAR (20) AS
BEGIN
	SELECT Categoria 
	FROM Empresas 
	WHERE @ciudad = NombreCiudad
	GROUP BY Categoria
END
GO

-----------------------------------------SP CIUDADES--------------------------------------------------------


CREATE PROCEDURE AltaCiudad @codigoDepto CHAR, @nombre VARCHAR(20) AS
BEGIN
	IF (EXISTS(SELECT *	FROM BuscarCiuSinFiltro WHERE @codigoDepto = CodigoDepto AND Nombre = @nombre))
		RETURN -1; --YA EXISTE LA CATEGORIA
		
	INSERT INTO Ciudades(CodigoDepto, Nombre) VALUES(@codigoDepto, @nombre)
END 
GO


CREATE PROCEDURE BajaCiudad @codigoDepto CHAR, @nombre VARCHAR(20) AS
BEGIN
	IF (EXISTS(
			SELECT Empresas.* 
			FROM Empresas INNER JOIN  Ciudades
				ON Empresas.Departamento  = Ciudades.CodigoDepto AND Empresas.Nombre = Ciudades.Nombre				
			WHERE Departamento = @codigoDepto AND NombreCiudad = @nombre AND Ciudades.Eliminado = 0))
		UPDATE Ciudades SET Eliminado = 1 WHERE CodigoDepto = @codigoDepto AND Nombre = @nombre  --SI TIENE ASOCIADO UNA EMPRESA LA CIUDAD SE DA BAJA LOGICA
	ELSE
		DELETE Ciudades WHERE CodigoDepto = @codigoDepto AND Nombre = @nombre --SI NO TIENE NINGUNA EMPRESA ASOCIADA SE DA BAJA FISICA
END
GO


CREATE PROCEDURE ListarCiudades @CodigoDepto CHAR  AS
BEGIN
	SELECT CodigoDepto, Nombre FROM Ciudades WHERE CodigoDepto = @CodigoDepto AND Eliminado = 0
END
GO

CREATE PROCEDURE BuscarCiudad @codigoDepto CHAR, @nombre VARCHAR(20) AS
BEGIN
	SELECT * FROM Ciudades WHERE CodigoDepto = @codigoDepto AND Nombre = @nombre AND Eliminado = 0
END
GO

CREATE VIEW BuscarCiuSinFiltro as
	SELECT * FROM Ciudades 
GO

CREATE PROCEDURE BuscarCiudadSinFiltro @codigoDepto CHAR, @nombre VARCHAR(20) AS
BEGIN
	SELECT * FROM BuscarCiuSinFiltro WHERE CodigoDepto = @codigoDepto AND Nombre = @nombre
END
GO



------------------------------------INICIO SP DE EMPRESA-------------------------------------------------------

CREATE PROCEDURE AgregarEmpresa
@rut VARCHAR(12),
@nombre VARCHAR (25),
@direccion VARCHAR (50),
@categoria VARCHAR(3),
@departamento CHAR,
@nombreCiudad VARCHAR(20)
AS
BEGIN
	IF EXISTS(
		SELECT * 
		FROM Empresas
		WHERE Rut = @rut
		)
	BEGIN
		return 1; --Ya existe la empresa con se rut
	END
	
	IF NOT EXISTS(
		SELECT* 
		FROM Categorias
		WHERE Id = @categoria 
	)
	BEGIN
		RETURN 2; --No existe la categoria
	END

	IF NOT EXISTS(
		SELECT* 
		FROM Ciudades
		WHERE CodigoDepto = @departamento AND Nombre = @nombreCiudad 
	)
	BEGIN
		RETURN 3; --No existe la ciudad
	END

	INSERT INTO Empresas
	VALUES(@rut, @nombre, @direccion, @categoria, @departamento, @nombreCiudad);

	IF(@@ERROR <> 0)
	BEGIN
		Return 4;
	END
END

go

CREATE PROCEDURE BuscarEmpresa
@rut varchar(12) 
AS
BEGIN
	SELECT *
	FROM Empresas
	WHERE Rut= @rut;
END

go

CREATE PROCEDURE EliminarEmpresa
@rut VARCHAR(12)
AS
BEGIN
	BEGIN TRANSACTION;
		DELETE Telefonos
		WHERE RutEmpresa = @rut;

		if(@@error <>0)
		begin
			ROLLBACK TRANSACTION;
			return 1;
		end

		DELETE Visitas
		WHERE RutEmpresa=@rut;

		if(@@error <>0)
		begin
			ROLLBACK TRANSACTION;
			return 2;
		end

		DELETE Empresas 
		WHERE Rut = @rut;

		if(@@error <>0)
		begin
			ROLLBACK TRANSACTION;
			return 3;
		end

	COMMIT TRANSACTION;
END

go

CREATE PROCEDURE ModificarEmpresa
@rut VARCHAR(12),
@nombre VARCHAR (25),
@direccion VARCHAR (50),
@categoria VARCHAR(3),
@departamento CHAR,
@nombreCiudad VARCHAR(20)
AS
BEGIN
	IF NOT EXISTS(
		SELECT* 
		FROM Empresas
		WHERE Rut = @rut 
	)
	BEGIN
		RETURN 1; --No existe la empresa que se quiere editar
	END

	IF NOT EXISTS(
		SELECT* 
		FROM Categorias
		WHERE Id = @categoria 
	)
	BEGIN
		RETURN 2; --No existe la categoria
	END

	IF NOT EXISTS(
		SELECT* 
		FROM Ciudades
		WHERE CodigoDepto = @departamento AND Nombre = @nombreCiudad 
	)
	BEGIN
		RETURN 3; --No existe la ciudad
	END

	UPDATE Empresas
	SET Nombre = @nombre, Direccion = @direccion, Categoria = @categoria, Departamento = @departamento, NombreCiudad = @nombreCiudad
	WHERE Rut = @rut;

	IF(@@ERROR <> 0)
	BEGIN
		RETURN 4;
	END
END

GO

CREATE PROCEDURE AgregarTelefono
@rut VARCHAR(12),
@telefono VARCHAR(10)
AS
BEGIN
	IF EXISTS(
		SELECT *
		FROM Telefonos
		WHERE Telefono = @telefono AND RutEmpresa = @rut
	)
	BEGIN
		RETURN 1; -- El teléfono ya está registrado para esa empresa
	END

	IF NOT EXISTS (
		SELECT *
		FROM Empresas
		WHERE Rut = @rut
	)
	BEGIN
		RETURN 2; -- La empresa no existe
	END

	INSERT INTO Telefonos
	VALUES(@rut, @telefono);

	IF(@@ERROR<>0)
	BEGIN
		RETURN 3;
	END
END

go

CREATE PROCEDURE EliminarTelefonosDeEmpresa
@rut varchar(12)
AS
BEGIN
	Delete Telefonos
	WHERE RutEmpresa = @rut;	
END

go

CREATE PROCEDURE TelefonosDeEmpresa
@rut varchar(12)
AS 
BEGIN
	SELECT *
	FROM Telefonos
	WHERE RutEmpresa = @rut; 
END
go

CREATE PROCEDURE NuevaVisita
@ciCliente INT,
@rutEmpresa VARCHAR(12),
@VisitaAceptada BIT
AS
BEGIN
	IF NOT EXISTS(
		SELECT *
		FROM Clientes
		WHERE CI = @ciCliente
	)
	BEGIN
		RETURN 1;  -- No existe el cliente
	END

	IF NOT EXISTS (
		SELECT *
		FROM Empresas
		WHERE Rut = @rutEmpresa
	)
	BEGIN
		RETURN 2; -- No existe la empresa
	END

	INSERT INTO Visitas (CICliente, RutEmpresa, VisitaAceptada)
	VALUES (@ciCliente, @rutEmpresa, @VisitaAceptada);

	IF(@@ERROR <> 0)
	BEGIN
		RETURN 3;
	END
END

go

CREATE PROCEDURE VisitasDeEmpresa
@rut varchar(12)
AS
BEGIN
	SELECT * 
	FROM Visitas
	WHERE RutEmpresa = @rut;
END
go

CREATE PROCEDURE ListarEmpresas
AS
BEGIN
	SELECT *
	FROM Empresas;
END

go

CREATE PROCEDURE ListarEmpresasXCiudadYCategoria 
@ciudad VARCHAR(20), 
@categoria VARCHAR(3)
AS
BEGIN
	SELECT *
	FROM Empresas
	WHERE @ciudad = NombreCiudad AND @categoria = Categoria;
END
-----------------------------------------SP USUARIOS--------------------------------------------------------
GO

CREATE PROCEDURE LogueoAdmin
@usuario VARCHAR (30),
@contrasenia VARCHAR (5)
AS
BEGIN
	SELECT *
	FROM Usuarios INNER JOIN Administradores
		ON Usuarios.CI = Administradores.CI
	WHERE Usuario = @usuario AND Contrasenia = @contrasenia
END
GO

CREATE PROCEDURE LogueoCliente
@usuario VARCHAR (30),
@contrasenia VARCHAR (5)
AS
BEGIN
	SELECT *
	FROM Usuarios INNER JOIN Clientes
		ON Usuarios.CI = Clientes.CI
	WHERE Usuario = @usuario AND Contrasenia = @contrasenia
END
GO

CREATE PROCEDURE AltaAdministrador
@ci INT,
@nombre VARCHAR(30),
@usuario VARCHAR (30),
@contrasenia VARCHAR (5),
@veListado BIT
AS
BEGIN
	IF EXISTS (
		SELECT *
		FROM Usuarios
		WHERE CI = @ci
		)
		BEGIN
			RETURN -1; --CI YA EXISTENTE
		END

	IF EXISTS (
		SELECT *
		FROM Usuarios
		WHERE Usuario = @usuario
		)
		BEGIN
			RETURN -2; --USUARIO YA EXISTENTE
		END

		BEGIN TRANSACTION;

		INSERT INTO Usuarios
		VALUES(@ci, @nombre, @usuario, @contrasenia);

		IF @@ERROR <> 0 
		BEGIN
			ROLLBACK TRANSACTION;

			RETURN -3; --ERROR AL INTENTAR AGREGAR USUARIO
		END

		INSERT INTO Administradores
		VALUES(@ci, @veListado);

		IF @@ERROR <> 0 
		BEGIN
			ROLLBACK TRANSACTION;

			RETURN -4; --ERROR AL INTENTAR AGREGAR ADMINISTRADOR
		END
		
		COMMIT TRANSACTION;
END
GO 

CREATE PROCEDURE AltaCliente
@ci INT,
@nombre VARCHAR(30),
@usuario VARCHAR (30),
@contrasenia VARCHAR (5),
@edad INT

AS
BEGIN
	IF EXISTS (
		SELECT *
		FROM Usuarios
		WHERE CI = @ci
		)
		BEGIN
			RETURN -1; --CI YA EXISTENTE
		END

	IF EXISTS (
		SELECT *
		FROM Usuarios
		WHERE Usuario = @usuario
		)
		BEGIN
			RETURN -2; --USUARIO YA EXISTENTE
		END

	BEGIN TRANSACTION;
		INSERT INTO Usuarios
		VALUES(@ci, @nombre, @usuario, @contrasenia);

		IF @@ERROR <> 0 
		BEGIN
			ROLLBACK TRANSACTION;

			RETURN -3; --ERROR AL INTENTAR AGREGAR USUARIO
		END

		INSERT INTO Clientes
		VALUES(@ci, @edad);

		IF @@ERROR <> 0 
		BEGIN
			ROLLBACK TRANSACTION;

			RETURN -4; --ERROR AL INTENTAR AGREGAR CLIENTE
		END
		
	COMMIT TRANSACTION;
END
GO

CREATE PROCEDURE BajaAdministrador
@ci INT
AS
BEGIN
	IF NOT EXISTS (
		SELECT *
		FROM Administradores
		WHERE CI = @ci
		)
		BEGIN
			RETURN -1; --CI NO EXISTENTE
		END

	BEGIN TRANSACTION;

		DELETE Administradores
		WHERE CI = @ci;

		IF @@ERROR <> 0 
		BEGIN
			ROLLBACK TRANSACTION;

			RETURN -2; --ERROR AL INTENTAR ELIMINAR ADMINISTRADOR
		END

		DELETE Usuarios
		WHERE CI = @ci;

		IF @@ERROR <> 0 
		BEGIN
			ROLLBACK TRANSACTION;

			RETURN -3; --ERROR AL INTENTAR ELIMINAR USUARIO
		END

	COMMIT TRANSACTION;
END
GO

CREATE PROCEDURE ModificarAdministrador
@ci INT,
@nombre VARCHAR(30),
@usuario VARCHAR (30),
@contrasenia VARCHAR (5),
@veListado BIT
AS
BEGIN
	IF NOT EXISTS (
		SELECT *
		FROM Administradores
		WHERE CI = @ci
		)
		BEGIN
			RETURN -1; --CI  NO EXISTENTE
		END

	IF EXISTS (
	SELECT *
	FROM Usuarios
	WHERE Usuario = @usuario AND CI <> @ci
	)

	BEGIN
		RETURN -2; --USUARIO YA EXISTENTE
	END

	BEGIN TRANSACTION;

		UPDATE Administradores
		SET VeListados = @veListado
		WHERE CI = @ci;

		IF @@ERROR <> 0 
		BEGIN
			ROLLBACK TRANSACTION;
			RETURN -3; --ERROR AL INTENTAR MODIFICAR ADMINISTRADOR
		END

		UPDATE Usuarios
		SET Nombre = @nombre, Contrasenia = @contrasenia, Usuario = @usuario
		WHERE CI = @ci;

		IF @@ERROR <> 0 
		BEGIN
			ROLLBACK TRANSACTION;

			RETURN -4; --ERROR AL INTENTAR MODIFICAR USUARIO
		END
		
	COMMIT TRANSACTION;
END
GO 

CREATE PROCEDURE BuscarAdministrador
@ci INT
AS
BEGIN
	SELECT Usuarios.*, Administradores.VeListados
	FROM Usuarios INNER JOIN Administradores
		ON Usuarios.CI = Administradores.CI
	WHERE Usuarios.CI = @ci
END
GO

CREATE PROCEDURE BuscarCliente
@ci INT
AS
BEGIN
	SELECT Usuarios.*, Clientes.Edad
	FROM Usuarios INNER JOIN Clientes
		ON Usuarios.CI = Clientes.CI
	WHERE Usuarios.CI = @ci
END
GO

--creacion de usuario IIS para que el webservice pueda acceder a la bd------------------------
USE master
GO

CREATE LOGIN [IIS APPPOOL\DefaultAppPool] FROM WINDOWS WITH DEFAULT_DATABASE = master
GO

USE Obligatorio2015
GO

CREATE USER [IIS APPPOOL\DefaultAppPool] FOR LOGIN [IIS APPPOOL\DefaultAppPool]
GO

GRANT EXECUTE TO [IIS APPPOOL\DefaultAppPool]
GO

-----------------------------------------------------------------------------------
-----------------------------DATOS DE PRUEBA---------------------------------------
-----------------------------------------------------------------------------------
GO

--#########--CATEGORIA--##########--

EXECUTE AltaCategoria 'AUD', 'AUDIO', 'Articulos de audio'
EXECUTE AltaCategoria 'FER', 'FERRETERIA', 'Articulos de construccion y ferreteria.'
EXECUTE AltaCategoria 'HOG', 'HOGAR', 'Articulos de la casa.';
EXECUTE AltaCategoria 'JAR', 'JARDIN', 'Articulos para el cuidado del jardin.';
EXECUTE AltaCategoria 'ELE', 'ELECTRICIDAD', 'Articulos para la instalacion y mantenimiento electricos.';

---------SP AltaCategoria-----------------

--DECLARE @resultado INT;

----EXECUTE @resultado = AltaCategoria 'TEC', 'Tecnologia', 'Articulos de tecnologia'; --AGREGADO CORRECTO
----EXECUTE @resultado = AltaCategoria 'FER', 'Ferreteria', 'Articulos de construccion y ferreteria.'; --ERROR -1: YA EXISTE LA CATOEGORIA
----EXECUTE @resultado = AltaCategoria 'aaa', 'Tecnologia', 'Articulos de tecnologia'--'YA EXISTE ESE NOMBRE DE CATEGORIA EN LA BASE DE DATOS

--PRINT @resultado;
--GO

---------SP BajaCategoria-----------------

--DECLARE @resultado INT;

----EXECUTE @resultado = BajaCategoria 'TTT'; --ERROR -1 NO EXISTE LA CATEGORIA
----EXECUTE @resultado = BajaCategoria 'AUD'; --BORRADO FISICO CORRECTO
----EXECUTE @resultado = BajaCategoria 'FER'; --BORRADO LOGICO POR ESTAR VINCULADA A UNA EMPRESA

--PRINT @resultado;
--GO

---------SP ModificarCategoria-----------------

--DECLARE @resultado INT;

----EXECUTE @resultado = ModificarCategoria 'TTT', 'NO EXISTE', 'Categoria inexistente.'; --ERROR -1 NO EXISTE LA CATEGORIA
----EXECUTE @resultado = ModificarCategoria 'JAR', 'HOGAR', 'Articulos de la casa.'; --ERROR -2 YA EXISTE ESE NOMBRE EN LA BASE DE DATOS PARA OTRA CATEGORIA
----EXECUTE @resultado = ModificarCategoria 'JAR', 'JARDIN', 'Nueva descripcion modificada.' --SOLO MODIFICA DESCRIPCION EN CASO QUE EL NOMBRE INGRESADO Y EL ID SEAN EL MISMO
----EXECUTE @resultado = ModificarCategoria 'ELE', 'ELEMENTALES', 'Articulos basicos de la tabla periodica.' --MODIFICACION CORRECTA DE NOMBRE Y DESCRIPCION

--PRINT @resultado;
--GO

---------SP BuscarCategoria-----------------

--EXECUTE BuscarCategoria 'HOG'; --BUSQUEDA CORRECTA
--EXECUTE BuscarCategoria 'FER'; --NO DEVUELVE NADA YA QUE ESTA BORRADA LOGICAMENTE (BORRAR EN LAS PRUEBAS DE BajaCategoria)

---------SP ListarCategorias-----------------

--EXECUTE ListarCategorias;
--GO

GO
--#########--CIUDAD--##########--

EXECUTE AltaCiudad 'A', 'SANTA LUCIA'
EXECUTE AltaCiudad 'A', 'LAS PIEDRAS'
EXECUTE AltaCiudad 'A', 'PANDO'
EXECUTE AltaCiudad 'B', 'PUNTA DEL ESTE'
EXECUTE AltaCiudad 'B', 'SAN CARLOS'
EXECUTE AltaCiudad 'C', 'LASCANO'
EXECUTE AltaCiudad 'C', 'LA PALOMA'
EXECUTE AltaCiudad 'C', 'CHUY'
EXECUTE AltaCiudad 'D', 'SANTA CLARA'
EXECUTE AltaCiudad 'E', 'ACEGUA'
EXECUTE AltaCiudad 'E', 'FRAILE MUERTO'
EXECUTE AltaCiudad 'F', 'RIVERA'
EXECUTE AltaCiudad 'F', 'TRANQUERAS'
EXECUTE AltaCiudad 'G', 'BELLA UNION'
EXECUTE AltaCiudad 'G', 'TOMAS GOMENSORO'
EXECUTE AltaCiudad 'H', 'SALTO'
EXECUTE AltaCiudad 'H', 'CONSTITUCION'
EXECUTE AltaCiudad 'H', 'BELEN'
EXECUTE AltaCiudad 'I', 'PAYSANDU'
EXECUTE AltaCiudad 'I', 'GUICHON'
EXECUTE AltaCiudad 'L', 'EL CANO'
EXECUTE AltaCiudad 'L', 'NUEVA PALMIRA'


---------SP AltaCiudad-----------------

--DECLARE @resultado INT;

----EXECUTE @resultado = AltaCiudad 'A', 'SAN JOSE'; --AGREGADO CORRECTO
----EXECUTE @resultado = AltaCiudad 'A', 'SANTA LUCIA'; --ERROR -1: YA EXISTE LA CIUDAD

--PRINT @resultado;
--GO

---------SP BajaCiudad-----------------

--EXECUTE BajaCiudad 'L', 'NUEVA PALMIRA'; --BAJA FISICA CORRECTA
--EXECUTE BajaCiudad 'I', 'PAYSANDU'; --BAJA LOGICA POR TENER ASOCIADA UNA EMPRESA
--GO

---------SP BuscarCiudad-----------------

--EXECUTE BuscarCiudad 'L', 'EL CANO'; --BUSQUEDA CORRECTA
--EXECUTE BuscarCiudad 'I', 'PAYSANDU'; --NO DEVUELVE NADA YA QUE ESTA BORRADA LOGICAMENTE (BORRAR EN PRUEBAS DE BajaCiudad)

---------SP ListarCiudades-----------------

--EXECUTE ListarCiudades;
--GO

GO

--#########--USUARIOS--##########--

EXECUTE AltaAdministrador 12312312, 'Nicolas Pedro', 'hola', '12345', 1;
EXECUTE AltaAdministrador 42799299, 'Marcelo Mesa', 'Teto', 'T_205', 1;
EXECUTE AltaAdministrador 98765432, 'Jorge Martinez', 'Jorgito', '+jor-', 0;
EXECUTE AltaAdministrador 87654321, 'Dario Stramil', 'Pimpo', 'pimpo', 1;
EXECUTE AltaAdministrador 12345672, 'Javier Pedro', 'zani15', '/*-+.', 0;

EXECUTE AltaCliente 36985214, 'Daniel Rodriguez', 'Dani2', '34562', 17;
EXECUTE AltaCliente 14725836, 'Alejandro Lopez', 'Lopez', 'sty45', 29;
EXECUTE AltaCliente 32198745, 'Pablo Garcia', 'Pablo', 'pablo', 34;
EXECUTE AltaCliente 11111111, 'Hugo Boss', 'HugoB', '98765', 28;
EXECUTE AltaCliente 75342869, 'David Sanchez', 'David', '235bs', 25;

---------SP AltaAdministrador-----------------
--DECLARE @resultado INT;

----EXECUTE @resultado = AltaAdministrador 12312312, 'Sebastian Ualde', 'Smaug', '2*3=6', 1; ----AGREGADO CORRECTO 
--EXECUTE @resultado = AltaAdministrador 12312312, 'Juan Valdez', 'Juanito', 'juan1', 0; ----ERROR -1: CI YA EXISTENTE
----EXECUTE @resultado = AltaAdministrador 32132121, 'Jack Rian', 'Smaug', '893fa', 0; ----ERROR -2: USUARIO YA EXISTENTE

--PRINT @resultado;
--GO

---------SP AltaCliente-----------------
--DECLARE @resultado INT;

--EXECUTE @resultado =  AltaCliente 37900152, 'Jason Ruiz', 'JasonRu', '1Ruiz', 33; --AGREGADO CORRECTO
--EXECUTE @resultado =  AltaCliente 37900152, 'Pablo Sanchez', 'Pablito', '75324', 8; --ERROR -1: CI YA EXISTENTE
--EXECUTE @resultado =  AltaCliente 13024907, 'Pablo Sanchez', 'Pablo', '75324', 12; --ERROR -2: USUARIO YA EXISTENTE


--PRINT @resultado;
--GO

---------SP ModificarAdministrador-----------------
--DECLARE @resultado INT;

----EXECUTE @resultado = ModificarAdministrador 98765432, 'Martin Jorge', 'Jorgito', '+jor-', 1; --MODIFICADO CORRECTAMENTE
----EXECUTE @resultado = ModificarAdministrador 90086142, 'Sebastian Ualde', 'Pepe', '23451', 1; --ERROR -1: CI  NO EXISTENTE
----EXECUTE @resultado = ModificarAdministrador 98765432, 'Martinez Jorge', 'Pimpo', '+jor-', 1; --ERROR -2: USUARIO YA EXISTENTE

--PRINT @resultado;
--GO

---------SP BajaAdministrador-----------------
--DECLARE @resultado INT;

----EXECUTE @resultado = BajaAdministrador 98765432 --ELIMINADOR CORRECTAMENTE
--EXECUTE @resultado = BajaAdministrador 123 --ERROR -1: CI  NO EXISTENTE


--PRINT @resultado;
--GO

--#########--EMPRESAS--##########--
go
EXECUTE AgregarEmpresa '123456890123', 'General Motors', 'Leandro Gomez 1412', 'FER', 'I', 'PAYSANDU';
EXECUTE AgregarEmpresa '234979324783', 'Barraca America', '18 Julio 2312', 'FER', 'I', 'PAYSANDU';
EXECUTE AgregarEmpresa '292374298732', 'Motociclo', 'Rivera 2552', 'HOG', 'I', 'PAYSANDU';

EXECUTE NuevaVisita 36985214, '123456890123', 1;
EXECUTE NuevaVisita 36985214, '292374298732', 1;
EXECUTE NuevaVisita 36985214, '292374298732', 0;
EXECUTE NuevaVisita 36985214, '123456890123', 1;
EXECUTE NuevaVisita 14725836, '123456890123', 0;
EXECUTE NuevaVisita 32198745, '292374298732', 1;
EXECUTE NuevaVisita 14725836, '292374298732', 0;
EXECUTE NuevaVisita 11111111, '292374298732', 1;

insert into Visitas(CICliente, RutEmpresa, FechaHora) values(36985214, '234979324783', '14/8/2014')
insert into Visitas(CICliente, RutEmpresa, FechaHora) values(36985214, '292374298732', '16/9/2014')
insert into Visitas(CICliente, RutEmpresa, FechaHora) values(36985214, '123456890123', '25/6/2015')
insert into Visitas(CICliente, RutEmpresa, FechaHora) values(14725836, '123456890123', '10/8/2015')
insert into Visitas(CICliente, RutEmpresa, FechaHora) values(32198745, '292374298732', '01/2/2015')
insert into Visitas(CICliente, RutEmpresa, FechaHora) values(14725836, '292374298732', '22/3/2015')
insert into Visitas(CICliente, RutEmpresa, FechaHora) values(11111111, '292374298732', '11/04/2014')
insert into Visitas(CICliente, RutEmpresa, FechaHora) values(11111111, '123456890123', '13/06/2014')



--EXECUTE AgregarTelefono '292374298732', '66658954';
EXECUTE AgregarTelefono '292374298732', '45545557';
EXECUTE AgregarTelefono '123456890123', '66995225';
EXECUTE AgregarTelefono '234979324783', '22222855';
EXECUTE AgregarTelefono '234979324783', '85868685';
EXECUTE AgregarTelefono '234979324783', '85868685';
EXECUTE AgregarTelefono '234979324783', '47844873';
EXECUTE AgregarTelefono '234979324783', '68446558';
EXECUTE AgregarTelefono '234979324783', '35636565';
EXECUTE AgregarTelefono '234979324783', '85868985';


go