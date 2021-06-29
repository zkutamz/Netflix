using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Project_Netflix.View
{
	public class EmailCharacterRule : ValidationRule
	{
		public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{
			string charString = value as string;
			Regex emailCheck = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
			Match match = emailCheck.Match(charString);
			bool isEmail = (match.Success) ? true : false;
			if (charString.Length == 0)
				return new ValidationResult(false, $"Hãy nhập email");
			else if (!isEmail)
			{
				return new ValidationResult(false, $"Email không hợp lệ.");
			}
			else
				return new ValidationResult(true, null);
		}
	}
	public class PhoneCharacterRule : ValidationRule
	{
		public int MiniPasswordCharacter { get; set; }
		public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{

			PasswordBox p = value as PasswordBox;
			string charString = value as string;
			Regex phoneCheck = new Regex(@"^(0 ?)(3[2 - 9] | 5[6 | 8 | 9] | 7[0 | 6 - 9] | 8[0 - 6 | 8 | 9] | 9[0 - 4 | 6 - 9])[0 - 9]{ 7}$");
			Match match = phoneCheck.Match(charString);
			bool isPhone = (match.Success) ? true : false;
			if (charString.Length == 0)
				return new ValidationResult(false, $"Hãy nhập số điện thoại.");
			else if(charString.Length < 10)
				return new ValidationResult(false, $"Số điện thoại phải có 10 số.");
			else if (!isPhone)
			{
				return new ValidationResult(false, $"Só điện thoại không hợp lệ.");
			}
			else
				return new ValidationResult(true, null);
		}
	}
	
	public class NameCharacterRule : ValidationRule
	{

		public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{
			string charString = value as string;
			if (charString.Length == 0)
				return new ValidationResult(false, $"Hãy nhập tên.");
			
			else
				return new ValidationResult(true, null);
		}
	}
}
