using Order.Domain.Core;

namespace Order.Domain.OrderAggregate;

public class Order : Entity, IAggregateRoot
{
    public Order(Address address, string buyerId)
    {
        _orderItems = new List<OrderItem>();
        CreatedTime = DateTime.Now;
        Address = address;
        BuyerId = buyerId;
    }

    public DateTime CreatedTime { get; private set; }
    public Address Address { get; private set; }
    public string BuyerId { get; private set; }

    private readonly List<OrderItem> _orderItems; //Backing Field

    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

    public void AddOrderItem(string productId, string productName, decimal price, string pictureUrl)
    {
        var existProduct = _orderItems.Any(x => x.ProductId == productId);

        if (!existProduct)
        {
            var newOrderItem = new OrderItem(productId, productName, pictureUrl, price);
            _orderItems.Add(newOrderItem);
        }
    }

    public decimal GetTotalPrice => _orderItems.Sum(x => x.Price);
}