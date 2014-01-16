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