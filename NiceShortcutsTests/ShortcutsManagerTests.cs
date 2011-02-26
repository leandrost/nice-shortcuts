using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using NUnit.Framework;
using NiceShortcuts;
using System.Windows.Forms;
using System.IO;

namespace NiceShortcutsTests
{
	/// <summary>
	/// Summary description for NiceShortcutsTests.
	/// </summary>
	[TestFixture]
	public class ShortcutsManagerTests
	{
		const string SHORTCUT_XML = @"<?xml version=""1.0""?>
<ArrayOfNiceShortcut xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <NiceShortcut Name=""Winamp"" HotKey=""Control+W"" Command=""Winamp"" />
</ArrayOfNiceShortcut>";

		NiceShortcut shortcut;
		ShortcutsManager shortcutsMananger;

		[SetUp]
		public void SetUp()
		{
			this.shortcutsMananger = new ShortcutsManager();

			this.shortcut = new NiceShortcut();
			this.shortcut.Name = "Winamp";
			this.shortcut.Command = "Winamp";
			this.shortcut.KeyCode = System.Windows.Forms.Keys.W;
			this.shortcut.Control = true;
		}

		[Test]
		public void should_save_shortcut_on_file()
		{
			//arrange
			var targetPath = "../../shortcuts.xml";

			if (File.Exists(targetPath))
				File.Delete(targetPath);
			//
			this.shortcutsMananger.Shortcuts = new List<NiceShortcut>();
			this.shortcutsMananger.Shortcuts.Add(this.shortcut);
			this.shortcutsMananger.SaveShorcuts(targetPath);

			Assert.IsTrue(File.Exists(targetPath));
			Assert.AreEqual(SHORTCUT_XML, File.ReadAllText(targetPath));
		}

		[Test]
		public void should_load_shortcus_from_a_file()
		{
			//arrange
			var targetPath = "../../shortcuts.xml";

			if (File.Exists(targetPath))
				File.Delete(targetPath);

			File.WriteAllText(targetPath, SHORTCUT_XML);
			//

			this.shortcutsMananger.LoadShortcuts(targetPath);

			Assert.AreNotEqual(0, this.shortcutsMananger.Shortcuts.Count);

			var shortcut = this.shortcutsMananger.Shortcuts[0];

			Assert.AreEqual(this.shortcut.KeyCode, shortcut.KeyCode, "KeyCode");
			Assert.AreEqual(this.shortcut.Alt, shortcut.Alt, "Alt");
			Assert.AreEqual(this.shortcut.Control, shortcut.Control, "Control");
			Assert.AreEqual(this.shortcut.Windows, shortcut.Windows, "Windows");
			Assert.AreEqual(this.shortcut.Command, shortcut.Command, "Command");
		}
	}
}