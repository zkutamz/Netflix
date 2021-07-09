using Project_Netflix.Command.Admin.Category;
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
		//private ObservableCollection<MOVIE> _DSMovie;
		//public ObservableCollection<MOVIE> DSMovie { get => _DSMovie; set { _DSMovie = value; OnPropertyChanged(); } }
		private CATEGORY _SelectedCategory;
		public CATEGORY SelectedCategory
		{
			get => _SelectedCategory; set
			{
				_SelectedCategory = value; OnPropertyChanged(); if (SelectedCategory != null)
				{
					Name = SelectedCategory.NAME;
					using (var db = new NETFLIX_DBEntities())
					{
						Categorys = new ObservableCollection<CATEGORY>(db.CATEGORies.Where(x => x.ID == SelectedCategory.ID).ToList());
					}
				}
			}
		}
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
		private Int64 _View;
		public Int64 View { get => _View; set { _View = value; OnPropertyChanged(); } }
		public ICommand CmdAddMovie { get; set; }
		public ICommand CmdGetAPI { get; set; }
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
			CmdGetAPI = new CmdGetAPI(this);
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
				//DSMovie = new ObservableCollection<MOVIE>(db.MOVIE_INFORMATIONMOVIEs.Include("MOVIE_INFORMATION").Include("CATEGORY").Include("MOVIE_TYPE").Where(x=>x.CATEGORY_ID==SelectedCategory.ID).ToList());
			}
		}
		public void addCategory()
		{
			using (var db = new NETFLIX_DBEntities())
			{
				if (db.CATEGORies.Where(x => x.NAME == Name).ToList().Count() == 0)
				{
					try
					{
						var category = new CATEGORY() { NAME = Name };
						db.CATEGORies.Add(category);
						db.SaveChanges();
						MessageBox.Show("Them thanh cong.");
						loadCategory();
					}
					catch (Exception e)
					{
						Console.WriteLine(e.Message);
						MessageBox.Show("Them that bai.");
					}
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
					try
					{
						db.CATEGORies.Where(x => x.ID == SelectedCategory.ID).Single().NAME = Name;
						db.SaveChanges();
						MessageBox.Show("Sua thanh cong.");
						loadCategory();
					}
					catch (Exception e)
					{
						Console.WriteLine(e.Message);
						MessageBox.Show("Sua that bai.");
					}
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
				if (db.MOVIEs.Where(x => x.CATEGORY_ID == SelectedCategory.ID).ToList().Count() == 0)
				{
					try
					{
						var category = db.CATEGORies.Where(x => x.ID == SelectedCategory.ID).Single();
						db.CATEGORies.Remove(category);
						db.SaveChanges();
						MessageBox.Show("Xoa thanh cong.");
						loadCategory();
					}
					catch (Exception e)
					{
						Console.WriteLine(e.Message);
						MessageBox.Show("Xoa that bai.");
					}
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
				if (db.MOVIEs.Where(x => x.NAME == NameMovie.Trim()).ToList().Count() == 0 && db.MOVIEs.Where(x => x.POSTER == Poster.Trim()).ToList().Count() == 0)
				{
					try
					{
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
						window.Close();
					}
					catch (Exception e)
					{
						Console.WriteLine(e.Message);
						MessageBox.Show("Them that bai.");
					}
				}
				else
				{
					MessageBox.Show("Khong the them phim.");
				}
				loadCategory();
			}
		}
		public void DeleteMovie()
		{
			try
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
					string posterOld = movie.POSTER;
					string trailerOld = movie.TRAILER_NAME;
					string movieOld = movie.MOVIE_NAME;
					if (db.FAVOURITE_MOVIES.Where(x => x.MOVIE_ID == movie.ID).ToList().Count() > 0)
					{
						MessageBox.Show("Movie dang duoc user yeu thich khong the xoa.");
					}
					else
					{
						db.MOVIE_INFORMATION.Remove(movie_information);
						db.MOVIEs.Remove(movie);
						db.SaveChanges();
						if (System.IO.File.Exists(path + "\\Movie\\Poster\\" + posterOld))
						{
							System.IO.File.Delete(path + "\\Movie\\Poster\\" + posterOld);
						}
						if (System.IO.File.Exists(path + "\\Movie\\" + trailerOld))
						{
							System.IO.File.Delete(path + "\\Movie\\" + trailerOld);
						}
						if (System.IO.File.Exists(path + "\\Movie\\" + movieOld))
						{
							System.IO.File.Delete(path + "\\Movie\\" + movieOld);
						}
						MessageBox.Show("Xoa movie thanh cong");
					}
					loadCategory();
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				MessageBox.Show("Xoa movie that bai.");
			}
		}
		void loadCategory()
		{
			using (var db = new NETFLIX_DBEntities())
			{
				DSCategory = new ObservableCollection<CATEGORY>(db.CATEGORies.Include("MOVIEs").ToList());
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
			try
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
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				MessageBox.Show("Connection Fail.");
				return null;
			}
		}
		public List<string> getAPIByID(string ID)
		{
			try
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
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				MessageBox.Show("Connection Fail.");
				return null;
			}
		}
		public void GetAPI()
		{
			try
			{
				List<string> listProperties = new List<string>();

				listProperties = getAPIByID(getAPIByName(NameMovie));
				//0:title, 1:year, 2:time, 3:desc, 4:country, 5:poster, 6:rate, 7: view,
				if (listProperties.Count > 0)
				{
					NameMovie = listProperties[0];
					Year = DateTime.Parse(listProperties[1]).Year;
					Description = (listProperties[3].Length <= 200) ? listProperties[3] : listProperties[3].Substring(0, 197) + "...";
					Country = listProperties[4];
					Rate = double.Parse(listProperties[6]);
					View = Int64.Parse(listProperties[7]);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				MessageBox.Show("Connection Fail.");
				return;
			}
		}
	}
}


