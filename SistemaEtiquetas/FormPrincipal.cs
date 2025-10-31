using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace SistemaEtiquetas
{


    // Form Principal
    public partial class FormPrincipal : Form
    {
        private List<Produto> produtos = new List<Produto>();
        private TemplateEtiqueta template;

        public FormPrincipal()
        {
            InitializeComponent();
            template = new TemplateEtiqueta();
        }

        private void InitializeComponent()
        {
            this.Text = "Sistema de Etiquetas - Menu Principal";
            this.Size = new Size(900, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            Panel panelTop = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = Color.FromArgb(52, 73, 94)
            };

            Label lblTitulo = new Label
            {
                Text = "SISTEMA DE ETIQUETAS",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(20, 10),
                AutoSize = true
            };

            Button btnDesigner = new Button
            {
                Text = "Designer de Etiqueta",
                Location = new Point(20, 45),
                Size = new Size(180, 30),
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            btnDesigner.FlatAppearance.BorderSize = 0;
            btnDesigner.Click += BtnDesigner_Click;

            Button btnImprimir = new Button
            {
                Text = "Imprimir Etiquetas",
                Location = new Point(210, 45),
                Size = new Size(150, 30),
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            btnImprimir.FlatAppearance.BorderSize = 0;
            btnImprimir.Click += BtnImprimir_Click;

            panelTop.Controls.AddRange(new Control[] { lblTitulo, btnDesigner, btnImprimir });

            GroupBox groupProduto = new GroupBox
            {
                Text = "Adicionar Produto",
                Location = new Point(10, 90),
                Size = new Size(860, 100),
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };

            Label lblNome = new Label { Text = "Nome:", Location = new Point(10, 30), AutoSize = true };
            TextBox txtNome = new TextBox { Name = "txtNome", Location = new Point(70, 27), Width = 200 };

            Label lblCodigo = new Label { Text = "Código:", Location = new Point(280, 30), AutoSize = true };
            TextBox txtCodigo = new TextBox { Name = "txtCodigo", Location = new Point(340, 27), Width = 120 };

            Label lblPreco = new Label { Text = "Preço:", Location = new Point(470, 30), AutoSize = true };
            TextBox txtPreco = new TextBox { Name = "txtPreco", Location = new Point(520, 27), Width = 100 };

            Label lblQtd = new Label { Text = "Qtd:", Location = new Point(630, 30), AutoSize = true };
            NumericUpDown numQtd = new NumericUpDown
            {
                Name = "numQtd",
                Location = new Point(670, 27),
                Width = 60,
                Minimum = 1,
                Maximum = 1000,
                Value = 1
            };

            Button btnAdicionar = new Button
            {
                Text = "Adicionar",
                Location = new Point(750, 25),
                Size = new Size(90, 25),
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnAdicionar.FlatAppearance.BorderSize = 0;
            btnAdicionar.Click += (s, e) => AdicionarProduto(txtNome.Text, txtCodigo.Text, txtPreco.Text, (int)numQtd.Value);

            groupProduto.Controls.AddRange(new Control[] {
                lblNome, txtNome, lblCodigo, txtCodigo,
                lblPreco, txtPreco, lblQtd, numQtd, btnAdicionar
            });

            DataGridView dgvProdutos = new DataGridView
            {
                Name = "dgvProdutos",
                Location = new Point(10, 200),
                Size = new Size(860, 340),
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = true,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None
            };

            dgvProdutos.Columns.Add(new DataGridViewCheckBoxColumn { Name = "Selecionar", HeaderText = "Sel.", Width = 40 });
            dgvProdutos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Nome", HeaderText = "Nome do Produto", Width = 300 });
            dgvProdutos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Codigo", HeaderText = "Código", Width = 120 });
            dgvProdutos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Preco", HeaderText = "Preço", Width = 100 });
            dgvProdutos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Quantidade", HeaderText = "Qtd", Width = 60 });

            var btnRemover = new DataGridViewButtonColumn
            {
                Name = "Remover",
                HeaderText = "Ação",
                Text = "Remover",
                UseColumnTextForButtonValue = true,
                Width = 100
            };
            dgvProdutos.Columns.Add(btnRemover);
            dgvProdutos.CellContentClick += DgvProdutos_CellContentClick;

            this.Controls.AddRange(new Control[] { panelTop, groupProduto, dgvProdutos });
        }

        private void AdicionarProduto(string nome, string codigo, string preco, int quantidade)
        {
            if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(codigo))
            {
                MessageBox.Show("Nome e Código são obrigatórios!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal precoDecimal;
            if (!decimal.TryParse(preco.Replace(",", "."), System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture, out precoDecimal))
            {
                MessageBox.Show("Preço inválido!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var produto = new Produto
            {
                Nome = nome,
                Codigo = codigo,
                Preco = precoDecimal,
                Quantidade = quantidade
            };

            produtos.Add(produto);

            var dgv = this.Controls.Find("dgvProdutos", true)[0] as DataGridView;
            dgv.Rows.Add(false, produto.Nome, produto.Codigo, produto.Preco.ToString("C2"), produto.Quantidade);

            ((TextBox)this.Controls.Find("txtNome", true)[0]).Clear();
            ((TextBox)this.Controls.Find("txtCodigo", true)[0]).Clear();
            ((TextBox)this.Controls.Find("txtPreco", true)[0]).Clear();
        }

        private void DgvProdutos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var dgv = sender as DataGridView;
            if (e.RowIndex >= 0 && dgv.Columns[e.ColumnIndex].Name == "Remover")
            {
                produtos.RemoveAt(e.RowIndex);
                dgv.Rows.RemoveAt(e.RowIndex);
            }
        }

        private void BtnDesigner_Click(object sender, EventArgs e)
        {
            var formDesigner = new FormDesigner(template);
            if (formDesigner.ShowDialog() == DialogResult.OK)
            {
                template = formDesigner.ObterTemplate();
                MessageBox.Show("Template salvo com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnImprimir_Click(object sender, EventArgs e)
        {
            var produtosSelecionados = ObterProdutosSelecionados();
            if (produtosSelecionados.Count == 0)
            {
                MessageBox.Show("Selecione pelo menos um produto!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (template.Elementos.Count == 0)
            {
                MessageBox.Show("Configure o template primeiro usando o Designer!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var formImpressao = new FormImpressao(produtosSelecionados, template);
            formImpressao.ShowDialog();
        }

        private List<Produto> ObterProdutosSelecionados()
        {
            var selecionados = new List<Produto>();
            var dgv = this.Controls.Find("dgvProdutos", true)[0] as DataGridView;

            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dgv.Rows[i].Cells["Selecionar"].Value))
                {
                    selecionados.Add(produtos[i]);
                }
            }

            return selecionados;
        }
    }

    // Classes de dados
    public class Produto
    {
        public string Nome { get; set; }
        public string Codigo { get; set; }
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }
    }

    public enum TipoElemento
    {
        Texto,
        Campo,
        CodigoBarras,
        Imagem
    }

    public class ElementoEtiqueta
    {
        public TipoElemento Tipo { get; set; }
        public string Conteudo { get; set; } // Para texto fixo ou nome do campo
        public Rectangle Bounds { get; set; }
        public Font Fonte { get; set; }
        public Color Cor { get; set; }
        public Image Imagem { get; set; }
        public bool Negrito { get; set; }
        public bool Italico { get; set; }

        public ElementoEtiqueta()
        {
            Fonte = new Font("Arial", 10);
            Cor = Color.Black;
        }
    }

    public class TemplateEtiqueta
    {
        public float Largura { get; set; } = 50; // mm
        public float Altura { get; set; } = 30; // mm
        public List<ElementoEtiqueta> Elementos { get; set; } = new List<ElementoEtiqueta>();
    }
}