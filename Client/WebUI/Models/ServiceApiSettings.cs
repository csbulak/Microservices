﻿namespace WebUI.Models;

public class ServiceApiSettings
{
    public string GatewayBaseUrl { get; set; }
    public string IdentityBaseUrl { get; set; }
    public string PhotoStockUrl { get; set; }
    public ServiceApi Catalog { get; set; }
    public ServiceApi PhotoStock { get; set; }
    public ServiceApi Basket { get; set; }
}

public class ServiceApi
{
    public string Path { get; set; }
}