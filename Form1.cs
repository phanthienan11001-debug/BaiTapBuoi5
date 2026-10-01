using System;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Windows.Forms;

namespace Lab03_02
{
    public partial class frmSoanThao : Form
    {

        private string currentFilePath = "";

        public frmSoanThao()
        {
            InitializeComponent();
        }

        private void frmSoanThao_Load(object sender, EventArgs e)
        {
            LoadFontsToComboBox();
            LoadSizesToComboBox();

            cmbFont.Text = "Tahoma";
            cmbSize.Text = "14";
            richText.Font = new Font("Tahoma", 14);

            UpdateWordCount();
        }
        private void LoadFontsToComboBox()
        {
            foreach (FontFamily font in new InstalledFontCollection().Families)
            {
                cmbFont.Items.Add(font.Name);
            }
        }


        private void LoadSizesToComboBox()
        {
            int[] sizes = { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };
            foreach (int size in sizes)
            {
                cmbSize.Items.Add(size);
            }
        }

        private void mnuNew_Click(object sender, EventArgs e)
        {
            richText.Clear();
            richText.Font = new Font("Tahoma", 14);
            cmbFont.Text = "Tahoma";
            cmbSize.Text = "14";
            currentFilePath = "";
            UpdateWordCount();

        }

        private void mnuOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Rich Text Format|*.rtf|Text Files|*.txt|All Files|*.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                richText.LoadFile(ofd.FileName, GetFileType(ofd.FileName));
                currentFilePath = ofd.FileName;
                Text = "Soạn thảo văn bản - " + Path.GetFileName(ofd.FileName);
                UpdateWordCount();
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            mnuNew_Click(sender, e);
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            mnuOpen_Click(sender, e);
        }
        private RichTextBoxStreamType GetFileType(string path)
        {
            if (path.EndsWith(".rtf", StringComparison.OrdinalIgnoreCase))
                return RichTextBoxStreamType.RichText;
            return RichTextBoxStreamType.PlainText;
        }

        private void mnuSave_Click(object sender, EventArgs e)
        {
         
            if (string.IsNullOrEmpty(currentFilePath))
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Rich Text Format|*.rtf";
                sfd.DefaultExt = "rtf";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    richText.SaveFile(sfd.FileName, RichTextBoxStreamType.RichText);
                    currentFilePath = sfd.FileName;
                    Text = "Soạn thảo văn bản - " + Path.GetFileName(sfd.FileName);
                    MessageBox.Show("Lưu văn bản thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                richText.SaveFile(currentFilePath, GetFileType(currentFilePath));
                MessageBox.Show("Lưu văn bản thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            mnuSave_Click(sender, e);
        }

        private void btnBold_Click(object sender, EventArgs e)
        {
            Font currentFont = richText.SelectionFont;
            if (currentFont != null)
            {
                FontStyle newStyle = currentFont.Style ^ FontStyle.Bold;
                richText.SelectionFont = new Font(currentFont.FontFamily,
                    currentFont.Size, newStyle);
            }
        }

        private void btnItalic_Click(object sender, EventArgs e)
        {
            Font currentFont = richText.SelectionFont;
            if (currentFont != null)
            {
                FontStyle newStyle = currentFont.Style ^ FontStyle.Italic;
                richText.SelectionFont = new Font(currentFont.FontFamily,
                    currentFont.Size, newStyle);
            }
        }

        private void btnUnderline_Click(object sender, EventArgs e)
        {
            Font currentFont = richText.SelectionFont;
            if (currentFont != null)
            {
                FontStyle newStyle = currentFont.Style ^ FontStyle.Underline;
                richText.SelectionFont = new Font(currentFont.FontFamily,
                    currentFont.Size, newStyle);
            }
        }

        private void mnuFont_Click(object sender, EventArgs e)
        {
            FontDialog fontDlg = new FontDialog();
            fontDlg.ShowColor = true;
            fontDlg.ShowApply = true;
            fontDlg.ShowEffects = true;
            fontDlg.ShowHelp = true;

            if (fontDlg.ShowDialog() != DialogResult.Cancel)
            {
                richText.ForeColor = fontDlg.Color;
                richText.Font = fontDlg.Font;

          
                cmbFont.Text = fontDlg.Font.Name;
                cmbSize.Text = fontDlg.Font.Size.ToString();
            }
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void richText_TextChanged(object sender, EventArgs e)
        {
            UpdateWordCount();
        }
        private void UpdateWordCount()
        {
            string text = richText.Text.Trim();
            int wordCount = 0;

            if (!string.IsNullOrEmpty(text))
            {
               
                string[] words = text.Split(new char[] { ' ', '\t', '\n', '\r' },
                    StringSplitOptions.RemoveEmptyEntries);
                wordCount = words.Length;
            }

            lblWordCount.Text = "Tổng số từ: " + wordCount;
        }

        private void cmbFont_Click(object sender, EventArgs e)
        {
        }

        private void cmbSize_Click(object sender, EventArgs e)
        {

        }

        private void cmbFont_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFont.SelectedItem != null)
            {
                float size = richText.SelectionFont?.Size ?? 14;
                richText.SelectionFont = new Font(cmbFont.SelectedItem.ToString(), size);
            }
        }

        private void cmbSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSize.SelectedItem != null)
            {
                string fontName = richText.SelectionFont?.FontFamily.Name ?? "Tahoma";
                float size = float.Parse(cmbSize.SelectedItem.ToString());
                richText.SelectionFont = new Font(fontName, size);
            }
        }
    }
}
