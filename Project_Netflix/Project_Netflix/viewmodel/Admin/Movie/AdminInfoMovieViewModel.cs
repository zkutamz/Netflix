using Project_Netflix.Command.Admin.Movie;
using Project_Netflix.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Controls;

namespace Project_Netflix.viewmodel.Admin.Movie
{
	public class AdminInfoMovieViewModel : BaseViewModel
	{
		private string _Name;
		public string Name { get => _Name; set { _Name = value; OnPropertyChanged(); } }
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
		private int _Year;
		public int Year { get => _Year; set { _Year = value; OnPropertyChanged(); } }
		private ObservableCollection<ItemCollection> _Category;
		public ObservableCollection<ItemCollection> Category { get => _Category; set { _Category = value; OnPropertyChanged(); } }
		public ICommand CmdAddMovie { get; set; }
		private ObservableCollection<int> _DSYear;
		public ObservableCollection<int> DSYear { get => _DSYear; set { _DSYear = value; OnPropertyChanged(); } }
		private ObservableCollection<string> _DSCountry;
		public ObservableCollection<string> DSCountry { get => _DSCountry; set { _DSCountry = value; OnPropertyChanged(); } }

		public AdminInfoMovieViewModel()
		{
			CmdAddMovie = new CmdAddMovie(this);
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
					RATE = 0,
					DISTRIBUTE_YEAR = Year,
					COUNTRY = Country,
				};
				var movie = new MOVIE()
				{
					NAME = Name.Trim(),
					POSTER = postername.Trim(),
					MOVIE1 = (movieName.Trim().Length > 0) ? movieName.Trim() : "",
					TRAILER = (trailerName.Trim().Length > 0) ? trailerName.Trim() : "",
					INFORMATION = InformationMovie.ID,
					ACTIVE = 1,
				};
				db.MOVIE_INFORMATION.Add(InformationMovie);
				db.MOVIEs.Add(movie);
				db.SaveChanges();
				if (postername != "")
					System.IO.File.Copy(Poster, path + "\\Movie\\Poster\\" + movie.POSTER, true);
				if (movieName != "")
					System.IO.File.Copy(Movie, path + "\\Movie\\" + movie.MOVIE1, true);
				if (trailerName != "")
					System.IO.File.Copy(Trailer, path + "\\Movie\\" + movie.TRAILER, true);
				MessageBox.Show("them thanh cong");
				MessageBox.Show(movieName);
			}
		}
	}
}
