using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using WindowsFormsApp1.CRUD;
using WindowsFormsApp1.Tables;

namespace WindowsFormsApp1
{
    public partial class DsManagement : Form
    {
        public DsManagement()
        {
            InitializeComponent();
        }

        private void DsManagement_Load(object sender, EventArgs e)
        {
            this.ReloadData();
        }

        private int CheckTab()
        {
            if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage1"])
                return 1;
            if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage2"])
                return 2;
            if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage3"])
                return 3;
            if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage4"])
                return 4;
            if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage5"])
                return 5;
            return 0;
        }

        private void ReloadData()
        {
            this.order_detailsTableAdapter.Fill(this.dataSet.order_details);
            this.productsTableAdapter.Fill(this.dataSet.products);
            this.ordersTableAdapter.Fill(this.dataSet.orders);
            this.developersTableAdapter.Fill(this.dataSet.developers);
            this.customersTableAdapter.Fill(this.dataSet.customers);
        }

        private void RefreshGrids()
        {
            this.dataSet.Clear();
            this.ReloadData();
            this.dataGridView1.Refresh();
            this.dataGridView2.Refresh();
            this.dataGridView3.Refresh();
            this.dataGridView4.Refresh();
            this.dataGridView5.Refresh();
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            try
            {
                switch (CheckTab())
                {
                    case 1:
                        if (CheckCustomerNullData())
                        {
                            Customers customer = new Customers(
                                textBox5.Text,
                                textBox3.Text,
                                textBox4.Text,
                                (int)numericUpDown7.Value
                            );
                            new CustomersCRUD().Insert(customer);
                        }
                        else
                            MessageBox.Show("Some or all required textboxes have not been filled.", "Error with updating.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case 2:
                        if (CheckDeveloperNullData())
                        {
                            Developers developer = new Developers(
                                textBox12.Text,
                                textBox1.Text
                            );
                            new DevelopersCRUD().Insert(developer);
                        }
                        else
                            MessageBox.Show("Some or all required textboxes have not been filled.", "Error with updating.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case 3:
                        if (CheckOrderNullData())
                        {
                            Orders order = new Orders(
                                (int)numericUpDown5.Value,
                                dateTimePicker1.Value
                            );
                            new OrdersCRUD().Insert(order);
                        }
                        else
                            MessageBox.Show("Some or all required textboxes have not been filled.", "Error with updating.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case 4:
                        if (CheckProductNullData())
                        {
                            Products product = new Products(
                                (int)numericUpDown4.Value,
                                textBox9.Text,
                                (float)numericUpDown8.Value
                            );
                            new ProductsCRUD().Insert(product);
                        }
                        else
                            MessageBox.Show("Some or all required textboxes have not been filled.", "Error with updating.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case 5:
                        if (CheckOrder_detailNullData())
                        {
                            OrderDetails orderDetails = new OrderDetails(
                                (int)numericUpDown3.Value,
                                (int)numericUpDown2.Value,
                                (int)numericUpDown6.Value,
                                checkBox1.Checked
                            );
                            new OrderDetailsCRUD().Insert(orderDetails);
                        }
                        else
                            MessageBox.Show("Some or all required textboxes have not been filled.", "Error with updating.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                {
                    MessageBox.Show("Foreign key does not exist.", "Error with inserting.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            finally
            {
                this.RefreshGrids();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            switch (CheckTab())
            {
                case 1:
                    new CustomersCRUD().Delete((int)numericUpDown1.Value);
                    break;
                case 2:
                    new DevelopersCRUD().Delete((int)numericUpDown1.Value);
                    break;
                case 3:
                    new OrdersCRUD().Delete((int)numericUpDown1.Value);
                    break;
                case 4:
                    new ProductsCRUD().Delete((int)numericUpDown1.Value);
                    break;
                case 5:
                    new OrderDetailsCRUD().Delete((int)numericUpDown1.Value);
                    break;
            }
            this.RefreshGrids();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                switch (CheckTab())
                {
                    case 1:
                        if (CheckCustomerNullData() && numericUpDown1.Value > 0)
                        {
                            Customers customer = new Customers(
                                (int)numericUpDown1.Value,
                                textBox5.Text,
                                textBox3.Text,
                                textBox4.Text,
                                (int)numericUpDown7.Value
                            );
                            new CustomersCRUD().Update(customer);
                        }
                        else
                            MessageBox.Show("Some or all required textboxes have not been filled.", "Error with updating.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case 2:
                        if (CheckDeveloperNullData() && numericUpDown1.Value > 0)
                        {
                            Developers developer = new Developers(
                                (int)numericUpDown1.Value,
                                textBox12.Text,
                                textBox1.Text
                            );
                            new DevelopersCRUD().Update(developer);
                        }
                        else
                            MessageBox.Show("Some or all required textboxes have not been filled.", "Error with updating.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case 3:
                        if (CheckOrderNullData() && numericUpDown1.Value > 0)
                        {
                            Orders order = new Orders(
                                (int)numericUpDown1.Value,
                                (int)numericUpDown5.Value,
                                dateTimePicker1.Value
                            );
                            new OrdersCRUD().Update(order);
                        }
                        else
                            MessageBox.Show("Some or all required textboxes have not been filled.", "Error with updating.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case 4:
                        if (CheckProductNullData() && numericUpDown1.Value > 0)
                        {
                            Products product = new Products(
                                (int)numericUpDown1.Value,
                                (int)numericUpDown4.Value,
                                textBox9.Text,
                                (float)numericUpDown8.Value
                            );
                            new ProductsCRUD().Update(product);
                        }
                        else
                            MessageBox.Show("Some or all required textboxes have not been filled.", "Error with updating.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case 5:
                        if (CheckOrder_detailNullData() && numericUpDown1.Value > 0)
                        {
                            OrderDetails orderDetails = new OrderDetails(
                                (int)numericUpDown1.Value,
                                (int)numericUpDown3.Value,
                                (int)numericUpDown2.Value,
                                (int)numericUpDown6.Value,
                                checkBox1.Checked
                            );
                            new OrderDetailsCRUD().Update(orderDetails);
                        }
                        else
                            MessageBox.Show("Some or all required textboxes have not been filled.", "Error with updating.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                {
                    MessageBox.Show("Foreign key does not exist.", "Error with updating.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            finally
            {
                this.RefreshGrids();
            }
        }

        private bool CheckCustomerNullData()
        {
            if (textBox5.Text.Equals("") || textBox3.Text.Equals("") || textBox4.Text.Equals("") || numericUpDown7.Value <= 0)
            {
                return false;
            }
            return true;
        }

        private bool CheckDeveloperNullData()
        {
            if (textBox1.Text.Equals("") || textBox12.Text.Equals(""))
            {
                return false;
            }
            return true;
        }

        private bool CheckOrderNullData()
        {
            if (numericUpDown5.Value <= 0)
            { 
                return false;
            }
            return true;
        }

        private bool CheckProductNullData()
        {
            if (numericUpDown4.Value <= 0 || textBox9.Text.Equals("") || numericUpDown8.Value <= 0)
            {
                return false;
            }
            return true;
        }

        private bool CheckOrder_detailNullData()
        {
            if (numericUpDown3.Value <= 0 || numericUpDown2.Value <= 0 || numericUpDown6.Value <= 0)
            {
                return false;
            }
            return true;
        }

        private void CheckNumBoxes(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '-')
            {
                e.Handled = true;
            }
            else if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                new Import().ImportXml(CheckTab());
            }
            catch
            {
                MessageBox.Show("Data in file are invalid for selected database table.", "Invalid datafile.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                this.RefreshGrids();
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Export function has not been implemented yet.", "Not implemented.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

    }
}