using System.Diagnostics;
using System.Linq;
using System.Threading;
using FilmsTranslator.Main.Code;
using FilmsTranslator.Main.Code.Parsers;
using FilmsTranslator.Main.Code.Spellers;
using FilmsTranslator.Main.EntityProviders;

namespace FilmsTranslator.Main
{
    /// <summary>
    ///     Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private const string DirPath = @"d:\film\";

        private readonly FilmManager _filmManager;
        private readonly FilmProvider _filmProvider;
        private readonly TranslationManager _translationManager;
        private readonly ClearManager _clearManager;
        private readonly ParasiteProvider _parasiteProvider;
        private readonly NewTitleProvider _newTitleProvider;
        private readonly KinopoiskParser _kinopoiskParser;
        private readonly KinopoiskProvider _kinopoiskProvider;
        private readonly KinopoiskFilmVariatorProvider _kinopoiskFilmVariatorProvider;
        private readonly KinopoiskFilmVariatorManager _kinopoiskFilmVariatorManager;

        public MainWindow()
        {
            InitializeComponent();
            // Database.SetInitializer(new DropCreateDatabaseAlways<EntityContext>());   

            _filmManager = new FilmManager();
            _filmProvider = new FilmProvider();
            _translationManager = new TranslationManager(
                new Transliteration(), new PredictorManager(
                    new YandexSpeller(new HttpRequester())));
            _clearManager = new ClearManager();
            _parasiteProvider = new ParasiteProvider();
            _newTitleProvider = new NewTitleProvider();
            _kinopoiskParser = new KinopoiskParser(new HttpRequester());
            _kinopoiskProvider = new KinopoiskProvider();
            _kinopoiskFilmVariatorProvider = new KinopoiskFilmVariatorProvider();
            _kinopoiskFilmVariatorManager = new KinopoiskFilmVariatorManager(new DamerauLevensteinMetric());

            var mainTh = new Thread(Init);
            mainTh.Start();

//            var mainGrabber = new Thread(ProxyMain.MainGrabber);
//            mainGrabber.Start();
        }

        public void Log(string text)
        {
            Dispatcher.Invoke(() => { Logger.Text += text + "\r\n"; });
        }

        private void Init()
        {
            var films = _filmManager.GetFilms(DirPath).ToList();
            _filmProvider.AddFilms(films);

//            var statistic = _statisticManager.GetStatistic(films);
//            _statisticManager.AddStatistic(statistic);

            var filmsUnchecked = _filmProvider.GetFilmsChecked(false).ToList();
            var parasitesWords = _parasiteProvider.GetAllWords().ToList();

            var newTitles = _clearManager.DoClearDictionary(filmsUnchecked, parasitesWords);
            newTitles.ForEach(nt => nt.ClearTitle =  _clearManager.DoClearYear(nt.ClearTitle));
            _newTitleProvider.AddNewTitles(newTitles);

            newTitles.ForEach(nt =>
            {
                _translationManager.DoTranslate(nt);  

                var filmVariations = _kinopoiskParser.GetFilmVariations(nt.Predictor);
                filmVariations.ForEach(v => v.NewTitleId = nt.Id);
                _kinopoiskFilmVariatorProvider.AddFilmVariators(filmVariations);

                var nearFilmVariation = _kinopoiskFilmVariatorManager.GetNearFilmVariation(nt.Predictor + " " + nt.Film.Year, filmVariations);

                var kinopoiskInfo = _kinopoiskParser.GetFilmInfo(nearFilmVariation.Url);
                if (kinopoiskInfo != null)
                {
                    _kinopoiskProvider.AddKinopoiskItem(kinopoiskInfo);
                    nt.KinopoiskId = kinopoiskInfo.Id;
                }
                _filmProvider.SetChecked(nt.FilmId, true);
                _newTitleProvider.SaveChanges();

                Debug.WriteLine(nt.TransliteTitle + " => " + nt.Predictor + " => " + ((kinopoiskInfo==null)?"null":kinopoiskInfo.RusName));
                Thread.Sleep(12000);
            });
         

            #region DontNeed =(

            //            var last =
            //                from t2 in
            //                    from lastTitle in _db.NewTitles.ToList()
            //                    group lastTitle by lastTitle.FilmId
            //                    into glt
            //                    select new {Id = glt.Max(t => t.Id), FilmId = glt.Key}
            //                from t1 in
            //                    from addTrans in _db.NewTitles.ToList()
            //                    where t2.Id == addTrans.Id
            //                    select new {addTrans.Id, addTrans.FilmId, addTrans.TransliteTitle}
            //                join rez in _db.Films.ToList() on t1.FilmId equals rez.Id
            //                select new {rez.StartTitle, t1.TransliteTitle};

            #endregion

            #region kinopoisk

/*            var tbl =
                from t in _db.NewTitles.ToList()
                group t by t.FilmId
                into tg
                select new {Id = tg.Max(x => x.Id), FilmId = tg.Key, SpellingGoogle = tg.Last().Predictor};


            foreach (var item in tbl)
            {
                Kinopoisk kinopoiskInfo = _kinopoiskParser.GetFilmInfo(item.SpellingGoogle);
                if (kinopoiskInfo == null)
                {
                    Dispatcher.Invoke(
                        () => Logger.Text += "#### FAIL ##### ID: " + item.Id + " filmdID: " + item.FilmId + "\n");
                    continue;
                }
                _db.Kinopoisks.Add(kinopoiskInfo);
                _db.NewTitles.First(x => x.Id == item.Id).KinopoiskId = kinopoiskInfo.Id;
                _db.SaveChanges();
                Dispatcher.Invoke(() => Logger.Text += item.SpellingGoogle + " : done" + "\n");
            }*/

            #endregion
        }
    }
}