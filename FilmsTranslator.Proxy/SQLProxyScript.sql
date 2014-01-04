Create table Sites(
	id int identity(1,1) PRIMARY KEY,
	site nvarchar(255) UNIQUE,
	lastcheck DateTime null
)

Create table IPlist(
	idSite int NOT NULL,
	ip nvarchar(21) PRIMARY KEY,
	status tinyint NOT NULL,
)

alter table IPlist add foreign key(idSite) references Sites(id) ON UPDATE NO ACTION ON DELETE CASCADE
