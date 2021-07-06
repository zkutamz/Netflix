using Project_Netflix.Command.Admin.Movie;
using Project_Netflix.model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Project_Netflix.viewmodel
{
	public class AdminMovie : BaseViewModel
	{
		private ObservableCollection<MOVIE> _DSMovie;
		public ObservableCollection<MOVIE> DSMovie { get => _DSMovie; set { _DSMovie = value; OnPropertyChanged(); } }
		private MOVIE _SelectedMovie;
		public MOVIE SelectedMovie { 
			get => _SelectedMovie; 
			set { _SelectedMovie = value; OnPropertyChanged(); 
				if (SelectedMovie != null) { Name = SelectedMovie.NAME; Poster = SelectedMovie.POSTER; Movie = SelectedMovie.MOVIE_NAME; Trailer = SelectedMovie.TRAILER_NAME; Description = SelectedMovie.MOVIE_INFORMATION.DESCRIPTION; Country = SelectedMovie.MOVIE_INFORMATION.COUNTRY.Trim() ; Category = SelectedMovie.CATEGORY; Type = SelectedMovie.MOVIE_TYPE; Year = (int)SelectedMovie.MOVIE_INFORMATION.DISTRIBUTE_YEAR; Rate = (double)SelectedMovie.MOVIE_INFORMATION.RATE; View = SelectedMovie.VIEWS; } } }
		private ObservableCollection<int> _DSYear;
		public ObservableCollection<int> DSYear { get => _DSYear; set { _DSYear = value; OnPropertyChanged(); } }
		private ObservableCollection<string> _DSCountry;
		public ObservableCollection<string> DSCountry { get => _DSCountry; set { _DSCountry = value; OnPropertyChanged(); } }
		private ObservableCollection<CATEGORY> _DSCategory;
		public ObservableCollection<CATEGORY> DSCategory { get => _DSCategory; set { _DSCategory = value; OnPropertyChanged(); } }
		private ObservableCollection<MOVIE_TYPE> _DSType;
		public ObservableCollection<MOVIE_TYPE> DSType { get => _DSType; set { _DSType = value; OnPropertyChanged(); } }
		public ICommand CmdUpdateMovie { get; set; }
		public ICommand CmdDeleteMovie { get; set; }
		public ICommand CmdGetAPIUpdate { get; set; }
		public ICommand CmdShowInformationMovie { get; set; }
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
		private CATEGORY _Category;
		public CATEGORY Category { get => _Category; set { _Category = value; OnPropertyChanged(); } }
		private MOVIE_TYPE _Type;
		public MOVIE_TYPE Type { get => _Type; set { _Type = value; OnPropertyChanged(); } }
		private double _Rate;
		public double Rate { get => _Rate; set { _Rate = value; OnPropertyChanged(); } }
		private int _Year;
		public int Year { get => _Year; set { _Year = value; OnPropertyChanged(); } }
		private long _View;
		public long View { get => _View; set { _View = value; OnPropertyChanged(); } }
		public AdminMovie()
		{
			CmdUpdateMovie = new CmdUpdateMovie(this);
			CmdDeleteMovie = new CmdDeleteMovie(this);
			CmdShowInformationMovie = new CmdInformationMovie(this);
			CmdGetAPIUpdate = new CmdGetAPIUpdate(this);

			loadMovie();
			string[] Countries = null;
			using (var sr = new StreamReader(@"..\..\viewmodel\Admin\Movie\Country.txt"))
			{
				string listCountry = sr.ReadToEnd();
				Countries = listCountry.Split('\n');	
			}
			var countries = from n in Countries orderby n select n;
			DSCountry = new ObservableCollection<string>(countries);
			int now = DateTime.Now.Year;
			List<int> years = new List<int>();
			for (int i = now + 10; i >= 1900; i--)
			{
				years.Add(i);
			}
			DSYear = new ObservableCollection<int>(years);
		}

		public void UpdateMovie(Window window)
		{
			String exePath = System.Environment.GetCommandLineArgs()[0];
			string[] exePaths = exePath.Split('\\');
			string path = exePaths[0];
			for (int i = 1; i < exePaths.Length - 5; i++)
				path += "\\" + exePaths[i];
			try
			{
				bool isChangePoster = false;
				bool isChangeMovie = false;
				bool isChangeTrailer = false;
				string posterOld = "";
				string trailerOld = "";
				string movieOld = "";
				using (var db = new NETFLIX_DBEntities())
				{
					var movie = db.MOVIEs.Where(x => x.ID == SelectedMovie.ID).Single();
					movie.NAME = SelectedMovie.NAME.Trim();
					movie.MOVIE_INFORMATION.DESCRIPTION = Description.Trim();
					movie.MOVIE_INFORMATION.COUNTRY = Country.Trim();
					movie.MOVIE_INFORMATION.DISTRIBUTE_YEAR = Year;
					movie.MOVIE_INFORMATION.RATE = Rate;
					movie.CATEGORY_ID = Category.ID;
					movie.TYPE_ID = Type.ID;
					if (movie.MOVIE_NAME != Movie)
					{
						movieOld = movie.MOVIE_NAME;
						movie.MOVIE_NAME = Movie.Split('\\').Last();
						isChangeMovie = true;
					}
					if (movie.POSTER != Poster)
					{
						posterOld = movie.POSTER;
						movie.POSTER = Poster.Split('\\').Last();
						isChangePoster = true;
					}
					if (movie.TRAILER_NAME != Trailer)
					{
						trailerOld = movie.TRAILER_NAME;
						movie.TRAILER_NAME = Trailer.Split('\\').Last();
						isChangeTrailer = true;
					}
					db.SaveChanges();

					if (isChangePoster)
					{
						try
						{
							System.IO.File.Delete(path + "\\Movie\\Poster\\" + posterOld);
							System.IO.File.Copy(Poster, path + "\\Movie\\Poster\\" + movie.POSTER, true);
						}
						catch (System.IO.IOException e)
						{
							Console.WriteLine(e.Message);
						}
					}
					if (isChangeMovie)
					{
						try
						{

							System.IO.File.Delete(path + "\\Movie\\" + movieOld);
							System.IO.File.Copy(Movie, path + "\\Movie\\" + movie.MOVIE_NAME, true);
						}
						catch (System.IO.IOException e)
						{
							Console.WriteLine(e.Message);
						}
					}
					if (isChangeTrailer)
					{
						try
						{
							System.IO.File.Delete(path + "\\Movie\\" + trailerOld);
							System.IO.File.Copy(Trailer, path + "\\Movie\\" + movie.TRAILER_NAME, true);
						}
						catch (System.IO.IOException e)
						{
							Console.WriteLine(e.Message);
						}
					}
				}
				loadMovie();
				window.Close();
			}
			catch (Exception e)
			{
				MessageBox.Show("Sua that bai");
			}
		}
		public void DeleteMovie()
		{
			String exePath = System.Environment.GetCommandLineArgs()[0];
			string[] exePaths = exePath.Split('\\');
			string path = exePaths[0];
			for (int i = 1; i < exePaths.Length - 5; i++)
				path += "\\" + exePaths[i];
			using(var db = new NETFLIX_DBEntities())
			{
				var movie = db.MOVIEs.Where(x => x.ID == SelectedMovie.ID).Single();
				var movie_information = db.MOVIE_INFORMATION.Where(x => x.ID == movie.INFORMATION).Single();
				db.MOVIE_INFORMATION.Remove(movie_information);
				db.MOVIEs.Remove(movie);
				db.SaveChanges();
				MessageBox.Show("Xoa movie thanh cong");
				loadMovie();
				if (System.IO.File.Exists(path + "\\Movie\\Poster\\" + movie.POSTER))
				{
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
		public void loadMovie()
		{
			using (var db = new NETFLIX_DBEntities())
			{
				DSMovie = new ObservableCollection<MOVIE>(db.MOVIEs.Include("MOVIE_INFORMATION").Include("CATEGORY").Include("MOVIE_TYPE").ToList());
				DSCategory = new ObservableCollection<CATEGORY>(db.CATEGORies.ToList());
				DSType = new ObservableCollection<MOVIE_TYPE>(db.MOVIE_TYPE.ToList());
			}
		}
		public string StringChangeURLEncode(string myString)
		{
			myString = myString.Replace(' ', '-');
			myString = myString.Replace('?', '-');
			myString = myString.Replace('.', '-');
			myString = myString.Replace(',', '-');
			myString = myString.Replace('!', '-');
			return myString;
		}
		public string getAPIByName(string Name)
		{
			if (Name.Length > 0)
			{
				Name = StringChangeURLEncode(Name);
				string path = "https://movie-database-imdb-alternative.p.rapidapi.com/?s=" + Name + "&page=1&r=json";
				var client = new RestClient(path);
				var request = new RestRequest(Method.GET);
				request.AddHeader("x-rapidapi-key", "d788b074d0msh06f1c060333e3cbp1eb3e9jsncdf4c50c28fe");
				request.AddHeader("x-rapidapi-host", "movie-database-imdb-alternative.p.rapidapi.com");
				IRestResponse response = client.Execute(request);

				string[] lst = response.Content.Substring(1, response.Content.Length - 2).Split(',');
				foreach (var item in lst)
				{
					if (item.Contains("imdbID"))
					{
						string[] tokkens = item.Split(':');
						return tokkens[1].Substring(1, tokkens[1].Length - 2);
					}
				}
				MessageBox.Show("Khong tim thay phim");
				return "";
			}
			MessageBox.Show("Hay nhap ten phim");
			return "";
		}
		public List<string> getAPIByID(string ID)
		{
			List<string> listProperty = new List<string>();
			if (ID.Length > 0)
			{
				var client = new RestClient("https://movie-database-imdb-alternative.p.rapidapi.com/?i=" + ID + "&r=json");
				var request = new RestRequest(Method.GET);
				request.AddHeader("x-rapidapi-key", "d788b074d0msh06f1c060333e3cbp1eb3e9jsncdf4c50c28fe");
				request.AddHeader("x-rapidapi-host", "movie-database-imdb-alternative.p.rapidapi.com");
				IRestResponse response = client.Execute(request);
				string content = response.Content;
				string[] condition = { "\",", };
				string[] lst = content.Substring(1, content.Length - 2).Split(condition, System.StringSplitOptions.RemoveEmptyEntries);
				for (int i = 0; i < lst.Length; i++)
				{
					lst[i] = lst[i].Replace('"', ' ');
					string[] tokkens = lst[i].Split(':');

					if (tokkens[0].Trim() == "Title")
					{
						string title = "";
						for (int j = 1; j < tokkens.Length; j++)
						{
							title += " " + tokkens[j].Trim();
						}
						listProperty.Add(title.Trim());
					}
					else if (tokkens[0].Contains("Released"))
					{
						listProperty.Add(tokkens[1]);
					}
					else if (tokkens[0].Contains("Runtime"))
					{
						listProperty.Add(tokkens[1]);
					}
					else if (tokkens[0].Contains("Plot"))
					{
						string plot = "";
						for (int j = 1; j < tokkens.Length; j++)
						{
							plot += " " + tokkens[j].Trim();
						}

						listProperty.Add(plot.Trim());
					}
					else if (tokkens[0].Contains("Country"))
					{
						string[] countrys = tokkens[1].Split(',');
						listProperty.Add(countrys[0].Trim());
					}
					else if (tokkens[0].Contains("Poster"))
					{
						listProperty.Add(tokkens[1]);
					}
					else if (tokkens[0].Contains("imdbRating"))
					{
						listProperty.Add(tokkens[1]);
					}
					else if (tokkens[0].Contains("imdbVotes"))
					{
						string vote = "";
						string[] votes = tokkens[1].Split(',');
						for (int j = 0; j < votes.Length; j++)
						{
							vote += votes[j];
						}
						listProperty.Add(vote);
					}
					else
					{
						continue;
					}
				}
			}
			return listProperty;
		}

		public void GetAPI()
		{
			List<string> listProperties = new List<string>();

			listProperties = getAPIByID(getAPIByName(Name));
			//0:title, 1:year, 2:time, 3:desc, 4:country, 5:poster, 6:rate, 7: view,

			Name = listProperties[0];
			Year = DateTime.Parse(listProperties[1]).Year;
			Description = (listProperties[3].Length <= 200) ? listProperties[3] : listProperties[3].Substring(0, 197) + "...";
			Country = listProperties[4];
			Rate = double.Parse(listProperties[6]);
			View = Int64.Parse(listProperties[7]);
		}
	}
}


