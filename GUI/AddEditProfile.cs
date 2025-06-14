using SuchByte.MacroDeck.GUI.CustomControls;
using System;
using System.Windows.Forms;

namespace MrVibesRSA.StreamerbotPlugin.GUI
{
    public partial class AddEditProfile : DialogForm
    {
        public AddEditProfile(string lable_PopupTitle, string name = "")
        {
            InitializeComponent();

            label_PopupTitle.Text = lable_PopupTitle;
            profile_roundedTextBox.Text = name;
        }

        public void btn_SaveProfile_Click(object sender, EventArgs e)
        {
            string id = Guid.NewGuid().ToString();
            string profile = profile_roundedTextBox.Text.Trim();

            if (label_PopupTitle.Text == "New Profile")
            {
                this.Tag = new
                {
                    Id = id,
                    Profile = profile,
                };
            }
            else
            {
                this.Tag = new
                {
                    Profile = profile,
                };
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void profile_roundedTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if(profile_roundedTextBox.Text.Length > 0)
            {
                btn_SaveProfile.Enabled = true;
            }
            else
            {
                btn_SaveProfile.Enabled = false;
            }
        }
    }
}
