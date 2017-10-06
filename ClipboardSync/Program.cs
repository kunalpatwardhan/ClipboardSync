using System;
using System.Windows.Forms;
using System.IO;
namespace ClipboardSync
{
	class MainClass
	{
		[STAThread]
		public static void Main (string[] args)
		{
			string prevText = "";//, prevFileText;
			string tempText="";
			string sharedFilePath;
			Console.WriteLine ("Hello World!");

			if (Directory.Exists ("/home/mei/Shared")) {
				sharedFilePath = "/home/mei/Shared/shared";
			} else if (Directory.Exists ("G:")) {
				sharedFilePath = "G:\\shared";
			} else {
				sharedFilePath = "";
				Console.WriteLine ("No Shared Directory Found !!");
				return;
			}
			
			while (true) {
				System.Threading.Thread.Sleep (100);
				if (Clipboard.ContainsText ()) {
					if (prevText != Clipboard.GetText ()) {
						prevText = Clipboard.GetText ();
						try
						{
						File.Delete (sharedFilePath);
						}
						catch {
						}
						System.IO.File.WriteAllText (sharedFilePath, prevText);
						Console.WriteLine ("Updating Shared File ...");
						Console.WriteLine (prevText);
					}
				}

				try
				{
					tempText=File.ReadAllText (sharedFilePath);
				}
				catch {
				}

					if ( tempText!= prevText) {
					prevText = tempText;
					try
					{
						File.Delete (sharedFilePath);
					}
					catch {
					}
					Clipboard.Clear ();
					try{
					if (prevText != null) {
						Clipboard.SetText (prevText);
						Console.WriteLine ("Updating Clipboard ...");
						Console.WriteLine (prevText);
					}
					}
					catch{
					}

				}
				
			}
		}
	}
}
