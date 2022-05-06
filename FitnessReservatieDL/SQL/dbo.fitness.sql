CREATE TABLE [Klant]
(
[klantnummer] INT NOT NULL PRIMARY KEY IDENTITY,
[naam] NVARCHAR(250) NOT NULL,
[voornaam] NVARCHAR(250) NOT NULL,
[mailadres] NVARCHAR(250) NOT NULL
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
[tijdslotid] INT NOT NULL PRIMARY KEY IDENTITY,
[beginuur] INT NOT NULL,
[einduur] INT NOT NULL,
);

CREATE TABLE [Reservatie]
(
[reservatienummer] INT NOT NULL PRIMARY KEY IDENTITY,
[klantnummer] INT NOT NULL,
[datum] DATE NOT NULL,
[tijdslotid] INT NOT NULL,
[toestelnummer] INT NOT NULL,
CONSTRAINT [FK_Reservatie_Klant] FOREIGN KEY ([klantnummer]) REFERENCES [Klant]([klantnummer]),
CONSTRAINT [FK_Reservatie_Tijdslot] FOREIGN KEY (tijdslotid) REFERENCES [Tijdslot](tijdslotid),
CONSTRAINT [FK_Reservatie_Toestel] FOREIGN KEY ([toestelnummer]) REFERENCES [Toestel]([toestelnummer])
);



