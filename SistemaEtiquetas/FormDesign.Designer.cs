namespace SistemaEtiquetas
{
    partial class FormDesigner
    {
        private System.ComponentModel.IContainer components = null;

        // Controles
        private System.Windows.Forms.Panel panelFerramentas;
        private System.Windows.Forms.Panel panelPropriedades;
        private System.Windows.Forms.Panel panelCentro;
        private System.Windows.Forms.Panel panelCanvas;
        private System.Windows.Forms.Panel panelBotoes;

        private System.Windows.Forms.Label lblFerramentas;
        private System.Windows.Forms.Label lblTamanho;
        private System.Windows.Forms.Label lblLargura;
        private System.Windows.Forms.NumericUpDown numLargura;
        private System.Windows.Forms.Label lblAltura;
        private System.Windows.Forms.NumericUpDown numAltura;
        private System.Windows.Forms.Label lblElementos;
        private System.Windows.Forms.ListBox lstElementos;
        private System.Windows.Forms.Button btnRemover;
        private System.Windows.Forms.GroupBox groupPresets;
        private System.Windows.Forms.ComboBox cmbPresets;

        private System.Windows.Forms.Label lblPropriedades;
        private System.Windows.Forms.Label lblConteudo;
        private System.Windows.Forms.TextBox txtConteudo;
        private System.Windows.Forms.Label lblFonte;
        private System.Windows.Forms.NumericUpDown numFonte;
        private System.Windows.Forms.CheckBox chkNegrito;
        private System.Windows.Forms.CheckBox chkItalico;
        private System.Windows.Forms.Button btnCor;

        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button btnCancelar;

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
            this.panelFerramentas = new System.Windows.Forms.Panel();
            this.lblFerramentas = new System.Windows.Forms.Label();
            this.lblTamanho = new System.Windows.Forms.Label();
            this.lblLargura = new System.Windows.Forms.Label();
            this.numLargura = new System.Windows.Forms.NumericUpDown();
            this.lblAltura = new System.Windows.Forms.Label();
            this.numAltura = new System.Windows.Forms.NumericUpDown();
            this.lblElementos = new System.Windows.Forms.Label();
            this.lstElementos = new System.Windows.Forms.ListBox();
            this.btnRemover = new System.Windows.Forms.Button();
            this.groupPresets = new System.Windows.Forms.GroupBox();
            this.cmbPresets = new System.Windows.Forms.ComboBox();
            this.panelPropriedades = new System.Windows.Forms.Panel();
            this.lblPropriedades = new System.Windows.Forms.Label();
            this.lblConteudo = new System.Windows.Forms.Label();
            this.txtConteudo = new System.Windows.Forms.TextBox();
            this.lblFonte = new System.Windows.Forms.Label();
            this.numFonte = new System.Windows.Forms.NumericUpDown();
            this.chkNegrito = new System.Windows.Forms.CheckBox();
            this.chkItalico = new System.Windows.Forms.CheckBox();
            this.btnCor = new System.Windows.Forms.Button();
            this.panelCentro = new System.Windows.Forms.Panel();
            this.panelCanvas = new System.Windows.Forms.Panel();
            this.panelBotoes = new System.Windows.Forms.Panel();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();

            this.panelFerramentas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLargura)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAltura)).BeginInit();
            this.groupPresets.SuspendLayout();
            this.panelPropriedades.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFonte)).BeginInit();
            this.panelCentro.SuspendLayout();
            this.panelBotoes.SuspendLayout();
            this.SuspendLayout();

            // panelFerramentas
            this.panelFerramentas.BackColor = System.Drawing.Color.FromArgb(44, 62, 80);
            this.panelFerramentas.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelFerramentas.Location = new System.Drawing.Point(0, 0);
            this.panelFerramentas.Name = "panelFerramentas";
            this.panelFerramentas.Padding = new System.Windows.Forms.Padding(10);
            this.panelFerramentas.Size = new System.Drawing.Size(250, 700);
            this.panelFerramentas.TabIndex = 0;

            // lblFerramentas
            this.lblFerramentas.AutoSize = true;
            this.lblFerramentas.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblFerramentas.ForeColor = System.Drawing.Color.White;
            this.lblFerramentas.Location = new System.Drawing.Point(10, 10);
            this.lblFerramentas.Name = "lblFerramentas";
            this.lblFerramentas.Size = new System.Drawing.Size(115, 20);
            this.lblFerramentas.TabIndex = 0;
            this.lblFerramentas.Text = "FERRAMENTAS";

            // lblTamanho
            this.lblTamanho.AutoSize = true;
            this.lblTamanho.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTamanho.ForeColor = System.Drawing.Color.White;
            this.lblTamanho.Location = new System.Drawing.Point(10, 380);
            this.lblTamanho.Name = "lblTamanho";
            this.lblTamanho.Size = new System.Drawing.Size(191, 19);
            this.lblTamanho.TabIndex = 7;
            this.lblTamanho.Text = "TAMANHO DA ETIQUETA";

            // lblLargura
            this.lblLargura.AutoSize = true;
            this.lblLargura.ForeColor = System.Drawing.Color.White;
            this.lblLargura.Location = new System.Drawing.Point(10, 410);
            this.lblLargura.Name = "lblLargura";
            this.lblLargura.Size = new System.Drawing.Size(77, 15);
            this.lblLargura.TabIndex = 8;
            this.lblLargura.Text = "Largura (mm):";

            // numLargura
            this.numLargura.DecimalPlaces = 1;
            this.numLargura.Location = new System.Drawing.Point(120, 408);
            this.numLargura.Maximum = new decimal(new int[] { 300, 0, 0, 0 });
            this.numLargura.Minimum = new decimal(new int[] { 20, 0, 0, 0 });
            this.numLargura.Name = "numLargura";
            this.numLargura.Size = new System.Drawing.Size(100, 23);
            this.numLargura.TabIndex = 9;
            this.numLargura.Value = new decimal(new int[] { 50, 0, 0, 0 });
            this.numLargura.ValueChanged += new System.EventHandler(this.numLargura_ValueChanged);

            // lblAltura
            this.lblAltura.AutoSize = true;
            this.lblAltura.ForeColor = System.Drawing.Color.White;
            this.lblAltura.Location = new System.Drawing.Point(10, 440);
            this.lblAltura.Name = "lblAltura";
            this.lblAltura.Size = new System.Drawing.Size(68, 15);
            this.lblAltura.TabIndex = 10;
            this.lblAltura.Text = "Altura (mm):";

            // numAltura
            this.numAltura.DecimalPlaces = 1;
            this.numAltura.Location = new System.Drawing.Point(120, 438);
            this.numAltura.Maximum = new decimal(new int[] { 300, 0, 0, 0 });
            this.numAltura.Minimum = new decimal(new int[] { 20, 0, 0, 0 });
            this.numAltura.Name = "numAltura";
            this.numAltura.Size = new System.Drawing.Size(100, 23);
            this.numAltura.TabIndex = 11;
            this.numAltura.Value = new decimal(new int[] { 30, 0, 0, 0 });
            this.numAltura.ValueChanged += new System.EventHandler(this.numAltura_ValueChanged);

            // lblElementos
            this.lblElementos.AutoSize = true;
            this.lblElementos.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblElementos.ForeColor = System.Drawing.Color.White;
            this.lblElementos.Location = new System.Drawing.Point(10, 540);
            this.lblElementos.Name = "lblElementos";
            this.lblElementos.Size = new System.Drawing.Size(92, 19);
            this.lblElementos.TabIndex = 12;
            this.lblElementos.Text = "ELEMENTOS";

            // lstElementos
            this.lstElementos.BackColor = System.Drawing.Color.White;
            this.lstElementos.FormattingEnabled = true;
            this.lstElementos.ItemHeight = 15;
            this.lstElementos.Location = new System.Drawing.Point(10, 570);
            this.lstElementos.Name = "lstElementos";
            this.lstElementos.Size = new System.Drawing.Size(220, 109);
            this.lstElementos.TabIndex = 13;
            this.lstElementos.SelectedIndexChanged += new System.EventHandler(this.lstElementos_SelectedIndexChanged);

            // btnRemover
            this.btnRemover.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.btnRemover.FlatAppearance.BorderSize = 0;
            this.btnRemover.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemover.ForeColor = System.Drawing.Color.White;
            this.btnRemover.Location = new System.Drawing.Point(10, 690);
            this.btnRemover.Name = "btnRemover";
            this.btnRemover.Size = new System.Drawing.Size(220, 30);
            this.btnRemover.TabIndex = 14;
            this.btnRemover.Text = "Remover Selecionado";
            this.btnRemover.UseVisualStyleBackColor = false;
            this.btnRemover.Click += new System.EventHandler(this.btnRemover_Click);

            // groupPresets
            this.groupPresets.Controls.Add(this.cmbPresets);
            this.groupPresets.ForeColor = System.Drawing.Color.White;
            this.groupPresets.Location = new System.Drawing.Point(10, 470);
            this.groupPresets.Name = "groupPresets";
            this.groupPresets.Size = new System.Drawing.Size(220, 60);
            this.groupPresets.TabIndex = 15;
            this.groupPresets.TabStop = false;
            this.groupPresets.Text = "Tamanhos Pré-definidos";

            // cmbPresets
            this.cmbPresets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPresets.FormattingEnabled = true;
            this.cmbPresets.Items.AddRange(new object[] {
            "Personalizado",
            "50x30mm (Padrão)",
            "60x40mm",
            "70x30mm",
            "100x50mm"});
            this.cmbPresets.Location = new System.Drawing.Point(10, 25);
            this.cmbPresets.Name = "cmbPresets";
            this.cmbPresets.Size = new System.Drawing.Size(200, 23);
            this.cmbPresets.TabIndex = 0;
            this.cmbPresets.SelectedIndex = 0;
            this.cmbPresets.SelectedIndexChanged += new System.EventHandler(this.cmbPresets_SelectedIndexChanged);

            // Adiciona controles fixos
            this.panelFerramentas.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblFerramentas,
                this.lblTamanho, this.lblLargura, this.numLargura,
                this.lblAltura, this.numAltura,
                this.groupPresets, this.lblElementos,
                this.lstElementos, this.btnRemover
            });

            // panelPropriedades
            this.panelPropriedades.BackColor = System.Drawing.Color.FromArgb(44, 62, 80);
            this.panelPropriedades.Controls.Add(this.btnCor);
            this.panelPropriedades.Controls.Add(this.chkItalico);
            this.panelPropriedades.Controls.Add(this.chkNegrito);
            this.panelPropriedades.Controls.Add(this.numFonte);
            this.panelPropriedades.Controls.Add(this.lblFonte);
            this.panelPropriedades.Controls.Add(this.txtConteudo);
            this.panelPropriedades.Controls.Add(this.lblConteudo);
            this.panelPropriedades.Controls.Add(this.lblPropriedades);
            this.panelPropriedades.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelPropriedades.Location = new System.Drawing.Point(950, 0);
            this.panelPropriedades.Name = "panelPropriedades";
            this.panelPropriedades.Padding = new System.Windows.Forms.Padding(10);
            this.panelPropriedades.Size = new System.Drawing.Size(250, 640);
            this.panelPropriedades.TabIndex = 1;

            // (demais propriedades iguais às originais...)

            // panelCentro
            this.panelCentro.AutoScroll = true;
            this.panelCentro.BackColor = System.Drawing.Color.FromArgb(236, 240, 241);
            this.panelCentro.Controls.Add(this.panelCanvas);
            this.panelCentro.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCentro.Location = new System.Drawing.Point(250, 0);
            this.panelCentro.Name = "panelCentro";
            this.panelCentro.Size = new System.Drawing.Size(700, 640);
            this.panelCentro.TabIndex = 2;

            // panelCanvas
            this.panelCanvas.BackColor = System.Drawing.Color.White;
            this.panelCanvas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelCanvas.Location = new System.Drawing.Point(50, 50);
            this.panelCanvas.Name = "panelCanvas";
            this.panelCanvas.Size = new System.Drawing.Size(200, 120);
            this.panelCanvas.TabIndex = 0;

            // panelBotoes
            this.panelBotoes.BackColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.panelBotoes.Controls.Add(this.btnCancelar);
            this.panelBotoes.Controls.Add(this.btnSalvar);
            this.panelBotoes.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBotoes.Location = new System.Drawing.Point(250, 640);
            this.panelBotoes.Name = "panelBotoes";
            this.panelBotoes.Size = new System.Drawing.Size(700, 60);
            this.panelBotoes.TabIndex = 3;

            // btnSalvar
            this.btnSalvar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSalvar.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnSalvar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSalvar.FlatAppearance.BorderSize = 0;
            this.btnSalvar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalvar.ForeColor = System.Drawing.Color.White;
            this.btnSalvar.Location = new System.Drawing.Point(520, 15);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(140, 30);
            this.btnSalvar.TabIndex = 0;
            this.btnSalvar.Text = "Salvar Template";
            this.btnSalvar.UseVisualStyleBackColor = false;

            // btnCancelar
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(149, 165, 166);
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(380, 15);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(130, 30);
            this.btnCancelar.TabIndex = 1;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;

            // FormDesigner
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.panelCentro);
            this.Controls.Add(this.panelBotoes);
            this.Controls.Add(this.panelPropriedades);
            this.Controls.Add(this.panelFerramentas);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormDesigner";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Designer de Etiqueta";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;

            this.panelFerramentas.ResumeLayout(false);
            this.panelFerramentas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLargura)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAltura)).EndInit();
            this.groupPresets.ResumeLayout(false);
            this.panelPropriedades.ResumeLayout(false);
            this.panelPropriedades.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFonte)).EndInit();
            this.panelCentro.ResumeLayout(false);
            this.panelBotoes.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private void CriarBotoesDinamicos()
        {
            var lblSeparador1 = new System.Windows.Forms.Label
            {
                Text = "━━━━━━━━━━━━━━━━━━━━━━",
                ForeColor = System.Drawing.Color.FromArgb(149, 165, 166),
                Location = new System.Drawing.Point(10, 35),
                AutoSize = true
            };

            var btnSalvarTemplate = new System.Windows.Forms.Button
            {
                Text = "💾 Salvar Template",
                Location = new System.Drawing.Point(10, 45),
                Size = new System.Drawing.Size(220, 30),
                BackColor = System.Drawing.Color.FromArgb(39, 174, 96),
                ForeColor = System.Drawing.Color.White,
                FlatStyle = System.Windows.Forms.FlatStyle.Flat,
                Font = new System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Bold)
            };
            btnSalvarTemplate.FlatAppearance.BorderSize = 0;
            btnSalvarTemplate.Click += BtnSalvarTemplate_Click;

            var btnCarregarTemplate = new System.Windows.Forms.Button
            {
                Text = "📂 Carregar Template",
                Location = new System.Drawing.Point(10, 80),
                Size = new System.Drawing.Size(220, 30),
                BackColor = System.Drawing.Color.FromArgb(241, 196, 15),
                ForeColor = System.Drawing.Color.White,
                FlatStyle = System.Windows.Forms.FlatStyle.Flat,
                Font = new System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Bold)
            };
            btnCarregarTemplate.FlatAppearance.BorderSize = 0;
            btnCarregarTemplate.Click += BtnCarregarTemplate_Click;

            var lblSeparador2 = new System.Windows.Forms.Label
            {
                Text = "━━━━━━━━━━━━━━━━━━━━━━",
                ForeColor = System.Drawing.Color.FromArgb(149, 165, 166),
                Location = new System.Drawing.Point(10, 115),
                AutoSize = true
            };

            var btnConfigEtiqueta = new System.Windows.Forms.Button
            {
                Text = "⚙️ Configurar Papel",
                Location = new System.Drawing.Point(10, 125),
                Size = new System.Drawing.Size(220, 30),
                BackColor = System.Drawing.Color.FromArgb(155, 89, 182),
                ForeColor = System.Drawing.Color.White,
                FlatStyle = System.Windows.Forms.FlatStyle.Flat,
                Font = new System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Bold)
            };
            btnConfigEtiqueta.FlatAppearance.BorderSize = 0;
            btnConfigEtiqueta.Click += BtnConfigEtiqueta_Click;

            var lblSeparador3 = new System.Windows.Forms.Label
            {
                Text = "━━━━━━━━━━━━━━━━━━━━━━",
                ForeColor = System.Drawing.Color.FromArgb(149, 165, 166),
                Location = new System.Drawing.Point(10, 160),
                AutoSize = true
            };

            var btnTexto = CriarBotaoFerramenta("Adicionar Texto", 175);
            btnTexto.Click += btnTexto_Click;

            var btnCampoNome = CriarBotaoFerramenta("Campo: Nome", 210);
            btnCampoNome.Click += btnCampoNome_Click;

            var btnCampoCodigo = CriarBotaoFerramenta("Campo: Código", 245);
            btnCampoCodigo.Click += btnCampoCodigo_Click;

            var btnCampoPreco = CriarBotaoFerramenta("Campo: Preço", 280);
            btnCampoPreco.Click += btnCampoPreco_Click;

            var btnCodigoBarras = CriarBotaoFerramenta("Código de Barras", 315);
            btnCodigoBarras.Click += btnCodigoBarras_Click;

            var btnImagem = CriarBotaoFerramenta("Adicionar Imagem", 350);
            btnImagem.Click += btnImagem_Click;

            panelFerramentas.Controls.AddRange(new System.Windows.Forms.Control[]
            {
                lblSeparador1, btnSalvarTemplate, btnCarregarTemplate, lblSeparador2,
                btnConfigEtiqueta, lblSeparador3,
                btnTexto, btnCampoNome, btnCampoCodigo, btnCampoPreco, btnCodigoBarras, btnImagem
            });
        }

        private System.Windows.Forms.Button CriarBotaoFerramenta(string texto, int y)
        {
            var btn = new System.Windows.Forms.Button
            {
                Text = texto,
                Location = new System.Drawing.Point(10, y),
                Size = new System.Drawing.Size(220, 30),
                BackColor = System.Drawing.Color.FromArgb(52, 152, 219),
                ForeColor = System.Drawing.Color.White,
                FlatStyle = System.Windows.Forms.FlatStyle.Flat,
                TextAlign = System.Drawing.ContentAlignment.MiddleLeft,
                Padding = new System.Windows.Forms.Padding(10, 0, 0, 0)
            };
            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }
    }
}
