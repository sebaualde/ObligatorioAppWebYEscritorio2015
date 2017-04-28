namespace AdministracionBiosSearch
{
    partial class FrmListadoGeneralEmpresas
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtVisitas = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ddlCategorias = new System.Windows.Forms.ComboBox();
            this.grillaEmpresas = new System.Windows.Forms.DataGridView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblMensaje = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnQuitalFiltros = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grillaEmpresas)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnQuitalFiltros);
            this.groupBox1.Controls.Add(this.txtVisitas);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.ddlCategorias);
            this.groupBox1.Location = new System.Drawing.Point(38, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(473, 70);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtrar por:";
            // 
            // txtVisitas
            // 
            this.txtVisitas.Location = new System.Drawing.Point(314, 25);
            this.txtVisitas.Name = "txtVisitas";
            this.txtVisitas.Size = new System.Drawing.Size(121, 20);
            this.txtVisitas.TabIndex = 3;
            this.txtVisitas.Validating += new System.ComponentModel.CancelEventHandler(this.txtVisitas_Validating);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(213, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Cantidad de Visitas:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Categoría:";
            // 
            // ddlCategorias
            // 
            this.ddlCategorias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlCategorias.FormattingEnabled = true;
            this.ddlCategorias.Location = new System.Drawing.Point(64, 25);
            this.ddlCategorias.Name = "ddlCategorias";
            this.ddlCategorias.Size = new System.Drawing.Size(121, 21);
            this.ddlCategorias.TabIndex = 0;
            this.ddlCategorias.SelectedIndexChanged += new System.EventHandler(this.ddlCategorias_SelectedIndexChanged);
            // 
            // grillaEmpresas
            // 
            this.grillaEmpresas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grillaEmpresas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grillaEmpresas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grillaEmpresas.Location = new System.Drawing.Point(38, 88);
            this.grillaEmpresas.Name = "grillaEmpresas";
            this.grillaEmpresas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grillaEmpresas.Size = new System.Drawing.Size(473, 260);
            this.grillaEmpresas.TabIndex = 5;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblMensaje});
            this.statusStrip1.Location = new System.Drawing.Point(0, 370);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(544, 22);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblMensaje
            // 
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(0, 17);
            // 
            // btnQuitalFiltros
            // 
            this.btnQuitalFiltros.BackColor = System.Drawing.SystemColors.Control;
            this.btnQuitalFiltros.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnQuitalFiltros.FlatAppearance.BorderSize = 0;
            this.btnQuitalFiltros.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuitalFiltros.Image = global::AdministracionBiosSearch.Properties.Resources.quitarFiltros;
            this.btnQuitalFiltros.Location = new System.Drawing.Point(441, 23);
            this.btnQuitalFiltros.Name = "btnQuitalFiltros";
            this.btnQuitalFiltros.Size = new System.Drawing.Size(28, 27);
            this.btnQuitalFiltros.TabIndex = 4;
            this.btnQuitalFiltros.UseVisualStyleBackColor = false;
            this.btnQuitalFiltros.Click += new System.EventHandler(this.btnQuitalFiltros_Click);
            // 
            // FrmListadoGeneralEmpresas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 392);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.grillaEmpresas);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmListadoGeneralEmpresas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmListadoGeneralEmpresas";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grillaEmpresas)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtVisitas;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox ddlCategorias;
        private System.Windows.Forms.DataGridView grillaEmpresas;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblMensaje;
        private System.Windows.Forms.Button btnQuitalFiltros;
    }
}