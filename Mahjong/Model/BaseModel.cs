using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using Mahjong.Model;
using System.Collections.ObjectModel;

namespace Mahjong.Model
{
    /// <summary>
    /// 基础数据
    /// </summary>
   public class BaseModel:ObservableObject
    {
        public BaseModel()
        {
            initialCards = new ObservableCollection<TypeModel>();
            //eastPlayerCards = new ObservableCollection<TypeModel>();
            //southPlayerCards = new ObservableCollection<TypeModel>();
            //westPlayerCards = new ObservableCollection<TypeModel>();
            //northPlayerCards = new ObservableCollection<TypeModel>();
            //eastPlayedCards = new ObservableCollection<TypeModel>();
            //southPlayedCards = new ObservableCollection<TypeModel>();
            //westPlayedCards = new ObservableCollection<TypeModel>();
            //northPlayedCards = new ObservableCollection<TypeModel>();
        }
        #region 属性定义
        #region 底牌牌面
        private ObservableCollection<TypeModel> initialCards;
        /// <summary>
        /// 底牌牌面
        /// </summary>
        public ObservableCollection<TypeModel> InitialCards
        {
            get { return initialCards; }
            set { initialCards = value; RaisePropertyChanged(nameof(InitialCards)); }
        }
        #endregion

        #region 玩家牌面 --临时取消
        //private ObservableCollection<TypeModel> eastPlayerCards;
        ///// <summary>
        ///// 东玩家牌面
        ///// </summary>
        //public ObservableCollection<TypeModel> EastPlayerCards
        //{
        //    get { return eastPlayerCards; }
        //    set { eastPlayerCards = value; RaisePropertyChanged(nameof(EastPlayerCards)); }
        //}

        //private ObservableCollection<TypeModel> southPlayerCards;
        ///// <summary>
        ///// 南玩家牌面
        ///// </summary>
        //public ObservableCollection<TypeModel> SouthPlayerCards
        //{
        //    get { return southPlayerCards; }
        //    set { southPlayerCards = value; RaisePropertyChanged(nameof(SouthPlayerCards)); }
        //}

        //private ObservableCollection<TypeModel> westPlayerCards;
        ///// <summary>
        ///// 西玩家牌面
        ///// </summary>
        //public ObservableCollection<TypeModel> WestPlayerCards
        //{
        //    get { return westPlayerCards; }
        //    set { westPlayerCards = value; RaisePropertyChanged(nameof(WestPlayerCards)); }
        //}

        //private ObservableCollection<TypeModel> northPlayerCards;
        ///// <summary>
        ///// 北玩家牌面
        ///// </summary>
        //public ObservableCollection<TypeModel> NorthPlayerCards
        //{
        //    get { return northPlayerCards; }
        //    set { northPlayerCards = value; RaisePropertyChanged(nameof(NorthPlayerCards)); }
        //}
        #endregion

        #region 已打牌面 --临时取消
        //private ObservableCollection<TypeModel> eastPlayedCards;
        ///// <summary>
        ///// 东已打牌面
        ///// </summary>
        //public ObservableCollection<TypeModel> EastPlayedCards
        //{
        //    get { return eastPlayedCards; }
        //    set { eastPlayedCards = value; RaisePropertyChanged(nameof(EastPlayedCards)); }
        //}

        //private ObservableCollection<TypeModel> southPlayedCards;
        ///// <summary>
        ///// 南已打牌面
        ///// </summary>
        //public ObservableCollection<TypeModel> SouthPlayedCards
        //{
        //    get { return southPlayedCards; }
        //    set { southPlayedCards = value; RaisePropertyChanged(nameof(SouthPlayedCards)); }
        //}

        //private ObservableCollection<TypeModel> westPlayedCards;
        ///// <summary>
        ///// 西已打牌面
        ///// </summary>
        //public ObservableCollection<TypeModel> WestPlayedCards
        //{
        //    get { return westPlayedCards; }
        //    set { westPlayedCards = value; RaisePropertyChanged(nameof(WestPlayedCards)); }
        //}

        //private ObservableCollection<TypeModel> northPlayedCards;
        ///// <summary>
        ///// 北已打牌面
        ///// </summary>
        //public ObservableCollection<TypeModel> NorthPlayedCards
        //{
        //    get { return northPlayedCards; }
        //    set { northPlayedCards = value; RaisePropertyChanged(nameof(NorthPlayedCards)); }
        //}

        //private int myVar;

        //public int MyProperty
        //{
        //    get { return myVar; }
        //    set { myVar = value; RaisePropertyChanged(nameof(MyProperty)); }
        //}

        #endregion
        #endregion
    }
}
