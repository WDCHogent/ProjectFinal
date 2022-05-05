CREATE TABLE [dbo].[Klant]
(
[klantnummer] INT NOT NULL PRIMARY KEY IDENTITY,
[Naam] NVARCHAR(250) NOT NULL,
[Voornaam] NVARCHAR(250) NOT NULL,
[mailadres] NVARCHAR(250) NOT NULL
);

CREATE TABLE [dbo].[ToestelType]
(
[toesteltype] VARCHAR(250) NOT NULL
);

CREATE TABLE [dbo].[Toestel]
(
[toestelnummer] INT NOT NULL PRIMARY KEY IDENTITY,
[toestelnaam] VARCHAR(250) NOT NULL,
[status] BIT NOT NULL DEFAULT 0,
[toesteltype] VARCHAR(250) NOT NULL
);

CREATE TABLE [dbo].[Tijdslot]
(
[tijdslot] INT NOT NULL 
);

CREATE TABLE [dbo].[Reservatie]
(
[reservatienummer] INT NOT NULL PRIMARY KEY IDENTITY,
[klantnummer] INT NOT NULL,
[datum] DATE NOT NULL,
[tijdslot],
[toestelnummer] INT NOT NULL
CONSTRAINT [FK_Reservatie_Klant] FOREIGN KEY ([klantnummer]) REFERENCES [Klant]([klantnummer]),
CONSTRAINT [FK_Reservatie_Toestel] FOREIGN KEY ([toestelnummer]) REFERENCES [Toestel]([toestelnummer])
);



