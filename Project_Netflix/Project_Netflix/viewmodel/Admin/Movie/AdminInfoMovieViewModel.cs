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
using RestSharp;

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
		private CATEGORY _Category;
		public CATEGORY Category { get => _Category; set { _Category = value; OnPropertyChanged(); } }
		private MOVIE_TYPE _Type;
		public MOVIE_TYPE Type { get => _Type; set { _Type = value; OnPropertyChanged(); } }
		private double _Rate;
		public double Rate { get => _Rate; set { _Rate = value; OnPropertyChanged(); } }
		private int _Year;
		public int Year { get => _Year; set { _Year = value; OnPropertyChanged(); } }
		private Int64 _View;
		public Int64 View { get => _View; set { _View = value; OnPropertyChanged(); } }
		

		public ICommand CmdAddMovie { get; set; }
		public ICommand CmdGetAPI { get; set; }
		private ObservableCollection<int> _DSYear;
		public ObservableCollection<int> DSYear { get => _DSYear; set { _DSYear = value; OnPropertyChanged(); } }
		private ObservableCollection<string> _DSCountry;
		public ObservableCollection<string> DSCountry { get => _DSCountry; set { _DSCountry = value; OnPropertyChanged(); } }
		private ObservableCollection<CATEGORY> _DSCategory;
		public ObservableCollection<CATEGORY> DSCategory { get => _DSCategory; set { _DSCategory = value; OnPropertyChanged(); } }
		private ObservableCollection<MOVIE_TYPE> _DSType;
		public ObservableCollection<MOVIE_TYPE> DSType { get => _DSType; set { _DSType = value; OnPropertyChanged(); } }


		public AdminInfoMovieViewModel()
		{
			CmdGetAPI = new CmdGetAPI(this);
			CmdAddMovie = new CmdAddMovie(this);
			View = 0;
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
				DSCategory = new ObservableCollection<CATEGORY>(db.CATEGORies.ToList());
				DSType = new ObservableCollection<MOVIE_TYPE>(db.MOVIE_TYPE.ToList());

			}
		}

		public void addMovie(Window window)
		{
			try {
				String exePath = System.Environment.GetCommandLineArgs()[0];
				string[] exePaths = exePath.Split('\\');

				string path = exePaths[0];
				for (int i = 1; i < exePaths.Length - 5; i++)
					path += "\\" + exePaths[i];

				using (var db = new NETFLIX_DBEntities())
				{
					if (db.MOVIEs.Where(x => x.NAME == Name.Trim()).ToList().Count() == 0 && db.MOVIEs.Where(x => x.POSTER == Poster.Trim()).ToList().Count() == 0) {
					
						string trailerName = (Trailer != null) ? Trailer.Split('\\').Last() : "";
						string movieName = (Movie != null) ? Movie.Split('\\').Last() : "";
						string postername = (Poster != null) ? Poster.Split('\\').Last() : "";
						MOVIE_INFORMATION InformationMovie = new MOVIE_INFORMATION()
						{
							DESCRIPTION = Description.Trim(),
							RATE = Rate,
							DISTRIBUTE_YEAR = Year,
							COUNTRY = Country.Trim(),
						};
						var movie = new MOVIE()
						{
							NAME = Name.Trim(),
							POSTER = postername.Trim(),
							MOVIE_NAME = (movieName.Trim().Length > 0) ? movieName.Trim() : "",
							TRAILER_NAME = (trailerName.Trim().Length > 0) ? trailerName.Trim() : "",
							INFORMATION = InformationMovie.ID,
							CATEGORY_ID = Category.ID,
							TYPE_ID = Type.ID,
							ACTIVE = 1,
							VIEWS = (int)View,
						};
						db.MOVIE_INFORMATION.Add(InformationMovie);
						db.MOVIEs.Add(movie);
						AdminMovie vm = new AdminMovie();
						vm.loadMovie();
						db.SaveChanges();
						if (postername != "")
							System.IO.File.Copy(Poster, path + "\\Movie\\Poster\\" + movie.POSTER, true);
						if (movieName != "")
							System.IO.File.Copy(Movie, path + "\\Movie\\" + movie.MOVIE_NAME, true);
						if (trailerName != "")
							System.IO.File.Copy(Trailer, path + "\\Movie\\" + movie.TRAILER_NAME, true);
						MessageBox.Show("them thanh cong");
					}
					else
					{

						MessageBox.Show("Phim da ton tai");
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				MessageBox.Show("Khong the them Movie");
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
			try { 
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
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				MessageBox.Show("Connection fail");
				return null;
			}
		}
		public List<string> getAPIByID(string ID)
		{
			try { 
			List<string> listProperty = new List<string>();
			if (ID.Length > 0)
			{
				var client = new RestClient("https://movie-database-imdb-alternative.p.rapidapi.com/?i=" + ID + "&r=json");
				var request = new RestRequest(Method.GET);
				request.AddHeader("x-rapidapi-key", "d788b074d0msh06f1c060333e3cbp1eb3e9jsncdf4c50c28fe");
				request.AddHeader("x-rapidapi-host", "movie-database-imdb-alternative.p.rapidapi.com");
				IRestResponse response = client.Execute(request);
				string content = response.Content;
				string[] condition = { "\",",};
				string[] lst = content.Substring(1,content.Length-2).Split(condition, System.StringSplitOptions.RemoveEmptyEntries);
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
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				MessageBox.Show("Connection fail");
				return null;
			}
		}
		public void GetAPI() {
			
			List<string> listProperties = new List<string>();
			
			listProperties = getAPIByID(getAPIByName(Name));
			//0:title, 1:year, 2:time, 3:desc, 4:country, 5:poster, 6:rate, 7: view,
			if (listProperties.Count > 0)
			{
				try
				{
					Name = listProperties[0];
					Year = DateTime.Parse(listProperties[1]).Year;
					Description = (listProperties[3].Length <= 200) ? listProperties[3] : listProperties[3].Substring(0, 197) + "...";
					Country = listProperties[4];
					Rate = double.Parse(listProperties[6]);
					View = Int64.Parse(listProperties[7]);
				}
				catch (Exception e)
				{

					Console.WriteLine(e.Message);

					MessageBox.Show("Lay phim that bai");
				}
			}
		}
	}
}
