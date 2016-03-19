namespace Tip_Calculator
{
    partial class Form1
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.billTotalInput = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.computedTipInput = new System.Windows.Forms.TextBox();
            this.billCostLabel = new System.Windows.Forms.Label();
            this.computeTipButton = new System.Windows.Forms.Button();
            this.tipPercent = new System.Windows.Forms.Label();
            this.tipPercentInput = new System.Windows.Forms.TextBox();
            this.totalCostLabel = new System.Windows.Forms.Label();
            this.totalCostInput = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // billTotalInput
            // 
            this.billTotalInput.Location = new System.Drawing.Point(134, 43);
            this.billTotalInput.Name = "billTotalInput";
            this.billTotalInput.Size = new System.Drawing.Size(134, 20);
            this.billTotalInput.TabIndex = 1;
            this.billTotalInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(49, 115);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 2;
            // 
            // computedTipInput
            // 
            this.computedTipInput.Location = new System.Drawing.Point(134, 115);
            this.computedTipInput.Name = "computedTipInput";
            this.computedTipInput.Size = new System.Drawing.Size(134, 20);
            this.computedTipInput.TabIndex = 3;
            this.computedTipInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // billCostLabel
            // 
            this.billCostLabel.AutoSize = true;
            this.billCostLabel.Location = new System.Drawing.Point(30, 43);
            this.billCostLabel.Name = "billCostLabel";
            this.billCostLabel.Size = new System.Drawing.Size(72, 13);
            this.billCostLabel.TabIndex = 4;
            this.billCostLabel.Text = "Enter Bill Cost";
            // 
            // computeTipButton
            // 
            this.computeTipButton.Location = new System.Drawing.Point(33, 112);
            this.computeTipButton.Name = "computeTipButton";
            this.computeTipButton.Size = new System.Drawing.Size(75, 23);
            this.computeTipButton.TabIndex = 5;
            this.computeTipButton.Text = "Compute Tip";
            this.computeTipButton.UseVisualStyleBackColor = true;
            this.computeTipButton.Click += new System.EventHandler(this.computeTipButton_Click);
            // 
            // tipPercent
            // 
            this.tipPercent.AutoSize = true;
            this.tipPercent.Location = new System.Drawing.Point(33, 77);
            this.tipPercent.Name = "tipPercent";
            this.tipPercent.Size = new System.Drawing.Size(62, 13);
            this.tipPercent.TabIndex = 6;
            this.tipPercent.Text = "Tip Percent";
            // 
            // tipPercentInput
            // 
            this.tipPercentInput.Location = new System.Drawing.Point(134, 77);
            this.tipPercentInput.Name = "tipPercentInput";
            this.tipPercentInput.Size = new System.Drawing.Size(134, 20);
            this.tipPercentInput.TabIndex = 7;
            this.tipPercentInput.Text = "20";
            this.tipPercentInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tipPercentInput.TextChanged += new System.EventHandler(this.tipPercentInput_TextChanged);
            // 
            // totalCostLabel
            // 
            this.totalCostLabel.AutoSize = true;
            this.totalCostLabel.Location = new System.Drawing.Point(33, 164);
            this.totalCostLabel.Name = "totalCostLabel";
            this.totalCostLabel.Size = new System.Drawing.Size(55, 13);
            this.totalCostLabel.TabIndex = 8;
            this.totalCostLabel.Text = "Total Cost";
            // 
            // totalCostInput
            // 
            this.totalCostInput.Location = new System.Drawing.Point(134, 156);
            this.totalCostInput.Name = "totalCostInput";
            this.totalCostInput.Size = new System.Drawing.Size(134, 20);
            this.totalCostInput.TabIndex = 9;
            this.totalCostInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.totalCostInput);
            this.Controls.Add(this.totalCostLabel);
            this.Controls.Add(this.tipPercentInput);
            this.Controls.Add(this.tipPercent);
            this.Controls.Add(this.computeTipButton);
            this.Controls.Add(this.billCostLabel);
            this.Controls.Add(this.computedTipInput);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.billTotalInput);
            this.ForeColor = System.Drawing.SystemColors.MenuText;
            this.Name = "Form1";
            this.Text = "Tip Calculator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox billTotalInput;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox computedTipInput;
        private System.Windows.Forms.Label billCostLabel;
        private System.Windows.Forms.Button computeTipButton;
        private System.Windows.Forms.Label tipPercent;
        private System.Windows.Forms.TextBox tipPercentInput;
        private System.Windows.Forms.Label totalCostLabel;
        private System.Windows.Forms.TextBox totalCostInput;
    }
}

