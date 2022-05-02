namespace WebUI.Models.Basket;

public class BasketViewModel
{
    public string UserId { get; set; }
    public string? DiscountCode { get; set; }
    public int? DiscountRate { get; set; }

    public List<BasketItemViewModel> _basketItems { get; set; }

    private List<BasketItemViewModel> BasketItems
    {
        get
        {
            if (HasDiscount)
            {
                _basketItems.ForEach(x =>
                {
                    var discountPrice = x.Price * ((decimal)DiscountRate / 100);
                    x.AppliedDiscount(Math.Round(x.Price - discountPrice, 2));
                });
            }

            return _basketItems;
        }
        set => _basketItems = value;
    }

    public decimal TotalPrice => BasketItems.Sum(x => x.GetCurrentPrice * x.Quantity);

    public bool HasDiscount => !string.IsNullOrEmpty(DiscountCode);
}