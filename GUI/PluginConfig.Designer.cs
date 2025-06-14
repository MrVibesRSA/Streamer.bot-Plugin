using SuchByte.MacroDeck.GUI.CustomControls;
using System.Drawing;
using System.Windows.Forms;

namespace MrVibesRSA.StreamerbotPlugin.GUI
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
            TreeNode treeNode3 = new TreeNode("Event");
            TreeNode treeNode4 = new TreeNode("Category", new TreeNode[] { treeNode3 });
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            address_roundedTextBox = new RoundedTextBox();
            port_roundedTextBox = new RoundedTextBox();
            btn_OK = new ButtonPrimary();
            btn_Connect = new ButtonPrimary();
            panel1 = new RoundedPanel();
            checkBox_AutoConnect = new CheckBox();
            showPassword_button = new ButtonPrimary();
            label4 = new Label();
            password_roundedTextBox = new RoundedTextBox();
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
            label_Status = new Label();
            roundedPanel2 = new RoundedPanel();
            button_RemoveProfile = new ButtonPrimary();
            buttonPrimary_EditProfile = new ButtonPrimary();
            buttonPrimary_AddNewProfile = new ButtonPrimary();
            label15 = new Label();
            roundedPanel3 = new RoundedPanel();
            comboBox_SelectedProfile = new System.Windows.Forms.ComboBox();
            linkLabel_ToGithub = new LinkLabel();
            label19 = new Label();
            roundedPanel_Websocket = new RoundedPanel();
            button_WSSettings = new Button();
            button_EventList = new Button();
            roundedPanel_Eventlist = new RoundedPanel();
            roundedPanel8 = new RoundedPanel();
            label16 = new Label();
            roundedPanel7 = new RoundedPanel();
            comboBox_ProfileListForEvents = new System.Windows.Forms.ComboBox();
            roundedPanel6 = new RoundedPanel();
            lbl_Defults = new Label();
            label14 = new Label();
            treeView_Events = new TreeView();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            roundedPanel1.SuspendLayout();
            roundedPanel2.SuspendLayout();
            roundedPanel3.SuspendLayout();
            roundedPanel_Websocket.SuspendLayout();
            roundedPanel_Eventlist.SuspendLayout();
            roundedPanel8.SuspendLayout();
            roundedPanel7.SuspendLayout();
            roundedPanel6.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.FromArgb(36, 36, 36);
            label1.Font = new Font("Tahoma", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(24, 10);
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
            label2.Location = new Point(128, 43);
            label2.Name = "label2";
            label2.Size = new Size(53, 16);
            label2.TabIndex = 4;
            label2.Text = "Address";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.FromArgb(36, 36, 36);
            label3.Location = new Point(151, 73);
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
            address_roundedTextBox.Location = new Point(187, 38);
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
            port_roundedTextBox.Location = new Point(187, 69);
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
            btn_OK.Location = new Point(292, 586);
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
            btn_Connect.Location = new Point(258, 185);
            btn_Connect.Name = "btn_Connect";
            btn_Connect.Progress = 0;
            btn_Connect.ProgressColor = Color.FromArgb(0, 103, 205);
            btn_Connect.Size = new Size(108, 25);
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
            panel1.Controls.Add(checkBox_AutoConnect);
            panel1.Controls.Add(showPassword_button);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(password_roundedTextBox);
            panel1.Controls.Add(endpoint_roundedTextBox);
            panel1.Controls.Add(port_roundedTextBox);
            panel1.Controls.Add(address_roundedTextBox);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(btn_Connect);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(label3);
            panel1.Location = new Point(13, 118);
            panel1.Name = "panel1";
            panel1.Size = new Size(625, 217);
            panel1.TabIndex = 11;
            // 
            // checkBox_AutoConnect
            // 
            checkBox_AutoConnect.AutoSize = true;
            checkBox_AutoConnect.Location = new Point(194, 159);
            checkBox_AutoConnect.Name = "checkBox_AutoConnect";
            checkBox_AutoConnect.Size = new Size(148, 20);
            checkBox_AutoConnect.TabIndex = 19;
            checkBox_AutoConnect.Text = "Auto connect on start";
            checkBox_AutoConnect.UseVisualStyleBackColor = true;
            checkBox_AutoConnect.CheckedChanged += checkBox_AutoConnect_CheckedChanged;
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
            showPassword_button.Location = new Point(431, 131);
            showPassword_button.Name = "showPassword_button";
            showPassword_button.Progress = 0;
            showPassword_button.ProgressColor = Color.FromArgb(65, 65, 65);
            showPassword_button.Size = new Size(54, 25);
            showPassword_button.TabIndex = 10;
            showPassword_button.Text = "Show";
            showPassword_button.UseVisualStyleBackColor = false;
            showPassword_button.UseWindowsAccentColor = true;
            showPassword_button.WriteProgress = true;
            showPassword_button.Click += showPassword_button_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.FromArgb(36, 36, 36);
            label4.Location = new Point(111, 135);
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
            password_roundedTextBox.Location = new Point(187, 131);
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
            // endpoint_roundedTextBox
            // 
            endpoint_roundedTextBox.BackColor = Color.FromArgb(65, 65, 65);
            endpoint_roundedTextBox.Font = new Font("Tahoma", 9F, FontStyle.Italic);
            endpoint_roundedTextBox.Icon = null;
            endpoint_roundedTextBox.Location = new Point(187, 100);
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
            label7.Location = new Point(125, 105);
            label7.Name = "label7";
            label7.Size = new Size(56, 16);
            label7.TabIndex = 12;
            label7.Text = "Endpoint";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.streamerbot_logo_text;
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
            roundedPanel1.Location = new Point(13, 341);
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
            label13.BackColor = Color.FromArgb(65, 65, 65);
            label13.Font = new Font("Tahoma", 9.75F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label13.Location = new Point(13, 441);
            label13.Name = "label13";
            label13.Size = new Size(352, 16);
            label13.TabIndex = 18;
            label13.Text = "* Only needed if you enabled authentication in Streamer.bot";
            // 
            // label_Status
            // 
            label_Status.AutoSize = true;
            label_Status.BackColor = Color.FromArgb(65, 65, 65);
            label_Status.ForeColor = Color.Gainsboro;
            label_Status.ImageAlign = ContentAlignment.MiddleRight;
            label_Status.Location = new Point(535, 441);
            label_Status.Name = "label_Status";
            label_Status.Size = new Size(103, 16);
            label_Status.TabIndex = 19;
            label_Status.Text = "Connected: 0 / 0";
            label_Status.TextAlign = ContentAlignment.MiddleRight;
            // 
            // roundedPanel2
            // 
            roundedPanel2.BackColor = Color.FromArgb(36, 36, 36);
            roundedPanel2.Controls.Add(button_RemoveProfile);
            roundedPanel2.Controls.Add(buttonPrimary_EditProfile);
            roundedPanel2.Controls.Add(buttonPrimary_AddNewProfile);
            roundedPanel2.Controls.Add(label15);
            roundedPanel2.Controls.Add(roundedPanel3);
            roundedPanel2.Controls.Add(linkLabel_ToGithub);
            roundedPanel2.Controls.Add(label19);
            roundedPanel2.Location = new Point(13, 11);
            roundedPanel2.Name = "roundedPanel2";
            roundedPanel2.Size = new Size(625, 101);
            roundedPanel2.TabIndex = 31;
            // 
            // button_RemoveProfile
            // 
            button_RemoveProfile.BorderRadius = 8;
            button_RemoveProfile.FlatAppearance.BorderColor = Color.Cyan;
            button_RemoveProfile.FlatStyle = FlatStyle.Flat;
            button_RemoveProfile.Font = new Font("Tahoma", 9.75F);
            button_RemoveProfile.ForeColor = Color.White;
            button_RemoveProfile.HoverColor = Color.Empty;
            button_RemoveProfile.Icon = null;
            button_RemoveProfile.Location = new Point(348, 38);
            button_RemoveProfile.Name = "button_RemoveProfile";
            button_RemoveProfile.Progress = 0;
            button_RemoveProfile.ProgressColor = Color.FromArgb(0, 103, 205);
            button_RemoveProfile.Size = new Size(75, 25);
            button_RemoveProfile.TabIndex = 30;
            button_RemoveProfile.Text = "Delete";
            button_RemoveProfile.UseVisualStyleBackColor = false;
            button_RemoveProfile.UseWindowsAccentColor = true;
            button_RemoveProfile.WriteProgress = true;
            button_RemoveProfile.Click += button_RemoveProfile_Click;
            // 
            // buttonPrimary_EditProfile
            // 
            buttonPrimary_EditProfile.BorderRadius = 8;
            buttonPrimary_EditProfile.FlatAppearance.BorderColor = Color.Cyan;
            buttonPrimary_EditProfile.FlatStyle = FlatStyle.Flat;
            buttonPrimary_EditProfile.Font = new Font("Tahoma", 9.75F);
            buttonPrimary_EditProfile.ForeColor = Color.White;
            buttonPrimary_EditProfile.HoverColor = Color.Empty;
            buttonPrimary_EditProfile.Icon = null;
            buttonPrimary_EditProfile.Location = new Point(267, 38);
            buttonPrimary_EditProfile.Name = "buttonPrimary_EditProfile";
            buttonPrimary_EditProfile.Progress = 0;
            buttonPrimary_EditProfile.ProgressColor = Color.FromArgb(0, 103, 205);
            buttonPrimary_EditProfile.Size = new Size(75, 25);
            buttonPrimary_EditProfile.TabIndex = 29;
            buttonPrimary_EditProfile.Text = "Edit";
            buttonPrimary_EditProfile.UseVisualStyleBackColor = false;
            buttonPrimary_EditProfile.UseWindowsAccentColor = true;
            buttonPrimary_EditProfile.WriteProgress = true;
            buttonPrimary_EditProfile.Click += buttonPrimary_EditProfile_Click;
            // 
            // buttonPrimary_AddNewProfile
            // 
            buttonPrimary_AddNewProfile.BorderRadius = 8;
            buttonPrimary_AddNewProfile.FlatAppearance.BorderColor = Color.Cyan;
            buttonPrimary_AddNewProfile.FlatStyle = FlatStyle.Flat;
            buttonPrimary_AddNewProfile.Font = new Font("Tahoma", 9.75F);
            buttonPrimary_AddNewProfile.ForeColor = Color.White;
            buttonPrimary_AddNewProfile.HoverColor = Color.Empty;
            buttonPrimary_AddNewProfile.Icon = null;
            buttonPrimary_AddNewProfile.Location = new Point(186, 38);
            buttonPrimary_AddNewProfile.Name = "buttonPrimary_AddNewProfile";
            buttonPrimary_AddNewProfile.Progress = 0;
            buttonPrimary_AddNewProfile.ProgressColor = Color.FromArgb(0, 103, 205);
            buttonPrimary_AddNewProfile.Size = new Size(75, 25);
            buttonPrimary_AddNewProfile.TabIndex = 28;
            buttonPrimary_AddNewProfile.Text = "New";
            buttonPrimary_AddNewProfile.UseVisualStyleBackColor = false;
            buttonPrimary_AddNewProfile.UseWindowsAccentColor = true;
            buttonPrimary_AddNewProfile.WriteProgress = true;
            buttonPrimary_AddNewProfile.Click += buttonPrimary_AddNewProfile_Click;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.BackColor = Color.FromArgb(36, 36, 36);
            label15.ForeColor = Color.Gainsboro;
            label15.Location = new Point(138, 75);
            label15.Name = "label15";
            label15.Size = new Size(43, 16);
            label15.TabIndex = 27;
            label15.Text = "Profile";
            // 
            // roundedPanel3
            // 
            roundedPanel3.BackColor = Color.FromArgb(65, 65, 65);
            roundedPanel3.Controls.Add(comboBox_SelectedProfile);
            roundedPanel3.Location = new Point(187, 69);
            roundedPanel3.Name = "roundedPanel3";
            roundedPanel3.Size = new Size(238, 22);
            roundedPanel3.TabIndex = 24;
            // 
            // comboBox_SelectedProfile
            // 
            comboBox_SelectedProfile.BackColor = Color.FromArgb(65, 65, 65);
            comboBox_SelectedProfile.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox_SelectedProfile.FlatStyle = FlatStyle.Flat;
            comboBox_SelectedProfile.Font = new Font("Tahoma", 9.75F, FontStyle.Italic, GraphicsUnit.Point, 0);
            comboBox_SelectedProfile.ForeColor = Color.Gainsboro;
            comboBox_SelectedProfile.FormattingEnabled = true;
            comboBox_SelectedProfile.Location = new Point(-1, -1);
            comboBox_SelectedProfile.Margin = new Padding(3, 3, 10, 3);
            comboBox_SelectedProfile.Name = "comboBox_SelectedProfile";
            comboBox_SelectedProfile.Size = new Size(240, 24);
            comboBox_SelectedProfile.TabIndex = 20;
            comboBox_SelectedProfile.SelectedIndexChanged += comboBox_SelectedProfile_SelectedIndexChanged;
            // 
            // linkLabel_ToGithub
            // 
            linkLabel_ToGithub.AutoSize = true;
            linkLabel_ToGithub.LinkColor = Color.White;
            linkLabel_ToGithub.Location = new Point(567, 12);
            linkLabel_ToGithub.Name = "linkLabel_ToGithub";
            linkLabel_ToGithub.Size = new Size(53, 16);
            linkLabel_ToGithub.TabIndex = 5;
            linkLabel_ToGithub.TabStop = true;
            linkLabel_ToGithub.Text = "How to?";
            linkLabel_ToGithub.LinkClicked += linkLabel1_LinkClicked;
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.BackColor = Color.FromArgb(36, 36, 36);
            label19.Font = new Font("Tahoma", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label19.Location = new Point(27, 9);
            label19.Name = "label19";
            label19.Size = new Size(138, 19);
            label19.TabIndex = 2;
            label19.Text = "Connection Profile";
            // 
            // roundedPanel_Websocket
            // 
            roundedPanel_Websocket.BackColor = Color.FromArgb(65, 65, 65);
            roundedPanel_Websocket.Controls.Add(label_Status);
            roundedPanel_Websocket.Controls.Add(roundedPanel2);
            roundedPanel_Websocket.Controls.Add(label13);
            roundedPanel_Websocket.Controls.Add(panel1);
            roundedPanel_Websocket.Controls.Add(roundedPanel1);
            roundedPanel_Websocket.Location = new Point(4, 107);
            roundedPanel_Websocket.Name = "roundedPanel_Websocket";
            roundedPanel_Websocket.Size = new Size(654, 464);
            roundedPanel_Websocket.TabIndex = 18;
            // 
            // button_WSSettings
            // 
            button_WSSettings.BackColor = Color.FromArgb(65, 65, 65);
            button_WSSettings.FlatAppearance.BorderColor = Color.FromArgb(65, 65, 65);
            button_WSSettings.FlatStyle = FlatStyle.Flat;
            button_WSSettings.ForeColor = Color.Gainsboro;
            button_WSSettings.Location = new Point(17, 78);
            button_WSSettings.Name = "button_WSSettings";
            button_WSSettings.Size = new Size(165, 31);
            button_WSSettings.TabIndex = 19;
            button_WSSettings.Text = "Websocket Settngs";
            button_WSSettings.UseVisualStyleBackColor = false;
            button_WSSettings.Click += button_WSSettings_Click;
            // 
            // button_EventList
            // 
            button_EventList.BackColor = Color.FromArgb(45, 45, 45);
            button_EventList.FlatAppearance.BorderColor = Color.FromArgb(65, 65, 65);
            button_EventList.FlatStyle = FlatStyle.Flat;
            button_EventList.ForeColor = Color.Gainsboro;
            button_EventList.Location = new Point(182, 78);
            button_EventList.Name = "button_EventList";
            button_EventList.Size = new Size(165, 31);
            button_EventList.TabIndex = 20;
            button_EventList.Text = "Event list";
            button_EventList.UseVisualStyleBackColor = false;
            button_EventList.Visible = false;
            button_EventList.Click += button_EventList_Click;
            // 
            // roundedPanel_Eventlist
            // 
            roundedPanel_Eventlist.BackColor = Color.FromArgb(65, 65, 65);
            roundedPanel_Eventlist.Controls.Add(roundedPanel8);
            roundedPanel_Eventlist.Controls.Add(roundedPanel6);
            roundedPanel_Eventlist.Location = new Point(4, 107);
            roundedPanel_Eventlist.Name = "roundedPanel_Eventlist";
            roundedPanel_Eventlist.Size = new Size(655, 472);
            roundedPanel_Eventlist.TabIndex = 32;
            roundedPanel_Eventlist.Visible = false;
            // 
            // roundedPanel8
            // 
            roundedPanel8.BackColor = Color.FromArgb(36, 36, 36);
            roundedPanel8.Controls.Add(label16);
            roundedPanel8.Controls.Add(roundedPanel7);
            roundedPanel8.Location = new Point(14, 7);
            roundedPanel8.Name = "roundedPanel8";
            roundedPanel8.Size = new Size(625, 40);
            roundedPanel8.TabIndex = 34;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.BackColor = Color.FromArgb(36, 36, 36);
            label16.ForeColor = Color.Gainsboro;
            label16.Location = new Point(17, 12);
            label16.Name = "label16";
            label16.Size = new Size(113, 16);
            label16.TabIndex = 35;
            label16.Text = "Connected Profiles";
            // 
            // roundedPanel7
            // 
            roundedPanel7.BackColor = Color.FromArgb(65, 65, 65);
            roundedPanel7.Controls.Add(comboBox_ProfileListForEvents);
            roundedPanel7.Location = new Point(136, 10);
            roundedPanel7.Name = "roundedPanel7";
            roundedPanel7.Size = new Size(238, 22);
            roundedPanel7.TabIndex = 34;
            // 
            // comboBox_ProfileListForEvents
            // 
            comboBox_ProfileListForEvents.BackColor = Color.FromArgb(65, 65, 65);
            comboBox_ProfileListForEvents.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox_ProfileListForEvents.FlatStyle = FlatStyle.Flat;
            comboBox_ProfileListForEvents.Font = new Font("Tahoma", 9.75F, FontStyle.Italic, GraphicsUnit.Point, 0);
            comboBox_ProfileListForEvents.ForeColor = Color.Gainsboro;
            comboBox_ProfileListForEvents.FormattingEnabled = true;
            comboBox_ProfileListForEvents.Location = new Point(-3, -1);
            comboBox_ProfileListForEvents.Margin = new Padding(3, 3, 10, 3);
            comboBox_ProfileListForEvents.Name = "comboBox_ProfileListForEvents";
            comboBox_ProfileListForEvents.Size = new Size(240, 24);
            comboBox_ProfileListForEvents.TabIndex = 20;
            comboBox_ProfileListForEvents.SelectedIndexChanged += comboBox_ProfileListForEvents_SelectedIndexChanged;
            // 
            // roundedPanel6
            // 
            roundedPanel6.BackColor = Color.FromArgb(36, 36, 36);
            roundedPanel6.Controls.Add(lbl_Defults);
            roundedPanel6.Controls.Add(label14);
            roundedPanel6.Controls.Add(treeView_Events);
            roundedPanel6.Location = new Point(14, 51);
            roundedPanel6.Name = "roundedPanel6";
            roundedPanel6.Size = new Size(625, 413);
            roundedPanel6.TabIndex = 33;
            // 
            // lbl_Defults
            // 
            lbl_Defults.AutoSize = true;
            lbl_Defults.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbl_Defults.Location = new Point(23, 377);
            lbl_Defults.Name = "lbl_Defults";
            lbl_Defults.Size = new Size(299, 13);
            lbl_Defults.TabIndex = 4;
            lbl_Defults.Text = "Defaults: Action Added, Updated, Deleted. Glogals Updated.";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.BackColor = Color.FromArgb(36, 36, 36);
            label14.Font = new Font("Tahoma", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label14.Location = new Point(8, 11);
            label14.Name = "label14";
            label14.Size = new Size(147, 19);
            label14.TabIndex = 3;
            label14.Text = "Subscribe to Events";
            // 
            // treeView_Events
            // 
            treeView_Events.BackColor = Color.FromArgb(36, 36, 36);
            treeView_Events.BorderStyle = BorderStyle.FixedSingle;
            treeView_Events.CheckBoxes = true;
            treeView_Events.ForeColor = Color.Gainsboro;
            treeView_Events.LineColor = Color.FromArgb(65, 65, 65);
            treeView_Events.Location = new Point(17, 35);
            treeView_Events.Name = "treeView_Events";
            treeNode3.Name = "Event";
            treeNode3.Text = "Event";
            treeNode4.Name = "Category";
            treeNode4.Text = "Category";
            treeView_Events.Nodes.AddRange(new TreeNode[] { treeNode4 });
            treeView_Events.Size = new Size(594, 339);
            treeView_Events.TabIndex = 0;
            treeView_Events.AfterCheck += treeView_Events_AfterCheck;
            // 
            // PluginConfig
            // 
            AutoScaleDimensions = new SizeF(7F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImageLayout = ImageLayout.Center;
            ClientSize = new Size(662, 617);
            Controls.Add(roundedPanel_Eventlist);
            Controls.Add(pictureBox1);
            Controls.Add(btn_OK);
            Controls.Add(roundedPanel_Websocket);
            Controls.Add(button_EventList);
            Controls.Add(button_WSSettings);
            ForeColor = Color.Gainsboro;
            Name = "PluginConfig";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            roundedPanel1.ResumeLayout(false);
            roundedPanel1.PerformLayout();
            roundedPanel2.ResumeLayout(false);
            roundedPanel2.PerformLayout();
            roundedPanel3.ResumeLayout(false);
            roundedPanel_Websocket.ResumeLayout(false);
            roundedPanel_Websocket.PerformLayout();
            roundedPanel_Eventlist.ResumeLayout(false);
            roundedPanel8.ResumeLayout(false);
            roundedPanel8.PerformLayout();
            roundedPanel7.ResumeLayout(false);
            roundedPanel6.ResumeLayout(false);
            roundedPanel6.PerformLayout();
            ResumeLayout(false);
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
        private Label label_Status;
        private RoundedPanel roundedPanel2;
        private ButtonPrimary button_RemoveProfile;
        private ButtonPrimary buttonPrimary_EditProfile;
        private ButtonPrimary buttonPrimary_AddNewProfile;
        private Label label15;
        private RoundedPanel roundedPanel3;
        private System.Windows.Forms.ComboBox comboBox_SelectedProfile;
        private LinkLabel linkLabel_ToGithub;
        private Label label19;
        private RoundedPanel roundedPanel_Websocket;
        private Button button_WSSettings;
        private Button button_EventList;
        private RoundedPanel roundedPanel_Eventlist;
        private RoundedPanel roundedPanel7;
        private System.Windows.Forms.ComboBox comboBox_ProfileListForEvents;
        private RoundedPanel roundedPanel8;
        private Label label16;
        private RoundedPanel roundedPanel6;
        private Label label14;
        private TreeView treeView_Events;
        private Label lbl_Defults;
    }
}
