namespace WindowsFormsApp1.Tables
{
    public class Developers : IBaseID
    {
        private int _id;
        private string _developer;
        private string _country;

        public int ID { get => _id; set => _id = value; }
        public string Developer { get => _developer; set => _developer = value; }
        public string Country { get => _country; set => _country = value; }

        public Developers(int id, string developer, string country)
        {
            ID = id;
            Developer = developer;
            Country = country;
        }

        public Developers(string developer, string country)
        {
            Developer = developer;
            Country = country;
        }

        public Developers() { }

    }
}