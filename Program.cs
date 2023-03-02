using System;
using System.Data.SqlClient;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    internal static class Program
    {
        static void Main()
        {
            LoadScreen loadScreen = new LoadScreen();
            new Thread(() => Application.Run(loadScreen)).Start();

            try
            {
                SqlConnection connection = DatabaseConnection.Connect();
                Thread.Sleep(2000);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally 
            {
                loadScreen.Close();
            }

            DsManagement managementScreen = new DsManagement();

            managementScreen.Load += (sender, args) =>
            {
                ((Form)sender).Activate();
            };

            Application.Run(managementScreen);
        }

    }
}