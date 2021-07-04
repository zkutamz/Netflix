using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Project_Netflix.Command.MainWindow;
using Project_Netflix.model;
using Project_Netflix.View;

namespace Project_Netflix.viewmodel
{
	public class PayViewModel : BaseViewModel
	{
		private PACKAGE _Basic;
		public PACKAGE Basic { get => _Basic; set { _Basic = value; OnPropertyChanged(); } }
		private string _PriceBasic;
		public string PriceBasic { get => _PriceBasic; set { _PriceBasic = value; OnPropertyChanged(); } }

		private PACKAGE _Standard;
		public PACKAGE Standard { get => _Standard; set { _Standard = value; OnPropertyChanged(); } }
		private string _PriceStandard;
		public string PriceStandard { get => _PriceStandard; set { _PriceStandard = value; OnPropertyChanged(); } }

		private PACKAGE _Premium;
		public PACKAGE Premium { get => _Premium; set { _Premium = value; OnPropertyChanged(); } }
		private string _PricePremium;
		public string PricePremium { get => _PricePremium; set { _PricePremium = value; OnPropertyChanged(); } }
		public ICommand CmdPay { get; set; }

		public PayViewModel()
		{
			CmdPay = new CmdPay(this);
			using(var db = new NETFLIX_DBEntities())
			{
				var package = db.PACKAGEs.ToList();
				Basic = package[0];
				Standard = package[1];
				Premium = package[2];
				PriceBasic = Basic.PRICE + " / đ";
				PriceStandard = Standard.PRICE + " / đ";
				PricePremium = Premium.PRICE + " / đ";
			}
		}

		public void Pay(Window window)
		{
			using(var db = new NETFLIX_DBEntities())
			{
				DateTime now = DateTime.Now;
				DateTime outTime = DateTime.Now;
				int idPackage = 0;
				switch (PagePay.choose)
				{
					case "Basic":
						outTime = outTime.AddDays(7);
						idPackage = Basic.ID;
						break;
					case "Standard":
						outTime = outTime.AddMonths(1);
						idPackage = Standard.ID;
						break;
					case "Premium":
						outTime = outTime.AddMonths(6);
						idPackage = Premium.ID;
						break;
				}
				var purchase = new PURCHASE()
				{
					PACKAGE_ID = idPackage,
					USER_ID = DangNhapViewModel.User.ID,
					PURCHASED_DATE = now,
					OUTOFDATE = outTime,
				};
				db.PURCHASEs.Add(purchase);
				db.ACCOUNTs.Where(x => x.ID == DangNhapViewModel.User.ID).Single().ACTIVE = 1;
				db.SaveChanges();
				MainWindow main = new MainWindow();
				window.Close();
				main.Show();
			}
		}
	}
}
