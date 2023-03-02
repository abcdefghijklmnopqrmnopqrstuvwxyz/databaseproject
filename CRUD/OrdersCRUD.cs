using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using WindowsFormsApp1.Tables;

namespace WindowsFormsApp1.CRUD
{
    internal class OrdersCRUD : ICRUD<Orders>
    {
        public void Insert(Orders element)
        {
            SqlConnection connection = DatabaseConnection.Connect();

            if (DuplicateData(element))
            {
                DialogResult result = MessageBox.Show("Duplicate data warning", "You are trying to insert duplicate data. Do you wants to continue?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.No)
                {
                    return;
                }
            }

            using (SqlCommand command = new SqlCommand("insert into dbo.orders (cust_id, date) values (@cust_id, @date)", connection))
            {
                command.Parameters.Add(new SqlParameter("@cust_id", element.Customer_ID));
                command.Parameters.Add(new SqlParameter("@date", element.Date));
                command.ExecuteNonQuery();
                command.CommandText = "Select @@Identity";
                element.ID = Convert.ToInt32(command.ExecuteScalar());
            }
        }

        public void Delete(int id)
        {
            SqlConnection connection = DatabaseConnection.Connect();

            if (CheckForID(id) > 0)
            {
                using (SqlCommand command = new SqlCommand("delete from dbo.orders where id = @id", connection))
                {
                    command.Parameters.Add(new SqlParameter("@id", id));
                    command.ExecuteNonQuery();
                }
            }
            else
            {
                MessageBox.Show("User ID not found. No deletes were made.", "Error with deleting.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
}

        public IEnumerable<Orders> GetAll()
        {
            SqlConnection connection = DatabaseConnection.Connect();

            using (SqlCommand command = new SqlCommand("select * from dbo.orders", connection))
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Orders order = new Orders(
                        Convert.ToInt32(reader[0].ToString()),
                        Convert.ToInt32(reader[1].ToString()),
                        DateTime.Parse(reader[2].ToString())
                    );
                    yield return order;
                }
                reader.Close();
            }
        }

        public void Update(Orders element)
        {
            SqlConnection connection = DatabaseConnection.Connect();

            if (CheckForID(element.ID) > 0)
            {
                using (SqlCommand command = new SqlCommand("update dbo.orders set cust_id = @cust_id, date = @date where id = @id", connection))
                {
                    command.Parameters.Add(new SqlParameter("@id", element.ID));
                    command.Parameters.Add(new SqlParameter("@cust_id", element.Customer_ID));
                    command.Parameters.Add(new SqlParameter("@date", element.Date.ToString()));
                    command.ExecuteNonQuery();
                }
            }
            else
            {
                MessageBox.Show("User ID not found. No updates were made.", "Error with updating.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public int CheckForID(int id)
        {
            SqlCommand IDcheck = new SqlCommand("select count(dbo.orders.id) from dbo.orders where id = @id", DatabaseConnection.Connect());
            IDcheck.Parameters.AddWithValue("@id", id);
            return (int)IDcheck.ExecuteScalar();
        }

        public bool DuplicateData(Orders element)
        {
            SqlCommand DataCheck = new SqlCommand("select count(dbo.orders.id) from dbo.orders where cust_id = @cust_id and date = @date", DatabaseConnection.Connect());
            DataCheck.Parameters.AddWithValue("@cust_id", element.Customer_ID);
            DataCheck.Parameters.AddWithValue("@date", element.Date);
            return (int)DataCheck.ExecuteScalar() > 0;
        }

    }
}