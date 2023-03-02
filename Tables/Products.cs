namespace WindowsFormsApp1.Tables
{
    public class Products : IBaseID
    {
        private int _id;
        private int _developer_id;
        private string _product;
        private float _price;

        public int ID { get => _id; set => _id = value; }
        public int Developer_ID { get => _developer_id; set => _developer_id = value; }
        public string Product { get => _product; set => _product = value; }
        public float Price { get => _price; set => _price = value; }

        public Products(int id, int developer_id, string name, float price)
        {
            ID = id;
            Developer_ID = developer_id;
            Product = name;
            Price = price;
        }

        public Products(int developer_ID, string product, float price)
        {
            Developer_ID = developer_ID;
            Product = product;
            Price = price;
        }

        public Products() { }

    }
}