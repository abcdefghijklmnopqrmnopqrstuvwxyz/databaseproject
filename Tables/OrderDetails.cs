namespace WindowsFormsApp1.Tables
{
    public class OrderDetails : IBaseID
    {
        private int _id;
        private int _order_id;
        private int _product_id;
        private int _amount;
        private bool _paid;

        public int ID { get => _id; set => _id = value; }
        public int Order_ID { get => _order_id; set => _order_id = value; }
        public int Product_ID { get => _product_id; set => _product_id = value; }
        public int Amount { get => _amount; set => _amount = value; }
        public bool Paid { get => _paid; set => _paid = value; }

        public OrderDetails(int id, int order_id, int product_id, int amout, bool paid)
        {
            ID = id;
            Order_ID = order_id;
            Product_ID = product_id;
            Amount = amout;
            Paid = paid;
        }

        public OrderDetails(int order_ID, int product_ID, int amout, bool paid)
        {
            Order_ID = order_ID;
            Product_ID = product_ID;
            Amount = amout;
            Paid = paid;
        }

        public OrderDetails() { }

    }
}