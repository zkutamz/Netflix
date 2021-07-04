using Project_Netflix.model;
using Project_Netflix.viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Project_Netflix.Command.Admin.Movie
{
	public class CmdInformationMovie : ICommand
	{
		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}
		AdminMovie vm;
		public bool CanExecute(object parameter)
		{
			//if(parameter != null)
			return true;
			//else
			//	return false;
		}
		public void Execute(object parameter)
		{
			String exePath = System.Environment.GetCommandLineArgs()[0];
			string[] exePaths = exePath.Split('\\');

			string path = exePaths[0];
			for (int i = 1; i < exePaths.Length - 5; i++)
				path += "\\" + exePaths[i];
			var pathMovie = (MOVIE)parameter;
			//vm.UpdateMovie(window);
			MediaElement video = new MediaElement();
			if(pathMovie!=null){

				video.Source = new Uri(path + "\\Movie\\" + pathMovie.MOVIE_NAME);
				
				MessageBox.Show(String.Format("{0} / {1}", video.Position.ToString(@"mm\:ss"), video.NaturalDuration.TimeSpan.ToString(@"mm\:ss")));
			}
			
		}
		public CmdInformationMovie(AdminMovie vm)
		{
			this.vm = vm;
		}
	}
}
