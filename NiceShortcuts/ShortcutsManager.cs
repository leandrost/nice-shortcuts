using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NiceKeyboard;
using System.Diagnostics;
using System.Configuration;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace NiceShortcuts
{
	public class ShortcutsManager
	{
		XmlSerializer serializer = new XmlSerializer(typeof(List<NiceShortcut>));
		public List<NiceShortcut> Shortcuts { get; set; }

		public ShortcutsManager()
		{
			this.Shortcuts = new List<NiceShortcut>();
		}

		public static void Main()
		{
			try
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);

				var shortcutsManager = new ShortcutsManager();
				shortcutsManager.LoadShortcuts();

				var frmShortcuts = new frmShortcuts();
				shortcutsManager.RegisterShortcuts(frmShortcuts);
				Application.Run(frmShortcuts);

				shortcutsManager.UnregisterShortcuts();
			}
			catch (Exception ex)
			{
				ShortcutsManager.ShowError(ex.Message);
			}
		}

		private static void ShowError(string message)
		{
			MessageBox.Show(message, "'Not' Nice! Shortcuts Error"
				, MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		private void RegisteShortcut(NiceShortcut shortcut, Form form)
		{
			shortcut.Pressed += delegate
			{
				try
				{
					ProcessStartInfo startInfo = new ProcessStartInfo();
					startInfo.FileName = shortcut.Command;
					Process.Start(startInfo);
				}
				catch(Exception ex)
				{
					ShortcutsManager.ShowError(string.Format("Whoops, fail to execute command '{0}'.\n {1}",
						shortcut.Command, ex.Message));
				}
			};

			if (shortcut.GetCanRegister(form))
					shortcut.Register(form);
			else
				ShortcutsManager.ShowError("Whoops, looks like attempts to register will fail or throw an exception, show an error/visual user feedback");	
		}

		private void RegisterShortcuts(Form form)
		{
			foreach (var shortcut in this.Shortcuts)
				this.RegisteShortcut(shortcut, form);
		}

		public void UnregisterShortcut(NiceShortcut shortcut)
		{
			if (shortcut.Registered)
				shortcut.Unregister();
		}

		private void UnregisterShortcuts()
		{
			foreach (var shortcut in this.Shortcuts)
				this.UnregisterShortcut(shortcut);
		}

		public void SaveShorcuts(string filePath)
		{
			using (var fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
			{
				serializer.Serialize(fileStream, this.Shortcuts);
				fileStream.Flush();
			}
		}

		public void LoadShortcuts(string filePath)
		{
			using (var fileStream = new FileStream(filePath, FileMode.Open))
			{
				using (var reader = XmlReader.Create(fileStream))
				{
					this.Shortcuts = (List<NiceShortcut>)this.serializer.Deserialize(reader);
				};
			}
		}

		public void LoadShortcuts()
		{
			string shortcutsFile = ConfigurationManager.AppSettings["ShortcutsFile"];

			if (string.IsNullOrEmpty(shortcutsFile))
				shortcutsFile = "shortcuts.xml";

			this.LoadShortcuts(shortcutsFile);
		}
	}
}
