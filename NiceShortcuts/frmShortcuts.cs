using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NiceKeyboard
{
	public partial class frmShortcuts : Form
	{
		bool visibleCoreInitialized = false;

		public frmShortcuts()
		{
			InitializeComponent();
		}

		private void frmShortcuts_Resize(object sender, EventArgs e)
		{
			if (FormWindowState.Minimized == this.WindowState)
			{
				notifyicon.Visible = true;
				this.Hide();
			}
			else if (FormWindowState.Normal == this.WindowState)
				notifyicon.Visible = false;
		}

		protected override void SetVisibleCore(bool value)
		{
			if (!visibleCoreInitialized)
			{
				base.SetVisibleCore(false);
				visibleCoreInitialized = true;
				return;
			}

			base.SetVisibleCore(value);
		}

		private void notifyicon_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			this.Show();
			this.WindowState = FormWindowState.Normal;
		}
	}
}
