CREATE TABLE [Admin]
(
[id] [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
[adminnummer]  AS ('A'+right('000'+CONVERT([varchar](10),[ID]),(3))) PERSISTED,
[naam] [nvarchar](250) NOT NULL,
[voornaam] [nvarchar](250) NOT NULL
)

CREATE TABLE [Klant]
(
[klantnummer] INT NOT NULL PRIMARY KEY IDENTITY,
[naam] NVARCHAR(250) NOT NULL,
[voornaam] NVARCHAR(250) NOT NULL,
[mailadres] NVARCHAR(250) NOT NULL,
);

CREATE TABLE [Toesteltype]
(
[toesteltypeid] INT NOT NULL PRIMARY KEY IDENTITY,
[toesteltypenaam] VARCHAR(250) NOT NULL
);

CREATE TABLE [Toestel]
(
[toestelnummer] INT NOT NULL PRIMARY KEY IDENTITY,
[toestelnaam] VARCHAR(250) NOT NULL,
[status] VARCHAR(250) NOT NULL,
[toesteltype] INT NOT NULL,
CONSTRAINT [FK_Toestel_Toesteltype] FOREIGN KEY ([toesteltype]) REFERENCES [Toesteltype]([toesteltypeid])
);

CREATE TABLE [Tijdslot]
(
[tijdslot] INT PRIMARY KEY NOT NULL,
);

CREATE TABLE [Reservatie]
(
[reservatienummer] INT NOT NULL PRIMARY KEY IDENTITY,
[klantnummer] INT NOT NULL,
[datum] DATE NOT NULL,
CONSTRAINT [FK_Reservatie_Klant] FOREIGN KEY ([klantnummer]) REFERENCES [Klant]([klantnummer])
);

CREATE TABLE [ReservatieInfo]
(
[reservatienummer] INT NOT NULL,
[beginuur] INT NOT NULL,
[einduur] INT NOT NULL,
[toestelnummer] INT NOT NULL,
CONSTRAINT [FK_ReservatieInfo_Reservatie] FOREIGN KEY ([reservatienummer]) REFERENCES [Reservatie](reservatienummer),
CONSTRAINT [FK_ReservatieInfo_BeginTijdslot] FOREIGN KEY (beginuur) REFERENCES [Tijdslot]([tijdslot]),
CONSTRAINT [FK_ReservatieInfo_EindTijdslot] FOREIGN KEY (einduur) REFERENCES [Tijdslot]([tijdslot]),
CONSTRAINT [FK_ReservatieInfo_Toestel] FOREIGN KEY ([toestelnummer]) REFERENCES [Toestel]([toestelnummer])
);

DECLARE @tijdslot UNIQUEIDENTIFIER = NEWID();
DECLARE @mailadres UNIQUEIDENTIFIER = NEWID();

SELECT t.toestelnummer,t.toestelnaam,t.status,tt.toesteltypenaam FROM Toestel t
LEFT JOIN Toesteltype tt ON t.toesteltype=tt.toesteltypeid
WHERE t.[status]='operatief' AND tt.toesteltypenaam='x' AND t.toestelnummer 
NOT IN(SELECT i.toestelnummer FROM ReservatieInfo i 
LEFT JOIN Reservatie r ON i.reservatienummer=r.reservatienummer 
WHERE r.datum LIKE 'x' AND (i.beginuur BETWEEN x AND x-1 OR i.einduur BETWEEN x+1 AND x));

