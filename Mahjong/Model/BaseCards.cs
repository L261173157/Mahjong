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
   public class BaseCards:ObservableObject
    {
        public BaseCards()
        {
            initialCards = new ObservableCollection<TypeModel>();
            undeterminedCard = new TypeModel();
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

        private TypeModel undeterminedCard;
        /// <summary>
        /// 待定牌
        /// </summary>
        public TypeModel UndeterminedCard
        {
            get { return undeterminedCard; }
            set { undeterminedCard = value; RaisePropertyChanged(nameof(UndeterminedCard)); }
        }

        #endregion
        #endregion
    }
}
