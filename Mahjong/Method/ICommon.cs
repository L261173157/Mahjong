using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Mahjong.Model;
namespace Mahjong.Method
{
    interface ICommon
    {
        #region 属性定义
        //ObservableCollection<TypeModel> IInitialCards { get; set; }
        //ObservableCollection<TypeModel> IEastPlayerCards { get; set; }
        //ObservableCollection<TypeModel> ISouthPlayerCards { get; set; }
        //ObservableCollection<TypeModel> IWestPlayerCards { get; set; }
        //ObservableCollection<TypeModel> INorthPlayerCards { get; set; }
        //ObservableCollection<TypeModel> IEastPlayedCards { get; set; }
        //ObservableCollection<TypeModel> ISouthPlayedCards { get; set; }
        //ObservableCollection<TypeModel> IWestPlayedCards { get; set; }
        //ObservableCollection<TypeModel> INorthPlayedCards { get; set; }
        #endregion
        #region 方法定义
        /// <summary>
        /// 洗牌方法
        /// </summary>
        /// <param name="IInitialCards">底牌牌面</param>
        /// <returns></returns>
        ObservableCollection<TypeModel> ShuffleCards(ObservableCollection<TypeModel> IInitialCards);
        /// <summary>
        /// 初始发牌方法
        /// </summary>
        /// <param name="IInitialCards">底牌牌面</param>
        /// <param name="EastPlayerCards">东玩家牌面</param>
        /// <param name="SouthPlayerCardss">南玩家牌面</param>
        /// <param name="WestPlayerCards">西玩家牌面</param>
        /// <param name="NorthPlayerCards">北玩家牌面</param>
        void FirstDealCards(ObservableCollection<TypeModel> IInitialCards,
            ObservableCollection<TypeModel> EastPlayerCards,
            ObservableCollection<TypeModel> SouthPlayerCards,
            ObservableCollection<TypeModel> WestPlayerCards,
            ObservableCollection<TypeModel> NorthPlayerCards);
        #endregion

    }
}
