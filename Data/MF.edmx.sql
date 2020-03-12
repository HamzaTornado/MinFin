
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 03/09/2020 15:07:38
-- Generated from EDMX file: C:\Users\Dell\source\repos\MinFin\Data\MF.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [MinistreFin];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_avoir]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[compte_rendu] DROP CONSTRAINT [FK_avoir];
GO
IF OBJECT_ID(N'[dbo].[FK_Class]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[groue_detail] DROP CONSTRAINT [FK_Class];
GO
IF OBJECT_ID(N'[dbo].[FK_Class2]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[groue_detail] DROP CONSTRAINT [FK_Class2];
GO
IF OBJECT_ID(N'[dbo].[FK_contient]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Projet] DROP CONSTRAINT [FK_contient];
GO
IF OBJECT_ID(N'[dbo].[FK_contient2]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Agenda_Activites] DROP CONSTRAINT [FK_contient2];
GO
IF OBJECT_ID(N'[dbo].[FK_contient3]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Agenda_Activites] DROP CONSTRAINT [FK_contient3];
GO
IF OBJECT_ID(N'[dbo].[FK_creer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Agenda] DROP CONSTRAINT [FK_creer];
GO
IF OBJECT_ID(N'[dbo].[FK_creer2]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Programme] DROP CONSTRAINT [FK_creer2];
GO
IF OBJECT_ID(N'[dbo].[FK_creer3]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Utilisateur_Evenement2] DROP CONSTRAINT [FK_creer3];
GO
IF OBJECT_ID(N'[dbo].[FK_creer4]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Utilisateur_Evenement2] DROP CONSTRAINT [FK_creer4];
GO
IF OBJECT_ID(N'[dbo].[FK_creer5]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Articles] DROP CONSTRAINT [FK_creer5];
GO
IF OBJECT_ID(N'[dbo].[FK_creer6]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Initiatives] DROP CONSTRAINT [FK_creer6];
GO
IF OBJECT_ID(N'[dbo].[FK_FKActivites519089]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Activites] DROP CONSTRAINT [FK_FKActivites519089];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Activites]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Activites];
GO
IF OBJECT_ID(N'[dbo].[Agenda]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Agenda];
GO
IF OBJECT_ID(N'[dbo].[Agenda_Activites]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Agenda_Activites];
GO
IF OBJECT_ID(N'[dbo].[Articles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Articles];
GO
IF OBJECT_ID(N'[dbo].[compte_rendu]', 'U') IS NOT NULL
    DROP TABLE [dbo].[compte_rendu];
GO
IF OBJECT_ID(N'[dbo].[Evenement]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Evenement];
GO
IF OBJECT_ID(N'[dbo].[groue_detail]', 'U') IS NOT NULL
    DROP TABLE [dbo].[groue_detail];
GO
IF OBJECT_ID(N'[dbo].[Groupe_thematiqe]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Groupe_thematiqe];
GO
IF OBJECT_ID(N'[dbo].[Initiatives]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Initiatives];
GO
IF OBJECT_ID(N'[dbo].[Programme]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Programme];
GO
IF OBJECT_ID(N'[dbo].[Projet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Projet];
GO
IF OBJECT_ID(N'[dbo].[Type_Activite]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Type_Activite];
GO
IF OBJECT_ID(N'[dbo].[Utilisateur]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Utilisateur];
GO
IF OBJECT_ID(N'[dbo].[Utilisateur_Evenement2]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Utilisateur_Evenement2];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Activites'
CREATE TABLE [dbo].[Activites] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Type_ActiviteID] int  NOT NULL,
    [Nom_activ] varchar(255)  NULL,
    [Objectif_activ] varchar(255)  NULL,
    [Date] datetime  NULL
);
GO

-- Creating table 'Agenda'
CREATE TABLE [dbo].[Agenda] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [UtilisateurID] int  NOT NULL,
    [Nom_agenda] varchar(255)  NULL,
    [Date_creation] datetime  NULL
);
GO

-- Creating table 'Articles'
CREATE TABLE [dbo].[Articles] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [UtilisateurID] int  NOT NULL,
    [Titre_article] varchar(255)  NULL,
    [Description] int  NULL,
    [Contenu_article] int  NULL,
    [Image] varchar(255)  NULL,
    [Url_video] varchar(255)  NULL,
    [Date_creation] datetime  NULL
);
GO

-- Creating table 'compte_rendu'
CREATE TABLE [dbo].[compte_rendu] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [ActivitesID] int  NOT NULL,
    [Titre_cr] varchar(255)  NULL,
    [Date_debut] datetime  NULL,
    [Date_fin] datetime  NULL,
    [Sujet1] int  NULL,
    [Sujet2] int  NULL,
    [Autre] int  NULL,
    [Statut] bit  NULL
);
GO

-- Creating table 'Evenements'
CREATE TABLE [dbo].[Evenements] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Titre_even] varchar(255)  NULL,
    [Description] int  NULL,
    [Image] varchar(255)  NULL,
    [Date_debut] datetime  NULL,
    [Date_fin] datetime  NULL,
    [Statut] bit  NULL
);
GO

-- Creating table 'groue_detail'
CREATE TABLE [dbo].[groue_detail] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Groupe_thematiqeID] int  NOT NULL,
    [UtilisateurID] int  NOT NULL
);
GO

-- Creating table 'Groupe_thematiqe'
CREATE TABLE [dbo].[Groupe_thematiqe] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Nom_groupe] varchar(255)  NULL,
    [Date_createion] datetime  NULL,
    [Statut] bit  NULL
);
GO

-- Creating table 'Initiatives'
CREATE TABLE [dbo].[Initiatives] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [UtilisateurID] int  NOT NULL,
    [Nom_init] varchar(255)  NULL,
    [Statu_init] bit  NULL,
    [Date_debu] datetime  NULL,
    [Date_fin] datetime  NULL,
    [Objectifs_generaux] varchar(255)  NULL,
    [Obgectifs_specifiques] varchar(255)  NULL,
    [Description_court] varchar(255)  NULL,
    [Description_detaillee] varchar(255)  NULL,
    [Budget] real  NULL,
    [Approbateur] varchar(255)  NULL,
    [Cofinancement] varchar(255)  NULL,
    [Regions] varchar(255)  NULL
);
GO

-- Creating table 'Programmes'
CREATE TABLE [dbo].[Programmes] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [UtilisateurID] int  NOT NULL,
    [Nom_prog] varchar(255)  NULL,
    [Objectif_prog] varchar(255)  NULL,
    [Date_debut] datetime  NULL,
    [Date_fin] datetime  NULL
);
GO

-- Creating table 'Projets'
CREATE TABLE [dbo].[Projets] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [ProgrammeID] int  NOT NULL,
    [Nom_projet] varchar(255)  NULL,
    [Description] varchar(255)  NULL,
    [Objectif_projet] varchar(255)  NULL,
    [Date_debut] datetime  NULL,
    [Date_fin] datetime  NULL
);
GO

-- Creating table 'Type_Activite'
CREATE TABLE [dbo].[Type_Activite] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Nom_type] varchar(255)  NULL
);
GO

-- Creating table 'Utilisateurs'
CREATE TABLE [dbo].[Utilisateurs] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Nom] varchar(255)  NULL,
    [Prenom] varchar(255)  NULL,
    [Email] varchar(255)  NULL,
    [Telephone] varchar(255)  NULL,
    [Mot_passe] varchar(255)  NULL,
    [Discriminator] varchar(255)  NOT NULL
);
GO

-- Creating table 'Agenda_Activites'
CREATE TABLE [dbo].[Agenda_Activites] (
    [Agenda_ID] int  NOT NULL,
    [Activites_ID] int  NOT NULL
);
GO

-- Creating table 'Utilisateur_Evenement2'
CREATE TABLE [dbo].[Utilisateur_Evenement2] (
    [Utilisateurs_ID] int  NOT NULL,
    [Evenements_ID] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'Activites'
ALTER TABLE [dbo].[Activites]
ADD CONSTRAINT [PK_Activites]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Agenda'
ALTER TABLE [dbo].[Agenda]
ADD CONSTRAINT [PK_Agenda]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Articles'
ALTER TABLE [dbo].[Articles]
ADD CONSTRAINT [PK_Articles]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'compte_rendu'
ALTER TABLE [dbo].[compte_rendu]
ADD CONSTRAINT [PK_compte_rendu]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Evenements'
ALTER TABLE [dbo].[Evenements]
ADD CONSTRAINT [PK_Evenements]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'groue_detail'
ALTER TABLE [dbo].[groue_detail]
ADD CONSTRAINT [PK_groue_detail]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Groupe_thematiqe'
ALTER TABLE [dbo].[Groupe_thematiqe]
ADD CONSTRAINT [PK_Groupe_thematiqe]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Initiatives'
ALTER TABLE [dbo].[Initiatives]
ADD CONSTRAINT [PK_Initiatives]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Programmes'
ALTER TABLE [dbo].[Programmes]
ADD CONSTRAINT [PK_Programmes]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Projets'
ALTER TABLE [dbo].[Projets]
ADD CONSTRAINT [PK_Projets]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Type_Activite'
ALTER TABLE [dbo].[Type_Activite]
ADD CONSTRAINT [PK_Type_Activite]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Utilisateurs'
ALTER TABLE [dbo].[Utilisateurs]
ADD CONSTRAINT [PK_Utilisateurs]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [Agenda_ID], [Activites_ID] in table 'Agenda_Activites'
ALTER TABLE [dbo].[Agenda_Activites]
ADD CONSTRAINT [PK_Agenda_Activites]
    PRIMARY KEY CLUSTERED ([Agenda_ID], [Activites_ID] ASC);
GO

-- Creating primary key on [Utilisateurs_ID], [Evenements_ID] in table 'Utilisateur_Evenement2'
ALTER TABLE [dbo].[Utilisateur_Evenement2]
ADD CONSTRAINT [PK_Utilisateur_Evenement2]
    PRIMARY KEY CLUSTERED ([Utilisateurs_ID], [Evenements_ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ActivitesID] in table 'compte_rendu'
ALTER TABLE [dbo].[compte_rendu]
ADD CONSTRAINT [FK_avoir]
    FOREIGN KEY ([ActivitesID])
    REFERENCES [dbo].[Activites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_avoir'
CREATE INDEX [IX_FK_avoir]
ON [dbo].[compte_rendu]
    ([ActivitesID]);
GO

-- Creating foreign key on [Type_ActiviteID] in table 'Activites'
ALTER TABLE [dbo].[Activites]
ADD CONSTRAINT [FK_FKActivites519089]
    FOREIGN KEY ([Type_ActiviteID])
    REFERENCES [dbo].[Type_Activite]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FKActivites519089'
CREATE INDEX [IX_FK_FKActivites519089]
ON [dbo].[Activites]
    ([Type_ActiviteID]);
GO

-- Creating foreign key on [UtilisateurID] in table 'Agenda'
ALTER TABLE [dbo].[Agenda]
ADD CONSTRAINT [FK_creer]
    FOREIGN KEY ([UtilisateurID])
    REFERENCES [dbo].[Utilisateurs]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_creer'
CREATE INDEX [IX_FK_creer]
ON [dbo].[Agenda]
    ([UtilisateurID]);
GO

-- Creating foreign key on [UtilisateurID] in table 'Articles'
ALTER TABLE [dbo].[Articles]
ADD CONSTRAINT [FK_creer5]
    FOREIGN KEY ([UtilisateurID])
    REFERENCES [dbo].[Utilisateurs]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_creer5'
CREATE INDEX [IX_FK_creer5]
ON [dbo].[Articles]
    ([UtilisateurID]);
GO

-- Creating foreign key on [Groupe_thematiqeID] in table 'groue_detail'
ALTER TABLE [dbo].[groue_detail]
ADD CONSTRAINT [FK_Class]
    FOREIGN KEY ([Groupe_thematiqeID])
    REFERENCES [dbo].[Groupe_thematiqe]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Class'
CREATE INDEX [IX_FK_Class]
ON [dbo].[groue_detail]
    ([Groupe_thematiqeID]);
GO

-- Creating foreign key on [UtilisateurID] in table 'groue_detail'
ALTER TABLE [dbo].[groue_detail]
ADD CONSTRAINT [FK_Class2]
    FOREIGN KEY ([UtilisateurID])
    REFERENCES [dbo].[Utilisateurs]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Class2'
CREATE INDEX [IX_FK_Class2]
ON [dbo].[groue_detail]
    ([UtilisateurID]);
GO

-- Creating foreign key on [UtilisateurID] in table 'Initiatives'
ALTER TABLE [dbo].[Initiatives]
ADD CONSTRAINT [FK_creer6]
    FOREIGN KEY ([UtilisateurID])
    REFERENCES [dbo].[Utilisateurs]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_creer6'
CREATE INDEX [IX_FK_creer6]
ON [dbo].[Initiatives]
    ([UtilisateurID]);
GO

-- Creating foreign key on [ProgrammeID] in table 'Projets'
ALTER TABLE [dbo].[Projets]
ADD CONSTRAINT [FK_contient]
    FOREIGN KEY ([ProgrammeID])
    REFERENCES [dbo].[Programmes]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_contient'
CREATE INDEX [IX_FK_contient]
ON [dbo].[Projets]
    ([ProgrammeID]);
GO

-- Creating foreign key on [UtilisateurID] in table 'Programmes'
ALTER TABLE [dbo].[Programmes]
ADD CONSTRAINT [FK_creer2]
    FOREIGN KEY ([UtilisateurID])
    REFERENCES [dbo].[Utilisateurs]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_creer2'
CREATE INDEX [IX_FK_creer2]
ON [dbo].[Programmes]
    ([UtilisateurID]);
GO

-- Creating foreign key on [Agenda_ID] in table 'Agenda_Activites'
ALTER TABLE [dbo].[Agenda_Activites]
ADD CONSTRAINT [FK_Agenda_Activites_Agenda]
    FOREIGN KEY ([Agenda_ID])
    REFERENCES [dbo].[Agenda]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Activites_ID] in table 'Agenda_Activites'
ALTER TABLE [dbo].[Agenda_Activites]
ADD CONSTRAINT [FK_Agenda_Activites_Activites]
    FOREIGN KEY ([Activites_ID])
    REFERENCES [dbo].[Activites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Agenda_Activites_Activites'
CREATE INDEX [IX_FK_Agenda_Activites_Activites]
ON [dbo].[Agenda_Activites]
    ([Activites_ID]);
GO

-- Creating foreign key on [Utilisateurs_ID] in table 'Utilisateur_Evenement2'
ALTER TABLE [dbo].[Utilisateur_Evenement2]
ADD CONSTRAINT [FK_Utilisateur_Evenement2_Utilisateur]
    FOREIGN KEY ([Utilisateurs_ID])
    REFERENCES [dbo].[Utilisateurs]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Evenements_ID] in table 'Utilisateur_Evenement2'
ALTER TABLE [dbo].[Utilisateur_Evenement2]
ADD CONSTRAINT [FK_Utilisateur_Evenement2_Evenement]
    FOREIGN KEY ([Evenements_ID])
    REFERENCES [dbo].[Evenements]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Utilisateur_Evenement2_Evenement'
CREATE INDEX [IX_FK_Utilisateur_Evenement2_Evenement]
ON [dbo].[Utilisateur_Evenement2]
    ([Evenements_ID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------