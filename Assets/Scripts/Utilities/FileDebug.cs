using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace UnityUtility
{
	public class FileDebug
	{
		static FileStream _outputFile;
		static StreamWriter _writer;
		static StreamWriter Writer
		{
			get
			{
				if (_outputFile == null)
				{
					_outputFile = new FileStream(Application.dataPath + "/debug.log", FileMode.Create);
				}

				if (_writer == null)
				{
					_writer = new StreamWriter(_outputFile);
				}

				return _writer;
			}
		}
		public static void Write(object message)
		{
			Writer.Write(message);
			Writer.Flush();
		}

		public static void WriteLine(object message)
		{
			Writer.WriteLine(message);
			Writer.Flush();
		}
	}
}
