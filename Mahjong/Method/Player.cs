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
                PlayedCards.Add(PlayerCards[SerialNumber]);
                PlayerCards.RemoveAt(SerialNumber);
            }
            else
            {
                throw new ArgumentException("IsComputer is wrong");
            }
            
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

        }
        #endregion

        #region 内部方法
        /// <summary>
        /// 排序
        /// </summary>
        public void Sort()
        {
            List<TypeModel> lst = new List<TypeModel>(PlayerCards);
            //lst.Sort();
            //PlayerCards = new ObservableCollection<TypeModel>(lst);
        }
        

        #endregion



    }
}
