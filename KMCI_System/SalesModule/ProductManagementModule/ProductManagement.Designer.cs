namespace KMCI_System.SalesModule.ProductManagementModule
{
    partial class ProductManagement
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            header = new Label();
            line1 = new Label();
            header2 = new Label();
            subHeader = new Label();
            btnAddProduct = new Button();
            txtSearch = new TextBox();
            lblFilterBrand = new Label();
            cboFilterBrand = new ComboBox();
            lblFilterCategory = new Label();
            cboFilterCategory = new ComboBox();
            SuspendLayout();
            // 
            // header
            // 
            header.AutoSize = true;
            header.Font = new Font("Segoe UI", 15F, FontStyle.Bold, GraphicsUnit.Point, 0);
            header.Location = new Point(15, 15);
            header.Name = "label1";
            header.Size = new Size(118, 15);
            header.TabIndex = 1;
            header.Text = "Products";
            //
            // line1
            //
            line1.BackColor = SystemColors.ActiveCaptionText;
            line1.Location = new Point(0, 60);
            line1.Name = "line1";
            line1.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            line1.Size = new Size(150, 1);
            line1.TabIndex = 2;
            // 
            // header2
            // 
            header2.AutoSize = true;
            header2.Font = new Font("Segoe UI", 20F, FontStyle.Bold, GraphicsUnit.Point, 0);
            header2.Location = new Point(10, 80);
            header2.Name = "label1";
            header2.Size = new Size(118, 15);
            header2.TabIndex = 1;
            header2.Text = "Product Management";
            // 
            // subHeader
            // 
            subHeader.AutoSize = true;
            subHeader.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            subHeader.ForeColor = SystemColors.GrayText;
            subHeader.Location = new Point(15, 120);
            subHeader.Name = "label1";
            subHeader.Size = new Size(118, 15);
            subHeader.TabIndex = 1;
            subHeader.Text = "Manage Product List";
            //
            // btnAddProduct
            //
            btnAddProduct.BackColor = Color.FromArgb(0, 120, 215);
            btnAddProduct.ForeColor = Color.White;
            btnAddProduct.FlatStyle = FlatStyle.Flat;
            btnAddProduct.Location = new Point(15, 120);
            btnAddProduct.Name = "btnAddProduct";
            btnAddProduct.Size = new Size(120, 30);
            btnAddProduct.TabIndex = 3;
            btnAddProduct.Text = "Add Product";
            btnAddProduct.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            btnAddProduct.Click += btnAddProduct_Click;
            //
            // txtSearch
            //
            txtSearch.PlaceholderText = "Search Products...";
            txtSearch.Location = new Point(20, 170);
            txtSearch.Size = new Size(400, 30);
            txtSearch.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            txtSearch.TabIndex = 4;
            txtSearch.BorderStyle = BorderStyle.FixedSingle;
            txtSearch.Font = new Font("Segoe UI", 10F);
            txtSearch.TextChanged += TxtSearch_TextChanged;
            //
            // lblFilterCategory
            //
            lblFilterCategory.AutoSize = true;
            lblFilterCategory.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblFilterCategory.Location = new Point(-220, 150);
            lblFilterCategory.Name = "lblFilterCategory";
            lblFilterCategory.Size = new Size(100, 20);
            lblFilterCategory.TabIndex = 7;
            lblFilterCategory.Text = "Filter by Category:";
            lblFilterCategory.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            //
            // cboFilterCategory
            //
            cboFilterCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cboFilterCategory.Font = new Font("Segoe UI", 10F);
            cboFilterCategory.Location = new Point(-220, 170);
            cboFilterCategory.Name = "cboFilterCategory";
            cboFilterCategory.Size = new Size(200, 30);
            cboFilterCategory.TabIndex = 8;
            cboFilterCategory.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            cboFilterCategory.FlatStyle = FlatStyle.Flat;
            cboFilterCategory.SelectedIndexChanged += CboFilterCategory_SelectedIndexChanged;
            //
            // lblFilterBrand
            //
            lblFilterBrand.AutoSize = true;
            lblFilterBrand.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblFilterBrand.Location = new Point(-430, 150);
            lblFilterBrand.Name = "lblFilterBrand";
            lblFilterBrand.Size = new Size(80, 20);
            lblFilterBrand.TabIndex = 5;
            lblFilterBrand.Text = "Filter by Brand:";
            lblFilterBrand.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            //
            // cboFilterBrand
            //
            cboFilterBrand.DropDownStyle = ComboBoxStyle.DropDownList;
            cboFilterBrand.Font = new Font("Segoe UI", 10F);
            cboFilterBrand.Location = new Point(-430, 170);
            cboFilterBrand.Name = "cboFilterBrand";
            cboFilterBrand.Size = new Size(200, 30);
            cboFilterBrand.TabIndex = 6;
            cboFilterBrand.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            cboFilterBrand.FlatStyle = FlatStyle.Flat;
            cboFilterBrand.SelectedIndexChanged += CboFilterBrand_SelectedIndexChanged;
            // 
            // ProductManagement
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(header);
            Controls.Add(line1);
            Controls.Add(header2);
            Controls.Add(subHeader);
            Controls.Add(btnAddProduct);
            Controls.Add(txtSearch);
            Controls.Add(lblFilterBrand);
            Controls.Add(cboFilterBrand);
            Controls.Add(lblFilterCategory);
            Controls.Add(cboFilterCategory);
            Name = "ProductManagement";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label header;
        private Label line1;
        private Label header2;
        private Label subHeader;
        private Button btnAddProduct;
        private TextBox txtSearch;
        private Label lblFilterBrand;
        private ComboBox cboFilterBrand;
        private Label lblFilterCategory;
        private ComboBox cboFilterCategory;
    }
}
