namespace Movies.Client.Models
{
    public class OrderMovie
    {
        public string Address { get; private set; } = string.Empty;

        public OrderMovie(string address)
        {
            Address = address;
        }
    }
}
