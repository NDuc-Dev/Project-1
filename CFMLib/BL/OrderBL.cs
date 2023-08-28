using Persistence;
using DAL;


namespace BL
{
    public class OrderBL
    {
        private OrderDAL orderDAL = new OrderDAL();
        public bool CreateOrder(Order order)
        {
            bool result = orderDAL.SaveOrder(order);
            return result;
        }
        public bool UpdateOrder(Order order)
        {
            bool result = orderDAL.UpdateOrder(order);
            return result;
        }

        public bool DeleteOrder(Order order)
        {
            return orderDAL.DeleteOrder(order);
        }

        public List<Order> GetOrdersInBar()
        {
            return orderDAL.GetOrdersInBar();
        }

        public Order GetOrderByTable(int tableId)
        {
            return orderDAL.GetOrderByTable(tableId);
        }

        public List<Order> GetTakeAwayOrders()
        {
            return orderDAL.GetTakeAwayOrders();
        }
        
        public bool CompleteOrder(Order order)
        {
            return orderDAL.CompleteOrder(order);
        }
        public Order GetOrderById(int orderId)
        {
            return orderDAL.GetOrderById(orderId);
        }

        public List<Order> GetOrdersCompleteInDay()
        {
            return orderDAL.GetOrdersCompleteInDay();
        }

         public bool ChangeTableOrder(int newTableId, Order order)
        {
            bool result;
            result = orderDAL.ChangeTableOrder(newTableId, order);
            return result;
        }
    }
}