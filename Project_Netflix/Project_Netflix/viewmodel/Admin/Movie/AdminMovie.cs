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


		public ICommand CmdUpdateMovie { get; set; }
		public ICommand CmdDeleteMovie { get; set; }
		static AdminMovie()
		{
			AdminMovieProperty = DependencyProperty.Register("DSMovie", typeof(ObservableCollection<MOVIE>), typeof(AdminMovie));
			SelectMovieProperty = DependencyProperty.Register("SelectedMovie", typeof(MOVIE), typeof(AdminMovie));
			YearProperty = DependencyProperty.Register("DSYear", typeof(ObservableCollection<int>), typeof(AdminMovie));
			CountryProperty = DependencyProperty.Register("DSCountry", typeof(ObservableCollection<string>), typeof(AdminMovie));
		}
		public ObservableCollection<MOVIE> DSMovie
		{
			get => (ObservableCollection<MOVIE>)GetValue(AdminMovieProperty);
			set => SetValue(AdminMovieProperty, value);
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
				if(movie.MOVIE1 != SelectedMovie.MOVIE1)
				{
					movie.MOVIE1 = SelectedMovie.MOVIE1;
					isChangeMovie = true;
				}
				if(movie.POSTER != SelectedMovie.POSTER)
				{
					movie.POSTER = SelectedMovie.POSTER;
					isChangePoster = true;
				}
				if(movie.TRAILER != SelectedMovie.TRAILER)
				{
					movie.TRAILER = SelectedMovie.TRAILER;
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
					if (System.IO.File.Exists(path + "\\Movie\\" + movie.MOVIE1))
					{
						// Use a try block to catch IOExceptions, to
						// handle the case of the file already being
						// opened by another process.
						try
						{
							System.IO.File.Delete(path + "\\Movie\\" + movie.MOVIE1);
							System.IO.File.Copy(SelectedMovie.MOVIE1, path + "\\Movie\\" + movie.MOVIE1, true);
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
					if (System.IO.File.Exists(path + "\\Movie\\" + movie.TRAILER))
					{
						// Use a try block to catch IOExceptions, to
						// handle the case of the file already being
						// opened by another process.
						try
						{
							System.IO.File.Delete(path + "\\Movie\\" + movie.TRAILER);
							System.IO.File.Copy(SelectedMovie.POSTER, path + "\\Movie\\" + movie.TRAILER, true);
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

				if (System.IO.File.Exists(path + "\\Movie\\" + movie.TRAILER))
				{
					// Use a try block to catch IOExceptions, to
					// handle the case of the file already being
					// opened by another process.
					try
					{
						System.IO.File.Delete(path + "\\Movie\\" + movie.TRAILER);
					}
					catch (System.IO.IOException e)
					{
						Console.WriteLine(e.Message);
						return;
					}
				}
				if (System.IO.File.Exists(path + "\\Movie\\" + movie.MOVIE1))
				{
					// Use a try block to catch IOExceptions, to
					// handle the case of the file already being
					// opened by another process.
					try
					{
						System.IO.File.Delete(path + "\\Movie\\" + movie.MOVIE1);
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


