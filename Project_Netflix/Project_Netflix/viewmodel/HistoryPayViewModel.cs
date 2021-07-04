using Project_Netflix.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Netflix.viewmodel
{
	public class HistoryPayViewModel : BaseViewModel
	{
		private ObservableCollection<string> _DSPay;
		public ObservableCollection<string> DSPay { get => _DSPay; set { _DSPay = value;OnPropertyChanged(); } }

		public HistoryPayViewModel()
		{
			using(var db = new NETFLIX_DBEntities())
			{
				DSPay = new ObservableCollection<string>();
				if (DangNhapViewModel.IsLogin)
				{
					var user = db.PURCHASEs.Where(x => x.USER_ID == DangNhapViewModel.User.ID).ToList();
					foreach (var item in user)
					{
						string history = String.Format(@"Bạn đã đăng ký gói {0} vào ngày: {1}. Kết thúc vào ngày: {2}.", item.PACKAGE.NAME, item.PURCHASED_DATE, item.OUTOFDATE);
						DSPay.Add(history);
					}
				}
			}
		}
	}
}
