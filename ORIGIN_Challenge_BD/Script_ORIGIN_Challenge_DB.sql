USE [master]
GO
/****** Object:  Database [ORIGIN_Challenge_DB]    Script Date: 18/05/2024 18:24:55 ******/
CREATE DATABASE [ORIGIN_Challenge_DB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ORIGIN_Challenge_DB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\ORIGIN_Challenge_DB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ORIGIN_Challenge_DB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\ORIGIN_Challenge_DB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [ORIGIN_Challenge_DB] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ORIGIN_Challenge_DB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ORIGIN_Challenge_DB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ORIGIN_Challenge_DB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ORIGIN_Challenge_DB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ORIGIN_Challenge_DB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ORIGIN_Challenge_DB] SET ARITHABORT OFF 
GO
ALTER DATABASE [ORIGIN_Challenge_DB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ORIGIN_Challenge_DB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ORIGIN_Challenge_DB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ORIGIN_Challenge_DB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ORIGIN_Challenge_DB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ORIGIN_Challenge_DB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ORIGIN_Challenge_DB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ORIGIN_Challenge_DB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ORIGIN_Challenge_DB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ORIGIN_Challenge_DB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ORIGIN_Challenge_DB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ORIGIN_Challenge_DB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ORIGIN_Challenge_DB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ORIGIN_Challenge_DB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ORIGIN_Challenge_DB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ORIGIN_Challenge_DB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ORIGIN_Challenge_DB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ORIGIN_Challenge_DB] SET RECOVERY FULL 
GO
ALTER DATABASE [ORIGIN_Challenge_DB] SET  MULTI_USER 
GO
ALTER DATABASE [ORIGIN_Challenge_DB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ORIGIN_Challenge_DB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ORIGIN_Challenge_DB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ORIGIN_Challenge_DB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ORIGIN_Challenge_DB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ORIGIN_Challenge_DB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'ORIGIN_Challenge_DB', N'ON'
GO
ALTER DATABASE [ORIGIN_Challenge_DB] SET QUERY_STORE = ON
GO
ALTER DATABASE [ORIGIN_Challenge_DB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [ORIGIN_Challenge_DB]
GO
/****** Object:  Table [dbo].[Operaciones]    Script Date: 18/05/2024 18:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Operaciones](
	[id_operacion] [int] IDENTITY(1,1) NOT NULL,
	[id_tarjeta] [int] NOT NULL,
	[fecha] [datetime] NULL,
	[codigo_operacion] [int] NOT NULL,
	[cantidad_retiro] [decimal](10, 2) NULL,
	[balance] [decimal](10, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_operacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tarjetas]    Script Date: 18/05/2024 18:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tarjetas](
	[id_tarjeta] [int] IDENTITY(1,1) NOT NULL,
	[numero_tarjeta] [varchar](16) NOT NULL,
	[pin] [varchar](4) NOT NULL,
	[bloqueada] [bit] NULL,
	[dinero_en_cuenta] [decimal](10, 2) NOT NULL,
	[fecha_vencimiento] [date] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_tarjeta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[numero_tarjeta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Operaciones] ADD  DEFAULT (getdate()) FOR [fecha]
GO
ALTER TABLE [dbo].[Operaciones] ADD  DEFAULT ((0)) FOR [balance]
GO
ALTER TABLE [dbo].[Tarjetas] ADD  DEFAULT ((0)) FOR [bloqueada]
GO
ALTER TABLE [dbo].[Operaciones]  WITH CHECK ADD FOREIGN KEY([id_tarjeta])
REFERENCES [dbo].[Tarjetas] ([id_tarjeta])
GO
/****** Object:  StoredProcedure [dbo].[sp_InsertarTarjetasAleatorias]    Script Date: 18/05/2024 18:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_InsertarTarjetasAleatorias]
AS
BEGIN
DECLARE @NumeroTarjeta NVARCHAR(16);
        DECLARE @Pin NVARCHAR(4);

        -- Generar un número de tarjeta aleatorio
        SET @NumeroTarjeta = '';
        WHILE LEN(@NumeroTarjeta) < 16
        BEGIN
            SET @NumeroTarjeta = @NumeroTarjeta + CAST(FLOOR(RAND() * 10) AS NVARCHAR(1));
        END;

        -- Generar un PIN aleatorio
        SET @Pin = '';
        WHILE LEN(@Pin) < 4
        BEGIN
            SET @Pin = @Pin + CAST(FLOOR(RAND() * 10) AS NVARCHAR(1));
        END;

        -- Insertar la tarjeta en la tabla Tarjetas
        INSERT INTO Tarjetas (numero_tarjeta, pin, bloqueada, dinero_en_cuenta, fecha_vencimiento)
        VALUES (@NumeroTarjeta, @Pin, 0, RAND() * 1000000, DATEADD(YEAR, 1, GETDATE()));
END;
GO
USE [master]
GO
ALTER DATABASE [ORIGIN_Challenge_DB] SET  READ_WRITE 
GO
