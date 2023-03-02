using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using WindowsFormsApp1.Tables;

namespace WindowsFormsApp1.CRUD
{
    internal class DevelopersCRUD : ICRUD<Developers>
    {
        public void Insert(Developers element)
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

            using (SqlCommand command = new SqlCommand("insert into dbo.developers (developer, country) values (@developer, @country)", connection))
            {
                command.Parameters.Add(new SqlParameter("@developer", element.Developer));
                command.Parameters.Add(new SqlParameter("@country", element.Country));
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
                using (SqlCommand command = new SqlCommand("delete from dbo.developers where id = @id", connection))
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

        public IEnumerable<Developers> GetAll()
        {
            SqlConnection connection = DatabaseConnection.Connect();

            using (SqlCommand command = new SqlCommand("select * from dbo.developers", connection))
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Developers developer = new Developers(
                        Convert.ToInt32(reader[0].ToString()),
                        reader[1].ToString(),
                        reader[2].ToString()
                    );
                    yield return developer;
                }
                reader.Close();
            }
        }

        public void Update(Developers element)
        {
            SqlConnection connection = DatabaseConnection.Connect();

            if (CheckForID(element.ID) > 0)
            {
                using (SqlCommand command = new SqlCommand("update dbo.developers set developer = @developer, country = @country where id = @id", connection))
                {
                    command.Parameters.Add(new SqlParameter("@id", element.ID));
                    command.Parameters.Add(new SqlParameter("@developer", element.Developer));
                    command.Parameters.Add(new SqlParameter("@country", element.Country));
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
            SqlCommand IDcheck = new SqlCommand("select count(dbo.developers.id) from dbo.developers where id = @id", DatabaseConnection.Connect());
            IDcheck.Parameters.AddWithValue("@id", id);
            return (int)IDcheck.ExecuteScalar();
        }

        public bool DuplicateData(Developers element)
        {
            SqlCommand DataCheck = new SqlCommand("select count(dbo.developers.id) from dbo.developers where developer = @developer and country = @country", DatabaseConnection.Connect());
            DataCheck.Parameters.AddWithValue("@developer", element.Developer);
            DataCheck.Parameters.AddWithValue("@country", element.Country);
            return (int)DataCheck.ExecuteScalar() > 0;
        }

    }
}