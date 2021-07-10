using Project_Netflix.model;
using Project_Netflix.View.Admin.Report.Revenue;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Project_Netflix.viewmodel.Admin.Report.Revenue
{
	public class RevenueViewModel : BaseViewModel
	{
		private ObservableCollection<string> _DSCachTinh;
		public ObservableCollection<string> DSCachTinh { get => _DSCachTinh; set { _DSCachTinh = value; OnPropertyChanged(); } }

		private ObservableCollection<PURCHASE> _DSPay;
		public ObservableCollection<PURCHASE> DSPay { get => _DSPay; set { _DSPay = value; OnPropertyChanged(); } }

		private string _SelectedCachTinh;
		public string SelectedCachTinh { get => _SelectedCachTinh; set { _SelectedCachTinh = value; OnPropertyChanged(); } }

		private object _Currentsolution;
		public object Currentsolution { get => _Currentsolution; set { _Currentsolution = value; OnPropertyChanged(); } }
		private double _Total;
		public double Total { get => _Total; set { _Total = value; OnPropertyChanged(); } }
		public Quy quy { get; set; }
		public Thang thang { get; set; }
		public Ngay ngay { get; set; }

		public ICommand SelectionChange { get; set; }
		public ICommand CmdLoad { get; set; }
		public RevenueViewModel()
		{
			quy = new Quy();
			thang = new Thang();
			ngay = new Ngay();
			Total = 0;
			Currentsolution = null;
			DSCachTinh = new ObservableCollection<string>(new List<string>() { "Quy", "Thang", "Ngay" });
			CmdLoad = new RelayCommand<object>(
				(o) =>
				{
					return true;
				}

				, (o) =>
			{
				Load();
			});
			SelectionChange = new RelayCommand<object>((o) =>
			{
				return true;
			}

				, (o) =>
			{
				//MessageBox.Show(SelectedCachTinh);
				switch (SelectedCachTinh)
				{
					case "Quy":
						Currentsolution = quy;
						break;
					case "Thang":
						Currentsolution = thang;
						break;
					case "Ngay":
						Currentsolution = ngay;
						break;
				}
			});
		}
		public void Load()
		{
			switch (SelectedCachTinh)
			{
				case "Quy":
					var year = Quy.vm.SelectedYear;
					var qUY = Quy.vm.SelectedQuy;
					using (var db = new NETFLIX_DBEntities())
					{
						DSPay = new ObservableCollection<PURCHASE>(db.PURCHASEs.Include("ACCOUNT").Include("PACKAGE").Where(x => x.PURCHASED_DATE.Year == year && x.PURCHASED_DATE.Month <= qUY * 3).ToList());
						if (DSPay.Count > 0)
						{
							foreach (var item in DSPay)
							{
								Total += (double)item.PACKAGE.PRICE;
							}
						}
					}
					break;
				case "Thang":
					var year1 = Thang.vm.SelectedYear;
					var tHANG = Thang.vm.SelectedMonth;
					using (var db = new NETFLIX_DBEntities())
					{
						DSPay = new ObservableCollection<PURCHASE>(db.PURCHASEs.Include("ACCOUNT").Include("PACKAGE").Where(x => x.PURCHASED_DATE.Year == year1 && x.PURCHASED_DATE.Month == tHANG).ToList());
						if (DSPay.Count > 0)
						{
							foreach (var item in DSPay)
							{
								Total += (double)item.PACKAGE.PRICE;
							}
						}
					}
					break;
				case "Ngay":
					var start = Ngay.vm.Start;
					var end = Ngay.vm.End;
					using (var db = new NETFLIX_DBEntities())
					{
						DSPay = new ObservableCollection<PURCHASE>(db.PURCHASEs.Include("ACCOUNT").Include("PACKAGE").Where(x => x.PURCHASED_DATE >= start && x.OUTOFDATE <= end).ToList());
						if (DSPay.Count > 0)
						{
							foreach (var item in DSPay)
							{
								Total += (double)item.PACKAGE.PRICE;
							}
						}
					}
					break;
			}
		}
	}
}

