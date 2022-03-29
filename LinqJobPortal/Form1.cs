using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinqJobPortal
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        DBJobPortalEntities dBJobPortalEntities = new DBJobPortalEntities();

        private void Form1_Load(object sender, EventArgs e)
        {
            showData();
        }

        private void addBtn_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(textBoxName.Text) || 
                string.IsNullOrEmpty(textBoxCompany.Text) || 
                string.IsNullOrEmpty(textBoxEmail.Text) || 
                string.IsNullOrEmpty(textBoxCity.Text))
            {
                MessageBox.Show("Please Fill All Textbox");
            }
            else
            {
                Employer emp = new Employer();
                emp.EmployerName = textBoxName.Text;
                emp.CompanyName = textBoxCompany.Text;
                emp.Email = textBoxEmail.Text;
                emp.City = textBoxCity.Text;

                dBJobPortalEntities.Employers.Add(emp);
                dBJobPortalEntities.SaveChanges();

                showData();

                MessageBox.Show("Record Inserted Successfully");

                clearData();
            }

        }

        public void showData()
        {
            dataGridView1.DataSource = dBJobPortalEntities.Employers.ToList();
        }

        public void clearData()
        {
            textBoxCity.Text = "";
            textBoxCompany.Text = "";
            textBoxEmail.Text = "";
            textBoxName.Text = "";
            textBoxId.Text = "";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBoxId.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBoxName.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBoxCompany.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textBoxEmail.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString(); 
            textBoxCity.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(textBoxId.Text) ||
                string.IsNullOrEmpty(textBoxName.Text) ||
               string.IsNullOrEmpty(textBoxCompany.Text) ||
               string.IsNullOrEmpty(textBoxEmail.Text) ||
               string.IsNullOrEmpty(textBoxCity.Text))
            {
                MessageBox.Show("Please Fill All Textbox");
            }
            else
            {

            int empId = Convert.ToInt32(textBoxId.Text);

            Employer emp = dBJobPortalEntities.Employers.Find(empId);
            emp.EmployerName = textBoxName.Text;
            emp.CompanyName = textBoxCompany.Text;
            emp.Email = textBoxEmail.Text;
            emp.City = textBoxCity.Text;

            dBJobPortalEntities.SaveChanges();

            showData();

            MessageBox.Show("Record Updated Successfully");

            clearData();

            }

        }

        private void deleteBtn_Click(object sender, EventArgs e){

            if (string.IsNullOrEmpty(textBoxId.Text))
            {
                MessageBox.Show("Something Wan't Wrong");
            }
            else
            {

                int empId = Convert.ToInt32(textBoxId.Text);

                Employer emp = dBJobPortalEntities.Employers.Find(empId);

                dBJobPortalEntities.Employers.Remove(emp);
                dBJobPortalEntities.SaveChanges();

                showData();

                MessageBox.Show("Record Deleted Successfully");

                clearData();
            }

        }

        private void searchBtn_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(textBoxSearch.Text))
            {
                MessageBox.Show("Search TextBox Can't Be Null");
            }
            else
            {
            string name = textBoxSearch.Text;
            dataGridView1.DataSource = (from emp in dBJobPortalEntities.Employers
                                        select new
                                        {
                                            emp.EmployerId,
                                            emp.EmployerName,
                                            emp.Email,
                                            emp.City,
                                            emp.CompanyName
                                        }).Where(emp => emp.EmployerName.Contains(name)
                                        || emp.CompanyName.Contains(name))
                                        .ToList();


            }

        }

        private void showAllBtn_Click(object sender, EventArgs e)
        {
            showData();
        }
    }
}
