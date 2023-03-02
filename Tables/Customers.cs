namespace WindowsFormsApp1.Tables
{
    public class Customers : IBaseID
    {
        private int _id;
        private string _name;
        private string _address;
        private string _state;
        private int _psc;

        public int ID { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public string Address { get => _address; set => _address = value; }
        public string State { get => _state; set => _state = value; }
        public int Psc { get => _psc; set => _psc = value; }

        public Customers(int id, string name, string address, string state, int psc)
        {
            ID = id;
            Name = name;
            Address = address;
            State = state;
            Psc = psc;
        }

        public Customers(string name, string address, string state, int psc)
        {
            Name = name;
            Address = address;
            State = state;
            Psc = psc;
        }

        public Customers() { }

    }
}