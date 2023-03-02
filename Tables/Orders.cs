using System;

namespace WindowsFormsApp1.Tables
{
    public class Orders : IBaseID
    {
        private int _id;
        private int _customer_id;
        private DateTime _date;

        public int ID { get => _id; set => _id = value; }
        public int Customer_ID { get => _customer_id; set => _customer_id = value; }
        public DateTime Date { get => _date; set => _date = value; }

        public Orders(int id, int customer_id, DateTime date)
        {
            ID = id;
            Customer_ID = customer_id;
            Date = date;
        }

        public Orders(int customer_ID, DateTime date)
        {
            Customer_ID = customer_ID;
            Date = date;
        }

        public Orders() { }

    }
}