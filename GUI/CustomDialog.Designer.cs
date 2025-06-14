namespace MrVibesRSA.StreamerbotPlugin.GUI
{
    partial class CustomDialog
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
            label_MessageTitle = new System.Windows.Forms.Label();
            btn_OK = new SuchByte.MacroDeck.GUI.CustomControls.ButtonPrimary();
            label2 = new System.Windows.Forms.Label();
            panel1 = new SuchByte.MacroDeck.GUI.CustomControls.RoundedPanel();
            btn_Cancel = new SuchByte.MacroDeck.GUI.CustomControls.ButtonPrimary();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // label_MessageTitle
            // 
            label_MessageTitle.AutoSize = true;
            label_MessageTitle.Location = new System.Drawing.Point(12, 6);
            label_MessageTitle.Name = "label_MessageTitle";
            label_MessageTitle.Size = new System.Drawing.Size(90, 16);
            label_MessageTitle.TabIndex = 2;
            label_MessageTitle.Text = "Error Message";
            // 
            // btn_OK
            // 
            btn_OK.BorderRadius = 8;
            btn_OK.FlatAppearance.BorderColor = System.Drawing.Color.Cyan;
            btn_OK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btn_OK.Font = new System.Drawing.Font("Tahoma", 9.75F);
            btn_OK.ForeColor = System.Drawing.Color.White;
            btn_OK.HoverColor = System.Drawing.Color.Empty;
            btn_OK.Icon = null;
            btn_OK.Location = new System.Drawing.Point(352, 176);
            btn_OK.Name = "btn_OK";
            btn_OK.Progress = 0;
            btn_OK.ProgressColor = System.Drawing.Color.FromArgb(0, 103, 205);
            btn_OK.Size = new System.Drawing.Size(75, 23);
            btn_OK.TabIndex = 9;
            btn_OK.Text = "OK";
            btn_OK.UseVisualStyleBackColor = false;
            btn_OK.UseWindowsAccentColor = true;
            btn_OK.WriteProgress = true;
            btn_OK.Click += btn_OK_Click;
            // 
            // label2
            // 
            label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label2.Location = new System.Drawing.Point(7, 9);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(433, 124);
            label2.TabIndex = 10;
            label2.Text = "label2";
            label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            panel1.BackColor = System.Drawing.Color.FromArgb(36, 36, 36);
            panel1.Controls.Add(label2);
            panel1.Location = new System.Drawing.Point(5, 31);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(446, 140);
            panel1.TabIndex = 12;
            // 
            // btn_Cancel
            // 
            btn_Cancel.BorderRadius = 8;
            btn_Cancel.FlatAppearance.BorderColor = System.Drawing.Color.Cyan;
            btn_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btn_Cancel.Font = new System.Drawing.Font("Tahoma", 9.75F);
            btn_Cancel.ForeColor = System.Drawing.Color.White;
            btn_Cancel.HoverColor = System.Drawing.Color.Empty;
            btn_Cancel.Icon = null;
            btn_Cancel.Location = new System.Drawing.Point(271, 176);
            btn_Cancel.Name = "btn_Cancel";
            btn_Cancel.Progress = 0;
            btn_Cancel.ProgressColor = System.Drawing.Color.FromArgb(0, 103, 205);
            btn_Cancel.Size = new System.Drawing.Size(75, 23);
            btn_Cancel.TabIndex = 13;
            btn_Cancel.Text = "Cancel";
            btn_Cancel.UseVisualStyleBackColor = false;
            btn_Cancel.UseWindowsAccentColor = true;
            btn_Cancel.WriteProgress = true;
            btn_Cancel.Click += btn_Cancel_Click;
            // 
            // CustomDialog
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(455, 203);
            Controls.Add(btn_Cancel);
            Controls.Add(btn_OK);
            Controls.Add(label_MessageTitle);
            Controls.Add(panel1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Name = "CustomDialog";
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label_MessageTitle;
        private SuchByte.MacroDeck.GUI.CustomControls.ButtonPrimary btn_OK;
        private System.Windows.Forms.Label label2;
        private SuchByte.MacroDeck.GUI.CustomControls.RoundedPanel panel1;
        private SuchByte.MacroDeck.GUI.CustomControls.ButtonPrimary btn_Cancel;
    }
}
