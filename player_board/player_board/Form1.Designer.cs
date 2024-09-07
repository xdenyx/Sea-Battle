namespace player_board
{
    partial class player_form
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Panel player1_panel;
        private System.Windows.Forms.Panel attack_panel;
        private System.Windows.Forms.Panel player2_panel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
        private System.Windows.Forms.Label status_label;
        private System.Windows.Forms.Button start_btn;
        private System.Windows.Forms.Button restart_btn;
        private System.Windows.Forms.Button autoPlace1_btn;
        private System.Windows.Forms.Button autoPlace2_btn;
        private System.Windows.Forms.ComboBox ship_selector;
        private System.Windows.Forms.Button orientation_btn;
        private System.Windows.Forms.Label PlayerFieldLabel;
        private System.Windows.Forms.Label AttackFieldLabel;
        private System.Windows.Forms.Label ComputerFieldLabel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.player1_panel = new System.Windows.Forms.Panel();
            this.attack_panel = new System.Windows.Forms.Panel();
            this.player2_panel = new System.Windows.Forms.Panel();
            this.PlayerFieldLabel = new System.Windows.Forms.Label();
            this.AttackFieldLabel = new System.Windows.Forms.Label();
            this.ComputerFieldLabel = new System.Windows.Forms.Label();
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.start_btn = new System.Windows.Forms.Button();
            this.restart_btn = new System.Windows.Forms.Button();
            this.autoPlace1_btn = new System.Windows.Forms.Button();
            this.autoPlace2_btn = new System.Windows.Forms.Button();
            this.orientation_btn = new System.Windows.Forms.Button();
            this.ship_selector = new System.Windows.Forms.ComboBox();
            this.status_label = new System.Windows.Forms.Label();
            this.tableLayoutPanel.SuspendLayout();
            this.flowLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.Controls.Add(this.player1_panel, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.attack_panel, 1, 1);
            this.tableLayoutPanel.Controls.Add(this.player2_panel, 0, 3);
            this.tableLayoutPanel.Controls.Add(this.PlayerFieldLabel, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.AttackFieldLabel, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.ComputerFieldLabel, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.flowLayoutPanel, 1, 3);
            this.tableLayoutPanel.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 4;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(727, 707);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // player1_panel
            // 
            this.player1_panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.player1_panel.Location = new System.Drawing.Point(3, 23);
            this.player1_panel.Name = "player1_panel";
            this.player1_panel.Size = new System.Drawing.Size(357, 327);
            this.player1_panel.TabIndex = 0;
            // 
            // attack_panel
            // 
            this.attack_panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.attack_panel.Location = new System.Drawing.Point(366, 23);
            this.attack_panel.Name = "attack_panel";
            this.attack_panel.Size = new System.Drawing.Size(358, 327);
            this.attack_panel.TabIndex = 1;
            // 
            // player2_panel
            // 
            this.player2_panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.player2_panel.Location = new System.Drawing.Point(3, 376);
            this.player2_panel.Name = "player2_panel";
            this.player2_panel.Size = new System.Drawing.Size(357, 328);
            this.player2_panel.TabIndex = 2;
            // 
            // PlayerFieldLabel
            // 
            this.PlayerFieldLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PlayerFieldLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PlayerFieldLabel.Location = new System.Drawing.Point(3, 0);
            this.PlayerFieldLabel.Name = "PlayerFieldLabel";
            this.PlayerFieldLabel.Size = new System.Drawing.Size(357, 20);
            this.PlayerFieldLabel.TabIndex = 10;
            this.PlayerFieldLabel.Text = "Поле 1";
            this.PlayerFieldLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AttackFieldLabel
            // 
            this.AttackFieldLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AttackFieldLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AttackFieldLabel.Location = new System.Drawing.Point(366, 0);
            this.AttackFieldLabel.Name = "AttackFieldLabel";
            this.AttackFieldLabel.Size = new System.Drawing.Size(358, 20);
            this.AttackFieldLabel.TabIndex = 11;
            this.AttackFieldLabel.Text = "Поле для атаки";
            this.AttackFieldLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ComputerFieldLabel
            // 
            this.ComputerFieldLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ComputerFieldLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ComputerFieldLabel.Location = new System.Drawing.Point(3, 353);
            this.ComputerFieldLabel.Name = "ComputerFieldLabel";
            this.ComputerFieldLabel.Size = new System.Drawing.Size(357, 20);
            this.ComputerFieldLabel.TabIndex = 12;
            this.ComputerFieldLabel.Text = "Поле 2";
            this.ComputerFieldLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.Controls.Add(this.start_btn);
            this.flowLayoutPanel.Controls.Add(this.restart_btn);
            this.flowLayoutPanel.Controls.Add(this.autoPlace1_btn);
            this.flowLayoutPanel.Controls.Add(this.autoPlace2_btn);
            this.flowLayoutPanel.Controls.Add(this.orientation_btn);
            this.flowLayoutPanel.Controls.Add(this.ship_selector);
            this.flowLayoutPanel.Controls.Add(this.status_label);
            this.flowLayoutPanel.Location = new System.Drawing.Point(366, 376);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Size = new System.Drawing.Size(358, 328);
            this.flowLayoutPanel.TabIndex = 3;
            // 
            // start_btn
            // 
            this.start_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.start_btn.Location = new System.Drawing.Point(3, 3);
            this.start_btn.Name = "start_btn";
            this.start_btn.Size = new System.Drawing.Size(108, 23);
            this.start_btn.TabIndex = 4;
            this.start_btn.Text = "Начать игру";
            this.start_btn.UseVisualStyleBackColor = true;
            this.start_btn.Click += new System.EventHandler(this.start_btn_Click);
            // 
            // restart_btn
            // 
            this.restart_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.restart_btn.Location = new System.Drawing.Point(117, 3);
            this.restart_btn.Name = "restart_btn";
            this.restart_btn.Size = new System.Drawing.Size(108, 23);
            this.restart_btn.TabIndex = 5;
            this.restart_btn.Text = "Перезапуск";
            this.restart_btn.UseVisualStyleBackColor = true;
            this.restart_btn.Click += new System.EventHandler(this.Restart_btn_Click);
            // 
            // autoPlace1_btn
            // 
            this.autoPlace1_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.autoPlace1_btn.Location = new System.Drawing.Point(231, 3);
            this.autoPlace1_btn.Name = "autoPlace1_btn";
            this.autoPlace1_btn.Size = new System.Drawing.Size(106, 23);
            this.autoPlace1_btn.TabIndex = 6;
            this.autoPlace1_btn.Text = "Авто 1";
            this.autoPlace1_btn.UseVisualStyleBackColor = true;
            this.autoPlace1_btn.Click += new System.EventHandler(this.AutoPlace1_btn_Click);
            // 
            // autoPlace2_btn
            // 
            this.autoPlace2_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.autoPlace2_btn.Location = new System.Drawing.Point(3, 32);
            this.autoPlace2_btn.Name = "autoPlace2_btn";
            this.autoPlace2_btn.Size = new System.Drawing.Size(108, 23);
            this.autoPlace2_btn.TabIndex = 7;
            this.autoPlace2_btn.Text = "Авто 2";
            this.autoPlace2_btn.UseVisualStyleBackColor = true;
            this.autoPlace2_btn.Click += new System.EventHandler(this.AutoPlace2_btn_Click);
            // 
            // orientation_btn
            // 
            this.orientation_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.orientation_btn.Location = new System.Drawing.Point(117, 32);
            this.orientation_btn.Name = "orientation_btn";
            this.orientation_btn.Size = new System.Drawing.Size(108, 23);
            this.orientation_btn.TabIndex = 9;
            this.orientation_btn.Text = "Горизонтально";
            this.orientation_btn.UseVisualStyleBackColor = true;
            this.orientation_btn.Click += new System.EventHandler(this.Orientation_btn_Click);
            // 
            // ship_selector
            // 
            this.ship_selector.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ship_selector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ship_selector.FormattingEnabled = true;
            this.ship_selector.Items.AddRange(new object[] {
            "4",
            "3",
            "3",
            "2",
            "2",
            "2",
            "1",
            "1",
            "1",
            "1"});
            this.ship_selector.Location = new System.Drawing.Point(231, 34);
            this.ship_selector.Name = "ship_selector";
            this.ship_selector.Size = new System.Drawing.Size(106, 21);
            this.ship_selector.TabIndex = 8;
            // 
            // status_label
            // 
            this.status_label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.status_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.status_label.Location = new System.Drawing.Point(3, 58);
            this.status_label.Name = "status_label";
            this.status_label.Size = new System.Drawing.Size(265, 44);
            this.status_label.TabIndex = 3;
            this.status_label.Text = "Разместите свои корабли";
            this.status_label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // player_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(751, 731);
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "player_form";
            this.Text = "Поле игрока";
            this.Resize += new System.EventHandler(this.player_form_Resize);
            this.tableLayoutPanel.ResumeLayout(false);
            this.flowLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
