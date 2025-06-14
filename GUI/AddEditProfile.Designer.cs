namespace MrVibesRSA.StreamerbotPlugin.GUI
{
    partial class AddEditProfile
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new SuchByte.MacroDeck.GUI.CustomControls.RoundedPanel();
            profile_roundedTextBox = new SuchByte.MacroDeck.GUI.CustomControls.RoundedTextBox();
            label1 = new System.Windows.Forms.Label();
            btn_SaveProfile = new SuchByte.MacroDeck.GUI.CustomControls.ButtonPrimary();
            label_PopupTitle = new System.Windows.Forms.Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = System.Drawing.Color.FromArgb(36, 36, 36);
            panel1.Controls.Add(profile_roundedTextBox);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(btn_SaveProfile);
            panel1.Controls.Add(label_PopupTitle);
            panel1.Location = new System.Drawing.Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(519, 116);
            panel1.TabIndex = 12;
            // 
            // profile_roundedTextBox
            // 
            profile_roundedTextBox.BackColor = System.Drawing.Color.FromArgb(65, 65, 65);
            profile_roundedTextBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Italic);
            profile_roundedTextBox.Icon = null;
            profile_roundedTextBox.Location = new System.Drawing.Point(168, 38);
            profile_roundedTextBox.MaxCharacters = 32767;
            profile_roundedTextBox.Multiline = false;
            profile_roundedTextBox.Name = "profile_roundedTextBox";
            profile_roundedTextBox.Padding = new System.Windows.Forms.Padding(8, 5, 8, 5);
            profile_roundedTextBox.PasswordChar = false;
            profile_roundedTextBox.PlaceHolderColor = System.Drawing.Color.Gray;
            profile_roundedTextBox.PlaceHolderText = "";
            profile_roundedTextBox.ReadOnly = false;
            profile_roundedTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            profile_roundedTextBox.SelectionStart = 0;
            profile_roundedTextBox.Size = new System.Drawing.Size(238, 25);
            profile_roundedTextBox.TabIndex = 21;
            profile_roundedTextBox.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            profile_roundedTextBox.KeyUp += profile_roundedTextBox_KeyUp;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = System.Drawing.Color.FromArgb(36, 36, 36);
            label1.ForeColor = System.Drawing.Color.Gainsboro;
            label1.Location = new System.Drawing.Point(82, 43);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(80, 16);
            label1.TabIndex = 20;
            label1.Text = "Profile Name";
            // 
            // btn_SaveProfile
            // 
            btn_SaveProfile.BorderRadius = 8;
            btn_SaveProfile.FlatAppearance.BorderColor = System.Drawing.Color.Cyan;
            btn_SaveProfile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btn_SaveProfile.Font = new System.Drawing.Font("Tahoma", 9.75F);
            btn_SaveProfile.ForeColor = System.Drawing.Color.White;
            btn_SaveProfile.HoverColor = System.Drawing.Color.Empty;
            btn_SaveProfile.Icon = null;
            btn_SaveProfile.Location = new System.Drawing.Point(209, 74);
            btn_SaveProfile.Name = "btn_SaveProfile";
            btn_SaveProfile.Progress = 0;
            btn_SaveProfile.ProgressColor = System.Drawing.Color.FromArgb(0, 103, 205);
            btn_SaveProfile.Size = new System.Drawing.Size(108, 25);
            btn_SaveProfile.TabIndex = 10;
            btn_SaveProfile.Text = "Save";
            btn_SaveProfile.UseVisualStyleBackColor = false;
            btn_SaveProfile.UseWindowsAccentColor = true;
            btn_SaveProfile.WriteProgress = true;
            btn_SaveProfile.Click += btn_SaveProfile_Click;
            // 
            // label_PopupTitle
            // 
            label_PopupTitle.AutoSize = true;
            label_PopupTitle.BackColor = System.Drawing.Color.FromArgb(36, 36, 36);
            label_PopupTitle.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            label_PopupTitle.Location = new System.Drawing.Point(209, 9);
            label_PopupTitle.Name = "label_PopupTitle";
            label_PopupTitle.Size = new System.Drawing.Size(104, 19);
            label_PopupTitle.TabIndex = 2;
            label_PopupTitle.Text = "New Profile";
            // 
            // AddEditProfile
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(548, 141);
            Controls.Add(panel1);
            Name = "AddEditProfile";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private SuchByte.MacroDeck.GUI.CustomControls.RoundedPanel panel1;
        private System.Windows.Forms.Label label_PopupTitle;
        private SuchByte.MacroDeck.GUI.CustomControls.RoundedTextBox profile_roundedTextBox;
        private System.Windows.Forms.Label label1;
        private SuchByte.MacroDeck.GUI.CustomControls.ButtonPrimary btn_SaveProfile;
    }
}
