using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using NUnit.Framework;
using NiceShortcuts;
using System.Windows.Forms;

namespace NiceShortcutsTests
{
	[TestFixture]
	public class NiceShortcutTests
	{
		NiceShortcut shortcut;

		[SetUp]
		public void SetUp()
		{
			this.shortcut = new NiceShortcut();
		}

		[Test]
		public void deve_ativar_Control_na_hot_key_quando_estiver_no_atalho()
		{
			this.shortcut.HotKey = "ctrl+a";
			Assert.IsTrue(this.shortcut.Control);

			this.shortcut.HotKey = "Control+c";
			Assert.IsTrue(this.shortcut.Control);
		}

		[Test]
		public void deve_ativar_alt_na_hot_key_quando_estiver_no_atalho()
		{
			this.shortcut.HotKey = "alt+a";
			Assert.IsTrue(this.shortcut.Alt);
		}

		[Test]
		public void deve_ativar_shift_na_hot_key_quando_estiver_no_atalho()
		{
			this.shortcut.HotKey = "Shift+a";
			Assert.IsTrue(this.shortcut.Shift);
		}

		[Test]
		public void deve_ativar_windows_na_hot_key_quando_estiver_no_atalho()
		{
			this.shortcut.HotKey = "windows+a";
			Assert.IsTrue(this.shortcut.Windows);
		}

		[Test]
		public void deve_setar_codigo_da_tecla_da_hot_key_com_a_tecla_do_atalho()
		{
			this.shortcut.HotKey = "ctrl+Y";
			Assert.AreEqual(Keys.Y, this.shortcut.KeyCode);

			this.shortcut.HotKey = "alt+s";
			Assert.AreEqual(Keys.S, this.shortcut.KeyCode);

			this.shortcut.HotKey = "windows+T";
			Assert.AreEqual(Keys.T, this.shortcut.KeyCode);
		}
	}
}