using System;

namespace SLR
{
	public class my_String
	{
		string local_String;

		public my_String (string str)
		{
			this.local_String = str;
		}

		/// <summary>
		/// Get_s the number_or_ char.
		/// </summary>
		/// <returns>Number of incidents within of string. Return -1 if the char isn't within string</returns>
		/// <param name="c">char will count</param>
		public int count_Char_incident (char c)
		{
			if( this.local_String.Contains (c.ToString()))
			{
				int ac = 0;

				foreach (char character in local_String)
				{
					ac = (character == c) ? ac + 1 : ac;
				}

				return ac;
			}
			return -1;
		}

		public int count_Char_incident (char c_1, char c_2)
		{
			if( this.local_String.Contains (c_1.ToString()) ||  this.local_String.Contains (c_2.ToString()))
			{
				int ac = 0;

				foreach (char character in local_String)
				{
					ac = (character == c_1 || character == c_2) ? ac + 1 : ac;
				}

				return ac;
			}
			return -1;
		}
	}
}

