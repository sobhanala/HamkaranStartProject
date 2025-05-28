using System.Windows.Forms;
using AnbarDomain.Tabels;
using AnbarDomain.Tabels.AnbarDataSetTableAdapters;

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
    }
}