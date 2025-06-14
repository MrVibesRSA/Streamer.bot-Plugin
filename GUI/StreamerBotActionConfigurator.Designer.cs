

using SuchByte.MacroDeck.GUI.CustomControls;
using System.Drawing;
using System.Windows.Forms;

namespace MrVibesRSA.StreamerbotPlugin.GUI
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
            components = new System.ComponentModel.Container();
            roundedPanel_Actions = new RoundedPanel();
            roundedPanel2 = new RoundedPanel();
            comboBox_SelectedProfile = new System.Windows.Forms.ComboBox();
            label5 = new Label();
            roundedPanel1 = new RoundedPanel();
            textBox = new TextBox();
            contextMenu_JsonTextBox = new ContextMenuStrip(components);
            copyToolStripMenuItem = new ToolStripMenuItem();
            pastToolStripMenuItem = new ToolStripMenuItem();
            selectAllToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator3 = new ToolStripSeparator();
            formatJsonToolStripMenuItem = new ToolStripMenuItem();
            validateJsonToolStripMenuItem = new ToolStripMenuItem();
            btn_Connect = new ButtonPrimary();
            label_Argument = new Label();
            roundedPanel_ActionCombobox = new RoundedPanel();
            comboBox_ActionList = new System.Windows.Forms.ComboBox();
            roundedPanel_ActioGroupCombobox = new RoundedPanel();
            comboBox_ActionGroup = new System.Windows.Forms.ComboBox();
            label_Action = new Label();
            label_ActionGroup = new Label();
            errorPanel = new RoundedPanel();
            label_Error = new Label();
            roundedPanel_ActionInfo = new RoundedPanel();
            label_triggerCount = new Label();
            label_subactionCount = new Label();
            label_actionEnabled = new Label();
            label_actionId = new Label();
            label4 = new Label();
            label_Subactioncout_Text = new Label();
            label_AnctionEnables_Text = new Label();
            label_AntionID_Text = new Label();
            label1 = new Label();
            roundedPanel_Actions.SuspendLayout();
            roundedPanel2.SuspendLayout();
            roundedPanel1.SuspendLayout();
            contextMenu_JsonTextBox.SuspendLayout();
            roundedPanel_ActionCombobox.SuspendLayout();
            roundedPanel_ActioGroupCombobox.SuspendLayout();
            errorPanel.SuspendLayout();
            roundedPanel_ActionInfo.SuspendLayout();
            SuspendLayout();
            // 
            // roundedPanel_Actions
            // 
            roundedPanel_Actions.BackColor = Color.FromArgb(36, 36, 36);
            roundedPanel_Actions.Controls.Add(roundedPanel2);
            roundedPanel_Actions.Controls.Add(label5);
            roundedPanel_Actions.Controls.Add(roundedPanel1);
            roundedPanel_Actions.Controls.Add(btn_Connect);
            roundedPanel_Actions.Controls.Add(label_Argument);
            roundedPanel_Actions.Controls.Add(roundedPanel_ActionCombobox);
            roundedPanel_Actions.Controls.Add(roundedPanel_ActioGroupCombobox);
            roundedPanel_Actions.Controls.Add(label_Action);
            roundedPanel_Actions.Controls.Add(label_ActionGroup);
            roundedPanel_Actions.Location = new Point(18, 6);
            roundedPanel_Actions.Name = "roundedPanel_Actions";
            roundedPanel_Actions.Size = new Size(820, 323);
            roundedPanel_Actions.TabIndex = 18;
            // 
            // roundedPanel2
            // 
            roundedPanel2.BackColor = Color.FromArgb(65, 65, 65);
            roundedPanel2.Controls.Add(comboBox_SelectedProfile);
            roundedPanel2.Location = new Point(20, 31);
            roundedPanel2.Name = "roundedPanel2";
            roundedPanel2.Size = new Size(336, 22);
            roundedPanel2.TabIndex = 26;
            // 
            // comboBox_SelectedProfile
            // 
            comboBox_SelectedProfile.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox_SelectedProfile.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBox_SelectedProfile.BackColor = Color.FromArgb(65, 65, 65);
            comboBox_SelectedProfile.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox_SelectedProfile.FlatStyle = FlatStyle.Flat;
            comboBox_SelectedProfile.Font = new Font("Tahoma", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBox_SelectedProfile.ForeColor = Color.Gainsboro;
            comboBox_SelectedProfile.FormattingEnabled = true;
            comboBox_SelectedProfile.Location = new Point(-2, -1);
            comboBox_SelectedProfile.Margin = new Padding(3, 3, 10, 3);
            comboBox_SelectedProfile.Name = "comboBox_SelectedProfile";
            comboBox_SelectedProfile.Size = new Size(337, 24);
            comboBox_SelectedProfile.TabIndex = 20;
            comboBox_SelectedProfile.SelectedIndexChanged += comboBox_SelectedProfile_SelectedIndexChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.FromArgb(36, 36, 36);
            label5.Font = new Font("Tahoma", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.Gainsboro;
            label5.Location = new Point(20, 7);
            label5.Name = "label5";
            label5.Size = new Size(54, 19);
            label5.TabIndex = 25;
            label5.Text = "Profile";
            // 
            // roundedPanel1
            // 
            roundedPanel1.BackColor = Color.FromArgb(65, 65, 65);
            roundedPanel1.Controls.Add(textBox);
            roundedPanel1.ForeColor = Color.Gainsboro;
            roundedPanel1.Location = new Point(20, 131);
            roundedPanel1.Name = "roundedPanel1";
            roundedPanel1.Size = new Size(781, 182);
            roundedPanel1.TabIndex = 24;
            // 
            // textBox
            // 
            textBox.AcceptsReturn = true;
            textBox.AcceptsTab = true;
            textBox.BackColor = Color.FromArgb(65, 65, 65);
            textBox.BorderStyle = BorderStyle.None;
            textBox.ContextMenuStrip = contextMenu_JsonTextBox;
            textBox.Font = new Font("Tahoma", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox.ForeColor = Color.Gainsboro;
            textBox.Location = new Point(6, 5);
            textBox.Multiline = true;
            textBox.Name = "textBox";
            textBox.PlaceholderText = "*Arguments are passed to Streamer.bot as string and can be used using the %value% variable.\n   • Example 1: #002564 (plain text)\n   • Example 2: { \"json\":  \"format\"}";
            textBox.ScrollBars = ScrollBars.Both;
            textBox.Size = new Size(794, 212);
            textBox.TabIndex = 20;
            textBox.WordWrap = false;
            // 
            // contextMenu_JsonTextBox
            // 
            contextMenu_JsonTextBox.Items.AddRange(new ToolStripItem[] { copyToolStripMenuItem, pastToolStripMenuItem, selectAllToolStripMenuItem, toolStripSeparator3, formatJsonToolStripMenuItem, validateJsonToolStripMenuItem });
            contextMenu_JsonTextBox.Name = "contextMenu_JsonTextBox";
            contextMenu_JsonTextBox.Size = new Size(142, 120);
            // 
            // copyToolStripMenuItem
            // 
            copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            copyToolStripMenuItem.Size = new Size(141, 22);
            copyToolStripMenuItem.Text = "Copy";
            copyToolStripMenuItem.Click += copyToolStripMenuItem_Click;
            // 
            // pastToolStripMenuItem
            // 
            pastToolStripMenuItem.Name = "pastToolStripMenuItem";
            pastToolStripMenuItem.Size = new Size(141, 22);
            pastToolStripMenuItem.Text = "Past";
            pastToolStripMenuItem.Click += pastToolStripMenuItem_Click;
            // 
            // selectAllToolStripMenuItem
            // 
            selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            selectAllToolStripMenuItem.Size = new Size(141, 22);
            selectAllToolStripMenuItem.Text = "Select All";
            selectAllToolStripMenuItem.Click += selectAllToolStripMenuItem_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(138, 6);
            // 
            // formatJsonToolStripMenuItem
            // 
            formatJsonToolStripMenuItem.Name = "formatJsonToolStripMenuItem";
            formatJsonToolStripMenuItem.Size = new Size(141, 22);
            formatJsonToolStripMenuItem.Text = "Format Json";
            formatJsonToolStripMenuItem.Click += formatJsonToolStripMenuItem_Click;
            // 
            // validateJsonToolStripMenuItem
            // 
            validateJsonToolStripMenuItem.Name = "validateJsonToolStripMenuItem";
            validateJsonToolStripMenuItem.Size = new Size(141, 22);
            validateJsonToolStripMenuItem.Text = "Validate Json";
            validateJsonToolStripMenuItem.Click += validateJsonToolStripMenuItem_Click;
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
            btn_Connect.Location = new Point(704, 79);
            btn_Connect.Name = "btn_Connect";
            btn_Connect.Progress = 0;
            btn_Connect.ProgressColor = Color.FromArgb(0, 103, 205);
            btn_Connect.Size = new Size(97, 25);
            btn_Connect.TabIndex = 23;
            btn_Connect.Text = "Refresh";
            btn_Connect.UseVisualStyleBackColor = false;
            btn_Connect.UseWindowsAccentColor = true;
            btn_Connect.WriteProgress = true;
            // 
            // label_Argument
            // 
            label_Argument.AutoSize = true;
            label_Argument.BackColor = Color.FromArgb(36, 36, 36);
            label_Argument.Font = new Font("Tahoma", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_Argument.ForeColor = Color.Gainsboro;
            label_Argument.Location = new Point(20, 109);
            label_Argument.Name = "label_Argument";
            label_Argument.Size = new Size(89, 19);
            label_Argument.TabIndex = 22;
            label_Argument.Text = "Argument*";
            // 
            // roundedPanel_ActionCombobox
            // 
            roundedPanel_ActionCombobox.BackColor = Color.FromArgb(65, 65, 65);
            roundedPanel_ActionCombobox.Controls.Add(comboBox_ActionList);
            roundedPanel_ActionCombobox.Location = new Point(369, 81);
            roundedPanel_ActionCombobox.Name = "roundedPanel_ActionCombobox";
            roundedPanel_ActionCombobox.Size = new Size(329, 22);
            roundedPanel_ActionCombobox.TabIndex = 21;
            // 
            // comboBox_ActionList
            // 
            comboBox_ActionList.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox_ActionList.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBox_ActionList.BackColor = Color.FromArgb(65, 65, 65);
            comboBox_ActionList.FlatStyle = FlatStyle.Flat;
            comboBox_ActionList.Font = new Font("Tahoma", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBox_ActionList.ForeColor = Color.Gainsboro;
            comboBox_ActionList.FormattingEnabled = true;
            comboBox_ActionList.Location = new Point(-1, -1);
            comboBox_ActionList.Margin = new Padding(3, 3, 10, 3);
            comboBox_ActionList.Name = "comboBox_ActionList";
            comboBox_ActionList.Size = new Size(330, 24);
            comboBox_ActionList.TabIndex = 21;
            comboBox_ActionList.SelectedIndexChanged += comboBox_ActionList_SelectedIndexChanged;
            // 
            // roundedPanel_ActioGroupCombobox
            // 
            roundedPanel_ActioGroupCombobox.BackColor = Color.FromArgb(65, 65, 65);
            roundedPanel_ActioGroupCombobox.Controls.Add(comboBox_ActionGroup);
            roundedPanel_ActioGroupCombobox.Location = new Point(20, 81);
            roundedPanel_ActioGroupCombobox.Name = "roundedPanel_ActioGroupCombobox";
            roundedPanel_ActioGroupCombobox.Size = new Size(336, 22);
            roundedPanel_ActioGroupCombobox.TabIndex = 20;
            // 
            // comboBox_ActionGroup
            // 
            comboBox_ActionGroup.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox_ActionGroup.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBox_ActionGroup.BackColor = Color.FromArgb(65, 65, 65);
            comboBox_ActionGroup.FlatStyle = FlatStyle.Flat;
            comboBox_ActionGroup.Font = new Font("Tahoma", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBox_ActionGroup.ForeColor = Color.Gainsboro;
            comboBox_ActionGroup.FormattingEnabled = true;
            comboBox_ActionGroup.Location = new Point(-2, -1);
            comboBox_ActionGroup.Margin = new Padding(3, 3, 10, 3);
            comboBox_ActionGroup.Name = "comboBox_ActionGroup";
            comboBox_ActionGroup.Size = new Size(337, 24);
            comboBox_ActionGroup.TabIndex = 20;
            comboBox_ActionGroup.SelectedIndexChanged += comboBox_ActionGroup_SelectedIndexChanged;
            // 
            // label_Action
            // 
            label_Action.AutoSize = true;
            label_Action.BackColor = Color.FromArgb(36, 36, 36);
            label_Action.Font = new Font("Tahoma", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_Action.ForeColor = Color.Gainsboro;
            label_Action.Location = new Point(369, 57);
            label_Action.Name = "label_Action";
            label_Action.Size = new Size(54, 19);
            label_Action.TabIndex = 4;
            label_Action.Text = "Action";
            // 
            // label_ActionGroup
            // 
            label_ActionGroup.AutoSize = true;
            label_ActionGroup.BackColor = Color.FromArgb(36, 36, 36);
            label_ActionGroup.Font = new Font("Tahoma", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_ActionGroup.ForeColor = Color.Gainsboro;
            label_ActionGroup.Location = new Point(20, 57);
            label_ActionGroup.Name = "label_ActionGroup";
            label_ActionGroup.Size = new Size(103, 19);
            label_ActionGroup.TabIndex = 3;
            label_ActionGroup.Text = "Action Group";
            // 
            // errorPanel
            // 
            errorPanel.BackColor = Color.FromArgb(45, 45, 45);
            errorPanel.Controls.Add(label_Error);
            errorPanel.Location = new Point(3, 3);
            errorPanel.Name = "errorPanel";
            errorPanel.Size = new Size(851, 425);
            errorPanel.TabIndex = 20;
            // 
            // label_Error
            // 
            label_Error.BackColor = Color.FromArgb(45, 45, 45);
            label_Error.ForeColor = Color.Gainsboro;
            label_Error.Location = new Point(0, 33);
            label_Error.Name = "label_Error";
            label_Error.Size = new Size(848, 353);
            label_Error.TabIndex = 0;
            label_Error.Text = "\"Failed to populate the action group list. Please check your connection to Streamer.bot.\"";
            label_Error.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // roundedPanel_ActionInfo
            // 
            roundedPanel_ActionInfo.BackColor = Color.FromArgb(36, 36, 36);
            roundedPanel_ActionInfo.Controls.Add(label_triggerCount);
            roundedPanel_ActionInfo.Controls.Add(label_subactionCount);
            roundedPanel_ActionInfo.Controls.Add(label_actionEnabled);
            roundedPanel_ActionInfo.Controls.Add(label_actionId);
            roundedPanel_ActionInfo.Controls.Add(label4);
            roundedPanel_ActionInfo.Controls.Add(label_Subactioncout_Text);
            roundedPanel_ActionInfo.Controls.Add(label_AnctionEnables_Text);
            roundedPanel_ActionInfo.Controls.Add(label_AntionID_Text);
            roundedPanel_ActionInfo.Controls.Add(label1);
            roundedPanel_ActionInfo.Location = new Point(17, 334);
            roundedPanel_ActionInfo.Name = "roundedPanel_ActionInfo";
            roundedPanel_ActionInfo.Size = new Size(820, 87);
            roundedPanel_ActionInfo.TabIndex = 19;
            // 
            // label_triggerCount
            // 
            label_triggerCount.AutoSize = true;
            label_triggerCount.BackColor = Color.FromArgb(36, 36, 36);
            label_triggerCount.Font = new Font("Tahoma", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_triggerCount.ForeColor = Color.Gainsboro;
            label_triggerCount.Location = new Point(514, 57);
            label_triggerCount.Name = "label_triggerCount";
            label_triggerCount.Size = new Size(19, 14);
            label_triggerCount.TabIndex = 15;
            label_triggerCount.Text = "   ";
            // 
            // label_subactionCount
            // 
            label_subactionCount.AutoSize = true;
            label_subactionCount.BackColor = Color.FromArgb(36, 36, 36);
            label_subactionCount.Font = new Font("Tahoma", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_subactionCount.ForeColor = Color.Gainsboro;
            label_subactionCount.Location = new Point(514, 41);
            label_subactionCount.Name = "label_subactionCount";
            label_subactionCount.Size = new Size(19, 14);
            label_subactionCount.TabIndex = 14;
            label_subactionCount.Text = "   ";
            // 
            // label_actionEnabled
            // 
            label_actionEnabled.AutoSize = true;
            label_actionEnabled.BackColor = Color.FromArgb(36, 36, 36);
            label_actionEnabled.Font = new Font("Tahoma", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_actionEnabled.ForeColor = Color.Gainsboro;
            label_actionEnabled.Location = new Point(124, 57);
            label_actionEnabled.Name = "label_actionEnabled";
            label_actionEnabled.Size = new Size(19, 14);
            label_actionEnabled.TabIndex = 13;
            label_actionEnabled.Text = "   ";
            // 
            // label_actionId
            // 
            label_actionId.AutoSize = true;
            label_actionId.BackColor = Color.FromArgb(36, 36, 36);
            label_actionId.Font = new Font("Tahoma", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_actionId.ForeColor = Color.Gainsboro;
            label_actionId.Location = new Point(124, 41);
            label_actionId.Name = "label_actionId";
            label_actionId.Size = new Size(19, 14);
            label_actionId.TabIndex = 12;
            label_actionId.Text = "   ";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.FromArgb(36, 36, 36);
            label4.Font = new Font("Tahoma", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.Gainsboro;
            label4.Location = new Point(406, 57);
            label4.Name = "label4";
            label4.Size = new Size(87, 14);
            label4.TabIndex = 11;
            label4.Text = "Trigger Count:";
            // 
            // label_Subactioncout_Text
            // 
            label_Subactioncout_Text.AutoSize = true;
            label_Subactioncout_Text.BackColor = Color.FromArgb(36, 36, 36);
            label_Subactioncout_Text.Font = new Font("Tahoma", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_Subactioncout_Text.ForeColor = Color.Gainsboro;
            label_Subactioncout_Text.Location = new Point(406, 41);
            label_Subactioncout_Text.Name = "label_Subactioncout_Text";
            label_Subactioncout_Text.Size = new Size(102, 14);
            label_Subactioncout_Text.TabIndex = 10;
            label_Subactioncout_Text.Text = "Subaction Count:";
            // 
            // label_AnctionEnables_Text
            // 
            label_AnctionEnables_Text.AutoSize = true;
            label_AnctionEnables_Text.BackColor = Color.FromArgb(36, 36, 36);
            label_AnctionEnables_Text.Font = new Font("Tahoma", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_AnctionEnables_Text.ForeColor = Color.Gainsboro;
            label_AnctionEnables_Text.Location = new Point(20, 57);
            label_AnctionEnables_Text.Name = "label_AnctionEnables_Text";
            label_AnctionEnables_Text.Size = new Size(93, 14);
            label_AnctionEnables_Text.TabIndex = 9;
            label_AnctionEnables_Text.Text = "Action Enabled:";
            // 
            // label_AntionID_Text
            // 
            label_AntionID_Text.AutoSize = true;
            label_AntionID_Text.BackColor = Color.FromArgb(36, 36, 36);
            label_AntionID_Text.Font = new Font("Tahoma", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_AntionID_Text.ForeColor = Color.Gainsboro;
            label_AntionID_Text.Location = new Point(20, 41);
            label_AntionID_Text.Name = "label_AntionID_Text";
            label_AntionID_Text.Size = new Size(62, 14);
            label_AntionID_Text.TabIndex = 8;
            label_AntionID_Text.Text = "Action ID:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.FromArgb(36, 36, 36);
            label1.Font = new Font("Tahoma", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Gainsboro;
            label1.Location = new Point(20, 13);
            label1.Name = "label1";
            label1.Size = new Size(88, 19);
            label1.TabIndex = 4;
            label1.Text = "Action Info";
            // 
            // StreamerBotActionConfigurator
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            Controls.Add(roundedPanel_ActionInfo);
            Controls.Add(roundedPanel_Actions);
            Controls.Add(errorPanel);
            Name = "StreamerBotActionConfigurator";
            Size = new Size(857, 431);
            roundedPanel_Actions.ResumeLayout(false);
            roundedPanel_Actions.PerformLayout();
            roundedPanel2.ResumeLayout(false);
            roundedPanel1.ResumeLayout(false);
            roundedPanel1.PerformLayout();
            contextMenu_JsonTextBox.ResumeLayout(false);
            roundedPanel_ActionCombobox.ResumeLayout(false);
            roundedPanel_ActioGroupCombobox.ResumeLayout(false);
            errorPanel.ResumeLayout(false);
            roundedPanel_ActionInfo.ResumeLayout(false);
            roundedPanel_ActionInfo.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private RoundedPanel roundedPanel_Actions;
        private RoundedPanel roundedPanel_ActionInfo;
        private Label label_Action;
        private RoundedPanel roundedPanel_ActionCombobox;
        private RoundedPanel roundedPanel_ActioGroupCombobox;
        private Label label_Argument;
        private ButtonPrimary btn_Connect;
        private Label label_ActionGroup;
        private Label label1;
        private Label label_AntionID_Text;
        private Label label_AnctionEnables_Text;
        private Label label4;
        private Label label_Subactioncout_Text;
        private Label label_actionId;
        private Label label_actionEnabled;
        private Label label_subactionCount;
        private Label label_triggerCount;
        private System.Windows.Forms.ComboBox comboBox_ActionGroup;
        private System.Windows.Forms.ComboBox comboBox_ActionList;
        private RoundedPanel roundedPanel1;
        private TextBox textBox;
        private ContextMenuStrip contextMenu_JsonTextBox;
        private ToolStripMenuItem pastToolStripMenuItem;
        private ToolStripMenuItem copyToolStripMenuItem;
        private ToolStripMenuItem selectAllToolStripMenuItem;
        private ToolStripMenuItem formatJsonToolStripMenuItem;
        private ToolStripMenuItem validateJsonToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator3;
        private RoundedPanel errorPanel;
        private Label label_Error;
        private RoundedPanel roundedPanel2;
        private System.Windows.Forms.ComboBox comboBox_SelectedProfile;
        private Label label5;
    }
}
