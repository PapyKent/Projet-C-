
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/22/2014 19:50:26
-- Generated from EDMX file: C:\Users\Yannick\CoursProjet\IUT 2A\C#.net\Projet C#\EntityFramework\Entity.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [TODOLISTUCBL];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_EtatTache]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TacheSet] DROP CONSTRAINT [FK_EtatTache];
GO
IF OBJECT_ID(N'[dbo].[FK_UtilisateurTache]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TacheSet] DROP CONSTRAINT [FK_UtilisateurTache];
GO
IF OBJECT_ID(N'[dbo].[FK_UtilisateurCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CategorySet] DROP CONSTRAINT [FK_UtilisateurCategory];
GO
IF OBJECT_ID(N'[dbo].[FK_CategoryTache_Category]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CategoryTache] DROP CONSTRAINT [FK_CategoryTache_Category];
GO
IF OBJECT_ID(N'[dbo].[FK_CategoryTache_Tache]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CategoryTache] DROP CONSTRAINT [FK_CategoryTache_Tache];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[UtilisateurSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UtilisateurSet];
GO
IF OBJECT_ID(N'[dbo].[CategorySet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CategorySet];
GO
IF OBJECT_ID(N'[dbo].[TacheSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TacheSet];
GO
IF OBJECT_ID(N'[dbo].[EtatSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EtatSet];
GO
IF OBJECT_ID(N'[dbo].[CategoryTache]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CategoryTache];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'UtilisateurSet'
CREATE TABLE [dbo].[UtilisateurSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nom] nvarchar(max)  NOT NULL,
    [Prenom] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Login] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'CategorySet'
CREATE TABLE [dbo].[CategorySet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nom] nvarchar(max)  NOT NULL,
    [ParDefaut] bit  NOT NULL,
    [Utilisateur_Id] int  NOT NULL
);
GO

-- Creating table 'TacheSet'
CREATE TABLE [dbo].[TacheSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nom] nvarchar(max)  NOT NULL,
    [Debut] datetime  NOT NULL,
    [Fin] datetime  NOT NULL,
    [Detail] nvarchar(max)  NOT NULL,
    [Etat_Id] int  NOT NULL,
    [Utilisateur_Id] int  NOT NULL
);
GO

-- Creating table 'EtatSet'
CREATE TABLE [dbo].[EtatSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Code] nvarchar(max)  NOT NULL,
    [Libelle] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'CategoryTache'
CREATE TABLE [dbo].[CategoryTache] (
    [Categories_Id] int  NOT NULL,
    [Taches_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'UtilisateurSet'
ALTER TABLE [dbo].[UtilisateurSet]
ADD CONSTRAINT [PK_UtilisateurSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CategorySet'
ALTER TABLE [dbo].[CategorySet]
ADD CONSTRAINT [PK_CategorySet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TacheSet'
ALTER TABLE [dbo].[TacheSet]
ADD CONSTRAINT [PK_TacheSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EtatSet'
ALTER TABLE [dbo].[EtatSet]
ADD CONSTRAINT [PK_EtatSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Categories_Id], [Taches_Id] in table 'CategoryTache'
ALTER TABLE [dbo].[CategoryTache]
ADD CONSTRAINT [PK_CategoryTache]
    PRIMARY KEY CLUSTERED ([Categories_Id], [Taches_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Etat_Id] in table 'TacheSet'
ALTER TABLE [dbo].[TacheSet]
ADD CONSTRAINT [FK_EtatTache]
    FOREIGN KEY ([Etat_Id])
    REFERENCES [dbo].[EtatSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EtatTache'
CREATE INDEX [IX_FK_EtatTache]
ON [dbo].[TacheSet]
    ([Etat_Id]);
GO

-- Creating foreign key on [Utilisateur_Id] in table 'TacheSet'
ALTER TABLE [dbo].[TacheSet]
ADD CONSTRAINT [FK_UtilisateurTache]
    FOREIGN KEY ([Utilisateur_Id])
    REFERENCES [dbo].[UtilisateurSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UtilisateurTache'
CREATE INDEX [IX_FK_UtilisateurTache]
ON [dbo].[TacheSet]
    ([Utilisateur_Id]);
GO

-- Creating foreign key on [Utilisateur_Id] in table 'CategorySet'
ALTER TABLE [dbo].[CategorySet]
ADD CONSTRAINT [FK_UtilisateurCategory]
    FOREIGN KEY ([Utilisateur_Id])
    REFERENCES [dbo].[UtilisateurSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UtilisateurCategory'
CREATE INDEX [IX_FK_UtilisateurCategory]
ON [dbo].[CategorySet]
    ([Utilisateur_Id]);
GO

-- Creating foreign key on [Categories_Id] in table 'CategoryTache'
ALTER TABLE [dbo].[CategoryTache]
ADD CONSTRAINT [FK_CategoryTache_Category]
    FOREIGN KEY ([Categories_Id])
    REFERENCES [dbo].[CategorySet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Taches_Id] in table 'CategoryTache'
ALTER TABLE [dbo].[CategoryTache]
ADD CONSTRAINT [FK_CategoryTache_Tache]
    FOREIGN KEY ([Taches_Id])
    REFERENCES [dbo].[TacheSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CategoryTache_Tache'
CREATE INDEX [IX_FK_CategoryTache_Tache]
ON [dbo].[CategoryTache]
    ([Taches_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------