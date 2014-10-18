using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;
using System.Windows.Input;
using AutoBid.Utils;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Threading;
using System.Collections.ObjectModel;
using AutoBid.Repositories;
using AutoBid.Models;
using System.Threading;
using System.ComponentModel;
using AllegroClassLibrary.Repositories;

namespace AutoBid.ViewModels
{
    [ImplementPropertyChanged]
    public class MainViewModel
    {
        private DispatcherTimer dispatcherTimer;
        private WygrajToDbRepository repo;
        private ServiceRepository allegroRepo;

        public MainViewModel()
        {
            LoginString = "";
            BidCounter = 0;
            TotalElementsInQueue = 0;
            ActiveTimeSpan = TimeSpan.Zero;
            LoginControlVisibility = Visibility.Visible;
            MainControlVisibility = Visibility.Collapsed;
            IsActive = false;

            repo = new WygrajToDbRepository();
            BidCollection = new ThreadSafeObservableCollection<Bid>();
            allegroRepo = new ServiceRepository();

            TimerInit();
            InitWorkers();
            AttachCommands();
        }

        private void TimerInit()
        {
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(DispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            ActiveTimeSpan += new TimeSpan(0, 0, 1);
        }












        #region Threading


        BackgroundWorker PopulateBidCollectionThread = new BackgroundWorker();
        BackgroundWorker MakeBidsThread = new BackgroundWorker();

        private void InitWorkers()
        {
            PopulateBidCollectionThread.DoWork += PopulateBidCollectionThread_DoWork;
            MakeBidsThread.DoWork += MakeBidsThread_DoWork;

            MakeBidsThread.WorkerSupportsCancellation = true;
            PopulateBidCollectionThread.WorkerSupportsCancellation = true;
        }

        private void MakeBidsThread_DoWork(object sender, DoWorkEventArgs e)
        {
            MakeBidsBGW();
        }

        private void PopulateBidCollectionThread_DoWork(object sender, DoWorkEventArgs e)
        {
            PopulateBidCollectionBGW(new TimeSpan(0, 0, 1));
        }



        private void PopulateBidCollectionBGW(TimeSpan interval)
        {
            while (IsActive == true)
            {
                var list = repo.GetBids(DateTime.Now);

                foreach (var bid in list)
                {
                    if (!BidCollection.Contains(bid))
                    {
                        BidCollection.Add(bid);
                    }
                }
                Thread.Sleep(interval);
            }
        }

        private void MakeBidsBGW()
        {

            while (IsActive == true)
            {
                DateTime start = DateTime.Now;
                //var postCollection = BidCollection.Where(x => x.BidTime == DateTime.Now - new TimeSpan(0, 0, 1));
                //var postCollection = BidCollection.Where(x => x.AuctionId == 1);
                var postCollection = BidCollection;

                //parael foreach
                Parallel.ForEach(postCollection, bid =>
                {
                    try
                    {
                        //allegroRepo.MakeBid(bid);
                        bid.Posted = true;
                    }
                    finally
                    {
                        BidCollection.Remove(bid);
                    }
                });

                if (DateTime.Now - start < new TimeSpan(0, 0, 0, 0, 750))
                {
                    Thread.Sleep(new TimeSpan(0, 0, 0, 0, 750));
                }
            }
        }


        #endregion


        public string LoginString { get; set; }
        public Visibility LoginControlVisibility { get; set; }
        public Visibility MainControlVisibility { get; set; }
        public DateTime LoginTime { get; set; }
        public TimeSpan ActiveTimeSpan { get; set; }
        public int BidCounter { get; set; }
        public int TotalElementsInQueue { get; set; }
        public bool IsActive { get; set; }
        public ThreadSafeObservableCollection<Bid> BidCollection { get; set; }



        #region Commands

        public ICommand LoginCommand { get; internal set; }
        public ICommand StartStopButtonCommand { get; internal set; }

        private void AttachCommands()
        {
            LoginCommand = new RelayCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
            StartStopButtonCommand = new RelayCommand(ExecuteStartStopButtonCommand, CanExecuteStartStopButtonCommand);
        }

        private bool CanExecuteStartStopButtonCommand(object arg)
        {
            return true;
        }

        /// <summary>
        /// Check login time, popup LoginControl, hide MainControl
        /// start/stop timer
        /// </summary>
        /// <param name="arg"></param>
        private async void ExecuteStartStopButtonCommand(object arg)
        {
            MessageBoxButton buttonBox = MessageBoxButton.OKCancel;
            var result = MessageBox.Show("Na pewno?", "Komunikat", buttonBox);

            if (result == MessageBoxResult.OK)
            {
                if (DateTime.Now - LoginTime > new TimeSpan(0, 15, 0))
                {
                    LoginControlVisibility = Visibility.Visible;
                    MainControlVisibility = Visibility.Collapsed;
                }
                else
                {
                    if (IsActive == true)
                    {
                        dispatcherTimer.Stop();
                        IsActive = false;

                        //stopujemy watki
                        PopulateBidCollectionThread.CancelAsync();
                        MakeBidsThread.CancelAsync();
                    }
                    else
                    {
                        ActiveTimeSpan = TimeSpan.Zero;
                        dispatcherTimer.Start();
                        IsActive = true;

                        //odpalamy watki
                        PopulateBidCollectionThread.RunWorkerAsync();
                        MakeBidsThread.RunWorkerAsync();
                    }
                }

            }
        }

        /// <summary>
        /// Check if username is empty/whitespace/null
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private bool CanExecuteLoginCommand(object arg)
        {
            return !String.IsNullOrEmpty(LoginString) && !String.IsNullOrWhiteSpace(LoginString);
        }

        /// <summary>
        /// Check passwd/login, hide/show controls, save login time
        /// </summary>
        /// <param name="arg"></param>
        private void ExecuteLoginCommand(object arg)
        {
            var passwdbox = arg as PasswordBox;
            if (passwdbox.Password == "haslo" && LoginString == "admin")
            {
                LoginControlVisibility = Visibility.Collapsed;
                MainControlVisibility = Visibility.Visible;
                LoginTime = DateTime.Now;
            }
        }

        #endregion
    }
}
