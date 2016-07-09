using System;
using Gtk;
using System.IO;
using SLR;
public partial class MainWindow: Gtk.Window
{
	private string path_OpenFiles;
	private my_String aux_String;

	public MainWindow () : base (Gtk.WindowType.Toplevel)
	{
		this.path_OpenFiles = System.IO.Directory.GetCurrentDirectory().ToString();
		Build ();
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
		

	protected void click_start_Button (object sender, EventArgs e)
	{
		File.WriteAllText (this.path_OpenFiles + "/my_file.txt", this.textview1.Buffer.Text);
		this.textview2.Buffer.Text = File.ReadAllText (this.path_OpenFiles + "/my_file.txt");
	}



	protected void Save_Press (object sender, EventArgs e)
	{
		Gtk.FileChooserDialog file_Dialog = new FileChooserDialog ("Save File",
			                                    this,
			                                    FileChooserAction.Save,
			                                    "Cancel", ResponseType.Cancel,
			                                    "Save", ResponseType.Accept);
		
		file_Dialog.SetCurrentFolder (this.path_OpenFiles);
		file_Dialog.Gravity = Gdk.Gravity.Center;
		if (file_Dialog.Run () == (int)ResponseType.Accept) {
			File.WriteAllText (file_Dialog.Filename, this.textview1.Buffer.Text);
		}
		file_Dialog.Destroy ();
	}


	protected void Open_Press (object sender, EventArgs e)
	{
		Gtk.FileChooserDialog file_Dialog = new FileChooserDialog ("Open File",
			this,
			FileChooserAction.Open,
			"Cancel", ResponseType.Cancel,
			"Open", ResponseType.Accept);

		file_Dialog.SetCurrentFolder (this.path_OpenFiles);//Establish the open folder default
		file_Dialog.Gravity = Gdk.Gravity.Center;
		if (file_Dialog.Run () == (int)ResponseType.Accept) {
			this.textview1.Buffer.Clear();
			this.textview1.Buffer.Text = File.ReadAllText (file_Dialog.Filename);
		}
		file_Dialog.Destroy ();
	}

	[GLib.ConnectBefore]
	protected void Edit_TView_Key_Press_Event (object o, KeyPressEventArgs args)
	{
		string[] lines;
		if (
				args.Event.Key == Gdk.Key.space 
			|| 
				(args.Event.Key >= Gdk.Key.a && args.Event.Key <= Gdk.Key.z || args.Event.Key >= Gdk.Key.A && args.Event.Key <= Gdk.Key.Z)
			||
				args.Event.Key == Gdk.Key.BackSpace
			||
				args.Event.Key == Gdk.Key.greater
			||
				args.Event.Key == Gdk.Key.equal		
			||
				(args.Event.Key >= Gdk.Key.Key_0 && args.Event.Key <= Gdk.Key.Key_9)
			||
				args.Event.Key == Gdk.Key.Return
			) {
			this.textview1.Editable = true;		
			if (args.Event.Key == Gdk.Key.Return) {
				lines = this.textview1.Buffer.Text.Split ('\n'); /*Catch all the lines within of texview buffer*/
				if ( this.verify_Validity (lines [lines.Length - 1]) == true) {
					this.textview1.Editable = true;		
				}
				else {
					this.textview1.Editable = false;
				}
			}
			
		} 
		else {
			this.textview1.Editable = false;
		}
		//this.textview2.Buffer.Text += args.Event.Key;
		
	}

	private bool verify_Validity (string str)
	{
		this.aux_String = new my_String (str);

		int cont_1 = aux_String.count_Char_incident ('=');
		int cont_2 = aux_String.count_Char_incident ('>');

		if (cont_1 == 1 || cont_2 == 1) {
			if (str.IndexOf ('>') < str.IndexOf ('=')) {
				return true;
			}
		}
		return false;
	}

}





















