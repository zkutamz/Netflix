using Project_Netflix.Command.Admin.Movie;
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

namespace Project_Netflix.viewmodel
{
	public class AdminMovie : DependencyObject
	{
		public static readonly DependencyProperty AdminMovieProperty;
		public static readonly DependencyProperty SelectMovieProperty;
		public static readonly DependencyProperty YearProperty;
		public static readonly DependencyProperty CountryProperty;
		public static readonly DependencyProperty CategoryProperty;
		public static readonly DependencyProperty TypeProperty;

		public ICommand CmdUpdateMovie { get; set; }
		public ICommand CmdDeleteMovie { get; set; }
		static AdminMovie()
		{
			AdminMovieProperty = DependencyProperty.Register("DSMovie", typeof(ObservableCollection<MOVIE>), typeof(AdminMovie));
			SelectMovieProperty = DependencyProperty.Register("SelectedMovie", typeof(MOVIE), typeof(AdminMovie));
			YearProperty = DependencyProperty.Register("DSYear", typeof(ObservableCollection<int>), typeof(AdminMovie));
			CountryProperty = DependencyProperty.Register("DSCountry", typeof(ObservableCollection<string>), typeof(AdminMovie));
			CategoryProperty = DependencyProperty.Register("DSCategory", typeof(ObservableCollection<CATEGORY>), typeof(AdminMovie));
			TypeProperty = DependencyProperty.Register("DSType", typeof(ObservableCollection<MOVIE_TYPE>), typeof(AdminMovie));
		}
		public ObservableCollection<MOVIE> DSMovie
		{
			get => (ObservableCollection<MOVIE>)GetValue(AdminMovieProperty);
			set => SetValue(AdminMovieProperty, value);
		}
		public ObservableCollection<CATEGORY> DSCategory
		{
			get => (ObservableCollection<CATEGORY>)GetValue(CategoryProperty);
			set => SetValue(CategoryProperty, value);
		}
		public ObservableCollection<MOVIE_TYPE> DSType
		{
			get => (ObservableCollection<MOVIE_TYPE>)GetValue(TypeProperty);
			set => SetValue(TypeProperty, value);
		}
		public MOVIE SelectedMovie
		{
			get => (MOVIE)GetValue(SelectMovieProperty);
			set { SetValue(SelectMovieProperty, value); }
		}
		public ObservableCollection<int> DSYear { get => (ObservableCollection<int>)GetValue(YearProperty); set { SetValue(YearProperty,value); } }
		public ObservableCollection<string> DSCountry { get => (ObservableCollection<string>)GetValue(CountryProperty); set { SetValue(CountryProperty,value); } }
		public AdminMovie()
		{
			CmdUpdateMovie = new CmdUpdateMovie(this);
			CmdDeleteMovie = new CmdDeleteMovie(this);
			using (var db = new NETFLIX_DBEntities())
			{
				DSMovie = new ObservableCollection<MOVIE>(db.MOVIEs.Include("MOVIE_INFORMATION").ToList());
				DSCategory = new ObservableCollection<CATEGORY>(db.CATEGORies.ToList());
				DSType = new ObservableCollection<MOVIE_TYPE>(db.MOVIE_TYPE.ToList());
			}
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
		public void UpdateMovie(Window window)
		{
			String exePath = System.Environment.GetCommandLineArgs()[0];
			string[] exePaths = exePath.Split('\\');
			string path = exePaths[0];
			for (int i = 1; i < exePaths.Length - 5; i++)
				path += "\\" + exePaths[i];
			bool isChangePoster = false;
			bool isChangeMovie = false;
			bool isChangeTrailer = false;
			using (var db = new NETFLIX_DBEntities())
			{
				var movie = db.MOVIEs.Where(x => x.ID == SelectedMovie.ID).Single();
				movie.NAME = SelectedMovie.NAME;
				movie.MOVIE_INFORMATION.DESCRIPTION = SelectedMovie.MOVIE_INFORMATION.DESCRIPTION;
				movie.MOVIE_INFORMATION.COUNTRY = SelectedMovie.MOVIE_INFORMATION.COUNTRY;
				movie.MOVIE_INFORMATION.DISTRIBUTE_YEAR = SelectedMovie.MOVIE_INFORMATION.DISTRIBUTE_YEAR;
				movie.MOVIE_INFORMATION.RATE = SelectedMovie.MOVIE_INFORMATION.RATE;
				movie.CATEGORY_ID = SelectedMovie.CATEGORY.ID;
				movie.TYPE_ID = SelectedMovie.MOVIE_TYPE.ID;

				if (movie.MOVIE_NAME != SelectedMovie.MOVIE_NAME)
				{
					movie.MOVIE_NAME = SelectedMovie.MOVIE_NAME;
					isChangeMovie = true;
				}
				if (movie.POSTER != SelectedMovie.POSTER)
				{
					movie.POSTER = SelectedMovie.POSTER;
					isChangePoster = true;
				}
				if (movie.TRAILER_NAME != SelectedMovie.TRAILER_NAME)
				{
					movie.TRAILER_NAME = SelectedMovie.TRAILER_NAME;
					isChangeTrailer = true;
				}
				db.SaveChanges();
				if (isChangePoster)
				{
					if (System.IO.File.Exists(path + "\\Movie\\Poster" + movie.POSTER))
					{
						// Use a try block to catch IOExceptions, to
						// handle the case of the file already being
						// opened by another process.
						try
						{
							System.IO.File.Delete(path + "\\Movie\\" + movie.POSTER);
							System.IO.File.Copy(SelectedMovie.POSTER, path + "\\Movie\\Poster\\" + movie.POSTER, true);
						}
						catch (System.IO.IOException e)
						{
							Console.WriteLine(e.Message);
							return;
						}
					}
				}
				if (isChangeMovie)
				{
					if (System.IO.File.Exists(path + "\\Movie\\" + movie.MOVIE_NAME))
					{
						// Use a try block to catch IOExceptions, to
						// handle the case of the file already being
						// opened by another process.
						try
						{
							System.IO.File.Delete(path + "\\Movie\\" + movie.MOVIE_NAME);
							System.IO.File.Copy(SelectedMovie.MOVIE_NAME, path + "\\Movie\\" + movie.MOVIE_NAME, true);
						}
						catch (System.IO.IOException e)
						{
							Console.WriteLine(e.Message);
							return;
						}
					}
				}
				if (isChangeTrailer)
				{
					if (System.IO.File.Exists(path + "\\Movie\\" + movie.TRAILER_NAME))
					{
						// Use a try block to catch IOExceptions, to
						// handle the case of the file already being
						// opened by another process.
						try
						{
							System.IO.File.Delete(path + "\\Movie\\" + movie.TRAILER_NAME);
							System.IO.File.Copy(SelectedMovie.POSTER, path + "\\Movie\\" + movie.TRAILER_NAME, true);
						}
						catch (System.IO.IOException e)
						{
							Console.WriteLine(e.Message);
							return;
						}
					}
				}
			}
			window.Close();
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
		void loadMovie()
		{
			using (var db = new NETFLIX_DBEntities())
			{
				DSMovie = new ObservableCollection<MOVIE>(db.MOVIEs.Include("MOVIE_INFORMATION").ToList());
			}
		}
	}
}


