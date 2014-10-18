using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

using PropertyChanged;
using Allegro.Models;
using Allegro.Utility;
using Allegro.pl.allegro.webapi;
using Allegro.Repositories;

namespace Allegro
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<CountryInfoType> countries { get; set; }
        public DateTime date { get; set; }
        
        public string Login { get; set; }


        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            LoginRepository repoE = new LoginRepository();
            var hash= Converter.GetHashCode(pass);
            var details= repoE.LoginEnc(login, hash, Countries.Poland);

            FeedbackRepository r = new FeedbackRepository(details.SessionHandle);

            //var wystawioneN= r.GetFeedback(FeedbackType.Given, details.UserId, CommentType.Negative, 0);
            //var wystawioneP = r.GetFeedback(FeedbackType.Given, details.UserId, CommentType.Positive, 0);
            //var wystawioneNeu = r.GetFeedback(FeedbackType.Given, details.UserId, CommentType.Neutral, 0);

            //var recived=r.GetAllFeedback(FeedbackType.All, details.UserId, CommentType.Negative);
            //var given = r.GetAllFeedback(FeedbackType.Recived, details.UserId, CommentType.Positive);
            //List<UserFeedback> list = new List<UserFeedback>();
            //List<UserFeedback> list2 = new List<UserFeedback>();

            //foreach (var feedback in given)
            //{
            //    var converted = Converter.GetUserFeedback(feedback);
            //    list.Add(converted);
            //}
            //foreach (var feedback in recived)
            //{
            //    var converted = Converter.GetUserFeedback(feedback);
            //    list2.Add(converted);
            //}

            //int downloaded=r.GetNumberOfFeedbacks(FeedbackType.Given,CommentType.Positive,details.UserId);
            //var PozytywneOtrzymane = r.GetFeedback(FeedbackType.Recived, details.UserId, CommentType.Positive, 0);
            //var porcja2 = r.GetFeedback(FeedbackType.Recived, details.UserId, CommentType.Positive, 25, out downloaded);
            //var poracja3 = r.GetFeedback(FeedbackType.Recived, details.UserId, CommentType.Positive, 50, out downloaded);

            //PozytywneWszystkie= PozytywneWszystkie.Concat(r.GetFeedback(FeedbackType.All, details.UserId, CommentType.Positive, 25)).ToList();
            //var otrzymaneP = r.GetFeedback(FeedbackType.Recived, details.UserId, CommentType.Positive, 0);
            //var otrzymaneNeu = r.GetFeedback(FeedbackType.Recived, details.UserId, CommentType.Neutral, 0);


            SellRatingEstimationStruct rate = new SellRatingEstimationStruct { sellratinggroupestimation = 1, sellratinggroupid = 1, sellratingreasonid = 1 };
            var list = new List<SellRatingEstimationStruct>();
            list.Add(rate);
            //ShopFeedback sf = new ShopFeedback(list);

            var serialized = Converter.SerializeToString<List<SellRatingEstimationStruct>>(list);
            var deser = Converter.DeserializeFromString<List<SellRatingEstimationStruct>>(serialized);

            //TEST.Text = serializedString;
        }










































































































        string pass = "645054288", login = "Hangman93";


    }
}
