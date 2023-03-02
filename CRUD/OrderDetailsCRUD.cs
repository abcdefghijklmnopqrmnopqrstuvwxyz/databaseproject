using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using WindowsFormsApp1.Tables;

namespace WindowsFormsApp1.CRUD
{
    internal class OrderDetailsCRUD : ICRUD<OrderDetails>
    {
        public void Insert(OrderDetails element)
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

            using (SqlCommand command = new SqlCommand("insert into dbo.order_details (order_id, product_id, amount, paid) values (@order_id, @product_id, @amount, @paid)", connection))
            {
                command.Parameters.Add(new SqlParameter("@order_id", element.Order_ID));
                command.Parameters.Add(new SqlParameter("@product_id", element.Product_ID));
                command.Parameters.Add(new SqlParameter("@amount", element.Amount));
                if (element.Paid)
                    command.Parameters.Add(new SqlParameter("@paid", "1"));
                else
                    command.Parameters.Add(new SqlParameter("@paid", "0"));
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

                using (SqlCommand command = new SqlCommand("delete from dbo.order_details where id = @id", connection))
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

        public IEnumerable<OrderDetails> GetAll()
        {
            SqlConnection connection = DatabaseConnection.Connect();

            using (SqlCommand command = new SqlCommand("select * from dbo.order_details", connection))
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    OrderDetails details = new OrderDetails(
                        Convert.ToInt32(reader[0].ToString()),
                        Convert.ToInt32(reader[1].ToString()),
                        Convert.ToInt32(reader[2].ToString()),
                        Convert.ToInt32(reader[3].ToString()),
                        reader[4].Equals(1) ? true : false
                    );
                    yield return details;
                }
                reader.Close();
            }
        }

        public void Update(OrderDetails element)
        {
            SqlConnection connection = DatabaseConnection.Connect();

            if (CheckForID(element.ID) > 0)
            {
                using (SqlCommand command = new SqlCommand("update dbo.order_details set order_id = @order_id, product_id = @product_id, amout = @amout, paid = @paid where id = @id", connection))
                {
                    command.Parameters.Add(new SqlParameter("@id", element.ID));
                    command.Parameters.Add(new SqlParameter("@order_id", element.Order_ID));
                    command.Parameters.Add(new SqlParameter("@product_id", element.Product_ID));
                    command.Parameters.Add(new SqlParameter("@amout", element.Amount));
                    if (element.Paid)
                        command.Parameters.Add(new SqlParameter("@paid", 1));
                    else
                        command.Parameters.Add(new SqlParameter("@paid", 0));
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
            SqlCommand IDcheck = new SqlCommand("select count(dbo.order_details.id) from dbo.order_details where id = @id", DatabaseConnection.Connect());
            IDcheck.Parameters.AddWithValue("@id", id);
            return (int)IDcheck.ExecuteScalar();
        }

        public bool DuplicateData(OrderDetails element)
        {
            SqlCommand DataCheck = new SqlCommand("select count(dbo.order_details.id) from dbo.order_details where order_id = @order_id and product_id = @product_id and amount = @amount and paid = @paid", DatabaseConnection.Connect());
            DataCheck.Parameters.AddWithValue("@order_id", element.Order_ID);
            DataCheck.Parameters.AddWithValue("@product_id", element.Product_ID);
            DataCheck.Parameters.AddWithValue("@amnout", element.Amount);
            DataCheck.Parameters.AddWithValue("@paid", element.Paid);
            return (int)DataCheck.ExecuteScalar() > 0;
        }

    }
}