using SuchByte.MacroDeck.GUI.CustomControls;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MrVibesRSA.StreamerbotPlugin.GUI
{
    public enum CustomDialogType
    {
        Info,
        Warning,
        Error,
        Confirm
    }

    public partial class CustomDialog : DialogForm
    {
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DialogResult Result { get; private set; } = DialogResult.None;

        public CustomDialog(string message, CustomDialogType type = CustomDialogType.Info,
                            string okButtonText = "OK", string cancelButtonText = "Cancel")
        {
            InitializeComponent();

            label2.Text = message;
            btn_OK.Text = okButtonText;
            btn_Cancel.Text = cancelButtonText;

            // Visual styling depending on type
            switch (type)
            {
                case CustomDialogType.Info:
                    label_MessageTitle = new Label { Text = "Information", Font = new Font("Arial", 12, FontStyle.Bold) };
                    this.BackColor = Color.LightBlue;
                    break;
                case CustomDialogType.Warning:
                    label_MessageTitle = new Label { Text = "Warning", Font = new Font("Arial", 12, FontStyle.Bold) };
                    this.BackColor = Color.Khaki;
                    break;
                case CustomDialogType.Error:
                    label_MessageTitle = new Label { Text = "Error", Font = new Font("Arial", 12, FontStyle.Bold) };
                    this.BackColor = Color.LightCoral;
                    break;
                case CustomDialogType.Confirm:
                    label_MessageTitle = new Label { Text = "Confirmation", Font = new Font("Arial", 12, FontStyle.Bold) };
                    btn_Cancel.Visible = true;
                    break;
            }

            // Hide Cancel if not Confirm
            btn_Cancel.Visible = type == CustomDialogType.Confirm;
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            Result = DialogResult.OK;
            this.Close();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            Result = DialogResult.Cancel;
            this.Close();
        }
    }
}
