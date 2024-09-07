namespace computer_board
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel ComputerPanel;

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
            this.ComputerPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // ComputerPanel
            // 
            this.ComputerPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ComputerPanel.Location = new System.Drawing.Point(12, 12);
            this.ComputerPanel.Name = "ComputerPanel";
            this.ComputerPanel.Size = new System.Drawing.Size(300, 300);
            this.ComputerPanel.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 321);
            this.Controls.Add(this.ComputerPanel);
            this.Name = "Form1";
            this.Text = "Поле компьютера";
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.ResumeLayout(false);

        }
    }
}
