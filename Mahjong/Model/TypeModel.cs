using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace Mahjong.Model
{
	#region 基础枚举定义
	/// <summary>
	/// 花色
	/// </summary>
	public enum Suit
	{
		/// <summary>
		/// 条
		/// </summary>
		条,
		/// <summary>
		/// 饼
		/// </summary>
		饼,
		/// <summary>
		/// 万
		/// </summary>
		万,
		/// <summary>
		/// 东风
		/// </summary>
		东风,
		/// <summary>
		/// 南风
		/// </summary>
		南风,
		/// <summary>
		/// 西风
		/// </summary>
		西风,
		/// <summary>
		/// 北风
		/// </summary>
		北风,
		/// <summary>
		/// 红中
		/// </summary>
		红中,
		/// <summary>
		/// 发财
		/// </summary>
		发财,
		/// <summary>
		/// 白板
		/// </summary>
		白板
	}

	/// <summary>
	/// 面值
	/// </summary>
	public enum Rank
	{
		一,
		二,
		三,
		四,
		五,
		六,
		七,
		八,
		九
	}
	/// <summary>
	/// 座位
	/// </summary>
	public enum Seat
	{
		East,
		South,
		West,
		North
	}
	#endregion
	/// <summary>
	/// 牌张数据
	/// </summary>
	public class TypeModel: ObservableObject
    {
		/// <summary>
		/// 初始化赋值
		/// </summary>
		/// <param name="newsuit">花色</param>
		/// <param name="newrank">面值</param>
		/// <param name="newisOpen">是否明示</param>
		public TypeModel(Suit newsuit,Rank? newrank )
		{
			Suit = newsuit;
			Rank = newrank;
			
			SuitAndRank = this.rank.ToString() + "" + this.suit.ToString();
		}
		public TypeModel(TypeModel previousTypeModel)
		{
			Suit = previousTypeModel.Suit;
			Rank = previousTypeModel.Rank;
			SuitAndRank = previousTypeModel.SuitAndRank;
		}
		public TypeModel()
		{
			suit = null;
			rank = null;
		}
		public void Clear()
		{
			Suit = null;
			Rank = null;
			SuitAndRank = this.rank.ToString() + "" + this.suit.ToString();
		}
		#region 属性定义
		private Suit? suit;
		/// <summary>
		/// 花色属性
		/// </summary>
		public Suit? Suit
		{
			get { return suit; }
			set { suit = value; RaisePropertyChanged(nameof(Suit)); }
		}

		private Rank? rank;
		/// <summary>
		/// 面值属性
		/// </summary>
		public Rank? Rank
		{
			get { return rank; }
			set { rank = value; RaisePropertyChanged(nameof(Rank)); }
		}
		
		

		private string suitAndRank;
		/// <summary>
		/// 牌面+花色
		/// </summary>
		public string SuitAndRank
		{
			get { return suitAndRank; }
			set { suitAndRank = value; RaisePropertyChanged(nameof(SuitAndRank)); }
		}

		#endregion



	}
}
