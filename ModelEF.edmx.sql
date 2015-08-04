
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 08/02/2015 12:57:18
-- Generated from EDMX file: C:\Users\Administrador\Documents\Visual Studio 2015\Projects\TestPerson\TestPerson\ModelEF.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Test];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Personas_Personas]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Personas] DROP CONSTRAINT [FK_Personas_Personas];
GO
IF OBJECT_ID(N'[dbo].[FK_Telefonos_Telefonos]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Telefonos] DROP CONSTRAINT [FK_Telefonos_Telefonos];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Personas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Personas];
GO
IF OBJECT_ID(N'[dbo].[Telefonos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Telefonos];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Personas'
CREATE TABLE [dbo].[Personas] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nombre] varchar(150)  NOT NULL,
    [FechaNacimiento] datetime  NOT NULL,
    [FechaRegistra] datetime  NULL
);
GO

-- Creating table 'Telefonos'
CREATE TABLE [dbo].[Telefonos] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [IdPersona] int  NOT NULL,
    [Telefono] decimal(10,0)  NOT NULL,
    [FechaRegistra] datetime  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Personas'
ALTER TABLE [dbo].[Personas]
ADD CONSTRAINT [PK_Personas]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Telefonos'
ALTER TABLE [dbo].[Telefonos]
ADD CONSTRAINT [PK_Telefonos]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Id] in table 'Personas'
ALTER TABLE [dbo].[Personas]
ADD CONSTRAINT [FK_Personas_Personas]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Personas]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [IdPersona] in table 'Telefonos'
ALTER TABLE [dbo].[Telefonos]
ADD CONSTRAINT [FK_Telefonos_Telefonos]
    FOREIGN KEY ([IdPersona])
    REFERENCES [dbo].[Personas]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Telefonos_Telefonos'
CREATE INDEX [IX_FK_Telefonos_Telefonos]
ON [dbo].[Telefonos]
    ([IdPersona]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------