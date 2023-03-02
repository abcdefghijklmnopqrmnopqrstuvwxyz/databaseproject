using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using WindowsFormsApp1.Tables;

namespace WindowsFormsApp1.CRUD
{
    internal class ProductsCRUD : ICRUD<Products>
    {
        public void Insert(Products element)
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

            using (SqlCommand command = new SqlCommand("insert into dbo.products (dev_id, product, price) values (@dev_id, @product, @price)", connection))
            {
                command.Parameters.Add(new SqlParameter("@dev_id", element.Developer_ID));
                command.Parameters.Add(new SqlParameter("@product", element.Product));
                command.Parameters.Add(new SqlParameter("@price", SqlDbType.Decimal)).Value = Decimal.Round((decimal)element.Price, 2);
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
                using (SqlCommand command = new SqlCommand("delete from dbo.products where id = @id", connection))
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

        public IEnumerable<Products> GetAll()
        {
            SqlConnection connection = DatabaseConnection.Connect();

            using (SqlCommand command = new SqlCommand("select * from dbo.products", connection))
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Products product = new Products(
                        Convert.ToInt32(reader[0].ToString()),
                        Convert.ToInt32(reader[1].ToString()),
                        reader[2].ToString(),
                        float.Parse(reader[3].ToString())
                    );
                    yield return product;
                }
                reader.Close();
            }
        }

        public void Update(Products element)
        {
            SqlConnection connection = DatabaseConnection.Connect();

            if (CheckForID(element.ID) > 0)
            {
                using (SqlCommand command = new SqlCommand("update dbo.products set dev_id = @dev_id, product = @product, price = @price where id = @id", connection))
                {
                    command.Parameters.Add(new SqlParameter("@id", element.ID));
                    command.Parameters.Add(new SqlParameter("@dev_id", element.Developer_ID));
                    command.Parameters.Add(new SqlParameter("@product", element.Product));
                    command.Parameters.Add(new SqlParameter("@price", element.Price));
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
            SqlCommand IDcheck = new SqlCommand("select count(dbo.products.id) from dbo.products where id = @id", DatabaseConnection.Connect());
            IDcheck.Parameters.AddWithValue("@id", id);
            return (int)IDcheck.ExecuteScalar();
        }

        public bool DuplicateData(Products element)
        {
            SqlCommand DataCheck = new SqlCommand("select count(dbo.products.id) from dbo.products where dev_id = @dev_id and product = @product and price = @price", DatabaseConnection.Connect());
            DataCheck.Parameters.AddWithValue("@dev_id", element.Developer_ID);
            DataCheck.Parameters.AddWithValue("@product", element.Product);
            DataCheck.Parameters.AddWithValue("@price", element.Price);
            return (int)DataCheck.ExecuteScalar() > 0;
        }

    }
}