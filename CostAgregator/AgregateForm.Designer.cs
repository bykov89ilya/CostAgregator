namespace CostAgregator
{
    partial class AgregateForm
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
            this.CreateReportButton = new System.Windows.Forms.Button();
            this.OpenTinkoffFileButton = new System.Windows.Forms.Button();
            this.OpenCashFileButton = new System.Windows.Forms.Button();
            this.listViewCash = new System.Windows.Forms.ListView();
            this.listViewTinkoff = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // CreateReportButton
            // 
            this.CreateReportButton.Location = new System.Drawing.Point(417, 224);
            this.CreateReportButton.Name = "CreateReportButton";
            this.CreateReportButton.Size = new System.Drawing.Size(101, 23);
            this.CreateReportButton.TabIndex = 0;
            this.CreateReportButton.Text = "Создать отчет";
            this.CreateReportButton.UseVisualStyleBackColor = true;
            this.CreateReportButton.Click += new System.EventHandler(this.CreateReportButton_Click);
            // 
            // OpenTinkoffFileButton
            // 
            this.OpenTinkoffFileButton.Location = new System.Drawing.Point(12, 12);
            this.OpenTinkoffFileButton.Name = "OpenTinkoffFileButton";
            this.OpenTinkoffFileButton.Size = new System.Drawing.Size(158, 23);
            this.OpenTinkoffFileButton.TabIndex = 1;
            this.OpenTinkoffFileButton.Text = "Открыть отчеты Тинькофф";
            this.OpenTinkoffFileButton.UseVisualStyleBackColor = true;
            this.OpenTinkoffFileButton.Click += new System.EventHandler(this.OpenTinkoffFileButton_Click);
            // 
            // OpenCashFileButton
            // 
            this.OpenCashFileButton.Location = new System.Drawing.Point(13, 144);
            this.OpenCashFileButton.Name = "OpenCashFileButton";
            this.OpenCashFileButton.Size = new System.Drawing.Size(157, 23);
            this.OpenCashFileButton.TabIndex = 3;
            this.OpenCashFileButton.Text = "Открыть отчет Наличка";
            this.OpenCashFileButton.UseVisualStyleBackColor = true;
            this.OpenCashFileButton.Click += new System.EventHandler(this.openCashFileButton_Click);
            // 
            // listViewCash
            // 
            this.listViewCash.Location = new System.Drawing.Point(12, 174);
            this.listViewCash.Name = "listViewCash";
            this.listViewCash.Size = new System.Drawing.Size(506, 44);
            this.listViewCash.TabIndex = 4;
            this.listViewCash.UseCompatibleStateImageBehavior = false;
            this.listViewCash.View = System.Windows.Forms.View.List;
            // 
            // listViewTinkoff
            // 
            this.listViewTinkoff.Location = new System.Drawing.Point(13, 42);
            this.listViewTinkoff.Name = "listViewTinkoff";
            this.listViewTinkoff.Size = new System.Drawing.Size(505, 96);
            this.listViewTinkoff.TabIndex = 5;
            this.listViewTinkoff.UseCompatibleStateImageBehavior = false;
            this.listViewTinkoff.View = System.Windows.Forms.View.SmallIcon;
            // 
            // AgregateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 259);
            this.Controls.Add(this.listViewTinkoff);
            this.Controls.Add(this.listViewCash);
            this.Controls.Add(this.OpenCashFileButton);
            this.Controls.Add(this.OpenTinkoffFileButton);
            this.Controls.Add(this.CreateReportButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "AgregateForm";
            this.ShowIcon = false;
            this.Text = "Агрегатор отчетов";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button CreateReportButton;
        private System.Windows.Forms.Button OpenTinkoffFileButton;
        private System.Windows.Forms.Button OpenCashFileButton;
        private System.Windows.Forms.ListView listViewCash;
        private System.Windows.Forms.ListView listViewTinkoff;
    }
}

