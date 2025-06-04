using System.Windows.Forms;

namespace AnbarForm.MainForm.Reciteforms.selectors
{
    public partial class BaseSelector : Form
    {
        protected string FieldName { get; set; }
        protected string SearchField { get; set; }

        public BaseSelector()
        {
            InitializeComponent();
        }

        protected void UpdateFieldDisplay()
        {
            this.Text = FieldName + "selector";

            if (lbl_select != null)
                lbl_select.Text += FieldName;

            if (lbl_search != null)
            {
                lbl_search.Text += SearchField;
            }
        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Lbl_select_Click(object sender, System.EventArgs e)
        {

        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}