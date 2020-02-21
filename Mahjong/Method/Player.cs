using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using Mahjong.Model;
using System.Collections.ObjectModel;

namespace Mahjong.Method
{
    /// <summary>
    /// 玩家运算
    /// </summary>
   public class Player
    {
        public Player()
        {
            PlayerCards = new ObservableCollection<TypeModel>();
            PlayedCards = new ObservableCollection<TypeModel>();
            OpenedCards = new ObservableCollection<TypeModel>();
            IsComputer = false;
        }
        #region 属性定义
        /// <summary>
        /// 玩家牌面
        /// </summary>
        public ObservableCollection<TypeModel> PlayerCards { get;}
        /// <summary>
        /// 玩家已打牌面
        /// </summary>
        public ObservableCollection<TypeModel> PlayedCards { get;}
        /// <summary>
        /// 玩家明示牌面
        /// </summary>
        public ObservableCollection<TypeModel> OpenedCards { get;}
        /// <summary>
        /// 是否电脑玩家（1是，0否）
        /// </summary>
        public Boolean IsComputer { get; set; }
        #endregion

        #region 外部方法
        /// <summary>
        /// 人工打牌方法
        /// </summary>
        /// <param name="SerialNumber">第几张牌</param>
        public void Play(int SerialNumber)
        {
            if (IsComputer==false)
            {
                if (SerialNumber!=-1)
                {
                    PlayedCards.Add(PlayerCards[SerialNumber]);
                    PlayerCards.RemoveAt(SerialNumber);
                }
                else
                {
                    PlayedCards.Add(PlayerCards.Last());
                    PlayerCards.RemoveAt(PlayerCards.Count-1);
                }
                
            }
            else
            {
                throw new ArgumentException("IsComputer is wrong");
            }
            Sort();
        }
        /// <summary>
        /// 电脑打牌方法
        /// </summary>
        public void Play()
        {
            if (IsComputer)
            {
                //需添加打牌判断
                Random random = new Random();
                int Index = random.Next(PlayedCards.Count);
                PlayedCards.Add(PlayerCards[Index]);
                PlayerCards.RemoveAt(Index);
            }
            else
            {
                throw new ArgumentException("IsComputer is wrong");
            }
            Sort();

        }

        /// <summary>
        /// 排序
        /// </summary>
        public void Sort()
        {
            List<TypeModel> lst = new List<TypeModel>(PlayerCards);
            lst.Sort(
                (x, y) =>
                {
                    var oSuit = x.Suit - y.Suit;

                    if (oSuit == 0)
                    {
                        if (x.Rank > y.Rank)
                        {
                            return 1;
                        }

                        else if (x.Rank == y.Rank)
                        {
                            return 0;
                        }
                        else
                        {
                            return -1;
                        }
                    }
                    else
                        return oSuit;
                }
            );
            for (int i = 0; i < lst.Count; i++)
            {
                PlayerCards[i] = lst[i];
            }
        }
        public int Action(TypeModel card)
        {
            
        }
        #endregion

        #region 内部方法
        public void Open()
        {
            OpenedCards.Add(PlayerCards[0]);
            OpenedCards.Add(PlayerCards[1]);
            OpenedCards.Add(PlayerCards[2]);
            PlayerCards.RemoveAt(0);
            PlayerCards.RemoveAt(0);
            PlayerCards.RemoveAt(0);
        }
        /// <summary>
        /// 吃牌,0无动作,1动作
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        private int Chow(TypeModel card)
        {
            int num = 0;

            for (int i = 0; i < PlayerCards.Count; i++)
            {
                int? a = card.Rank - PlayerCards[i].Rank;
                
                if (card.Suit==PlayerCards[i].Suit)
                {
                    if (a==-1)
                    {

                    }
                }
            }
        }
        /// <summary>
        /// 碰牌
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        private int Pung(TypeModel card)
        {
            return 0;
        }
        /// <summary>
        /// 杠牌
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        private int Kong(TypeModel card)
        {
            return 0;
        }
        /// <summary>
        /// 和牌
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        private int Claim(TypeModel card)
        {
            return 0;
        }

        #endregion



    }
}
