using GalaSoft.MvvmLight;
using Mahjong.Model;
using GalaSoft.MvvmLight.CommandWpf;
using Mahjong.Method;
using System;
using System.Threading;
using GalaSoft.MvvmLight.Messaging;

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
        #region 属性定义
        #region 基础类定义
        private BaseModel baseModel;
        /// <summary>
        /// 牌面定义
        /// </summary>
        public BaseModel BaseModel
        {
            get { return baseModel; }
            set { baseModel = value; RaisePropertyChanged(nameof(BaseModel)); }
        }

        private BaseMethod baseMethod;
        /// <summary>
        /// 基本方法
        /// </summary>
        public BaseMethod BaseMothod
        {
            get { return baseMethod; }
            set { baseMethod = value; }
        }
        #endregion

        #region 玩家牌面定义
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

        #region 内部变量
        private int status;
        /// <summary>
        /// 状态机变量
        /// </summary>
        public int Status
        {
            get { return status; }
            set { status = value;RaisePropertyChanged(nameof(Status)); }
        }

        private int statusTemp;
        /// <summary>
        /// 状态机缓存变量
        /// </summary>
        public int StatusTemp
        {
            get { return statusTemp; }
            set { statusTemp = value; RaisePropertyChanged(nameof(StatusTemp)); }
        }

        private int playNumber = -1;

        public int PlayNumber
        {
            get { return playNumber; }
            set { playNumber = value; RaisePropertyChanged(nameof(PlayNumber)); }
        }



        #endregion

        #endregion
        #region 命令

        private RelayCommand startCmd;
        /// <summary>
        /// 游戏开始
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
        /// 东玩家打牌
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
        #region 命令方法

        private void start()
        {
            baseModel.InitialCards = BaseMethod.ShuffleCards(baseModel.InitialCards);
            BaseMethod.FirstDealCards(baseModel.InitialCards, eastPlayer.PlayerCards, SouthPlayer.PlayerCards, WestPlayer.PlayerCards, NorthPlayer.PlayerCards);
            eastPlayer.Sort();
            southPlayer.Sort();
            westPlayer.Sort();
            northPlayer.Sort();
            //主流程流转另开线程
            Thread t = new Thread(new ThreadStart(MainFlow));
            t.Start();
        }

        private void EastPlay(string SerialNumer)
        {
            if (Status == 110)
            {
                PlayNumber = Convert.ToInt32(SerialNumer);
            }
        }
        #endregion
        #region 普通方法
        /// <summary>
        /// 主流程-老版
        /// </summary>
        private void MainFlow_Old()
        {
            switch (Status)
            {
                case 10:
                    BaseMethod.EveryTimeDealCards(baseModel.InitialCards, eastPlayer.PlayerCards);
                    if (eastPlayer.IsComputer)
                    {
                        eastPlayer.Play();
                        MainFlow_Old();
                    }
                    else
                    {
                        Status = 11;
                    }
                    break;
                case 20:
                    BaseMethod.EveryTimeDealCards(baseModel.InitialCards, southPlayer.PlayerCards);
                    if (southPlayer.IsComputer)
                    {
                        southPlayer.Play();
                        
                        Status = 30;
                        MainFlow_Old();
                    }
                    break;
                case 30:
                    BaseMethod.EveryTimeDealCards(baseModel.InitialCards, westPlayer.PlayerCards);
                    if (westPlayer.IsComputer)
                    {
                        westPlayer.Play();
                        
                        Status = 40;
                        MainFlow_Old();
                    }
                    break;
                case 40:
                    BaseMethod.EveryTimeDealCards(baseModel.InitialCards, northPlayer.PlayerCards);
                    if (northPlayer.IsComputer)
                    {
                        northPlayer.Play();
                        
                        Status = 10;
                        MainFlow_Old();
                    }
                    break;
                default:
                    break;
            }

        }
        

        private void MainFlow()
        {
            do
            {
                switch (Status)
                {
                    case 0:
                        StatusTemp = 0;
                        Status = 100;
                        break;
                    #region 东玩家状态
                    case 100:
                        BaseMethod.EveryTimeDealCards(baseModel.InitialCards, eastPlayer.PlayerCards);
                        Status = 105;
                        break;
                    case 105:
                        if (eastPlayer.JudgeClaimSelf()==1)
                        {
                            StatusTemp = 106;
                            Status = 150;
                        }
                        if (eastPlayer.JudgeClaimSelf() == -1)
                        {
                            Status = 106;
                        }
                        break;
                    case 106:
                        if (eastPlayer.JudgeKongSelf() == 1)
                        {
                            StatusTemp = 110;
                            Status = 155;
                        }
                        if (eastPlayer.JudgeKongSelf() == -1)
                        {
                            Status = 110;
                        }
                        break;
                    case 110:
                        if (eastPlayer.IsComputer)
                        {
                            eastPlayer.Play();
                        }
                        else
                        {
                            do
                            {
                                if (playNumber != -2)
                                {
                                    eastPlayer.Play(PlayNumber);
                                }
                            } while (playNumber != -2);
                        }
                        Status = 121;
                        break;
                    case 120://占位
                        if (eastPlayer.JudgeClaim(eastPlayer.PlayedCards[eastPlayer.PlayedCards.Count-1])==1)
                        {
                            StatusTemp = 121;
                            Status = 150;
                        }
                        if (eastPlayer.JudgeClaim(eastPlayer.PlayedCards[eastPlayer.PlayedCards.Count - 1]) == -1)
                        {
                            
                            Status = 121; 
                        }
                        break;
                    case 121:
                        if (southPlayer.JudgeClaim(eastPlayer.PlayedCards[eastPlayer.PlayedCards.Count - 1]) == 1)
                        {
                            StatusTemp = 122;
                            Status = 251;
                            
                        }
                        if (southPlayer.JudgeClaim(eastPlayer.PlayedCards[eastPlayer.PlayedCards.Count - 1]) == -1)
                        {

                            Status = 122;
                           
                        }
                        break;
                    case 122:
                        if (westPlayer.JudgeClaim(eastPlayer.PlayedCards[eastPlayer.PlayedCards.Count - 1]) == 1)
                        {
                            StatusTemp = 123;
                            Status = 352;
                            
                        }
                        if (westPlayer.JudgeClaim(eastPlayer.PlayedCards[eastPlayer.PlayedCards.Count - 1]) == -1)
                        {

                            Status = 123;
                            
                        }
                        break;
                    case 123:
                        if (northPlayer.JudgeClaim(eastPlayer.PlayedCards[eastPlayer.PlayedCards.Count - 1]) == 1)
                        {
                            StatusTemp = 125;
                            Status = 453;

                        }
                        if (northPlayer.JudgeClaim(eastPlayer.PlayedCards[eastPlayer.PlayedCards.Count - 1]) == -1)
                        {

                            Status = 125;

                        }
                        break;
                    case 125:
                        if (southPlayer.JudgeKong(eastPlayer.PlayedCards[eastPlayer.PlayedCards.Count - 1]) == 1)
                        {
                            StatusTemp = 130;
                            Status = 255;
                            break;
                        }
                        if (westPlayer.JudgeKong(eastPlayer.PlayedCards[eastPlayer.PlayedCards.Count - 1]) == 1)
                        {
                            StatusTemp = 130;
                            Status = 355;
                            break;
                        }
                        if (northPlayer.JudgeKong(eastPlayer.PlayedCards[eastPlayer.PlayedCards.Count - 1]) == 1)
                        {
                            StatusTemp = 130;
                            Status = 455;
                            break;
                        }
                        Status = 130;
                        break;
                    case 130:
                        if (southPlayer.JudgePung(eastPlayer.PlayedCards[eastPlayer.PlayedCards.Count - 1]) == 1)
                        {
                            StatusTemp = 135;
                            Status = 260;
                            break;
                        }
                        if (westPlayer.JudgePung(eastPlayer.PlayedCards[eastPlayer.PlayedCards.Count - 1]) == 1)
                        {
                            StatusTemp = 135;
                            Status = 360;
                            break;
                        }
                        if (northPlayer.JudgePung(eastPlayer.PlayedCards[eastPlayer.PlayedCards.Count - 1]) == 1)
                        {
                            StatusTemp = 135;
                            Status = 460;
                            break;
                        }
                        Status = 135;
                        break;
                    case 135://占位
                        if (eastPlayer.JudgeChow(eastPlayer.PlayedCards[eastPlayer.PlayedCards.Count - 1]) == 1)
                        {
                            StatusTemp = 136;
                            Status = 165;
                        }
                        if (eastPlayer.JudgeChow(eastPlayer.PlayedCards[eastPlayer.PlayedCards.Count - 1]) == -1)
                        {
                            Status = 136;
                        }
                        break;
                    case 136:
                        if (southPlayer.JudgeChow(eastPlayer.PlayedCards[eastPlayer.PlayedCards.Count - 1]) == 1)
                        {
                            StatusTemp = 137;
                            Status = 266;
                        }
                        if (southPlayer.JudgeChow(eastPlayer.PlayedCards[eastPlayer.PlayedCards.Count - 1]) == -1)
                        {
                            Status = 137;
                        }
                        break;
                    case 137:
                        if (southPlayer.JudgeChow(eastPlayer.PlayedCards[eastPlayer.PlayedCards.Count - 1]) == 1)
                        {
                            StatusTemp = 138;
                            Status = 367;
                        }
                        if (southPlayer.JudgeChow(eastPlayer.PlayedCards[eastPlayer.PlayedCards.Count - 1]) == -1)
                        {
                            Status = 138;
                        }
                        break;
                    case 138:
                        if (southPlayer.JudgeChow(eastPlayer.PlayedCards[eastPlayer.PlayedCards.Count - 1]) == 1)
                        {
                            StatusTemp = 200;
                            Status = 468;
                        }
                        if (southPlayer.JudgeChow(eastPlayer.PlayedCards[eastPlayer.PlayedCards.Count - 1]) == -1)
                        {
                            Status = 200;
                        }
                        break;

                    case 150:
                        if (eastPlayer.ClaimSelf() == 1)
                        {
                            Status = 1000;
                        }
                        if (eastPlayer.ClaimSelf() == -1)
                        {
                            Status = StatusTemp;
                            StatusTemp = 0;
                        }
                        break;
                    case 151:
                        if (eastPlayer.Claim(southPlayer.PlayedCards[southPlayer.PlayedCards.Count - 1]) == 1)
                        {
                            Status = 1001;
                        }
                        if (eastPlayer.Claim(southPlayer.PlayedCards[southPlayer.PlayedCards.Count - 1]) == -1)
                        {
                            Status = StatusTemp;
                            StatusTemp = 0;
                        }
                        break;
                    case 152:
                        if (eastPlayer.Claim(westPlayer.PlayedCards[westPlayer.PlayedCards.Count - 1]) == 1)
                        {
                            Status = 1002;
                        }
                        if (eastPlayer.Claim(westPlayer.PlayedCards[westPlayer.PlayedCards.Count - 1]) == -1)
                        {
                            Status = StatusTemp;
                            StatusTemp = 0;
                        }
                        break;
                    case 153:
                        if (eastPlayer.Claim(northPlayer.PlayedCards[northPlayer.PlayedCards.Count - 1]) == 1)
                        {
                            Status = 1003;
                        }
                        if (eastPlayer.Claim(northPlayer.PlayedCards[northPlayer.PlayedCards.Count - 1]) == -1)
                        {
                            Status = StatusTemp;
                            StatusTemp = 0;
                        }
                        break;
                       
                    case 155:
                        switch (StatusTemp)
                        {
                            case 110:
                                if (eastPlayer.KongSelf() == 1)
                                {
                                    Status = 100;
                                }
                                if (eastPlayer.KongSelf() == -1)
                                {
                                    Status = StatusTemp;
                                    StatusTemp = 0;
                                }
                                break;
                            case 230:
                                if (eastPlayer.Kong(southPlayer.PlayedCards[southPlayer.PlayedCards.Count - 1]) == 1)
                                {
                                    Status = 100;
                                }
                                if (eastPlayer.Kong(southPlayer.PlayedCards[southPlayer.PlayedCards.Count - 1]) == -1)
                                {
                                    Status = StatusTemp;
                                    StatusTemp = 0;
                                }
                                break;
                            case 330:
                                if (eastPlayer.Kong(westPlayer.PlayedCards[westPlayer.PlayedCards.Count - 1]) == 1)
                                {
                                    Status = 100;
                                }
                                if (eastPlayer.Kong(westPlayer.PlayedCards[westPlayer.PlayedCards.Count - 1]) == -1)
                                {
                                    Status = StatusTemp;
                                    StatusTemp = 0;
                                }
                                break;
                            case 430:
                                if (eastPlayer.Kong(northPlayer.PlayedCards[northPlayer.PlayedCards.Count - 1]) == 1)
                                {
                                    Status = 100;
                                }
                                if (eastPlayer.Kong(northPlayer.PlayedCards[northPlayer.PlayedCards.Count - 1]) == -1)
                                {
                                    Status = StatusTemp;
                                    StatusTemp = 0;
                                }
                                break;

                            default:
                                break;
                        }
                        break;
                    case 160:
                        switch (StatusTemp)
                        {
                            case 135://占位
                                if (eastPlayer.Pung(eastPlayer.PlayedCards[eastPlayer.PlayedCards.Count - 1]) == 1)
                                {
                                    Status = 110;
                                }
                                if (eastPlayer.Pung(eastPlayer.PlayedCards[eastPlayer.PlayedCards.Count - 1]) == -1)
                                {
                                    Status = StatusTemp;
                                    StatusTemp = 0;
                                }
                                break;
                            case 235:
                                if (eastPlayer.Pung(southPlayer.PlayedCards[southPlayer.PlayedCards.Count - 1]) == 1)
                                {
                                    Status = 110;
                                }
                                if (eastPlayer.Pung(southPlayer.PlayedCards[southPlayer.PlayedCards.Count - 1]) == -1)
                                {
                                    Status = StatusTemp;
                                    StatusTemp = 0;
                                }
                                break;
                            case 335:
                                if (eastPlayer.Pung(westPlayer.PlayedCards[westPlayer.PlayedCards.Count - 1]) == 1)
                                {
                                    Status = 110;
                                }
                                if (eastPlayer.Pung(westPlayer.PlayedCards[westPlayer.PlayedCards.Count - 1]) == -1)
                                {
                                    Status = StatusTemp;
                                    StatusTemp = 0;
                                }
                                break;
                            case 435:
                                if (eastPlayer.Pung(northPlayer.PlayedCards[northPlayer.PlayedCards.Count - 1]) == 1)
                                {
                                    Status = 110;
                                }
                                if (eastPlayer.Pung(northPlayer.PlayedCards[northPlayer.PlayedCards.Count - 1]) == -1)
                                {
                                    Status = StatusTemp;
                                    StatusTemp = 0;
                                }
                                break;

                            default:
                                break;
                        }
                        break;
                    case 165://占位
                        if (eastPlayer.Chow(eastPlayer.PlayedCards[eastPlayer.PlayedCards.Count - 1]) == 1)
                        {
                            Status = 110;
                        }
                        if (eastPlayer.Chow(eastPlayer.PlayedCards[eastPlayer.PlayedCards.Count - 1]) == -1)
                        {
                            Status = StatusTemp;
                            StatusTemp = 0;
                        }
                        break;
                    case 166:
                        if (eastPlayer.Chow(southPlayer.PlayedCards[southPlayer.PlayedCards.Count - 1]) == 1)
                        {
                            Status = 110;
                        }
                        if (eastPlayer.Chow(southPlayer.PlayedCards[southPlayer.PlayedCards.Count - 1]) == -1)
                        {
                            Status = StatusTemp;
                            StatusTemp = 0;
                        }
                        break;
                    case 167:
                        if (eastPlayer.Chow(westPlayer.PlayedCards[westPlayer.PlayedCards.Count - 1]) == 1)
                        {
                            Status = 110;
                        }
                        if (eastPlayer.Chow(westPlayer.PlayedCards[westPlayer.PlayedCards.Count - 1]) == -1)
                        {
                            Status = StatusTemp;
                            StatusTemp = 0;
                        }
                        break;
                    case 168:
                        if (eastPlayer.Chow(northPlayer.PlayedCards[northPlayer.PlayedCards.Count - 1]) == 1)
                        {
                            Status = 110;
                        }
                        if (eastPlayer.Chow(northPlayer.PlayedCards[northPlayer.PlayedCards.Count - 1]) == -1)
                        {
                            Status = StatusTemp;
                            StatusTemp = 0;
                        }
                        break;
                    #endregion
                    default:
                        break;
                }
            } while (Status==1000);
        }
        #endregion
    }
}