using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace SistemaEtiquetas
{
    public partial class FormDesigner : Form
    {
        private TemplateEtiqueta template;
        private Panel panelCanvas;
        private ElementoEtiqueta elementoSelecionado;
        private Point pontoInicial;
        private bool arrastando = false;
        private bool redimensionando = false;
        private string handleRedimensionamento = "";
        private float escala = 4.0f; // pixels por mm
        private ListBox lstElementos;
        private Point offsetArrastar; // Offset do mouse em relação ao canto do elemento

        public FormDesigner(TemplateEtiqueta templateAtual)
        {
            this.template = new TemplateEtiqueta
            {
                Largura = templateAtual.Largura,
                Altura = templateAtual.Altura,
                Elementos = new List<ElementoEtiqueta>(templateAtual.Elementos.Select(e => ClonarElemento(e)))
            };

            InitializeComponent();
            AtualizarListaElementos();
        }

        private void InitializeComponent()
        {
            this.Text = "Designer de Etiqueta";
            this.Size = new Size(1200, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Maximized;

            // Painel esquerdo - Ferramentas
            Panel panelFerramentas = new Panel
            {
                Dock = DockStyle.Left,
                Width = 250,
                BackColor = Color.FromArgb(44, 62, 80),
                Padding = new Padding(10)
            };

            Label lblFerramentas = new Label
            {
                Text = "FERRAMENTAS",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Location = new Point(10, 10),
                AutoSize = true
            };

            // Botões de adicionar elementos
            Button btnTexto = CriarBotaoFerramenta("Adicionar Texto", 50);
            btnTexto.Click += (s, e) => AdicionarElemento(TipoElemento.Texto);

            Button btnCampoNome = CriarBotaoFerramenta("Campo: Nome", 90);
            btnCampoNome.Click += (s, e) => AdicionarCampo("Nome");

            Button btnCampoCodigo = CriarBotaoFerramenta("Campo: Código", 130);
            btnCampoCodigo.Click += (s, e) => AdicionarCampo("Codigo");

            Button btnCampoPreco = CriarBotaoFerramenta("Campo: Preço", 170);
            btnCampoPreco.Click += (s, e) => AdicionarCampo("Preco");

            Button btnCodigoBarras = CriarBotaoFerramenta("Código de Barras", 210);
            btnCodigoBarras.Click += (s, e) => AdicionarElemento(TipoElemento.CodigoBarras);

            Button btnImagem = CriarBotaoFerramenta("Adicionar Imagem", 250);
            btnImagem.Click += (s, e) => AdicionarImagem();

            Label lblTamanho = new Label
            {
                Text = "TAMANHO DA ETIQUETA",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(10, 300),
                AutoSize = true
            };

            Label lblLargura = new Label
            {
                Text = "Largura (mm):",
                ForeColor = Color.White,
                Location = new Point(10, 330),
                AutoSize = true
            };

            NumericUpDown numLargura = new NumericUpDown
            {
                Name = "numLargura",
                Location = new Point(120, 328),
                Width = 100,
                Minimum = 20,
                Maximum = 300,
                DecimalPlaces = 1,
                Value = (decimal)template.Largura
            };
            numLargura.ValueChanged += (s, e) => AtualizarTamanhoCanvas();

            Label lblAltura = new Label
            {
                Text = "Altura (mm):",
                ForeColor = Color.White,
                Location = new Point(10, 360),
                AutoSize = true
            };

            NumericUpDown numAltura = new NumericUpDown
            {
                Name = "numAltura",
                Location = new Point(120, 358),
                Width = 100,
                Minimum = 20,
                Maximum = 300,
                DecimalPlaces = 1,
                Value = (decimal)template.Altura
            };
            numAltura.ValueChanged += (s, e) => AtualizarTamanhoCanvas();

            Label lblElementos = new Label
            {
                Text = "ELEMENTOS",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(10, 400),
                AutoSize = true
            };

            lstElementos = new ListBox
            {
                Name = "lstElementos",
                Location = new Point(10, 430),
                Size = new Size(220, 120),
                BackColor = Color.White
            };
            lstElementos.SelectedIndexChanged += LstElementos_SelectedIndexChanged;

            Button btnRemover = new Button
            {
                Text = "Remover Selecionado",
                Location = new Point(10, 560),
                Size = new Size(220, 30),
                BackColor = Color.FromArgb(231, 76, 60),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnRemover.FlatAppearance.BorderSize = 0;
            btnRemover.Click += BtnRemover_Click;

            panelFerramentas.Controls.AddRange(new Control[] {
                lblFerramentas, btnTexto, btnCampoNome, btnCampoCodigo, btnCampoPreco,
                btnCodigoBarras, btnImagem, lblTamanho, lblLargura, numLargura,
                lblAltura, numAltura, lblElementos, lstElementos, btnRemover
            });

            // Painel direito - Propriedades
            Panel panelPropriedades = new Panel
            {
                Dock = DockStyle.Right,
                Width = 250,
                BackColor = Color.FromArgb(44, 62, 80),
                Padding = new Padding(10)
            };

            Label lblPropriedades = new Label
            {
                Text = "PROPRIEDADES",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Location = new Point(10, 10),
                AutoSize = true
            };

            Label lblConteudo = new Label
            {
                Text = "Conteúdo:",
                ForeColor = Color.White,
                Location = new Point(10, 50),
                AutoSize = true
            };

            TextBox txtConteudo = new TextBox
            {
                Name = "txtConteudo",
                Location = new Point(10, 75),
                Width = 220,
                Enabled = false
            };
            txtConteudo.TextChanged += TxtConteudo_TextChanged;

            Label lblFonte = new Label
            {
                Text = "Tamanho Fonte:",
                ForeColor = Color.White,
                Location = new Point(10, 110),
                AutoSize = true
            };

            NumericUpDown numFonte = new NumericUpDown
            {
                Name = "numFonte",
                Location = new Point(10, 135),
                Width = 80,
                Minimum = 6,
                Maximum = 72,
                Value = 10,
                Enabled = false
            };
            numFonte.ValueChanged += NumFonte_ValueChanged;

            CheckBox chkNegrito = new CheckBox
            {
                Name = "chkNegrito",
                Text = "Negrito",
                ForeColor = Color.White,
                Location = new Point(10, 165),
                AutoSize = true,
                Enabled = false
            };
            chkNegrito.CheckedChanged += ChkNegrito_CheckedChanged;

            CheckBox chkItalico = new CheckBox
            {
                Name = "chkItalico",
                Text = "Itálico",
                ForeColor = Color.White,
                Location = new Point(120, 165),
                AutoSize = true,
                Enabled = false
            };
            chkItalico.CheckedChanged += ChkItalico_CheckedChanged;

            Button btnCor = new Button
            {
                Name = "btnCor",
                Text = "Escolher Cor",
                Location = new Point(10, 195),
                Size = new Size(220, 30),
                BackColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Enabled = false
            };
            btnCor.Click += BtnCor_Click;

            panelPropriedades.Controls.AddRange(new Control[] {
                lblPropriedades, lblConteudo, txtConteudo, lblFonte, numFonte,
                chkNegrito, chkItalico, btnCor
            });

            // Painel central - Canvas
            Panel panelCentro = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(236, 240, 241),
                AutoScroll = true
            };

            panelCanvas = new Panel
            {
                Name = "panelCanvas",
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Location = new Point(50, 50)
            };

            AtualizarTamanhoCanvas();

            panelCanvas.Paint += PanelCanvas_Paint;
            panelCanvas.MouseDown += PanelCanvas_MouseDown;
            panelCanvas.MouseMove += PanelCanvas_MouseMove;
            panelCanvas.MouseUp += PanelCanvas_MouseUp;

            panelCentro.Controls.Add(panelCanvas);

            // Painel inferior - Botões
            Panel panelBotoes = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 60,
                BackColor = Color.FromArgb(52, 73, 94)
            };

            Button btnSalvar = new Button
            {
                Text = "Salvar Template",
                Location = new Point(panelBotoes.Width - 320, 15),
                Size = new Size(140, 30),
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                DialogResult = DialogResult.OK
            };
            btnSalvar.FlatAppearance.BorderSize = 0;
            btnSalvar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            Button btnCancelar = new Button
            {
                Text = "Cancelar",
                Location = new Point(panelBotoes.Width - 170, 15),
                Size = new Size(140, 30),
                BackColor = Color.FromArgb(149, 165, 166),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                DialogResult = DialogResult.Cancel
            };
            btnCancelar.FlatAppearance.BorderSize = 0;
            btnCancelar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            panelBotoes.Controls.AddRange(new Control[] { btnSalvar, btnCancelar });

            this.Controls.AddRange(new Control[] { panelCentro, panelFerramentas, panelPropriedades, panelBotoes });
        }

        private Button CriarBotaoFerramenta(string texto, int y)
        {
            var btn = new Button
            {
                Text = texto,
                Location = new Point(10, y),
                Size = new Size(220, 30),
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(10, 0, 0, 0)
            };
            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }

        private void AtualizarTamanhoCanvas()
        {
            var controles = this.Controls.Find("numLargura", true);
            if (controles.Length == 0) return;

            var numLargura = controles[0] as NumericUpDown;
            var numAltura = this.Controls.Find("numAltura", true)[0] as NumericUpDown;

            template.Largura = (float)numLargura.Value;
            template.Altura = (float)numAltura.Value;

            if (panelCanvas != null)
            {
                panelCanvas.Size = new Size((int)(template.Largura * escala), (int)(template.Altura * escala));
                panelCanvas.Invalidate();
            }
        }

        private void AdicionarElemento(TipoElemento tipo)
        {
            var elemento = new ElementoEtiqueta
            {
                Tipo = tipo,
                Bounds = new Rectangle(10, 10, 100, 30),
                Fonte = new Font("Arial", 10),
                Cor = Color.Black
            };

            if (tipo == TipoElemento.Texto)
            {
                elemento.Conteudo = "Texto";
            }
            else if (tipo == TipoElemento.CodigoBarras)
            {
                elemento.Conteudo = "Codigo";
                elemento.Bounds = new Rectangle(10, 10, 150, 40);
            }

            template.Elementos.Add(elemento);
            AtualizarListaElementos();
            panelCanvas.Invalidate();
        }

        private void AdicionarCampo(string campo)
        {
            var elemento = new ElementoEtiqueta
            {
                Tipo = TipoElemento.Campo,
                Conteudo = campo,
                Bounds = new Rectangle(10, 10, 120, 25),
                Fonte = new Font("Arial", 10),
                Cor = Color.Black
            };

            template.Elementos.Add(elemento);
            AtualizarListaElementos();
            panelCanvas.Invalidate();
        }

        private void AdicionarImagem()
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Imagens|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    var elemento = new ElementoEtiqueta
                    {
                        Tipo = TipoElemento.Imagem,
                        Imagem = Image.FromFile(ofd.FileName),
                        Bounds = new Rectangle(10, 10, 80, 60),
                        Conteudo = Path.GetFileName(ofd.FileName)
                    };

                    template.Elementos.Add(elemento);
                    AtualizarListaElementos();
                    panelCanvas.Invalidate();
                }
            }
        }

        private void AtualizarListaElementos()
        {
            lstElementos.Items.Clear();
            for (int i = 0; i < template.Elementos.Count; i++)
            {
                var elem = template.Elementos[i];
                string descricao = $"{i + 1}. {elem.Tipo}";
                if (!string.IsNullOrEmpty(elem.Conteudo))
                {
                    descricao += $": {elem.Conteudo}";
                }
                lstElementos.Items.Add(descricao);
            }
        }

        private void LstElementos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstElementos.SelectedIndex >= 0)
            {
                elementoSelecionado = template.Elementos[lstElementos.SelectedIndex];
                CarregarPropriedades();
                panelCanvas.Invalidate();
            }
        }

        private void CarregarPropriedades()
        {
            if (elementoSelecionado == null) return;

            var txtConteudo = this.Controls.Find("txtConteudo", true)[0] as TextBox;
            var numFonte = this.Controls.Find("numFonte", true)[0] as NumericUpDown;
            var chkNegrito = this.Controls.Find("chkNegrito", true)[0] as CheckBox;
            var chkItalico = this.Controls.Find("chkItalico", true)[0] as CheckBox;
            var btnCor = this.Controls.Find("btnCor", true)[0] as Button;

            bool podeEditar = elementoSelecionado.Tipo != TipoElemento.Imagem;

            txtConteudo.Text = elementoSelecionado.Conteudo ?? "";
            txtConteudo.Enabled = elementoSelecionado.Tipo == TipoElemento.Texto;

            numFonte.Value = (decimal)elementoSelecionado.Fonte.Size;
            numFonte.Enabled = podeEditar;

            chkNegrito.Checked = elementoSelecionado.Negrito;
            chkNegrito.Enabled = podeEditar;

            chkItalico.Checked = elementoSelecionado.Italico;
            chkItalico.Enabled = podeEditar;

            btnCor.BackColor = elementoSelecionado.Cor;
            btnCor.Enabled = podeEditar;
        }

        private void TxtConteudo_TextChanged(object sender, EventArgs e)
        {
            if (elementoSelecionado != null && elementoSelecionado.Tipo == TipoElemento.Texto)
            {
                elementoSelecionado.Conteudo = ((TextBox)sender).Text;
                panelCanvas.Invalidate();
            }
        }

        private void NumFonte_ValueChanged(object sender, EventArgs e)
        {
            if (elementoSelecionado != null)
            {
                FontStyle estilo = FontStyle.Regular;
                if (elementoSelecionado.Negrito) estilo |= FontStyle.Bold;
                if (elementoSelecionado.Italico) estilo |= FontStyle.Italic;

                elementoSelecionado.Fonte = new Font(elementoSelecionado.Fonte.FontFamily, (float)((NumericUpDown)sender).Value, estilo);
                panelCanvas.Invalidate();
            }
        }

        private void ChkNegrito_CheckedChanged(object sender, EventArgs e)
        {
            if (elementoSelecionado != null)
            {
                elementoSelecionado.Negrito = ((CheckBox)sender).Checked;
                AtualizarFonte();
            }
        }

        private void ChkItalico_CheckedChanged(object sender, EventArgs e)
        {
            if (elementoSelecionado != null)
            {
                elementoSelecionado.Italico = ((CheckBox)sender).Checked;
                AtualizarFonte();
            }
        }

        private void AtualizarFonte()
        {
            if (elementoSelecionado == null) return;

            FontStyle estilo = FontStyle.Regular;
            if (elementoSelecionado.Negrito) estilo |= FontStyle.Bold;
            if (elementoSelecionado.Italico) estilo |= FontStyle.Italic;

            elementoSelecionado.Fonte = new Font(elementoSelecionado.Fonte.FontFamily, elementoSelecionado.Fonte.Size, estilo);
            panelCanvas.Invalidate();
        }

        private void BtnCor_Click(object sender, EventArgs e)
        {
            if (elementoSelecionado != null)
            {
                using (ColorDialog cd = new ColorDialog())
                {
                    cd.Color = elementoSelecionado.Cor;
                    if (cd.ShowDialog() == DialogResult.OK)
                    {
                        elementoSelecionado.Cor = cd.Color;
                        ((Button)sender).BackColor = cd.Color;
                        panelCanvas.Invalidate();
                    }
                }
            }
        }

        private void BtnRemover_Click(object sender, EventArgs e)
        {
            if (lstElementos.SelectedIndex >= 0)
            {
                template.Elementos.RemoveAt(lstElementos.SelectedIndex);
                elementoSelecionado = null;
                AtualizarListaElementos();
                panelCanvas.Invalidate();
            }
        }

        private void PanelCanvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Desenhar grid
            using (Pen penGrid = new Pen(Color.FromArgb(220, 220, 220)))
            {
                for (int i = 0; i <= template.Largura; i += 5)
                {
                    int x = (int)(i * escala);
                    g.DrawLine(penGrid, x, 0, x, panelCanvas.Height);
                }
                for (int i = 0; i <= template.Altura; i += 5)
                {
                    int y = (int)(i * escala);
                    g.DrawLine(penGrid, 0, y, panelCanvas.Width, y);
                }
            }

            // Desenhar elementos
            foreach (var elem in template.Elementos)
            {
                DesenharElemento(g, elem, null);

                // Destacar elemento selecionado
                if (elem == elementoSelecionado)
                {
                    Rectangle bounds = ConverterParaPixels(elem.Bounds);
                    using (Pen penSelecao = new Pen(Color.Blue, 2))
                    {
                        penSelecao.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                        g.DrawRectangle(penSelecao, bounds);
                    }

                    // Handles de redimensionamento
                    DesenharHandles(g, bounds);
                }
            }
        }

        private void DesenharElemento(Graphics g, ElementoEtiqueta elem, Produto produto)
        {
            Rectangle bounds = ConverterParaPixels(elem.Bounds);

            switch (elem.Tipo)
            {
                case TipoElemento.Texto:
                    using (SolidBrush brush = new SolidBrush(elem.Cor))
                    {
                        StringFormat sf = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center };
                        g.DrawString(elem.Conteudo ?? "Texto", elem.Fonte, brush, bounds, sf);
                    }
                    break;

                case TipoElemento.Campo:
                    string valor = ObterValorCampo(elem.Conteudo, produto);
                    using (SolidBrush brush = new SolidBrush(elem.Cor))
                    {
                        StringFormat sf = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center };
                        g.DrawString(valor, elem.Fonte, brush, bounds, sf);
                    }
                    break;

                case TipoElemento.CodigoBarras:
                    string codigoBarras = produto?.Codigo ?? "000000000";
                    DesenharCodigoBarras(g, codigoBarras, bounds);
                    break;

                case TipoElemento.Imagem:
                    if (elem.Imagem != null)
                    {
                        g.DrawImage(elem.Imagem, bounds);
                    }
                    else
                    {
                        g.FillRectangle(Brushes.LightGray, bounds);
                        g.DrawString("Imagem", new Font("Arial", 8), Brushes.Black, bounds);
                    }
                    break;
            }

            // Borda do elemento
            g.DrawRectangle(Pens.LightGray, bounds);
        }

        private string ObterValorCampo(string campo, Produto produto)
        {
            if (produto == null)
            {
                return $"[{campo}]";
            }

            switch (campo)
            {
                case "Nome": return produto.Nome;
                case "Codigo": return produto.Codigo;
                case "Preco": return produto.Preco.ToString("C2");
                default: return "";
            }
        }

        private void DesenharCodigoBarras(Graphics g, string codigo, Rectangle bounds)
        {
            string codigoLimpo = new string(Array.FindAll(codigo.ToCharArray(), c => char.IsDigit(c)));
            if (string.IsNullOrEmpty(codigoLimpo)) codigoLimpo = "0000000000";
            if (codigoLimpo.Length < 8) codigoLimpo = codigoLimpo.PadLeft(8, '0');

            float larguraBarra = (float)bounds.Width / (codigoLimpo.Length * 2);
            float alturaBarras = bounds.Height * 0.7f;

            for (int i = 0; i < codigoLimpo.Length; i++)
            {
                int digito = int.Parse(codigoLimpo[i].ToString());
                float larguraAtual = (digito % 2 == 0) ? larguraBarra * 1.5f : larguraBarra * 0.8f;
                g.FillRectangle(Brushes.Black, bounds.X + (i * larguraBarra * 2), bounds.Y, larguraAtual, alturaBarras);
            }

            using (Font fontBarcode = new Font("Courier New", 7))
            {
                StringFormat sf = new StringFormat { Alignment = StringAlignment.Center };
                g.DrawString(codigo, fontBarcode, Brushes.Black,
                    new RectangleF(bounds.X, bounds.Y + alturaBarras, bounds.Width, bounds.Height - alturaBarras), sf);
            }
        }

        private void DesenharHandles(Graphics g, Rectangle bounds)
        {
            int tamanhoHandle = 10; // Aumentado para facilitar
            using (SolidBrush brush = new SolidBrush(Color.White))
            using (Pen pen = new Pen(Color.Blue, 2))
            {
                // Cantos
                DesenharHandle(g, brush, pen, bounds.Left, bounds.Top, tamanhoHandle);
                DesenharHandle(g, brush, pen, bounds.Right, bounds.Top, tamanhoHandle);
                DesenharHandle(g, brush, pen, bounds.Left, bounds.Bottom, tamanhoHandle);
                DesenharHandle(g, brush, pen, bounds.Right, bounds.Bottom, tamanhoHandle);

                // Meio das bordas
                DesenharHandle(g, brush, pen, bounds.Left + bounds.Width / 2, bounds.Top, tamanhoHandle);
                DesenharHandle(g, brush, pen, bounds.Left + bounds.Width / 2, bounds.Bottom, tamanhoHandle);
                DesenharHandle(g, brush, pen, bounds.Left, bounds.Top + bounds.Height / 2, tamanhoHandle);
                DesenharHandle(g, brush, pen, bounds.Right, bounds.Top + bounds.Height / 2, tamanhoHandle);
            }
        }

        private void DesenharHandle(Graphics g, SolidBrush brush, Pen pen, int x, int y, int tamanho)
        {
            g.FillRectangle(brush, x - tamanho / 2, y - tamanho / 2, tamanho, tamanho);
            g.DrawRectangle(pen, x - tamanho / 2, y - tamanho / 2, tamanho, tamanho);
        }

        private void PanelCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            pontoInicial = e.Location;

            // Verificar se clicou em algum elemento
            for (int i = template.Elementos.Count - 1; i >= 0; i--)
            {
                var elem = template.Elementos[i];
                Rectangle bounds = ConverterParaPixels(elem.Bounds);

                // Verificar handles de redimensionamento primeiro
                string handle = ObterHandle(bounds, e.Location);
                if (handle != null && elem == elementoSelecionado)
                {
                    redimensionando = true;
                    handleRedimensionamento = handle;
                    panelCanvas.Cursor = Cursors.SizeAll;
                    return;
                }

                // Verificar clique no elemento
                if (bounds.Contains(e.Location))
                {
                    elementoSelecionado = elem;
                    lstElementos.SelectedIndex = i;
                    arrastando = true;

                    // Calcular offset para arrastar de onde clicou
                    offsetArrastar = new Point(e.X - bounds.X, e.Y - bounds.Y);

                    panelCanvas.Cursor = Cursors.SizeAll;
                    panelCanvas.Invalidate();
                    return;
                }
            }

            elementoSelecionado = null;
            lstElementos.SelectedIndex = -1;
            panelCanvas.Cursor = Cursors.Default;
            panelCanvas.Invalidate();
        }

        private void PanelCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (elementoSelecionado == null)
            {
                panelCanvas.Cursor = Cursors.Default;
                return;
            }

            if (arrastando)
            {
                // Calcula a nova posição em pixels considerando o offset
                int novoX = e.X - offsetArrastar.X;
                int novoY = e.Y - offsetArrastar.Y;

                // Converte para milímetros
                var bounds = elementoSelecionado.Bounds;
                bounds.X = (int)(novoX / escala);
                bounds.Y = (int)(novoY / escala);

                // Limitar aos limites do canvas (com margem negativa permitida)
                bounds.X = Math.Max(-5, Math.Min(bounds.X, (int)template.Largura - 5));
                bounds.Y = Math.Max(-5, Math.Min(bounds.Y, (int)template.Altura - 5));

                elementoSelecionado.Bounds = bounds;
                panelCanvas.Invalidate();
            }
            else if (redimensionando)
            {
                var bounds = elementoSelecionado.Bounds;
                Rectangle boundsPixels = ConverterParaPixels(bounds);

                int deltaX = e.X - pontoInicial.X;
                int deltaY = e.Y - pontoInicial.Y;

                switch (handleRedimensionamento)
                {
                    case "TopLeft":
                        bounds.X = (int)((boundsPixels.X + deltaX) / escala);
                        bounds.Y = (int)((boundsPixels.Y + deltaY) / escala);
                        bounds.Width = (int)((boundsPixels.Width - deltaX) / escala);
                        bounds.Height = (int)((boundsPixels.Height - deltaY) / escala);
                        break;
                    case "TopRight":
                        bounds.Y = (int)((boundsPixels.Y + deltaY) / escala);
                        bounds.Width = (int)((boundsPixels.Width + deltaX) / escala);
                        bounds.Height = (int)((boundsPixels.Height - deltaY) / escala);
                        break;
                    case "BottomLeft":
                        bounds.X = (int)((boundsPixels.X + deltaX) / escala);
                        bounds.Width = (int)((boundsPixels.Width - deltaX) / escala);
                        bounds.Height = (int)((boundsPixels.Height + deltaY) / escala);
                        break;
                    case "BottomRight":
                        bounds.Width = (int)((boundsPixels.Width + deltaX) / escala);
                        bounds.Height = (int)((boundsPixels.Height + deltaY) / escala);
                        break;
                    case "Top":
                        bounds.Y = (int)((boundsPixels.Y + deltaY) / escala);
                        bounds.Height = (int)((boundsPixels.Height - deltaY) / escala);
                        break;
                    case "Bottom":
                        bounds.Height = (int)((boundsPixels.Height + deltaY) / escala);
                        break;
                    case "Left":
                        bounds.X = (int)((boundsPixels.X + deltaX) / escala);
                        bounds.Width = (int)((boundsPixels.Width - deltaX) / escala);
                        break;
                    case "Right":
                        bounds.Width = (int)((boundsPixels.Width + deltaX) / escala);
                        break;
                }

                // Tamanho mínimo
                if (bounds.Width < 5) bounds.Width = 5;
                if (bounds.Height < 3) bounds.Height = 3;

                // Limitar aos limites do canvas
                if (bounds.X < 0) bounds.X = 0;
                if (bounds.Y < 0) bounds.Y = 0;
                if (bounds.X + bounds.Width > template.Largura) bounds.Width = (int)template.Largura - bounds.X;
                if (bounds.Y + bounds.Height > template.Altura) bounds.Height = (int)template.Altura - bounds.Y;

                elementoSelecionado.Bounds = bounds;
                panelCanvas.Invalidate();
            }
            else
            {
                // Mudar cursor nos handles
                Rectangle bounds = ConverterParaPixels(elementoSelecionado.Bounds);
                string handle = ObterHandle(bounds, e.Location);

                if (handle != null)
                {
                    switch (handle)
                    {
                        case "TopLeft":
                        case "BottomRight":
                            panelCanvas.Cursor = Cursors.SizeNWSE;
                            break;
                        case "TopRight":
                        case "BottomLeft":
                            panelCanvas.Cursor = Cursors.SizeNESW;
                            break;
                        case "Top":
                        case "Bottom":
                            panelCanvas.Cursor = Cursors.SizeNS;
                            break;
                        case "Left":
                        case "Right":
                            panelCanvas.Cursor = Cursors.SizeWE;
                            break;
                    }
                }
                else if (bounds.Contains(e.Location))
                {
                    panelCanvas.Cursor = Cursors.SizeAll;
                }
                else
                {
                    panelCanvas.Cursor = Cursors.Default;
                }
            }
        }

        private void PanelCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            arrastando = false;
            redimensionando = false;
            handleRedimensionamento = "";

            // Resetar cursor
            if (elementoSelecionado != null)
            {
                Rectangle bounds = ConverterParaPixels(elementoSelecionado.Bounds);
                string handle = ObterHandle(bounds, e.Location);

                if (handle != null)
                {
                    // Manter cursor de redimensionamento
                }
                else if (bounds.Contains(e.Location))
                {
                    panelCanvas.Cursor = Cursors.SizeAll;
                }
                else
                {
                    panelCanvas.Cursor = Cursors.Default;
                }
            }
            else
            {
                panelCanvas.Cursor = Cursors.Default;
            }
        }

        private string ObterHandle(Rectangle bounds, Point ponto)
        {
            int margem = 12; // Aumentado para facilitar o clique

            // Cantos (prioridade maior)
            if (Math.Abs(ponto.X - bounds.Left) <= margem && Math.Abs(ponto.Y - bounds.Top) <= margem)
                return "TopLeft";
            if (Math.Abs(ponto.X - bounds.Right) <= margem && Math.Abs(ponto.Y - bounds.Top) <= margem)
                return "TopRight";
            if (Math.Abs(ponto.X - bounds.Left) <= margem && Math.Abs(ponto.Y - bounds.Bottom) <= margem)
                return "BottomLeft";
            if (Math.Abs(ponto.X - bounds.Right) <= margem && Math.Abs(ponto.Y - bounds.Bottom) <= margem)
                return "BottomRight";

            // Meio das bordas
            if (Math.Abs(ponto.X - (bounds.Left + bounds.Width / 2)) <= margem && Math.Abs(ponto.Y - bounds.Top) <= margem)
                return "Top";
            if (Math.Abs(ponto.X - (bounds.Left + bounds.Width / 2)) <= margem && Math.Abs(ponto.Y - bounds.Bottom) <= margem)
                return "Bottom";
            if (Math.Abs(ponto.X - bounds.Left) <= margem && Math.Abs(ponto.Y - (bounds.Top + bounds.Height / 2)) <= margem)
                return "Left";
            if (Math.Abs(ponto.X - bounds.Right) <= margem && Math.Abs(ponto.Y - (bounds.Top + bounds.Height / 2)) <= margem)
                return "Right";

            return null;
        }

        private Rectangle ConverterParaPixels(Rectangle mmRect)
        {
            return new Rectangle(
                (int)(mmRect.X * escala),
                (int)(mmRect.Y * escala),
                (int)(mmRect.Width * escala),
                (int)(mmRect.Height * escala)
            );
        }

        private ElementoEtiqueta ClonarElemento(ElementoEtiqueta original)
        {
            return new ElementoEtiqueta
            {
                Tipo = original.Tipo,
                Conteudo = original.Conteudo,
                Bounds = original.Bounds,
                Fonte = (Font)original.Fonte.Clone(),
                Cor = original.Cor,
                Imagem = original.Imagem,
                Negrito = original.Negrito,
                Italico = original.Italico
            };
        }

        public TemplateEtiqueta ObterTemplate()
        {
            return template;
        }
    }
}