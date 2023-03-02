using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using WindowsFormsApp1.Tables;

namespace WindowsFormsApp1.CRUD
{
    internal class CustomersCRUD : ICRUD<Customers>
    {
        public void Insert(Customers element)
        {
            SqlConnection connection = DatabaseConnection.Connect();

            if (DuplicateData(element))
            {
                DialogResult result = MessageBox.Show("You are trying to insert duplicate data. Do you wants to continue?", "Duplicate data warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.No)
                {
                    return;
                }
            }

            using (SqlCommand command = new SqlCommand("insert into dbo.customers (name, address, state, psc) values (@name, @address, @state, @psc)", connection))
            {
                command.Parameters.Add(new SqlParameter("@name", element.Name));
                command.Parameters.Add(new SqlParameter("@address", element.Address));
                command.Parameters.Add(new SqlParameter("@state", element.State));
                command.Parameters.Add(new SqlParameter("@psc", element.Psc));
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
                using (SqlCommand command = new SqlCommand("delete from dbo.customers where id = @id", connection))
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

        public IEnumerable<Customers> GetAll()
        {
            SqlConnection connection = DatabaseConnection.Connect();

            using (SqlCommand command = new SqlCommand("select * from dbo.customers", connection))
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Customers customer = new Customers(
                        Convert.ToInt32(reader[0].ToString()),
                        reader[1].ToString(),
                        reader[2].ToString(),
                        reader[3].ToString(),
                        Convert.ToInt32(reader[4].ToString())
                    );
                    yield return customer;
                }
                reader.Close();
            }
        }

        public void Update(Customers element)
        {
            SqlConnection connection = DatabaseConnection.Connect();

            if (CheckForID(element.ID) > 0)
            {
                using (SqlCommand command = new SqlCommand("update dbo.customers set name = @name, address = @address, state = @state, psc = @psc where id = @id", connection))
                {
                    command.Parameters.Add(new SqlParameter("@id", element.ID));
                    command.Parameters.Add(new SqlParameter("@name", element.Name));
                    command.Parameters.Add(new SqlParameter("@address", element.Address));
                    command.Parameters.Add(new SqlParameter("@state", element.State));
                    command.Parameters.Add(new SqlParameter("@psc", element.Psc));
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
            SqlCommand IDcheck = new SqlCommand("select count(dbo.customers.id) from dbo.customers where id = @id", DatabaseConnection.Connect());
            IDcheck.Parameters.AddWithValue("@id", id);
            return (int)IDcheck.ExecuteScalar();
        }

        public bool DuplicateData(Customers element)
        {
            SqlCommand DataCheck = new SqlCommand("select count(dbo.customers.id) from dbo.customers where name = @name and address = @address and state = @state and psc = @psc", DatabaseConnection.Connect());
            DataCheck.Parameters.AddWithValue("@name", element.Name);
            DataCheck.Parameters.AddWithValue("@address", element.Address);
            DataCheck.Parameters.AddWithValue("@state", element.State);
            DataCheck.Parameters.AddWithValue("@psc", element.Psc);
            return (int)DataCheck.ExecuteScalar() > 0;
        }

    }
}