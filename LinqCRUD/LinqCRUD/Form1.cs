using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinqCRUD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        StoreDBDataContext dc = new StoreDBDataContext();

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            idTextbox.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            nameTextbox.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            categoryTextbox.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            priceTextbox.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            qtyTextbox.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
        }

        public void showData()
        {

            StoreDBDataContext dc = new StoreDBDataContext();
            dataGridView1.DataSource = dc.Products;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            showData();
        }

        public void clearData()
        {
            idTextbox.Text = "";
            nameTextbox.Text = "";
            priceTextbox.Text = "";
            qtyTextbox.Text = "";
            categoryTextbox.Text = "";
        }

        private void insertBtn_Click(object sender, EventArgs e)
        {

            Product product = new Product();
            product.ProductName = nameTextbox.Text;
            product.ProductCategory =  Convert.ToInt32(categoryTextbox.Text);
            product.Price = Convert.ToInt32(priceTextbox.Text);
            product.Qty = Convert.ToInt32(qtyTextbox.Text);

            StoreDBDataContext dc = new StoreDBDataContext();

            dc.Products.InsertOnSubmit(product);
            dc.SubmitChanges();

            showData();

            MessageBox.Show("Record Inserted");

            clearData();

        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            

            Product product = dc.Products.SingleOrDefault(P => P.ProductId == int.Parse(idTextbox.Text));
            product.ProductName = nameTextbox.Text;
            product.ProductCategory = Convert.ToInt32(categoryTextbox.Text);
            product.Price = Convert.ToInt32(priceTextbox.Text);
            product.Qty = Convert.ToInt32(qtyTextbox.Text);

            dc.SubmitChanges();

            showData();

            MessageBox.Show("Record Updated Successfully");

        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {

            StoreDBDataContext dc = new StoreDBDataContext();

            Product product = dc.Products.SingleOrDefault(P => P.ProductId == int.Parse(idTextbox.Text));

            dc.Products.DeleteOnSubmit(product);
            dc.SubmitChanges();

            showData();
            clearData();

            MessageBox.Show("Record Deleted Successfully");
            
        }
    }
}
