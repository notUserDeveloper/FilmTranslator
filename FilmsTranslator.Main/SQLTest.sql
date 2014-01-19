/****** Скрипт для команды SelectTopNRows из среды SSMS  ******/
  DELETE FROM dbo.Films
  DELETE FROM dbo.Kinopoisks;
  DELETE FROM dbo.NewTitles;
  DELETE FROM substatistics;



  SELECT fl.StartTitle, rez.TransliteTitle,rez.Predictor,rez.RusName,rez.EngName FROM dbo.Films AS fl
  LEFT JOIN
  (SELECT nt.TransliteTitle, nt.Predictor, nt.FilmId,kp.RusName,kp.EngName FROM dbo.NewTitles AS nt
  LEFT JOIN
  (SELECT kp.id, kp.RusName, kp.EngName FROM dbo.Kinopoisks AS kp) AS kp
  ON kp.Id = nt.KinopoiskId) AS rez
ON fl.Id = rez.FilmId  


 insert into Parasites(Word,Count) values
  ('DVDRip',0),
('D',0),
('BDRip',0),
('DVDRip-AVC',0),
('HDRip',0),
('1400MB',0),
('P',0),
('V2',0),
('ELEKTRI4KA',0),
('by',0),
('torrents',0),
('ru',0),
('Dt',0),
('IRONCLUB',0),
('HELLYWOOD',0),
('XviD',0),
('WEBDLRip',0),
('700MB',0),
('WEB-DLRip',0),
('[uniongang',0),
('tv]',0),
('uniongang',0),
('tv',0),
('BDRip-AVC',0),
('1400',0),
('ts',0),
('2100',0),
('WEBRip',0),
('DUB',0),
('AC3',0),
('_by',0),
('ZNG505_',0),
('46Gb',0),
('745',0),
('RelizLab',0),
('MDteam',0),
('[torrents',0),
('ru]',0),
('EXTENDED',0),
('DUAL',0),
('AVC',0),
('2100Mb',0),
('x264',0),
('potroks',0),
('WEB',0),
('DLRip',0),
('Dalemake',0),
('46Gb_',0),
('Rus',0),
('Eng',0),
('FreeTorrents-UA',0),
('UNRATED',0),
('TMD',0),
('RG',0),
('nnm-club',0),
('WEBDL-Rip',0)