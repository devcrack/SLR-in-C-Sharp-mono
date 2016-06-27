using System;
using Gtk;
using System.IO;
public partial class MainWindow: Gtk.Window
{
	string path_OpenFiles;

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
			//FileStream file = File.OpenRead(
			this.textview1.Buffer.Clear();
			this.textview1.Buffer.Text = File.ReadAllText (file_Dialog.Filename);
			//this.textview2.Buffer.Text = file_Dialog.Filename;

		}
		file_Dialog.Destroy ();
	}
}
