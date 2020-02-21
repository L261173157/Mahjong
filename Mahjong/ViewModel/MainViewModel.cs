using GalaSoft.MvvmLight;
using Mahjong.Model;
using GalaSoft.MvvmLight.CommandWpf;
using Mahjong.Method;
using System;
using System.Threading;

namespace Mahjong.ViewModel
{
   
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            baseModel = new BaseModel();
            baseMethod = new BaseMethod();
            eastPlayer = new Player() { IsComputer = false};
            southPlayer = new Player() { IsComputer = true };
            westPlayer = new Player() { IsComputer = true };
            northPlayer = new Player() { IsComputer = true };
        }
        #region ���Զ���
        #region ����������
        private BaseModel baseModel;
        /// <summary>
        /// ���涨��
        /// </summary>
        public BaseModel BaseModel
        {
            get { return baseModel; }
            set { baseModel = value; RaisePropertyChanged(nameof(BaseModel)); }
        }

        private BaseMethod baseMethod;
        /// <summary>
        /// ��������
        /// </summary>
        public BaseMethod BaseMothod
        {
            get { return baseMethod; }
            set { baseMethod = value; }
        }
        #region ������涨��
        private Player eastPlayer;

        public Player EastPlayer
        {
            get { return eastPlayer; }
            set { eastPlayer = value; }
        }

        private Player southPlayer;

        public Player SouthPlayer
        {
            get { return southPlayer; }
            set { southPlayer = value; }
        }

        private Player westPlayer;

        public Player WestPlayer
        {
            get { return westPlayer; }
            set { westPlayer = value; }
        }

        private Player northPlayer;

        public Player NorthPlayer
        {
            get { return northPlayer; }
            set { northPlayer = value; }
        }
        #endregion


        #endregion
        #region �ڲ�����
        /// <summary>
        /// �ƾ�״̬
        /// </summary>
        public int status { get; set; }

        #endregion

        #endregion
        #region ����
        private RelayCommand testCmd;
        /// <summary>
        /// ��������1
        /// </summary>
        public RelayCommand TestCmd
        {
            get
            {
                if (testCmd == null)
                    return new RelayCommand(Test);
                return testCmd;
            }
            set
            {
                testCmd = value;
            }
        }

        private RelayCommand<string> testCmd2;
        /// <summary>
        /// ��������2
        /// </summary>
        public RelayCommand<string> TestCmd2
        {
            get
            {
                if (testCmd2 == null)
                    return new RelayCommand<string>(Test2);
                return testCmd2;
            }
            set
            {
                testCmd2 = value;
            }
        }

        private RelayCommand startCmd;
        /// <summary>
        /// ��Ϸ��ʼ
        /// </summary>
        public RelayCommand StartCmd
        {
            get
            {
                if (startCmd == null)
                    return new RelayCommand(start);
                return startCmd;
            }
            set
            {
                startCmd = value;
            }
        }

        private RelayCommand<string> eastPlayCmd;
        /// <summary>
        /// ����Ҵ���
        /// </summary>
        public RelayCommand<string> EastPlayCmd
        {
            get
            {
                if (eastPlayCmd == null)
                    return new RelayCommand<string>(EastPlay);
                return eastPlayCmd;
            }
            set
            {
                eastPlayCmd = value;
            }
        }
        #endregion
        #region �����
        private void Test()
        {
            eastPlayer.Open();
        }

        private void Test2(string SerialNumer)
        {
            eastPlayer.Open();
        }

        private void start()
        {
            baseModel.InitialCards = BaseMethod.ShuffleCards(baseModel.InitialCards);
            BaseMethod.FirstDealCards(baseModel.InitialCards, eastPlayer.PlayerCards, SouthPlayer.PlayerCards, WestPlayer.PlayerCards, NorthPlayer.PlayerCards);
            eastPlayer.Sort();
            status = 10;
            MainFlow();
        }

        private void EastPlay(string SerialNumer)
        {
            if (status == 11)
            {
                eastPlayer.Play(Convert.ToInt32(SerialNumer));
                status = 20;
                MainFlow();
            }
        }
        #endregion
        #region ��ͨ����
        /// <summary>
        /// ������
        /// </summary>
        private void MainFlow()
        {
            switch (status)
            {
                case 10:
                    BaseMethod.EveryTimeDealCards(baseModel.InitialCards, eastPlayer.PlayerCards);
                    if (eastPlayer.IsComputer)
                    {
                        eastPlayer.Play();
                        MainFlow();
                    }
                    else
                    {
                        status = 11;
                    }
                    break;
                case 20:
                    BaseMethod.EveryTimeDealCards(baseModel.InitialCards, southPlayer.PlayerCards);
                    if (southPlayer.IsComputer)
                    {
                        southPlayer.Play();
                        status = 30;
                        MainFlow();
                    }
                    break;
                case 30:
                    BaseMethod.EveryTimeDealCards(baseModel.InitialCards, westPlayer.PlayerCards);
                    if (westPlayer.IsComputer)
                    {
                        westPlayer.Play();
                        status = 40;
                        MainFlow();
                    }
                    break;
                case 40:
                    BaseMethod.EveryTimeDealCards(baseModel.InitialCards, northPlayer.PlayerCards);
                    if (northPlayer.IsComputer)
                    {
                        northPlayer.Play();
                        status = 10;
                        MainFlow();
                    }
                    break;
                default:
                    break;
            }

        }

        
        #endregion
    }
}