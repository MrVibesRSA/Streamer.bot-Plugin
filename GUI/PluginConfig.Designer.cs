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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            textBox_Address = new RoundedTextBox();
            textBox_Port = new RoundedTextBox();
            btn_OK = new ButtonPrimary();
            btn_Connect = new ButtonPrimary();
            panel1 = new RoundedPanel();
            linkLabel1 = new LinkLabel();
            textBox_Endpoint = new RoundedTextBox();
            label4 = new Label();
            label7 = new Label();
            pictureBox1 = new PictureBox();
            roundedPanel2 = new RoundedPanel();
            label6 = new Label();
            dataGridView1 = new DataGridView();
            roundedPanel1 = new RoundedPanel();
            label5 = new Label();
            roundedTextBox3 = new RoundedTextBox();
            buttonPrimary1 = new ButtonPrimary();
            checkboxColumn = new DataGridViewCheckBoxColumn();
            VariableName = new DataGridViewTextBoxColumn();
            VariableValue = new DataGridViewTextBoxColumn();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            roundedPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            roundedPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.FromArgb(36, 36, 36);
            label1.Font = new Font("Tahoma", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(24, 4);
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
            label2.Location = new Point(24, 25);
            label2.Name = "label2";
            label2.Size = new Size(53, 16);
            label2.TabIndex = 4;
            label2.Text = "Address";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.FromArgb(36, 36, 36);
            label3.Location = new Point(268, 25);
            label3.Name = "label3";
            label3.Size = new Size(30, 16);
            label3.TabIndex = 5;
            label3.Text = "Port";
            // 
            // textBox_Address
            // 
            textBox_Address.BackColor = Color.FromArgb(65, 65, 65);
            textBox_Address.Font = new Font("Tahoma", 9F);
            textBox_Address.Icon = null;
            textBox_Address.Location = new Point(24, 42);
            textBox_Address.MaxCharacters = 32767;
            textBox_Address.Multiline = false;
            textBox_Address.Name = "textBox_Address";
            textBox_Address.Padding = new Padding(8, 5, 8, 5);
            textBox_Address.PasswordChar = false;
            textBox_Address.PlaceHolderColor = Color.Gray;
            textBox_Address.PlaceHolderText = "";
            textBox_Address.ReadOnly = false;
            textBox_Address.ScrollBars = ScrollBars.None;
            textBox_Address.SelectionStart = 0;
            textBox_Address.Size = new Size(238, 25);
            textBox_Address.TabIndex = 6;
            textBox_Address.TextAlignment = HorizontalAlignment.Left;
            // 
            // textBox_Port
            // 
            textBox_Port.BackColor = Color.FromArgb(65, 65, 65);
            textBox_Port.Font = new Font("Tahoma", 9F);
            textBox_Port.Icon = null;
            textBox_Port.Location = new Point(268, 42);
            textBox_Port.MaxCharacters = 32767;
            textBox_Port.Multiline = false;
            textBox_Port.Name = "textBox_Port";
            textBox_Port.Padding = new Padding(8, 5, 8, 5);
            textBox_Port.PasswordChar = false;
            textBox_Port.PlaceHolderColor = Color.Gray;
            textBox_Port.PlaceHolderText = "";
            textBox_Port.ReadOnly = false;
            textBox_Port.ScrollBars = ScrollBars.None;
            textBox_Port.SelectionStart = 0;
            textBox_Port.Size = new Size(109, 25);
            textBox_Port.TabIndex = 7;
            textBox_Port.TextAlignment = HorizontalAlignment.Left;
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
            btn_OK.Location = new Point(293, 490);
            btn_OK.Name = "btn_OK";
            btn_OK.Progress = 0;
            btn_OK.ProgressColor = Color.FromArgb(0, 103, 205);
            btn_OK.Size = new Size(75, 23);
            btn_OK.TabIndex = 8;
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
            btn_Connect.Location = new Point(500, 43);
            btn_Connect.Name = "btn_Connect";
            btn_Connect.Progress = 0;
            btn_Connect.ProgressColor = Color.FromArgb(0, 103, 205);
            btn_Connect.Size = new Size(108, 23);
            btn_Connect.TabIndex = 9;
            btn_Connect.Text = "Connect";
            btn_Connect.UseVisualStyleBackColor = false;
            btn_Connect.UseWindowsAccentColor = true;
            btn_Connect.WriteProgress = true;
            btn_Connect.Click += btn_Connect_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(36, 36, 36);
            panel1.Controls.Add(linkLabel1);
            panel1.Controls.Add(textBox_Endpoint);
            panel1.Controls.Add(textBox_Port);
            panel1.Controls.Add(textBox_Address);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(btn_Connect);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(label3);
            panel1.Location = new Point(18, 71);
            panel1.Name = "panel1";
            panel1.Size = new Size(625, 76);
            panel1.TabIndex = 11;
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.LinkColor = Color.White;
            linkLabel1.Location = new Point(554, 7);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(53, 16);
            linkLabel1.TabIndex = 14;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "How to?";
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // textBox_Endpoint
            // 
            textBox_Endpoint.BackColor = Color.FromArgb(65, 65, 65);
            textBox_Endpoint.Font = new Font("Tahoma", 9F);
            textBox_Endpoint.Icon = null;
            textBox_Endpoint.Location = new Point(378, 42);
            textBox_Endpoint.MaxCharacters = 32767;
            textBox_Endpoint.Multiline = false;
            textBox_Endpoint.Name = "textBox_Endpoint";
            textBox_Endpoint.Padding = new Padding(8, 5, 8, 5);
            textBox_Endpoint.PasswordChar = false;
            textBox_Endpoint.PlaceHolderColor = Color.Gray;
            textBox_Endpoint.PlaceHolderText = "";
            textBox_Endpoint.ReadOnly = false;
            textBox_Endpoint.ScrollBars = ScrollBars.None;
            textBox_Endpoint.SelectionStart = 0;
            textBox_Endpoint.Size = new Size(116, 25);
            textBox_Endpoint.TabIndex = 11;
            textBox_Endpoint.TextAlignment = HorizontalAlignment.Left;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.FromArgb(36, 36, 36);
            label4.Location = new Point(260, 46);
            label4.Name = "label4";
            label4.Size = new Size(12, 16);
            label4.TabIndex = 13;
            label4.Text = ":";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = Color.FromArgb(36, 36, 36);
            label7.Location = new Point(378, 25);
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
            // roundedPanel2
            // 
            roundedPanel2.BackColor = Color.FromArgb(36, 36, 36);
            roundedPanel2.Controls.Add(label6);
            roundedPanel2.Controls.Add(dataGridView1);
            roundedPanel2.Location = new Point(18, 151);
            roundedPanel2.Name = "roundedPanel2";
            roundedPanel2.Size = new Size(625, 271);
            roundedPanel2.TabIndex = 13;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = Color.FromArgb(36, 36, 36);
            label6.Font = new Font("Tahoma", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.Location = new Point(23, 4);
            label6.Name = "label6";
            label6.Size = new Size(119, 19);
            label6.TabIndex = 3;
            label6.Text = "Gobal Variables";
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.BackgroundColor = Color.FromArgb(35, 35, 35);
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(35, 35, 35);
            dataGridViewCellStyle1.Font = new Font("Tahoma", 9.75F);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { checkboxColumn, VariableName, VariableValue });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(86, 86, 86);
            dataGridViewCellStyle2.Font = new Font("Tahoma", 9.75F);
            dataGridViewCellStyle2.ForeColor = Color.Gainsboro;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.GridColor = Color.FromArgb(45, 45, 45);
            dataGridView1.Location = new Point(23, 26);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(584, 238);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // roundedPanel1
            // 
            roundedPanel1.BackColor = Color.FromArgb(36, 36, 36);
            roundedPanel1.Controls.Add(label5);
            roundedPanel1.Controls.Add(roundedTextBox3);
            roundedPanel1.Controls.Add(buttonPrimary1);
            roundedPanel1.Location = new Point(18, 427);
            roundedPanel1.Name = "roundedPanel1";
            roundedPanel1.Size = new Size(625, 59);
            roundedPanel1.TabIndex = 14;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.FromArgb(36, 36, 36);
            label5.Font = new Font("Tahoma", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(24, 5);
            label5.Name = "label5";
            label5.Size = new Size(149, 19);
            label5.TabIndex = 10;
            label5.Text = "Action Import Code";
            // 
            // roundedTextBox3
            // 
            roundedTextBox3.BackColor = Color.FromArgb(65, 65, 65);
            roundedTextBox3.Enabled = false;
            roundedTextBox3.Font = new Font("Tahoma", 9F);
            roundedTextBox3.Icon = null;
            roundedTextBox3.Location = new Point(24, 25);
            roundedTextBox3.MaxCharacters = 32767;
            roundedTextBox3.Multiline = false;
            roundedTextBox3.Name = "roundedTextBox3";
            roundedTextBox3.Padding = new Padding(8, 5, 8, 5);
            roundedTextBox3.PasswordChar = false;
            roundedTextBox3.PlaceHolderColor = Color.Gray;
            roundedTextBox3.PlaceHolderText = "";
            roundedTextBox3.ReadOnly = false;
            roundedTextBox3.ScrollBars = ScrollBars.None;
            roundedTextBox3.SelectionStart = 0;
            roundedTextBox3.Size = new Size(470, 25);
            roundedTextBox3.TabIndex = 6;
            roundedTextBox3.TextAlignment = HorizontalAlignment.Left;
            // 
            // buttonPrimary1
            // 
            buttonPrimary1.BorderRadius = 8;
            buttonPrimary1.FlatAppearance.BorderColor = Color.Cyan;
            buttonPrimary1.FlatStyle = FlatStyle.Flat;
            buttonPrimary1.Font = new Font("Tahoma", 9.75F);
            buttonPrimary1.ForeColor = Color.White;
            buttonPrimary1.HoverColor = Color.Empty;
            buttonPrimary1.Icon = null;
            buttonPrimary1.Location = new Point(499, 26);
            buttonPrimary1.Name = "buttonPrimary1";
            buttonPrimary1.Progress = 0;
            buttonPrimary1.ProgressColor = Color.FromArgb(0, 103, 205);
            buttonPrimary1.Size = new Size(108, 23);
            buttonPrimary1.TabIndex = 9;
            buttonPrimary1.Text = "Copy Code";
            buttonPrimary1.UseVisualStyleBackColor = false;
            buttonPrimary1.UseWindowsAccentColor = true;
            buttonPrimary1.WriteProgress = true;
            buttonPrimary1.Click += buttonPrimary1_Click;
            // 
            // checkboxColumn
            // 
            checkboxColumn.HeaderText = "Use";
            checkboxColumn.Name = "checkboxColumn";
            checkboxColumn.Width = 50;
            // 
            // VariableName
            // 
            VariableName.HeaderText = "Variable Name";
            VariableName.Name = "VariableName";
            VariableName.ReadOnly = true;
            VariableName.Width = 200;
            // 
            // VariableValue
            // 
            VariableValue.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            VariableValue.HeaderText = "Variable Value";
            VariableValue.Name = "VariableValue";
            VariableValue.ReadOnly = true;
            // 
            // PluginConfig
            // 
            AutoScaleDimensions = new SizeF(7F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(664, 519);
            Controls.Add(roundedPanel1);
            Controls.Add(roundedPanel2);
            Controls.Add(pictureBox1);
            Controls.Add(btn_OK);
            Controls.Add(panel1);
            ForeColor = Color.Gainsboro;
            Location = new Point(0, 0);
            Name = "PluginConfig";
            Controls.SetChildIndex(panel1, 0);
            Controls.SetChildIndex(btn_OK, 0);
            Controls.SetChildIndex(pictureBox1, 0);
            Controls.SetChildIndex(roundedPanel2, 0);
            Controls.SetChildIndex(roundedPanel1, 0);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            roundedPanel2.ResumeLayout(false);
            roundedPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            roundedPanel1.ResumeLayout(false);
            roundedPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private RoundedTextBox textBox_Address;
        private RoundedTextBox textBox_Port;
        private ButtonPrimary btn_OK;
        private ButtonPrimary btn_Connect;
        private RoundedPanel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label7;
        private RoundedTextBox textBox_Endpoint;
        private RoundedPanel roundedPanel2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private Label label4;
        private Label label6;
        private RoundedPanel roundedPanel1;
        private RoundedTextBox roundedTextBox3;
        private ButtonPrimary buttonPrimary1;
        private Label label5;
        private LinkLabel linkLabel1;
        private DataGridViewCheckBoxColumn checkboxColumn;
        private DataGridViewTextBoxColumn VariableName;
        private DataGridViewTextBoxColumn VariableValue;
    }
}
