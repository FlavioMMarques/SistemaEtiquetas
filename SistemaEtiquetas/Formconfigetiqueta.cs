using System;
using System.Drawing;
using System.Windows.Forms;

namespace SistemaEtiquetas
{
    public partial class FormConfigEtiqueta : Form
    {
        private ConfiguracaoEtiqueta configuracao;
        private const float ESCALA_PREVIEW = 3.0f;

        public ConfiguracaoEtiqueta Configuracao => configuracao;

        public FormConfigEtiqueta(ConfiguracaoEtiqueta configAtual = null)
        {
            InitializeComponent();

            // Inicializa com configuração atual ou valores padrão
            configuracao = configAtual ?? new ConfiguracaoEtiqueta
            {
                NomeEtiqueta = "Gondola com Barras",
                ImpressoraPadrao = "BTP-L42(D)",
                PapelPadrao = "Tamanho do papel-SoftcomGondBar",
                LarguraEtiqueta = 100,
                AlturaEtiqueta = 30,
                NumColunas = 1,
                NumLinhas = 1,
                EspacamentoColunas = 0,
                EspacamentoLinhas = 0,
                MargemSuperior = 0,
                MargemInferior = 0,
                MargemEsquerda = 0,
                MargemDireita = 0
            };

            CarregarConfiguracoes();
            ConfigurarEventos();
            AtualizarPreview();
        }

        private void ConfigurarEventos()
        {
            // Eventos de mudança de valores
            txtNomeEtiqueta.TextChanged += (s, e) => AtualizarConfiguracao();
            cmbImpressora.SelectedIndexChanged += (s, e) => AtualizarConfiguracao();
            cmbPapel.SelectedIndexChanged += (s, e) => AtualizarConfiguracao();

            numLargura.ValueChanged += (s, e) => { AtualizarConfiguracao(); AtualizarPreview(); };
            numAltura.ValueChanged += (s, e) => { AtualizarConfiguracao(); AtualizarPreview(); };
            numColunas.ValueChanged += (s, e) => { AtualizarConfiguracao(); AtualizarPreview(); };
            numLinhas.ValueChanged += (s, e) => { AtualizarConfiguracao(); AtualizarPreview(); };
            numEspacamentoColunas.ValueChanged += (s, e) => { AtualizarConfiguracao(); AtualizarPreview(); };
            numEspacamentoLinhas.ValueChanged += (s, e) => { AtualizarConfiguracao(); AtualizarPreview(); };
            numMargemSuperior.ValueChanged += (s, e) => { AtualizarConfiguracao(); AtualizarPreview(); };
            numMargemInferior.ValueChanged += (s, e) => { AtualizarConfiguracao(); AtualizarPreview(); };
            numMargemEsquerda.ValueChanged += (s, e) => { AtualizarConfiguracao(); AtualizarPreview(); };
            numMargemDireita.ValueChanged += (s, e) => { AtualizarConfiguracao(); AtualizarPreview(); };

            // Evento de pintura do preview
            panelPreview.Paint += PanelPreview_Paint;
        }

        private void CarregarConfiguracoes()
        {
            // Carrega valores na interface
            txtNomeEtiqueta.Text = configuracao.NomeEtiqueta;

            // Carrega impressoras disponíveis
            CarregarImpressoras();
            if (cmbImpressora.Items.Contains(configuracao.ImpressoraPadrao))
                cmbImpressora.SelectedItem = configuracao.ImpressoraPadrao;
            else if (cmbImpressora.Items.Count > 0)
                cmbImpressora.SelectedIndex = 0;

            // Carrega tipos de papel
            CarregarTiposPapel();
            if (cmbPapel.Items.Contains(configuracao.PapelPadrao))
                cmbPapel.SelectedItem = configuracao.PapelPadrao;
            else if (cmbPapel.Items.Count > 0)
                cmbPapel.SelectedIndex = 0;

            // Dimensões da etiqueta
            numLargura.Value = (decimal)configuracao.LarguraEtiqueta;
            numAltura.Value = (decimal)configuracao.AlturaEtiqueta;

            // Layout
            numColunas.Value = configuracao.NumColunas;
            numLinhas.Value = configuracao.NumLinhas;
            numEspacamentoColunas.Value = (decimal)configuracao.EspacamentoColunas;
            numEspacamentoLinhas.Value = (decimal)configuracao.EspacamentoLinhas;

            // Margens
            numMargemSuperior.Value = (decimal)configuracao.MargemSuperior;
            numMargemInferior.Value = (decimal)configuracao.MargemInferior;
            numMargemEsquerda.Value = (decimal)configuracao.MargemEsquerda;
            numMargemDireita.Value = (decimal)configuracao.MargemDireita;
        }

