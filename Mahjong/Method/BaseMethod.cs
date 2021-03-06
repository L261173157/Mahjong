﻿using Mahjong.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mahjong.Method
{
    /// <summary>
    /// 基本方法
    /// </summary>
    public class BaseMethod 
    {
        
        #region 方法
        /// <summary>
        /// 洗牌方法
        /// </summary>
        /// <param name="IInitialCards"></param>
        public static ObservableCollection<TypeModel> ShuffleCards(ObservableCollection<TypeModel> IInitialCards)
        {
            if (IInitialCards is null)
            {
                throw new ArgumentNullException(nameof(IInitialCards));
            }

            IInitialCards.Clear();
            for (int i = 0; i < 4; i++)
            {
                foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                {
                    IInitialCards.Add(new TypeModel(Suit.条, rank));
                    IInitialCards.Add(new TypeModel(Suit.万, rank));
                    IInitialCards.Add(new TypeModel(Suit.饼, rank));
                }
                IInitialCards.Add(new TypeModel(Suit.红中, null));
                IInitialCards.Add(new TypeModel(Suit.发财, null));
                IInitialCards.Add(new TypeModel(Suit.白板, null));
                IInitialCards.Add(new TypeModel(Suit.东风, null));
                IInitialCards.Add(new TypeModel(Suit.南风, null));
                IInitialCards.Add(new TypeModel(Suit.西风, null));
                IInitialCards.Add(new TypeModel(Suit.北风, null));
            }
            Random random = new Random();
            var newlist = new ObservableCollection<TypeModel>();
            foreach (var item in IInitialCards)
            {
                newlist.Insert(random.Next(newlist.Count), item);
            }
            return newlist;
        }

        /// <summary>
        /// 初始发牌方法
        /// </summary>
        /// <param name="IInitialCards"></param>
        /// <param name="EastPlayerCards"></param>
        /// <param name="SouthPlayerCards"></param>
        /// <param name="WestPlayerCards"></param>
        /// <param name="NorthPlayerCards"></param>
        public static void FirstDealCards(ObservableCollection<TypeModel> IInitialCards,
                                   ObservableCollection<TypeModel> EastPlayerCards,
                                   ObservableCollection<TypeModel> SouthPlayerCards,
                                   ObservableCollection<TypeModel> WestPlayerCards,
                                   ObservableCollection<TypeModel> NorthPlayerCards)
        {
            if (IInitialCards==null )
            {
                throw new ArgumentNullException(nameof(IInitialCards));
            }
            if (EastPlayerCards == null)
            {
                throw new ArgumentNullException(nameof(EastPlayerCards));
            }
            if (SouthPlayerCards == null)
            {
                throw new ArgumentNullException(nameof(SouthPlayerCards));
            }
            if (WestPlayerCards == null)
            {
                throw new ArgumentNullException(nameof(WestPlayerCards));
            }
            if (NorthPlayerCards == null)
            {
                throw new ArgumentNullException(nameof(NorthPlayerCards));
            }

            EastPlayerCards.Clear();
            SouthPlayerCards.Clear();
            WestPlayerCards.Clear();
            NorthPlayerCards.Clear();

            for (int i = 0; i < 13; i++)
            {
                EastPlayerCards.Add(IInitialCards[0]);
                IInitialCards.RemoveAt(0);
                SouthPlayerCards.Add(IInitialCards[0]);
                IInitialCards.RemoveAt(0);
                WestPlayerCards.Add(IInitialCards[0]);
                IInitialCards.RemoveAt(0);
                NorthPlayerCards.Add(IInitialCards[0]);
                IInitialCards.RemoveAt(0);
            }
        }

        /// <summary>
        /// 发牌到待定
        /// </summary>
        /// <param name="IInitialCards"></param>      
        public static TypeModel DealCards(ObservableCollection<TypeModel> IInitialCards)
        {
            TypeModel t =new TypeModel();
            if (IInitialCards == null)
            {
                throw new ArgumentNullException(nameof(IInitialCards));
            }
            t =new TypeModel(IInitialCards[0]) ;
            IInitialCards.RemoveAt(0);
            return t;
        }

        
        #endregion
        
       

    }
}
