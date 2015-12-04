using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework
{
    public class StringOperations
    {

		public static string WriteBoolean(bool value, string strTrue, string strFalse)
		{
			return value ? strTrue : strFalse;
		}

		public static string CropDisplay(string text, int len, bool cropEnable)
		{
			if ( ! cropEnable || text.Length <= len)
				return text;

			string prm1 = text.Left(len / 2 - 3);
			string prm2 = text.Right(len / 2 - 3);

			return prm1 + " ... " + prm2;
		}

		public static void RemoveDoubleSpace(ref string text)
		{
			// duble boşlukları teke çevir
			while ( text.Contains("  ") )
				text = text.Replace("  ", " ");
		}

		public static void RemoveTripleSpace(ref string text)
		{
			// duble boşlukları teke çevir
			while ( text.Contains("   ") )
				text = text.Replace("   ", "  ");
		}

		public static string RepeatText(string text, int count)
		{
			string val = "";
			for ( int i = 0; i < count; i++ )
			{
				val += text;
			}
			return val;
		}

		public static string ConvertToFriendlyUrl(string text)
		{
			if ( text == null )
				return "";
				
			string strFriendly = text.ToString();
			strFriendly = strFriendly.Trim();
			strFriendly = strFriendly.Trim('-');
			strFriendly = strFriendly.ToLower();
			strFriendly = strFriendly.Replace(".", "-");
			strFriendly = strFriendly.Replace("ç", "c");
			strFriendly = strFriendly.Replace("ş", "s");
			strFriendly = strFriendly.Replace("ı", "i");
			strFriendly = strFriendly.Replace("ö", "o");
			strFriendly = strFriendly.Replace("ü", "u");
			strFriendly = strFriendly.Replace("ğ", "g");
			char[] allowedChars = @"abcdefghijklmnopqrstuvwxyz1234567890_- ".ToCharArray();
			foreach (char thatChar in strFriendly)
			{
				if (!allowedChars.Contains(thatChar))
				{
					strFriendly = strFriendly.Replace(thatChar.ToString(), String.Empty);
				}
			}
			strFriendly = strFriendly.Replace(" ", "-");
			strFriendly = strFriendly.Replace("--", "-");
			strFriendly = strFriendly.Replace("---", "-");
			strFriendly = strFriendly.Replace("----", "-");
			strFriendly = strFriendly.Replace("-----", "-");
			strFriendly = strFriendly.Replace("----", "-");
			strFriendly = strFriendly.Replace("---", "-");
			strFriendly = strFriendly.Replace("--", "-");
			strFriendly = strFriendly.Trim();
			strFriendly = strFriendly.Trim('-');
			return strFriendly;
		}

		public static string GetAsDoubleNumberedString(int number)
		{
			return  ("00" + number.ToString()).Right(2);
		}

		//Capitalizes the first letter of every word
		//the big story -> The Big Story
		public static string GetTitle(string input)
		{
			string[] words = input.Split(new char[] { ' ' });

			for ( int i = 0; i < words.Length; i++ )
			{
				if ( words[i].Length > 0 )
					words[i] = words[i].Substring(0, 1).ToUpper() + words[i].Substring(1);
			}

			return string.Join(" ", words);
		}

		//Checks if string has only numbers
		//12453 -> True
		//234d3 -> False
		public static bool IsNumeric(string input)
		{
			for ( int i = 0; i < input.Length; i++ )
			{
				if ( !( Convert.ToInt32(input[i]) >= 48 && Convert.ToInt32(input[i]) <= 57 ) )
				{
					//Not integer value
					return false;
				}
			}
			return true;
		}

		//Capitalizes a word or sentence
		//word -> Word
		//OR
		//this is a sentence -> This is a sentence
		public static string Capitalize(string input)
		{
			if ( input.Length == 0 ) return "";
			if ( input.Length == 1 ) return input.ToUpper();

			return input.Substring(0, 1).ToUpper() + input.Substring(1);
		}

		#region Pipe Operations

        public static string ConvertPipedStringToText(string pipedString, string seperator)
        {
            List<string> values = ConvertPipedStringToList<string>(pipedString);
            string text = "";
            for (int i = 0; i < values.Count; i++)
            {
                if (values[i] == "")
                    continue;

                text += values[i];

                if (i < values.Count - 1)
                    text += seperator;
            }
            return text;
        }

        public static string ConvertTextToPipedString(string text)
        {
            return ConvertListToPipedString(ConvertTextToList(text));
        }

        public static List<string> ConvertTextToList(string text)
        {
            string delimiter = "|";
            text = text.Trim().Replace(Environment.NewLine, delimiter).Replace(";", delimiter).Replace(",", delimiter);

            string[] values = text.Split('|');

            List<string> list = new List<string>();
            for (int i = 0; i < values.GetLength(0); i++)
            {
                if (values[i] == "")
                    continue;
                list.Add(values[i].Trim());
            }
            return list;
        }

		public static string ConvertListToPipedString(List<int> list)
		{
			List<string> list2 = new List<string>();

			list2 = list.ConvertAll<string>(new Converter<int, string>(new Func<int, string>((x) => { return x.ToString(); })));

			return ConvertListToPipedString(list2);
		}

        public static string ConvertListToPipedString(List<string> list)
        {
            if (list == null || list.Count == 0)
                return "";
            else
                return "|" + String.Join("|", list.ToArray()) + "|";
        }

        public static List<T> ConvertPipedStringToList<T>(string pipedString)
        {
            char delimiter = '|';
            List<T> list = new List<T>();

            if (String.IsNullOrEmpty(pipedString))
                return list;

            string[] values = pipedString.Split(delimiter);
            for (int i = 0; i < values.GetLength(0); i++)
            {
                if (values[i].Trim().Length != 0 && values[i].Trim() != delimiter.ToString())
                {
					try
					{
						list.Add((T)Convert.ChangeType(values[i].Trim(), typeof(T)));
					}
					catch 
					{ 
					
					}
                }
            }

            return list;
        }

        #endregion

    }
}