        private void CarregarImpressoras()
        {
            cmbImpressora.Items.Clear();

            // Adiciona impressoras instaladas no sistema
            foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                cmbImpressora.Items.Add(printer);
            }

            // Adiciona impressora padrão do exemplo
            //if (!cmbImpressora.Items.Contains("BTP-L42(D)"))
               // cmbImpressora.Items.Add("BTP-L42(D)");
        }

        private void CarregarTiposPapel()
        {
            cmbPapel.Items.Clear();
            cmbPapel.Items.AddRange(new object[]
            {
                "Tamanho do papel-SoftcomGondBar",
                "A4 (210 x 297 mm)",
                "Carta (216 x 279 mm)",
                "Etiqueta 100x50 mm",
                "Etiqueta 100x30 mm",
                "Etiqueta 50x30 mm",
                "Personalizado"
            });
        }

        private void AtualizarConfiguracao()
        {
            configuracao.NomeEtiqueta = txtNomeEtiqueta.Text;
            configuracao.ImpressoraPadrao = cmbImpressora.SelectedItem?.ToString() ?? "";
            configuracao.PapelPadrao = cmbPapel.SelectedItem?.ToString() ?? "";
            configuracao.LarguraEtiqueta = (float)numLargura.Value;
            configuracao.AlturaEtiqueta = (float)numAltura.Value;
            configuracao.NumColunas = (int)numColunas.Value;
            configuracao.NumLinhas = (int)numLinhas.Value;
            configuracao.EspacamentoColunas = (float)numEspacamentoColunas.Value;
            configuracao.EspacamentoLinhas = (float)numEspacamentoLinhas.Value;
            configuracao.MargemSuperior = (float)numMargemSuperior.Value;
            configuracao.MargemInferior = (float)numMargemInferior.Value;
            configuracao.MargemEsquerda = (float)numMargemEsquerda.Value;
            configuracao.MargemDireita = (float)numMargemDireita.Value;
        }

        private void AtualizarPreview()
        {
            panelPreview.Invalidate();
        }

        private void PanelPreview_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Fundo branco
            g.Clear(Color.White);

            // Calcula dimensões
            float larguraTotal = configuracao.MargemEsquerda +
                                (configuracao.LarguraEtiqueta * configuracao.NumColunas) +
                                (configuracao.EspacamentoColunas * (configuracao.NumColunas - 1)) +
                                configuracao.MargemDireita;

            float alturaTotal = configuracao.MargemSuperior +
                               (configuracao.AlturaEtiqueta * configuracao.NumLinhas) +
                               (configuracao.EspacamentoLinhas * (configuracao.NumLinhas - 1)) +
                               configuracao.MargemInferior;

            // Calcula escala para caber no painel
            float escalaX = (panelPreview.Width - 20) / (larguraTotal * ESCALA_PREVIEW);
            float escalaY = (panelPreview.Height - 20) / (alturaTotal * ESCALA_PREVIEW);
            float escala = Math.Min(escalaX, escalaY);
            escala = Math.Min(escala, 1.0f); // Não aumentar além do tamanho real

            // Centraliza
            float offsetX = (panelPreview.Width - (larguraTotal * ESCALA_PREVIEW * escala)) / 2;
            float offsetY = (panelPreview.Height - (alturaTotal * ESCALA_PREVIEW * escala)) / 2;

            // Desenha fundo da página
            RectangleF fundoPagina = new RectangleF(
                offsetX,
                offsetY,
                larguraTotal * ESCALA_PREVIEW * escala,
                alturaTotal * ESCALA_PREVIEW * escala
            );
            g.FillRectangle(Brushes.WhiteSmoke, fundoPagina);
            g.DrawRectangle(Pens.DarkGray, Rectangle.Round(fundoPagina));

            // Desenha margens (área cinza)
            using (Brush margemBrush = new SolidBrush(Color.FromArgb(50, 200, 200, 200)))
            {
                // Margem Superior
                if (configuracao.MargemSuperior > 0)
                {
                    g.FillRectangle(margemBrush,
                        offsetX,
                        offsetY,
                        larguraTotal * ESCALA_PREVIEW * escala,
                        configuracao.MargemSuperior * ESCALA_PREVIEW * escala);
                }

                // Margem Inferior
                if (configuracao.MargemInferior > 0)
                {
                    g.FillRectangle(margemBrush,
                        offsetX,
                        offsetY + (alturaTotal - configuracao.MargemInferior) * ESCALA_PREVIEW * escala,
                        larguraTotal * ESCALA_PREVIEW * escala,
                        configuracao.MargemInferior * ESCALA_PREVIEW * escala);
                }

                // Margem Esquerda
                if (configuracao.MargemEsquerda > 0)
                {
                    g.FillRectangle(margemBrush,
                        offsetX,
                        offsetY,
                        configuracao.MargemEsquerda * ESCALA_PREVIEW * escala,
                        alturaTotal * ESCALA_PREVIEW * escala);
                }

                // Margem Direita
                if (configuracao.MargemDireita > 0)
                {
                    g.FillRectangle(margemBrush,
                        offsetX + (larguraTotal - configuracao.MargemDireita) * ESCALA_PREVIEW * escala,
                        offsetY,
                        configuracao.MargemDireita * ESCALA_PREVIEW * escala,
                        alturaTotal * ESCALA_PREVIEW * escala);
                }
            }

