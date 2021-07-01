using Project_Netflix.Command.Admin.Category;
using Project_Netflix.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Project_Netflix.viewmodel.Admin.Category
{
	public class AdminCategoryViewModel : BaseViewModel
	{
		public ICommand CmdUpdateCategory { get; set; }
		public ICommand CmdDeleteCategory { get; set; }
		public ICommand CmdAddCategory { get; set; }
		public ICommand CmdDeleteCategoryMovie { get; set; }
		public ICommand CmdAddCategoryMovie { get; set; }
		private string _Name;
		public string Name { get => _Name; set { _Name = value; OnPropertyChanged(); } }
		private ObservableCollection<CATEGORY> _DSCategory;


		public ObservableCollection<CATEGORY> DSCategory { get => _DSCategory; set { _DSCategory = value; OnPropertyChanged(); } }
		private CATEGORY _SelectedCategory;
		public CATEGORY SelectedCategory { get => _SelectedCategory; set { _SelectedCategory = value; OnPropertyChanged(); if (SelectedCategory != null) { Name = SelectedCategory.NAME;
					using (var db = new NETFLIX_DBEntities()) {
						Categorys = new ObservableCollection<CATEGORY>(db.CATEGORies.Where(x => x.ID == SelectedCategory.ID).ToList());
					} } } }
		private MOVIE _SelectedMovie;
		public MOVIE SelectedMovie { get => _SelectedMovie; set { _SelectedMovie = value; OnPropertyChanged(); } }
		/// <summary>
		/// ////////////////////////
		private string _NameMovie;
		public string NameMovie { get => _NameMovie; set { _NameMovie = value; OnPropertyChanged(); } }
		private string _Poster;
		public string Poster { get => _Poster; set { _Poster = value; OnPropertyChanged(); } }
		private string _Movie;
		public string Movie { get => _Movie; set { _Movie = value; OnPropertyChanged(); } }
		private string _Trailer;
		public string Trailer { get => _Trailer; set { _Trailer = value; OnPropertyChanged(); } }
		private string _Description;
		public string Description { get => _Description; set { _Description = value; OnPropertyChanged(); } }
		private string _Country;
		public string Country { get => _Country; set { _Country = value; OnPropertyChanged(); } }
		private CATEGORY _Categori;
		public CATEGORY Categori { get => _Categori; set { _Categori = value; OnPropertyChanged(); } }
		private MOVIE_TYPE _Type;
		public MOVIE_TYPE Type { get => _Type; set { _Type = value; OnPropertyChanged(); } }
		private double _Rate;
		public double Rate { get => _Rate; set { _Rate = value; OnPropertyChanged(); } }
		private int _Year;
		public int Year { get => _Year; set { _Year = value; OnPropertyChanged(); } }
		public ICommand CmdAddMovie { get; set; }
		private ObservableCollection<int> _DSYear;
		public ObservableCollection<int> DSYear { get => _DSYear; set { _DSYear = value; OnPropertyChanged(); } }
		private ObservableCollection<string> _DSCountry;
		public ObservableCollection<string> DSCountry { get => _DSCountry; set { _DSCountry = value; OnPropertyChanged(); } }
		private ObservableCollection<CATEGORY> _Categorys;
		public ObservableCollection<CATEGORY> Categorys { get => _Categorys; set { _Categorys = value; OnPropertyChanged(); } }

		private ObservableCollection<MOVIE_TYPE> _DSType;
		public ObservableCollection<MOVIE_TYPE> DSType { get => _DSType; set { _DSType = value; OnPropertyChanged(); } }
		/// </summary>
		public AdminCategoryViewModel()
		{
			CmdAddCategory = new CmdAddCategory(this);
			CmdUpdateCategory = new CmdUpdateCategory(this);
			CmdDeleteCategory = new CmdDeleteCategory(this);
			CmdAddCategoryMovie = new CmdAddMovie(this);
			CmdDeleteCategoryMovie = new CmdDeleteMovie(this);
			using (var sr = new StreamReader(@"..\..\viewmodel\Admin\Movie\Country.txt"))
			{
				string listCountry = sr.ReadToEnd();
				string[] Countries = listCountry.Split('\n');

				var countries = from n in Countries orderby n select n;
				DSCountry = new ObservableCollection<string>(countries);

			}
			int now = DateTime.Now.Year;
			List<int> years = new List<int>();
			for (int i = now + 10; i >= 1900; i--)
			{
				years.Add(i);
			}
			DSYear = new ObservableCollection<int>(years);
			using (var db = new NETFLIX_DBEntities())
			{
				DSCategory = new ObservableCollection<CATEGORY>(db.CATEGORies.Include("MOVIEs").ToList());
				DSType = new ObservableCollection<MOVIE_TYPE>(db.MOVIE_TYPE.ToList());
			}
		}
		public void addCategory()
		{
			using (var db = new NETFLIX_DBEntities())
			{
				if (db.CATEGORies.Where(x => x.NAME == Name).ToList().Count() == 0)
				{
					var category = new CATEGORY() { NAME = Name };
					db.CATEGORies.Add(category);
					db.SaveChanges();
					MessageBox.Show("Them thanh cong.");
					loadCategory();
				}
				else
				{
					MessageBox.Show("The loai da ton tai.");
				}
			}
		}
		public void updateCategory()
		{
			using (var db = new NETFLIX_DBEntities())
			{
				if (db.CATEGORies.Where(x => x.NAME == Name).ToList().Count() == 0)
				{
					db.CATEGORies.Where(x => x.ID == SelectedCategory.ID).Single().NAME = Name;
					db.SaveChanges();
					MessageBox.Show("Sua thanh cong.");
					loadCategory();
				}
				else
				{
					MessageBox.Show("The loai da ton tai khong the.");
				}
			}
		}
		public void deleteCategory()
		{
			using (var db = new NETFLIX_DBEntities())
			{
				if (db.MOVIEs.Where(x => x.CATEGORY_ID == SelectedCategory.ID).ToList().Count() == 0) {
					var category = db.CATEGORies.Where(x => x.ID == SelectedCategory.ID).Single();
					db.CATEGORies.Remove(category);
					db.SaveChanges();
					MessageBox.Show("Xoa thanh cong.");
					loadCategory();
				}
				else
				{
					MessageBox.Show("The loai con nhieu movie, khong the xoa.");
				}
			}
		}
		public void addMovie(Window window)
		{
			String exePath = System.Environment.GetCommandLineArgs()[0];
			string[] exePaths = exePath.Split('\\');

			string path = exePaths[0];
			for (int i = 1; i < exePaths.Length - 5; i++)
				path += "\\" + exePaths[i];
			using (var db = new NETFLIX_DBEntities())
			{
				string trailerName = (Trailer != null) ? Trailer.Split('\\').Last() : "";
				string movieName = (Movie != null) ? Movie.Split('\\').Last() : "";
				string postername = (Poster != null) ? Poster.Split('\\').Last() : "";
				MOVIE_INFORMATION InformationMovie = new MOVIE_INFORMATION()
				{
					DESCRIPTION = Description.Trim(),
					RATE = Rate,
					DISTRIBUTE_YEAR = Year,
					COUNTRY = Country,
				};
				var movie = new MOVIE()
				{
					NAME = NameMovie.Trim(),
					POSTER = postername.Trim(),
					MOVIE_NAME = (movieName.Trim().Length > 0) ? movieName.Trim() : "",
					TRAILER_NAME = (trailerName.Trim().Length > 0) ? trailerName.Trim() : "",
					INFORMATION = InformationMovie.ID,
					CATEGORY_ID = Categori.ID,
					TYPE_ID = Type.ID,
					ACTIVE = 1,
				};
				db.MOVIE_INFORMATION.Add(InformationMovie);
				db.MOVIEs.Add(movie);
				db.SaveChanges();
				if (postername != "")
					System.IO.File.Copy(Poster, path + "\\Movie\\Poster\\" + movie.POSTER, true);
				if (movieName != "")
					System.IO.File.Copy(Movie, path + "\\Movie\\" + movie.MOVIE_NAME, true);
				if (trailerName != "")
					System.IO.File.Copy(Trailer, path + "\\Movie\\" + movie.TRAILER_NAME, true);
				MessageBox.Show("them thanh cong");
				loadCategory();
			}
		}
			public void DeleteMovie()
			{
				String exePath = System.Environment.GetCommandLineArgs()[0];
				string[] exePaths = exePath.Split('\\');
				string path = exePaths[0];
				for (int i = 1; i < exePaths.Length - 5; i++)
					path += "\\" + exePaths[i];
				using (var db = new NETFLIX_DBEntities())
				{
					var movie = db.MOVIEs.Where(x => x.ID == SelectedMovie.ID).Single();
					var movie_information = db.MOVIE_INFORMATION.Where(x => x.ID == movie.INFORMATION).Single();
					db.MOVIE_INFORMATION.Remove(movie_information);
					db.MOVIEs.Remove(movie);
					db.SaveChanges();
					MessageBox.Show("Xoa movie thanh cong");
					loadCategory();
					if (System.IO.File.Exists(path + "\\Movie\\Poster\\" + movie.POSTER))
					{
						// Use a try block to catch IOExceptions, to
						// handle the case of the file already being
						// opened by another process.
						try
						{
							System.IO.File.Delete(path + "\\Movie\\Poster\\" + movie.POSTER);
						}
						catch (System.IO.IOException e)
						{
							Console.WriteLine(e.Message);
							return;
						}
					}

					if (System.IO.File.Exists(path + "\\Movie\\" + movie.TRAILER_NAME))
					{
						// Use a try block to catch IOExceptions, to
						// handle the case of the file already being
						// opened by another process.
						try
						{
							System.IO.File.Delete(path + "\\Movie\\" + movie.TRAILER_NAME);
						}
						catch (System.IO.IOException e)
						{
							Console.WriteLine(e.Message);
							return;
						}
					}
					if (System.IO.File.Exists(path + "\\Movie\\" + movie.MOVIE_NAME))
					{
						// Use a try block to catch IOExceptions, to
						// handle the case of the file already being
						// opened by another process.
						try
						{
							System.IO.File.Delete(path + "\\Movie\\" + movie.MOVIE_NAME);
						}
						catch (System.IO.IOException e)
						{
							Console.WriteLine(e.Message);
							return;
						}
					}
				}
			}
			void loadCategory()
			{
				using (var db = new NETFLIX_DBEntities())
				{
					DSCategory = new ObservableCollection<CATEGORY>(db.CATEGORies.Include("MOVIEs").ToList());
				}
			}
		}
	}


