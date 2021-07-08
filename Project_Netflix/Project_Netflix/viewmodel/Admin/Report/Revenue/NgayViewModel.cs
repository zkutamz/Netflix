using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Netflix.viewmodel.Admin.Report.Revenue
{
	public class NgayViewModel : BaseViewModel
	{
		
		private DateTime _Start;
		public DateTime Start { get => _Start; set { _Start = value; OnPropertyChanged(); } }
		
		private DateTime _End;
		public DateTime End { get => _End; set { _End = value; OnPropertyChanged(); } }

		public NgayViewModel()
		{
		
		}
	}
}
