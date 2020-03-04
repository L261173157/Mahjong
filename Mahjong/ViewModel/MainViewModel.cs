using GalaSoft.MvvmLight;
using Mahjong.Model;
using GalaSoft.MvvmLight.CommandWpf;
using Mahjong.Method;
using System;
using System.Threading;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;

namespace Mahjong.ViewModel
{
   
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            baseCards = new BaseCards();
            baseMethod = new BaseMethod();
            eastPlayer = new Player() { IsComputer = false};
            southPlayer = new Player() { IsComputer = true };
            westPlayer = new Player() { IsComputer = true };
            northPlayer = new Player() { IsComputer = true };
        }
        #region 属性定义
        #region 基础类定义
        private BaseCards baseCards;
        /// <summary>
        /// 牌面定义
        /// </summary>
        public BaseCards BaseCards
        {
            get { return baseCards; }
            set { baseCards = value; RaisePropertyChanged(nameof(BaseCards)); }
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



        private Seat playerOrder;
        /// <summary>
        /// 状态机缓存变量
        /// </summary>
        public Seat PlayerOrder
        {
            get { return playerOrder; }
            set { playerOrder = value; RaisePropertyChanged(nameof(PlayerOrder)); }
        }

        private int operateNumber = -1;

        public int OperateNumber
        {
            get { return operateNumber; }
            set { operateNumber = value; RaisePropertyChanged(nameof(OperateNumber)); }
        }

        private bool operateStatus=false;

        public bool OperateStatus
        {
            get { return operateStatus; }
            set { operateStatus = value; RaisePropertyChanged(nameof(OperateStatus)); }
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

        private RelayCommand<string> operateCmd;
        /// <summary>
        /// 东玩家打牌
        /// </summary>
        public RelayCommand<string> OperateCmd
        {
            get
            {
                if (operateCmd == null)
                    return new RelayCommand<string>(Operate);
                return operateCmd;
            }
            set
            {
                operateCmd = value;
            }
        }

        #endregion
        #region 命令方法

        private void start()
        {
            EastPlayer.PlayedCards.Clear();
            SouthPlayer.PlayedCards.Clear();
            WestPlayer.PlayedCards.Clear();
            NorthPlayer.PlayedCards.Clear();

            baseCards.InitialCards = BaseMethod.ShuffleCards(baseCards.InitialCards);
            BaseMethod.FirstDealCards(baseCards.InitialCards, eastPlayer.PlayerCards, SouthPlayer.PlayerCards, WestPlayer.PlayerCards, NorthPlayer.PlayerCards);
            eastPlayer.Sort();
            southPlayer.Sort();
            westPlayer.Sort();
            northPlayer.Sort();
            status = 100;
            playerOrder = Seat.East;

            Task.Run(MainFlow);
            //主流程流转另开线程
            //Thread t = new Thread(MainFlow);
            //t.IsBackground = true;
            //t.Start();
            //MainFlow();
        }

        private void Operate(string SerialNumer)
        {
            OperateNumber = Convert.ToInt32(SerialNumer);
            OperateStatus = true;
        }

        
        #endregion
        #region 普通方法

        private int Operate()
        {
            int t=0;
            
            do
            {
               t= OperateNumber;
            } while (!OperateStatus);
            OperateStatus = false;
            return t;
            
        }

        private void MainFlow()
        {
            
            do
            {
                Thread.Sleep(1000);
                if (baseCards.InitialCards.Count==0)
                {
                    status = 2000;
                }
                switch (status)
                {
                    case 100://发牌
                        DealCardsFlow();
                        Status = 110;
                        break;
                    case 110://和
                        if (JudgeClaimFlow()>0)
                        {
                            Status = 1000;
                        }
                        else
                        {
                            Status = 120;
                        }
                        break;
                    case 120://杠
                        if (JudgeKongFlow() > 0)
                        {
                            Status = 100;
                        }
                        else
                        {
                            Status = 130;
                        }
                        break;
                    case 130://收牌
                        CommmenceFlow(BaseCards.UndeterminedCard);
                        status = 140;
                        break;
                    case 140://打牌
                        BaseCards.UndeterminedCard =new TypeModel(PlayFlow()) ;
                        status = 190;
                        break;
                    case 190:
                        CommmencePlayed(BaseCards.UndeterminedCard);
                        status = 100;
                        PlayerOrderChange();
                        break;
                    default:
                        break;
                }
            } while (status<1000);
        }
        
        private void PlayerOrderChange()
        {
            switch (playerOrder)
            {
                case Seat.East:
                    PlayerOrder = Seat.South;
                    break;
                case Seat.South:
                    PlayerOrder = Seat.West;
                    break;
                case Seat.West:
                    PlayerOrder = Seat.North;
                    break;
                case Seat.North:
                    PlayerOrder = Seat.East;
                    break;
                default:
                    break;
            }
        }
        
        private void DealCardsFlow()
        {
            BaseCards.UndeterminedCard = new TypeModel(BaseMethod.DealCards(baseCards.InitialCards));
        }

        private int JudgeClaimFlow()
        {
            return 0;
        }

        private int JudgeKongFlow()
        {
            return 0;
        }

        private void CommmenceFlow(TypeModel t)
        {
            switch (playerOrder)
            {
                case Seat.East:
                    EastPlayer.Commmence(t);
                    break;
                case Seat.South:
                    SouthPlayer.Commmence(t);
                    break;
                case Seat.West:
                    WestPlayer.Commmence(t);
                    break;
                case Seat.North:
                    NorthPlayer.Commmence(t);
                    break;
                default:
                    break;
            }
        }
        private  TypeModel PlayFlow()
        {
            TypeModel t;
            switch (playerOrder)
            {
                case Seat.East:
                    if (EastPlayer.IsComputer)
                    {
                        return eastPlayer.Play(0);
                    }
                    else
                    {
                        t = new TypeModel(eastPlayer.Play(Operate()));
                        return t;
                    }
                    
                case Seat.South:
                    return southPlayer.Play(0);
                case Seat.West:
                    return westPlayer.Play(0);
                case Seat.North:
                    return northPlayer.Play(0); 
                default:
                    return new TypeModel();
            }
        }

        private void CommmencePlayed(TypeModel t)
        {
            switch (playerOrder)
            {
                case Seat.East:
                    EastPlayer.CommmencePlayed(t);
                    break;
                case Seat.South:
                    SouthPlayer.CommmencePlayed(t);
                    break;
                case Seat.West:
                    WestPlayer.CommmencePlayed(t);
                    break;
                case Seat.North:
                    NorthPlayer.CommmencePlayed(t);
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}