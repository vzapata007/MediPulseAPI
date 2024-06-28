namespace MediPulseAPI.Models
{
    public class Response
    {
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }

        public List<Users> listUsers { get; set; } 

        public Users user { get; set; }

        public List<Medicines> listMedicines { get; set; }

        public Medicines medicines { get; set; }

        public List<Cart> listCart { get; set; }
        public List<Orders> listOrders { get; set; }
        public Orders orders { get; set; }

        public List<OrderItems> listOrderItems { get; set; }

        public OrderItems orderItems { get; set; }
    }
}
