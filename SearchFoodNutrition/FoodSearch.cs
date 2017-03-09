using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SearchFoodNutrition
{
    public partial class FoodSearch : Form
    {
        public delegate void UpdateStatus();
        public UpdateStatus UpdateStatusMessage;

        private AllFoods fds;
        private Foods fs;
        private BindingSource bs = new BindingSource();
        private BindingNavigator bn;

        public FoodSearch()
        {
            InitializeComponent();
        }

        private void FoodSearch_Load(object sender, EventArgs e)
        {
            //Load the FoodList
            fs.Index = -1;
            fds = new AllFoods();

            fds.Completed += FoodSearch_Completed;
            fds.Status += Fds_Status;

            fds.ProcessFoods();
            
            bn = bindingNavigator1;
            bn.BindingSource = bs;
            dgvFood.DataSource = bs;
            dgvFood.RowHeadersVisible = false;
            dgvFood.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        delegate void ChangeTextCallback(string text);

        private void Fds_Status(string message)
        {
            if (this.Status.InvokeRequired)
            {
                ChangeTextCallback MethodCallback = new ChangeTextCallback(Fds_Status);
                this.Invoke(MethodCallback, new object[] { message });
            }
            else
            {
                this.Status.Text = message;
            }
            Status.Text = message;
        }

        private void FoodSearch_Completed()
        {
            fs = fds.Current;
            bs.DataSource = fds.GetNames("").ConvertAll(x => new { Value = x });
            tFood.Enabled = true;
            dgvFood.Enabled = true;
        }

        private void tFood_TextChanged(object sender, EventArgs e)
        {
            if (fds == null)
                return;

            if (fds.Current.Index > -1)
                bs.DataSource = fds.GetNames(tFood.Text).ConvertAll(x => new { Value = x });
        }

        private void LoadValues()
        {
            if (dgvFood.SelectedRows.Count == 0)
                return;

            string value = dgvFood.SelectedRows[0].Cells[0].Value.ToString();
            Foods lf = fds.LoadActive(value);

            if (lf.Index == -1)
                return;
            //we have a value
            //load it to current
            fs = fds.LoadActive(value);
            //update the labels
            if (fs.Measure == null)
                vServing.Text = "No Measure or weight supplied";
            else
                vServing.Text = fs.Measure + " - " + fs.Weight + "(grams)";
            vCarbohydrates.Text = fs.Carb.ToString();
            vFibers.Text = fs.Fiber.ToString();
            vFats.Text = fs.Fat.ToString();
        }

        private void dgvFood_SelectionChanged(object sender, EventArgs e)
        {
            LoadValues();
        }

        private void dgvFood_DataSourceChanged(object sender, EventArgs e)
        {
            if (dgvFood.ColumnCount > 0)
                dgvFood.Columns[0].HeaderText = "Foods";
        }

        private void dgvFood_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            dgvFood.Columns[0].HeaderText = "Foods";
        }
    }
}
