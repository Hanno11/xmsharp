using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MediaPortal.Profile;
using MediaPortal.Configuration;

namespace XMSharp.MP
{
	public partial class frmSetup : Form
	{
        
		public frmSetup()
		{
			InitializeComponent();
		}



		private void saveSettings()
		{
			using (Settings settings = new Settings(Config.GetFile(Config.Dir.Config, "xmsharpmp.xml")))
			{
				//string quality = "High";

				//if (this.checkQualityLow.Checked)
				//    quality = "Low";

				settings.SetValue("XMSharpMP", "AccountEmail", this.textEmail.Text);
				settings.SetValue("XMSharpMP", "AccountPassword", this.textPassword.Text);

                if (!this.textMenuName.Text.Trim().ToLower().Equals(XMSharpMP.DefaultMenuName.ToLower()))
                    settings.SetValue("XMSharpMP", "MenuName", this.textMenuName.Text);

				settings.SetValue("XMSharpMP", "AccountTimeout", (int)this.textTimeout.Value);
                
				//settings.SetValue("XMSharpMP", "StreamQuality", quality);
			}
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			saveSettings();
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

        private void frmSetup_Load(object sender, EventArgs e)
        {
            using (Settings settings = new Settings(Config.GetFile(Config.Dir.Config, "xmsharpmp.xml")))
            {
                //string quality = "High";

                //if (this.checkQualityLow.Checked)
                //    quality = "Low";

                this.textEmail.Text = settings.GetValueAsString("XMSharpMP", "AccountEmail", "");
                this.textPassword.Text = settings.GetValueAsString("XMSharpMP", "AccountPassword", "");
				this.textTimeout.Value = settings.GetValueAsInt("XMSharpMP", "AccountTimeout", 5);
				//settings.SetValue("XMSharpMP", "StreamQuality", quality);

                this.textMenuName.Text = settings.GetValueAsString("XMSharpMP", "MenuName", XMSharpMP.DefaultMenuName);

				
				

            }
        }
	}
}
