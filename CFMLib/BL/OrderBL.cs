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

        public List<Order> GetOrdersInBarInprogress()
        {
            return orderDAL.GetOrdersInBarInprogress();
        }

        public Order GetOrderByTable(int tableId)
        {
            return orderDAL.GetOrderByTable(tableId);
        }

        public List<Order> GetTakeAwayOrdersInprogress()
        {
            return orderDAL.GetTakeAwayOrdersInprogress();
        }
        
        public List<Order> GetAllOrdersInprogress()
        {
            return orderDAL.GetAllOrdersInprogress();
        }

        public Order GetOrderById(int orderId)
        {
            return orderDAL.GetOrderById(orderId);
        }

        public bool UpdateOrder(Order order)
        {
            bool result = orderDAL.UpdateOrder(order);
            return result;
        }

        public bool ConfirmOrder(Order order)
        {
            bool result = orderDAL.ConfirmOrder(order);
            return result;
        }

        public List<Order> GetOrdersConfirmed()
        {
            return orderDAL.GetOrdersConfirmed();
        }

        public bool CompleteOrder(Order order)
        {
            return orderDAL.CompleteOrder(order);
        }

        public bool DeleteOrder(Order order)
        {
            return orderDAL.DeleteOrder(order);
        }

        public List<Order> GetOrdersCompleted()
        {
            return orderDAL.GetOrdersCompleted();
        }
    }
}