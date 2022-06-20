CREATE TABLE [dbo].[StudentTbl] (
    [NrStud]        INT           IDENTITY (1, 1) NOT NULL,
    [NumeSt]        VARCHAR (50)  NOT NULL,
    [PrenumeSt]     VARCHAR (50)  NOT NULL,
    [GenSt]         VARCHAR (50)  NOT NULL,
    [DataNastereSt] DATE          NOT NULL,
    [CnpStud]       VARCHAR (15)  NOT NULL,
    [AdresaSt]      VARCHAR (100) NOT NULL,
    [Id_Fac_St]     INT           NOT NULL,
    [NumeFacSt]     VARCHAR (50)  NOT NULL,
    [TelefonSt]     VARCHAR (15)  NOT NULL,
    [EmailSt]       VARCHAR (40)  NOT NULL,
    [Semestru]      VARCHAR(20)           NOT NULL,
    PRIMARY KEY CLUSTERED ([NrStud] ASC),
    CONSTRAINT [FK_1] FOREIGN KEY ([Id_Fac_St]) REFERENCES [dbo].[DepartamentTbl] ([NrFacultate])
);

