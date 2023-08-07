using Persistence;
using DAL;


namespace BL
{
    public class OrderBL
    {
        private OrderDAL orderDAL = new OrderDAL();
        public bool SaveOrder(Order order)
        {
            bool result = orderDAL.CreateOrder(order);
            return result;
        }

        public List<Order> GetOrdersInprogress()
        {
            return new OrderDAL().GetOrdersInprogress();
        }

        public Order GetOrderById(int orderId)
        {
            return new OrderDAL().GetOrderById(orderId);
        }

        public bool UpdateOrder(Order order)
        {
            bool result = orderDAL.UpdateOrder(order);
            return result;
        }
    }
}