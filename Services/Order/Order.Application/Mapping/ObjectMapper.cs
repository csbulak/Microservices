using AutoMapper;

namespace Order.Application.Mapping;

public static class ObjectMapper
{
    private static readonly Lazy<IMapper> Lazy = new(() =>
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<CustomMapping>();
        });

        return config.CreateMapper();
    }); // İstendiği zaman initialize eder.

    public static IMapper Mapper => Lazy.Value; // Mapper çağrılana kadar yukarıdaki kodlar çalışmaz.
}