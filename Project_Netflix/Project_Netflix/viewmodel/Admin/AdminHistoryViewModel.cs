using Project_Netflix.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Project_Netflix.viewmodel.Admin
{
	public class AdminHistoryViewModel : DependencyObject
	{
		public static readonly DependencyProperty PayProperty;


		static AdminHistoryViewModel()
		{
			PayProperty = DependencyProperty.Register("DSPay", typeof(ObservableCollection<PURCHASE>), typeof(AdminHistoryViewModel));

		}
		public ObservableCollection<PURCHASE> DSPay
		{
			get => (ObservableCollection<PURCHASE>)GetValue(PayProperty);
			set => SetValue(PayProperty, value);
		}
		public AdminHistoryViewModel(ACCOUNT account)
		{
			using (var db = new NETFLIX_DBEntities())
			{
				DSPay = new ObservableCollection<PURCHASE>(db.PURCHASEs.Include("ACCOUNT").Include("PACKAGE").Where(x => x.USER_ID == account.ID).ToList());
			}
		}
	}
}
