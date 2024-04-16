

using SuchByte.MacroDeck.GUI.CustomControls;
using System.Drawing;
using System.Windows.Forms;

namespace MrVibes_RSA.StreamerbotPlugin.GUI
{
    partial class StreamerBotActionConfigurator
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
            comboBox_ActionList = new RoundedComboBox();
            label1 = new Label();
            label2 = new Label();
            textBox_Arguments = new RoundedTextBox();
            btn_Refresh = new ButtonPrimary();
            panel1 = new RoundedPanel();
            label3 = new Label();
            roundedPanel1 = new RoundedPanel();
            label_subactionCount = new Label();
            label_actionEnabled = new Label();
            label_actionGroup = new Label();
            label_actionName = new Label();
            label_actionId = new Label();
            label10 = new Label();
            label9 = new Label();
            label8 = new Label();
            label7 = new Label();
            label5 = new Label();
            label4 = new Label();
            label6 = new Label();
            panel1.SuspendLayout();
            roundedPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // comboBox_ActionList
            // 
            comboBox_ActionList.BackColor = Color.FromArgb(65, 65, 65);
            comboBox_ActionList.DropDownStyle = ComboBoxStyle.DropDown;
            comboBox_ActionList.Font = new Font("Tahoma", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBox_ActionList.Icon = null;
            comboBox_ActionList.Location = new Point(23, 37);
            comboBox_ActionList.Name = "comboBox_ActionList";
            comboBox_ActionList.Padding = new Padding(8, 2, 8, 2);
            comboBox_ActionList.SelectedIndex = -1;
            comboBox_ActionList.SelectedItem = null;
            comboBox_ActionList.Size = new Size(662, 28);
            comboBox_ActionList.TabIndex = 1;
            comboBox_ActionList.SelectedIndexChanged += comboBox_ActionList_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.FromArgb(36, 36, 36);
            label1.Location = new Point(23, 11);
            label1.Name = "label1";
            label1.Size = new Size(117, 23);
            label1.TabIndex = 2;
            label1.Text = "Select Action";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.FromArgb(36, 36, 36);
            label2.Location = new Point(23, 66);
            label2.Name = "label2";
            label2.Size = new Size(56, 23);
            label2.TabIndex = 4;
            label2.Text = "Value";
            // 
            // textBox_Arguments
            // 
            textBox_Arguments.BackColor = Color.FromArgb(65, 65, 65);
            textBox_Arguments.Font = new Font("Tahoma", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox_Arguments.Icon = null;
            textBox_Arguments.Location = new Point(24, 92);
            textBox_Arguments.MaxCharacters = 32767;
            textBox_Arguments.Multiline = false;
            textBox_Arguments.Name = "textBox_Arguments";
            textBox_Arguments.Padding = new Padding(8, 5, 8, 5);
            textBox_Arguments.PasswordChar = false;
            textBox_Arguments.PlaceHolderColor = Color.Gray;
            textBox_Arguments.PlaceHolderText = "";
            textBox_Arguments.ReadOnly = false;
            textBox_Arguments.ScrollBars = ScrollBars.None;
            textBox_Arguments.SelectionStart = 0;
            textBox_Arguments.Size = new Size(742, 27);
            textBox_Arguments.TabIndex = 7;
            textBox_Arguments.TextAlignment = HorizontalAlignment.Left;
            // 
            // btn_Refresh
            // 
            btn_Refresh.BorderRadius = 8;
            btn_Refresh.FlatStyle = FlatStyle.Flat;
            btn_Refresh.Font = new Font("Tahoma", 9.75F);
            btn_Refresh.ForeColor = Color.White;
            btn_Refresh.HoverColor = Color.Empty;
            btn_Refresh.Icon = null;
            btn_Refresh.Location = new Point(691, 37);
            btn_Refresh.Name = "btn_Refresh";
            btn_Refresh.Progress = 0;
            btn_Refresh.ProgressColor = Color.FromArgb(0, 103, 205);
            btn_Refresh.Size = new Size(75, 23);
            btn_Refresh.TabIndex = 8;
            btn_Refresh.Text = "Refresh";
            btn_Refresh.UseVisualStyleBackColor = true;
            btn_Refresh.UseWindowsAccentColor = true;
            btn_Refresh.WriteProgress = true;
            btn_Refresh.Click += btn_Refresh_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(36, 36, 36);
            panel1.Controls.Add(textBox_Arguments);
            panel1.Controls.Add(btn_Refresh);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(comboBox_ActionList);
            panel1.Controls.Add(label1);
            panel1.Location = new Point(33, 56);
            panel1.Name = "panel1";
            panel1.Size = new Size(782, 134);
            panel1.TabIndex = 12;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Tahoma", 9.75F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label3.Location = new Point(76, 72);
            label3.Name = "label3";
            label3.Size = new Size(64, 16);
            label3.TabIndex = 11;
            label3.Text = "(Optional)";
            // 
            // roundedPanel1
            // 
            roundedPanel1.BackColor = Color.FromArgb(36, 36, 36);
            roundedPanel1.Controls.Add(label_subactionCount);
            roundedPanel1.Controls.Add(label_actionEnabled);
            roundedPanel1.Controls.Add(label_actionGroup);
            roundedPanel1.Controls.Add(label_actionName);
            roundedPanel1.Controls.Add(label_actionId);
            roundedPanel1.Controls.Add(label10);
            roundedPanel1.Controls.Add(label9);
            roundedPanel1.Controls.Add(label8);
            roundedPanel1.Controls.Add(label7);
            roundedPanel1.Controls.Add(label5);
            roundedPanel1.Controls.Add(label4);
            roundedPanel1.Controls.Add(label6);
            roundedPanel1.Location = new Point(33, 198);
            roundedPanel1.Name = "roundedPanel1";
            roundedPanel1.Size = new Size(782, 140);
            roundedPanel1.TabIndex = 13;
            // 
            // label_subactionCount
            // 
            label_subactionCount.AutoSize = true;
            label_subactionCount.Font = new Font("Tahoma", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_subactionCount.Location = new Point(161, 106);
            label_subactionCount.Name = "label_subactionCount";
            label_subactionCount.Size = new Size(0, 16);
            label_subactionCount.TabIndex = 21;
            // 
            // label_actionEnabled
            // 
            label_actionEnabled.AutoSize = true;
            label_actionEnabled.Font = new Font("Tahoma", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_actionEnabled.Location = new Point(161, 90);
            label_actionEnabled.Name = "label_actionEnabled";
            label_actionEnabled.Size = new Size(0, 16);
            label_actionEnabled.TabIndex = 20;
            // 
            // label_actionGroup
            // 
            label_actionGroup.AutoSize = true;
            label_actionGroup.Font = new Font("Tahoma", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_actionGroup.Location = new Point(161, 74);
            label_actionGroup.Name = "label_actionGroup";
            label_actionGroup.Size = new Size(0, 16);
            label_actionGroup.TabIndex = 19;
            // 
            // label_actionName
            // 
            label_actionName.AutoSize = true;
            label_actionName.Font = new Font("Tahoma", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_actionName.Location = new Point(161, 58);
            label_actionName.Name = "label_actionName";
            label_actionName.Size = new Size(0, 16);
            label_actionName.TabIndex = 18;
            // 
            // label_actionId
            // 
            label_actionId.AutoSize = true;
            label_actionId.Font = new Font("Tahoma", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_actionId.Location = new Point(161, 42);
            label_actionId.Name = "label_actionId";
            label_actionId.Size = new Size(0, 16);
            label_actionId.TabIndex = 17;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Tahoma", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label10.Location = new Point(24, 106);
            label10.Name = "label10";
            label10.Size = new Size(105, 16);
            label10.TabIndex = 16;
            label10.Text = "Subaction Count:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Tahoma", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label9.Location = new Point(23, 90);
            label9.Name = "label9";
            label9.Size = new Size(96, 16);
            label9.TabIndex = 15;
            label9.Text = "Action Enabled:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Tahoma", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label8.Location = new Point(24, 74);
            label8.Name = "label8";
            label8.Size = new Size(85, 16);
            label8.TabIndex = 14;
            label8.Text = "Action Group:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Tahoma", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.Location = new Point(24, 58);
            label7.Name = "label7";
            label7.Size = new Size(84, 16);
            label7.TabIndex = 13;
            label7.Text = "Action Name:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Tahoma", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(24, 42);
            label5.Name = "label5";
            label5.Size = new Size(63, 16);
            label5.TabIndex = 12;
            label5.Text = "Action ID:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.FromArgb(36, 36, 36);
            label4.Location = new Point(23, 9);
            label4.Name = "label4";
            label4.Size = new Size(101, 23);
            label4.TabIndex = 11;
            label4.Text = "Action Info";
            // 
            // label6
            // 
            label6.Location = new Point(79, 120);
            label6.Name = "label6";
            label6.RightToLeft = RightToLeft.No;
            label6.Size = new Size(156, 16);
            label6.TabIndex = 10;
            label6.TextAlign = ContentAlignment.MiddleCenter;
            label6.UseMnemonic = false;
            // 
            // StreamerBotActionConfigurator
            // 
            AutoScaleDimensions = new SizeF(10F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(roundedPanel1);
            Controls.Add(panel1);
            Name = "StreamerBotActionConfigurator";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            roundedPanel1.ResumeLayout(false);
            roundedPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private RoundedComboBox comboBox_ActionList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private RoundedTextBox textBox_Arguments;
        private ButtonPrimary btn_Refresh;
        private RoundedPanel panel1;
        private Label label3;
        private RoundedPanel roundedPanel1;
        private Label label4;
        private Label label6;
        private Label label7;
        private Label label5;
        private Label label_actionId;
        private Label label10;
        private Label label9;
        private Label label8;
        private Label label_subactionCount;
        private Label label_actionEnabled;
        private Label label_actionGroup;
        private Label label_actionName;
    }
}
