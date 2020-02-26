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

        #region 普通方法
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
        
        #endregion

        #region 判定方法
        /// <summary>
        /// 判定吃牌,-1无动作,>=1动作
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public int JudgeChow(TypeModel card)
        {
            List<int> orderNumber=new List<int>();
            int totalNumber=0;
            if (card.Suit==Suit.条|| card.Suit == Suit.饼|| card.Suit == Suit.万)
            {
                for (int i = 0; i < PlayerCards.Count - 1; i++)
                {
                    if (card.Suit == PlayerCards[i].Suit && card.Suit == PlayerCards[i + 1].Suit)
                    {
                        if (card.Rank+1==PlayerCards[i].Rank&& card.Rank + 2 == PlayerCards[i+1].Rank )
                        {
                            orderNumber.Add(i);
                            totalNumber++;
                        }
                        else if (card.Rank -1  == PlayerCards[i].Rank && card.Rank + 1 == PlayerCards[i + 1].Rank)
                        {
                            orderNumber.Add(i);
                            totalNumber++;
                        }
                        else if (card.Rank - 2 == PlayerCards[i].Rank && card.Rank - 1 == PlayerCards[i + 1].Rank)
                        {
                            orderNumber.Add(i);
                            totalNumber++;
                        }   
                    } 
                }              
            }
            else
            {
                totalNumber = -1;     
            }
            return totalNumber;
        }

        /// <summary>
        /// 判定碰牌
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public int JudgePung(TypeModel card)
        {
            return 0;
        }

        /// <summary>
        /// 判定杠牌
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public int JudgeKong(TypeModel card)
        {
            return 0;
        }

        /// <summary>
        /// 判定自身杠牌
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public int JudgeKongSelf()
        {
            return 0;
        }
        
        /// <summary>
        /// 判定和牌
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public int JudgeClaim(TypeModel card)
        {
            return 0;
        }

        /// <summary>
        /// 判定自身和牌
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public int JudgeClaimSelf()
        {
            return 0;
        }
        #endregion

        #region 动作方法
        /// <summary>
        /// 和牌
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public int Claim(TypeModel card)
        {
            return 0;
        }

        /// <summary>
        /// 自身和牌
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public int ClaimSelf()
        {
            return 0;
        }

        /// <summary>
        /// 杠牌
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public int Kong(TypeModel card)
        {
            return 0;
        }

        /// <summary>
        /// 自身杠牌
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public int KongSelf()
        {
            return 0;
        }
        /// <summary>
        /// 碰牌
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public int Pung(TypeModel card)
        {
            return 0;
        }

        /// <summary>
        /// 吃牌
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public int Chow(TypeModel card)
        {
            return 0;
        }

        #endregion



    }
}
