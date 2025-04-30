using SuchByte.MacroDeck.GUI.CustomControls;
using System.Drawing;
using System.Windows.Forms;

namespace MrVibes_RSA.StreamerbotPlugin.GUI
{
    partial class PluginConfig
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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            address_roundedTextBox = new RoundedTextBox();
            port_roundedTextBox = new RoundedTextBox();
            btn_OK = new ButtonPrimary();
            btn_Connect = new ButtonPrimary();
            panel1 = new RoundedPanel();
            showPassword_button = new ButtonPrimary();
            label4 = new Label();
            password_roundedTextBox = new RoundedTextBox();
            linkLabel1 = new LinkLabel();
            endpoint_roundedTextBox = new RoundedTextBox();
            label7 = new Label();
            pictureBox1 = new PictureBox();
            roundedPanel1 = new RoundedPanel();
            label_GlobalCount = new Label();
            label_ActionCount = new Label();
            label10 = new Label();
            label6 = new Label();
            label_OSVersion = new Label();
            label_OS = new Label();
            label12 = new Label();
            label11 = new Label();
            label_Version = new Label();
            label8 = new Label();
            label_ConnectedTo = new Label();
            label5 = new Label();
            label9 = new Label();
            label13 = new Label();
            checkBox_AutoConnect = new CheckBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            roundedPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.FromArgb(36, 36, 36);
            label1.Font = new Font("Tahoma", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(24, 9);
            label1.Name = "label1";
            label1.Size = new Size(219, 19);
            label1.TabIndex = 2;
            label1.Text = "Connect to Websocked Server";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.FromArgb(36, 36, 36);
            label2.ForeColor = Color.Gainsboro;
            label2.Location = new Point(139, 53);
            label2.Name = "label2";
            label2.Size = new Size(53, 16);
            label2.TabIndex = 4;
            label2.Text = "Address";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.FromArgb(36, 36, 36);
            label3.Location = new Point(162, 83);
            label3.Name = "label3";
            label3.Size = new Size(30, 16);
            label3.TabIndex = 5;
            label3.Text = "Port";
            // 
            // address_roundedTextBox
            // 
            address_roundedTextBox.BackColor = Color.FromArgb(65, 65, 65);
            address_roundedTextBox.Font = new Font("Tahoma", 9F, FontStyle.Italic);
            address_roundedTextBox.Icon = null;
            address_roundedTextBox.Location = new Point(198, 48);
            address_roundedTextBox.MaxCharacters = 32767;
            address_roundedTextBox.Multiline = false;
            address_roundedTextBox.Name = "address_roundedTextBox";
            address_roundedTextBox.Padding = new Padding(8, 5, 8, 5);
            address_roundedTextBox.PasswordChar = false;
            address_roundedTextBox.PlaceHolderColor = Color.Gray;
            address_roundedTextBox.PlaceHolderText = "";
            address_roundedTextBox.ReadOnly = false;
            address_roundedTextBox.ScrollBars = ScrollBars.None;
            address_roundedTextBox.SelectionStart = 0;
            address_roundedTextBox.Size = new Size(238, 25);
            address_roundedTextBox.TabIndex = 6;
            address_roundedTextBox.TextAlignment = HorizontalAlignment.Left;
            // 
            // port_roundedTextBox
            // 
            port_roundedTextBox.BackColor = Color.FromArgb(65, 65, 65);
            port_roundedTextBox.Font = new Font("Tahoma", 9F, FontStyle.Italic);
            port_roundedTextBox.Icon = null;
            port_roundedTextBox.Location = new Point(198, 79);
            port_roundedTextBox.MaxCharacters = 32767;
            port_roundedTextBox.Multiline = false;
            port_roundedTextBox.Name = "port_roundedTextBox";
            port_roundedTextBox.Padding = new Padding(8, 5, 8, 5);
            port_roundedTextBox.PasswordChar = false;
            port_roundedTextBox.PlaceHolderColor = Color.Gray;
            port_roundedTextBox.PlaceHolderText = "";
            port_roundedTextBox.ReadOnly = false;
            port_roundedTextBox.ScrollBars = ScrollBars.None;
            port_roundedTextBox.SelectionStart = 0;
            port_roundedTextBox.Size = new Size(109, 25);
            port_roundedTextBox.TabIndex = 7;
            port_roundedTextBox.TextAlignment = HorizontalAlignment.Left;
            // 
            // btn_OK
            // 
            btn_OK.BorderRadius = 8;
            btn_OK.FlatAppearance.BorderColor = Color.Cyan;
            btn_OK.FlatStyle = FlatStyle.Flat;
            btn_OK.Font = new Font("Tahoma", 9.75F);
            btn_OK.ForeColor = Color.White;
            btn_OK.HoverColor = Color.Empty;
            btn_OK.Icon = null;
            btn_OK.Location = new Point(291, 428);
            btn_OK.Name = "btn_OK";
            btn_OK.Progress = 0;
            btn_OK.ProgressColor = Color.FromArgb(0, 103, 205);
            btn_OK.Size = new Size(75, 23);
            btn_OK.TabIndex = 11;
            btn_OK.Text = "OK";
            btn_OK.UseVisualStyleBackColor = false;
            btn_OK.UseWindowsAccentColor = true;
            btn_OK.WriteProgress = true;
            btn_OK.Click += btn_OK_Click;
            // 
            // btn_Connect
            // 
            btn_Connect.BorderRadius = 8;
            btn_Connect.FlatAppearance.BorderColor = Color.Cyan;
            btn_Connect.FlatStyle = FlatStyle.Flat;
            btn_Connect.Font = new Font("Tahoma", 9.75F);
            btn_Connect.ForeColor = Color.White;
            btn_Connect.HoverColor = Color.Empty;
            btn_Connect.Icon = null;
            btn_Connect.Location = new Point(258, 183);
            btn_Connect.Name = "btn_Connect";
            btn_Connect.Progress = 0;
            btn_Connect.ProgressColor = Color.FromArgb(0, 103, 205);
            btn_Connect.Size = new Size(108, 23);
            btn_Connect.TabIndex = 10;
            btn_Connect.Text = "Connect";
            btn_Connect.UseVisualStyleBackColor = false;
            btn_Connect.UseWindowsAccentColor = true;
            btn_Connect.WriteProgress = true;
            btn_Connect.Click += btn_Connect_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(36, 36, 36);
            panel1.Controls.Add(showPassword_button);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(password_roundedTextBox);
            panel1.Controls.Add(linkLabel1);
            panel1.Controls.Add(endpoint_roundedTextBox);
            panel1.Controls.Add(port_roundedTextBox);
            panel1.Controls.Add(address_roundedTextBox);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(btn_Connect);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(label3);
            panel1.Location = new Point(18, 78);
            panel1.Name = "panel1";
            panel1.Size = new Size(625, 219);
            panel1.TabIndex = 11;
            // 
            // showPassword_button
            // 
            showPassword_button.BorderRadius = 8;
            showPassword_button.FlatAppearance.BorderColor = Color.Cyan;
            showPassword_button.FlatStyle = FlatStyle.Flat;
            showPassword_button.Font = new Font("Tahoma", 9.75F);
            showPassword_button.ForeColor = Color.White;
            showPassword_button.HoverColor = Color.Empty;
            showPassword_button.Icon = null;
            showPassword_button.Location = new Point(442, 143);
            showPassword_button.Name = "showPassword_button";
            showPassword_button.Progress = 0;
            showPassword_button.ProgressColor = Color.FromArgb(0, 103, 205);
            showPassword_button.Size = new Size(54, 23);
            showPassword_button.TabIndex = 10;
            showPassword_button.Text = "Show";
            showPassword_button.UseVisualStyleBackColor = false;
            showPassword_button.UseWindowsAccentColor = true;
            showPassword_button.WriteProgress = true;
            showPassword_button.Click += showPassword_button_Click_1;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.FromArgb(36, 36, 36);
            label4.Location = new Point(130, 146);
            label4.Name = "label4";
            label4.Size = new Size(70, 16);
            label4.TabIndex = 16;
            label4.Text = "password*";
            // 
            // password_roundedTextBox
            // 
            password_roundedTextBox.BackColor = Color.FromArgb(65, 65, 65);
            password_roundedTextBox.Font = new Font("Tahoma", 9F);
            password_roundedTextBox.Icon = null;
            password_roundedTextBox.Location = new Point(198, 141);
            password_roundedTextBox.MaxCharacters = 32767;
            password_roundedTextBox.Multiline = false;
            password_roundedTextBox.Name = "password_roundedTextBox";
            password_roundedTextBox.Padding = new Padding(8, 5, 8, 5);
            password_roundedTextBox.PasswordChar = true;
            password_roundedTextBox.PlaceHolderColor = Color.Gray;
            password_roundedTextBox.PlaceHolderText = "";
            password_roundedTextBox.ReadOnly = false;
            password_roundedTextBox.ScrollBars = ScrollBars.None;
            password_roundedTextBox.SelectionStart = 0;
            password_roundedTextBox.Size = new Size(238, 25);
            password_roundedTextBox.TabIndex = 9;
            password_roundedTextBox.TextAlignment = HorizontalAlignment.Left;
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.LinkColor = Color.White;
            linkLabel1.Location = new Point(555, 12);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(53, 16);
            linkLabel1.TabIndex = 5;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "How to?";
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // endpoint_roundedTextBox
            // 
            endpoint_roundedTextBox.BackColor = Color.FromArgb(65, 65, 65);
            endpoint_roundedTextBox.Font = new Font("Tahoma", 9F, FontStyle.Italic);
            endpoint_roundedTextBox.Icon = null;
            endpoint_roundedTextBox.Location = new Point(198, 110);
            endpoint_roundedTextBox.MaxCharacters = 32767;
            endpoint_roundedTextBox.Multiline = false;
            endpoint_roundedTextBox.Name = "endpoint_roundedTextBox";
            endpoint_roundedTextBox.Padding = new Padding(8, 5, 8, 5);
            endpoint_roundedTextBox.PasswordChar = false;
            endpoint_roundedTextBox.PlaceHolderColor = Color.Gray;
            endpoint_roundedTextBox.PlaceHolderText = "";
            endpoint_roundedTextBox.ReadOnly = false;
            endpoint_roundedTextBox.ScrollBars = ScrollBars.None;
            endpoint_roundedTextBox.SelectionStart = 0;
            endpoint_roundedTextBox.Size = new Size(238, 25);
            endpoint_roundedTextBox.TabIndex = 8;
            endpoint_roundedTextBox.TextAlignment = HorizontalAlignment.Left;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = Color.FromArgb(36, 36, 36);
            label7.Location = new Point(136, 115);
            label7.Name = "label7";
            label7.Size = new Size(56, 16);
            label7.TabIndex = 12;
            label7.Text = "Endpoint";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = MrVibesRSA.StreamerbotPlugin.Properties.Resources.streamerbot_logo_text;
            pictureBox1.Location = new Point(18, 9);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(324, 59);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 12;
            pictureBox1.TabStop = false;
            // 
            // roundedPanel1
            // 
            roundedPanel1.BackColor = Color.FromArgb(36, 36, 36);
            roundedPanel1.Controls.Add(label_GlobalCount);
            roundedPanel1.Controls.Add(label_ActionCount);
            roundedPanel1.Controls.Add(label10);
            roundedPanel1.Controls.Add(label6);
            roundedPanel1.Controls.Add(label_OSVersion);
            roundedPanel1.Controls.Add(label_OS);
            roundedPanel1.Controls.Add(label12);
            roundedPanel1.Controls.Add(label11);
            roundedPanel1.Controls.Add(label_Version);
            roundedPanel1.Controls.Add(label8);
            roundedPanel1.Controls.Add(label_ConnectedTo);
            roundedPanel1.Controls.Add(label5);
            roundedPanel1.Controls.Add(label9);
            roundedPanel1.Location = new Point(18, 303);
            roundedPanel1.Name = "roundedPanel1";
            roundedPanel1.Size = new Size(625, 97);
            roundedPanel1.TabIndex = 17;
            // 
            // label_GlobalCount
            // 
            label_GlobalCount.AutoSize = true;
            label_GlobalCount.BackColor = Color.FromArgb(36, 36, 36);
            label_GlobalCount.ForeColor = Color.Gainsboro;
            label_GlobalCount.Location = new Point(409, 68);
            label_GlobalCount.Name = "label_GlobalCount";
            label_GlobalCount.Size = new Size(0, 16);
            label_GlobalCount.TabIndex = 16;
            // 
            // label_ActionCount
            // 
            label_ActionCount.AutoSize = true;
            label_ActionCount.BackColor = Color.FromArgb(36, 36, 36);
            label_ActionCount.ForeColor = Color.Gainsboro;
            label_ActionCount.Location = new Point(117, 68);
            label_ActionCount.Name = "label_ActionCount";
            label_ActionCount.Size = new Size(0, 16);
            label_ActionCount.TabIndex = 15;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.BackColor = Color.FromArgb(36, 36, 36);
            label10.ForeColor = Color.Gainsboro;
            label10.Location = new Point(297, 68);
            label10.Name = "label10";
            label10.Size = new Size(106, 16);
            label10.TabIndex = 14;
            label10.Text = "Global var Count:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = Color.FromArgb(36, 36, 36);
            label6.ForeColor = Color.Gainsboro;
            label6.Location = new Point(27, 68);
            label6.Name = "label6";
            label6.Size = new Size(84, 16);
            label6.TabIndex = 13;
            label6.Text = "Action Count:";
            // 
            // label_OSVersion
            // 
            label_OSVersion.AutoSize = true;
            label_OSVersion.BackColor = Color.FromArgb(36, 36, 36);
            label_OSVersion.ForeColor = Color.Gainsboro;
            label_OSVersion.Location = new Point(409, 52);
            label_OSVersion.Name = "label_OSVersion";
            label_OSVersion.Size = new Size(0, 16);
            label_OSVersion.TabIndex = 12;
            // 
            // label_OS
            // 
            label_OS.AutoSize = true;
            label_OS.BackColor = Color.FromArgb(36, 36, 36);
            label_OS.ForeColor = Color.Gainsboro;
            label_OS.Location = new Point(409, 36);
            label_OS.Name = "label_OS";
            label_OS.Size = new Size(0, 16);
            label_OS.TabIndex = 11;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.BackColor = Color.FromArgb(36, 36, 36);
            label12.ForeColor = Color.Gainsboro;
            label12.Location = new Point(349, 52);
            label12.Name = "label12";
            label12.Size = new Size(55, 16);
            label12.TabIndex = 10;
            label12.Text = "Version:";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.BackColor = Color.FromArgb(36, 36, 36);
            label11.ForeColor = Color.Gainsboro;
            label11.Location = new Point(374, 36);
            label11.Name = "label11";
            label11.Size = new Size(29, 16);
            label11.TabIndex = 9;
            label11.Text = "OS:";
            // 
            // label_Version
            // 
            label_Version.AutoSize = true;
            label_Version.BackColor = Color.FromArgb(36, 36, 36);
            label_Version.ForeColor = Color.Gainsboro;
            label_Version.Location = new Point(117, 52);
            label_Version.Name = "label_Version";
            label_Version.Size = new Size(0, 16);
            label_Version.TabIndex = 8;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.BackColor = Color.FromArgb(36, 36, 36);
            label8.ForeColor = Color.Gainsboro;
            label8.Location = new Point(56, 52);
            label8.Name = "label8";
            label8.Size = new Size(55, 16);
            label8.TabIndex = 7;
            label8.Text = "Version:";
            // 
            // label_ConnectedTo
            // 
            label_ConnectedTo.AutoSize = true;
            label_ConnectedTo.BackColor = Color.FromArgb(36, 36, 36);
            label_ConnectedTo.ForeColor = Color.Gainsboro;
            label_ConnectedTo.Location = new Point(117, 36);
            label_ConnectedTo.Name = "label_ConnectedTo";
            label_ConnectedTo.Size = new Size(0, 16);
            label_ConnectedTo.TabIndex = 6;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.FromArgb(36, 36, 36);
            label5.ForeColor = Color.Gainsboro;
            label5.Location = new Point(24, 36);
            label5.Name = "label5";
            label5.Size = new Size(87, 16);
            label5.TabIndex = 5;
            label5.Text = "Connected to:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.BackColor = Color.FromArgb(36, 36, 36);
            label9.Font = new Font("Tahoma", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label9.Location = new Point(24, 9);
            label9.Name = "label9";
            label9.Size = new Size(122, 19);
            label9.TabIndex = 2;
            label9.Text = "Connection Info";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.BackColor = Color.FromArgb(45, 45, 45);
            label13.Font = new Font("Tahoma", 9.75F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label13.Location = new Point(18, 405);
            label13.Name = "label13";
            label13.Size = new Size(352, 16);
            label13.TabIndex = 18;
            label13.Text = "* Only needed if you enabled authentication in Streamer.bot";
            // 
            // checkBox_AutoConnect
            // 
            checkBox_AutoConnect.AutoSize = true;
            checkBox_AutoConnect.Location = new Point(495, 404);
            checkBox_AutoConnect.Name = "checkBox_AutoConnect";
            checkBox_AutoConnect.Size = new Size(148, 20);
            checkBox_AutoConnect.TabIndex = 19;
            checkBox_AutoConnect.Text = "Auto connect on start";
            checkBox_AutoConnect.UseVisualStyleBackColor = true;
            checkBox_AutoConnect.CheckedChanged += checkBox_AutoConnect_CheckedChanged;
            // 
            // PluginConfig
            // 
            AutoScaleDimensions = new SizeF(7F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImageLayout = ImageLayout.Center;
            ClientSize = new Size(664, 464);
            Controls.Add(checkBox_AutoConnect);
            Controls.Add(label13);
            Controls.Add(roundedPanel1);
            Controls.Add(pictureBox1);
            Controls.Add(btn_OK);
            Controls.Add(panel1);
            ForeColor = Color.Gainsboro;
            Name = "PluginConfig";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            roundedPanel1.ResumeLayout(false);
            roundedPanel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private RoundedTextBox address_roundedTextBox;
        private RoundedTextBox port_roundedTextBox;
        private ButtonPrimary btn_OK;
        private ButtonPrimary btn_Connect;
        private RoundedPanel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label7;
        private RoundedTextBox endpoint_roundedTextBox;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private LinkLabel linkLabel1;
        private Label label4;
        private RoundedTextBox password_roundedTextBox;
        private ButtonPrimary showPassword_button;
        private RoundedPanel roundedPanel1;
        private Label label9;
        private Label label_OSVersion;
        private Label label_OS;
        private Label label12;
        private Label label11;
        private Label label_Version;
        private Label label8;
        private Label label_ConnectedTo;
        private Label label5;
        private Label label_GlobalCount;
        private Label label_ActionCount;
        private Label label10;
        private Label label6;
        private Label label13;
        private CheckBox checkBox_AutoConnect;
    }
}
