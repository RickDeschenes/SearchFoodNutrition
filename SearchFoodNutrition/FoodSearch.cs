using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SearchFoodNutrition
{
    public partial class FoodSearch : Form
    {
        public delegate void UpdateStatus();
        public UpdateStatus UpdateStatusMessage;
        AllFoods fds;
        Foods fs;

        public FoodSearch()
        {
            InitializeComponent();
        }

        private void FoodSearch_Load(object sender, EventArgs e)
        {
            //Load the FoodList
            fs.Index = -1;
            fds = new AllFoods();

            fds.Completed += Fs_Completed;
            fds.Status += Fds_Status;

            fds.ProcessFoods();
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

        private void Fs_Completed()
        {
            fs = fds.Current;
            tFood.AutoCompleteCustomSource = fds.GetNames("");
            tFood.Enabled = true;
        }

        private void tFood_TextChanged(object sender, EventArgs e)
        {
            if (fds == null)
                return;

            if (fds.Current.Index > -1)
            {
                tFood.AutoCompleteCustomSource = fds.GetNames(tFood.Text);
                tFood.AutoCompleteMode = AutoCompleteMode.Suggest;
                tFood.AutoCompleteSource = AutoCompleteSource.CustomSource;
            }
            LoadValues();
        }

        private void tFood_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && tFood.Text.Length > 0)
                LoadValues();
        }

        private void tFood_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                LoadValues();
        }

        private void LoadValues()
        {
            Foods lf = fds.LoadActive(tFood.Text);

            if (lf.Index == -1)
                return;
            //we have a value
            //load it to current
            fs = fds.LoadActive(tFood.Text);
            //update the labels
            if (fs.Measure == null)
                vServing.Text = "No Measure or weight supplied";
            else
                vServing.Text = fs.Measure + " - " + fs.Weight + "(grams)";
            vCarbohydrates.Text = fs.Carb.ToString();
            vFibers.Text = fs.Fiber.ToString();
            vFats.Text = fs.Fat.ToString();
        }
    }
}
