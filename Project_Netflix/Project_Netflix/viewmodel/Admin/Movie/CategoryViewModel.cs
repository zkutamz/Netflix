using Project_Netflix.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Project_Netflix.viewmodel
{
	class CategoryViewModel : BaseViewModel
	{
		private ObservableCollection<CATEGORY> _DSCategory;
		public ObservableCollection<CATEGORY> DSCategory
		{ get => _DSCategory; set { _DSCategory = value; OnPropertyChanged(); } }

		private CATEGORY _SelectedItem;
		public CATEGORY SelectedItem
		{ get => _SelectedItem; set { _SelectedItem = value; OnPropertyChanged(); } }

		public CategoryViewModel()
		{
			using (var db = new NETFLIX_DBEntities()) {
				DSCategory = new ObservableCollection<CATEGORY>(db.CATEGORies.ToList());
			}
		}
		public CATEGORY ShowSelection()
		{
			return SelectedItem;
		}
	}
}
