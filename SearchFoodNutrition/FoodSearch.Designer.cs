namespace SearchFoodNutrition
{
    partial class FoodSearch
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
            this.tFood = new System.Windows.Forms.TextBox();
            this.Status = new System.Windows.Forms.Label();
            this.vServing = new System.Windows.Forms.Label();
            this.Serving = new System.Windows.Forms.Label();
            this.vFats = new System.Windows.Forms.Label();
            this.Fats = new System.Windows.Forms.Label();
            this.vFibers = new System.Windows.Forms.Label();
            this.Fibers = new System.Windows.Forms.Label();
            this.vCarbohydrates = new System.Windows.Forms.Label();
            this.Carbohydrates = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tFood
            // 
            this.tFood.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tFood.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.tFood.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.tFood.Enabled = false;
            this.tFood.Location = new System.Drawing.Point(15, 25);
            this.tFood.Name = "tFood";
            this.tFood.Size = new System.Drawing.Size(284, 20);
            this.tFood.TabIndex = 0;
            this.tFood.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tFood_MouseClick);
            this.tFood.TextChanged += new System.EventHandler(this.tFood_TextChanged);
            this.tFood.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tFood_KeyUp);
            // 
            // Status
            // 
            this.Status.AutoSize = true;
            this.Status.Location = new System.Drawing.Point(12, 9);
            this.Status.Name = "Status";
            this.Status.Size = new System.Drawing.Size(164, 13);
            this.Status.TabIndex = 5;
            this.Status.Text = "Loading Food data, please wait...";
            // 
            // vServing
            // 
            this.vServing.AutoSize = true;
            this.vServing.Location = new System.Drawing.Point(128, 55);
            this.vServing.Name = "vServing";
            this.vServing.Size = new System.Drawing.Size(10, 13);
            this.vServing.TabIndex = 7;
            this.vServing.Text = ":";
            // 
            // Serving
            // 
            this.Serving.AutoSize = true;
            this.Serving.Location = new System.Drawing.Point(15, 55);
            this.Serving.Name = "Serving";
            this.Serving.Size = new System.Drawing.Size(104, 13);
            this.Serving.TabIndex = 6;
            this.Serving.Text = "Serving size - weight";
            // 
            // vFats
            // 
            this.vFats.AutoSize = true;
            this.vFats.Location = new System.Drawing.Point(128, 115);
            this.vFats.Name = "vFats";
            this.vFats.Size = new System.Drawing.Size(10, 13);
            this.vFats.TabIndex = 5;
            this.vFats.Text = ":";
            // 
            // Fats
            // 
            this.Fats.AutoSize = true;
            this.Fats.Location = new System.Drawing.Point(15, 115);
            this.Fats.Name = "Fats";
            this.Fats.Size = new System.Drawing.Size(27, 13);
            this.Fats.TabIndex = 4;
            this.Fats.Text = "Fats";
            // 
            // vFibers
            // 
            this.vFibers.AutoSize = true;
            this.vFibers.Location = new System.Drawing.Point(128, 96);
            this.vFibers.Name = "vFibers";
            this.vFibers.Size = new System.Drawing.Size(10, 13);
            this.vFibers.TabIndex = 3;
            this.vFibers.Text = ":";
            // 
            // Fibers
            // 
            this.Fibers.AutoSize = true;
            this.Fibers.Location = new System.Drawing.Point(15, 96);
            this.Fibers.Name = "Fibers";
            this.Fibers.Size = new System.Drawing.Size(35, 13);
            this.Fibers.TabIndex = 2;
            this.Fibers.Text = "Fibers";
            // 
            // vCarbohydrates
            // 
            this.vCarbohydrates.AutoSize = true;
            this.vCarbohydrates.Location = new System.Drawing.Point(128, 77);
            this.vCarbohydrates.Name = "vCarbohydrates";
            this.vCarbohydrates.Size = new System.Drawing.Size(10, 13);
            this.vCarbohydrates.TabIndex = 1;
            this.vCarbohydrates.Text = ":";
            // 
            // Carbohydrates
            // 
            this.Carbohydrates.AutoSize = true;
            this.Carbohydrates.Location = new System.Drawing.Point(15, 77);
            this.Carbohydrates.Name = "Carbohydrates";
            this.Carbohydrates.Size = new System.Drawing.Size(75, 13);
            this.Carbohydrates.TabIndex = 0;
            this.Carbohydrates.Text = "Carbohydrates";
            // 
            // FoodSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(311, 148);
            this.Controls.Add(this.Status);
            this.Controls.Add(this.vServing);
            this.Controls.Add(this.Serving);
            this.Controls.Add(this.tFood);
            this.Controls.Add(this.vFats);
            this.Controls.Add(this.Fats);
            this.Controls.Add(this.Carbohydrates);
            this.Controls.Add(this.vFibers);
            this.Controls.Add(this.vCarbohydrates);
            this.Controls.Add(this.Fibers);
            this.Name = "FoodSearch";
            this.Text = "Food Search";
            this.Load += new System.EventHandler(this.FoodSearch_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tFood;
        private System.Windows.Forms.Label vServing;
        private System.Windows.Forms.Label Serving;
        private System.Windows.Forms.Label vFats;
        private System.Windows.Forms.Label Fats;
        private System.Windows.Forms.Label vFibers;
        private System.Windows.Forms.Label Fibers;
        private System.Windows.Forms.Label vCarbohydrates;
        private System.Windows.Forms.Label Carbohydrates;
        private System.Windows.Forms.Label Status;
    }
}

