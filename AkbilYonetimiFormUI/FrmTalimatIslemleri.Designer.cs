
namespace AkbilYonetimiFormUI
{
    partial class FrmTalimatIslemleri
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
            this.components = new System.ComponentModel.Container();
            this.menuStripTalimatlar = new System.Windows.Forms.MenuStrip();
            this.anaMenuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cikisyapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.lblBekleyenTalimat = new System.Windows.Forms.Label();
            this.cmbBoxAkbiller = new System.Windows.Forms.ComboBox();
            this.checkBoxBekleyenTalimatlar = new System.Windows.Forms.CheckBox();
            this.dataGridViewTalimatlar = new System.Windows.Forms.DataGridView();
            this.timerBekleyenTalimat = new System.Windows.Forms.Timer(this.components);
            this.menuStripTalimatlar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTalimatlar)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStripTalimatlar
            // 
            this.menuStripTalimatlar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.anaMenuToolStripMenuItem,
            this.cikisyapToolStripMenuItem});
            this.menuStripTalimatlar.Location = new System.Drawing.Point(0, 0);
            this.menuStripTalimatlar.Name = "menuStripTalimatlar";
            this.menuStripTalimatlar.Size = new System.Drawing.Size(612, 24);
            this.menuStripTalimatlar.TabIndex = 0;
            this.menuStripTalimatlar.Text = "menuStrip1";
            // 
            // anaMenuToolStripMenuItem
            // 
            this.anaMenuToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.anaMenuToolStripMenuItem.Name = "anaMenuToolStripMenuItem";
            this.anaMenuToolStripMenuItem.Size = new System.Drawing.Size(81, 20);
            this.anaMenuToolStripMenuItem.Text = "ANA MENÜ";
            // 
            // cikisyapToolStripMenuItem
            // 
            this.cikisyapToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.cikisyapToolStripMenuItem.Name = "cikisyapToolStripMenuItem";
            this.cikisyapToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
            this.cikisyapToolStripMenuItem.Text = "ÇIKIŞ YAP";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(432, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Bekleyen Talimat";
            // 
            // lblBekleyenTalimat
            // 
            this.lblBekleyenTalimat.AutoSize = true;
            this.lblBekleyenTalimat.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblBekleyenTalimat.ForeColor = System.Drawing.Color.Red;
            this.lblBekleyenTalimat.Location = new System.Drawing.Point(481, 78);
            this.lblBekleyenTalimat.Name = "lblBekleyenTalimat";
            this.lblBekleyenTalimat.Size = new System.Drawing.Size(19, 20);
            this.lblBekleyenTalimat.TabIndex = 2;
            this.lblBekleyenTalimat.Text = "0";
            // 
            // cmbBoxAkbiller
            // 
            this.cmbBoxAkbiller.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.cmbBoxAkbiller.FormattingEnabled = true;
            this.cmbBoxAkbiller.Location = new System.Drawing.Point(36, 46);
            this.cmbBoxAkbiller.Name = "cmbBoxAkbiller";
            this.cmbBoxAkbiller.Size = new System.Drawing.Size(345, 24);
            this.cmbBoxAkbiller.TabIndex = 3;
            this.cmbBoxAkbiller.Text = "Akbil Seçiniz...";
            // 
            // checkBoxBekleyenTalimatlar
            // 
            this.checkBoxBekleyenTalimatlar.AutoSize = true;
            this.checkBoxBekleyenTalimatlar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.checkBoxBekleyenTalimatlar.ForeColor = System.Drawing.Color.Red;
            this.checkBoxBekleyenTalimatlar.Location = new System.Drawing.Point(36, 99);
            this.checkBoxBekleyenTalimatlar.Name = "checkBoxBekleyenTalimatlar";
            this.checkBoxBekleyenTalimatlar.Size = new System.Drawing.Size(255, 19);
            this.checkBoxBekleyenTalimatlar.TabIndex = 4;
            this.checkBoxBekleyenTalimatlar.Text = "Sadece Bekleyen Talimatları Göster";
            this.checkBoxBekleyenTalimatlar.UseVisualStyleBackColor = true;
            // 
            // dataGridViewTalimatlar
            // 
            this.dataGridViewTalimatlar.AllowUserToAddRows = false;
            this.dataGridViewTalimatlar.AllowUserToDeleteRows = false;
            this.dataGridViewTalimatlar.AllowUserToOrderColumns = true;
            this.dataGridViewTalimatlar.BackgroundColor = System.Drawing.Color.Silver;
            this.dataGridViewTalimatlar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTalimatlar.Location = new System.Drawing.Point(36, 146);
            this.dataGridViewTalimatlar.Name = "dataGridViewTalimatlar";
            this.dataGridViewTalimatlar.ReadOnly = true;
            this.dataGridViewTalimatlar.Size = new System.Drawing.Size(536, 156);
            this.dataGridViewTalimatlar.TabIndex = 5;
            // 
            // FrmTalimatIslemleri
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(612, 324);
            this.Controls.Add(this.dataGridViewTalimatlar);
            this.Controls.Add(this.checkBoxBekleyenTalimatlar);
            this.Controls.Add(this.cmbBoxAkbiller);
            this.Controls.Add(this.lblBekleyenTalimat);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStripTalimatlar);
            this.MainMenuStrip = this.menuStripTalimatlar;
            this.Name = "FrmTalimatIslemleri";
            this.Text = "Talimat İşlemleri";
            this.menuStripTalimatlar.ResumeLayout(false);
            this.menuStripTalimatlar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTalimatlar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStripTalimatlar;
        private System.Windows.Forms.ToolStripMenuItem anaMenuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cikisyapToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblBekleyenTalimat;
        private System.Windows.Forms.ComboBox cmbBoxAkbiller;
        private System.Windows.Forms.CheckBox checkBoxBekleyenTalimatlar;
        private System.Windows.Forms.DataGridView dataGridViewTalimatlar;
        private System.Windows.Forms.Timer timerBekleyenTalimat;
    }
}