            // Desenha etiquetas
            using (Pen etiquetaPen = new Pen(Color.FromArgb(231, 76, 60), 2))
            using (Brush etiquetaBrush = new SolidBrush(Color.FromArgb(30, 231, 76, 60)))
            {
                for (int linha = 0; linha < configuracao.NumLinhas; linha++)
                {
                    for (int coluna = 0; coluna < configuracao.NumColunas; coluna++)
                    {
                        float x = offsetX + (configuracao.MargemEsquerda +
                                            (coluna * (configuracao.LarguraEtiqueta + configuracao.EspacamentoColunas)))
                                            * ESCALA_PREVIEW * escala;

                        float y = offsetY + (configuracao.MargemSuperior +
                                            (linha * (configuracao.AlturaEtiqueta + configuracao.EspacamentoLinhas)))
                                            * ESCALA_PREVIEW * escala;

                        float largura = configuracao.LarguraEtiqueta * ESCALA_PREVIEW * escala;
                        float altura = configuracao.AlturaEtiqueta * ESCALA_PREVIEW * escala;

                        RectangleF etiqueta = new RectangleF(x, y, largura, altura);

                        // Preenche
                        g.FillRectangle(etiquetaBrush, etiqueta);

                        // Contorno
                        g.DrawRectangle(etiquetaPen, Rectangle.Round(etiqueta));

                        // Desenha "rascunho" de código de barras
                        if (altura > 15)
                        {
                            float barrasY = y + (altura * 0.3f);
                            float barrasAltura = altura * 0.4f;
                            float barrasX = x + (largura * 0.1f);
                            float barrasLargura = largura * 0.8f;

                            using (Pen barraPen = new Pen(Color.FromArgb(150, 231, 76, 60), 1))
                            {
                                // Desenha algumas barras simulando código de barras
                                for (float i = 0; i < barrasLargura; i += 3)
                                {
                                    if ((int)(i / 3) % 3 != 0)
                                    {
                                        g.DrawLine(barraPen,
                                            barrasX + i, barrasY,
                                            barrasX + i, barrasY + barrasAltura);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            // Desenha dimensões totais
            using (Font dimensaoFont = new Font("Segoe UI", 8))
            using (Brush textBrush = Brushes.DarkSlateGray)
            {
                string dimensoes = $"{larguraTotal:F1} x {alturaTotal:F1} mm";
                SizeF textSize = g.MeasureString(dimensoes, dimensaoFont);
                g.DrawString(dimensoes, dimensaoFont, textBrush,
                    (panelPreview.Width - textSize.Width) / 2,
                    panelPreview.Height - textSize.Height - 5);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            // Validações
            if (string.IsNullOrWhiteSpace(txtNomeEtiqueta.Text))
            {
                MessageBox.Show("Por favor, informe o nome da etiqueta.", "Atenção",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNomeEtiqueta.Focus();
                return;
            }

            if (cmbImpressora.SelectedIndex < 0)
            {
                MessageBox.Show("Por favor, selecione uma impressora.", "Atenção",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbImpressora.Focus();
                return;
            }

            if (cmbPapel.SelectedIndex < 0)
            {
                MessageBox.Show("Por favor, selecione um tipo de papel.", "Atenção",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbPapel.Focus();
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnTestarImpressao_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Função de teste de impressão será implementada em breve!",
                "Teste de Impressão", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void cmbPapel_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbImpressora_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }

    // Classe para armazenar a configuração da etiqueta
    public class ConfiguracaoEtiqueta
    {
        public string NomeEtiqueta { get; set; }
        public string ImpressoraPadrao { get; set; }
        public string PapelPadrao { get; set; }
        public float LarguraEtiqueta { get; set; }
        public float AlturaEtiqueta { get; set; }
        public int NumColunas { get; set; }
        public int NumLinhas { get; set; }
        public float EspacamentoColunas { get; set; }
        public float EspacamentoLinhas { get; set; }
        public float MargemSuperior { get; set; }
        public float MargemInferior { get; set; }
        public float MargemEsquerda { get; set; }
        public float MargemDireita { get; set; }
    }
}