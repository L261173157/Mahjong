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
    /// 底牌数据
    /// </summary>
   public class BaseModel:ObservableObject
    {
        public BaseModel()
        {
            initialCards = new ObservableCollection<TypeModel>();
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
        #endregion
    }
}
