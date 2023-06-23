namespace CalculatorWithUnusedSettings;

public static class Program
{
    private static void Main(string[] args)
    {
        var settings = new Settings
        {
            Type = ServiceType.Premium
        };

        var service = new Service(settings);

        service.HandleA();

        service.HandleB();
    }
}

public interface IService
{
    Settings Settings { get; }
    void HandleA();
    void HandleB();
}

public enum ServiceType
{
    Premium,
    Ultimate
}

public class Settings
{
    public ServiceType Type { get; init; }

    public int SettingsA1 { get; set; }
    public int SettingsA2 { get; set; }
    public int SettingsA3 { get; set; }

    public int SettingsB1 { get; set; }
    public int SettingsB2 { get; set; }
    public int SettingsB3 { get; set; }
}

public class Service : IService
{
    private readonly IService _implementor;

    public Service(Settings settings)
    {
        Settings = settings;
        _implementor = Settings.Type switch
        {
            ServiceType.Premium => new PremiumService(settings),
            ServiceType.Ultimate => new UltimateService(settings),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public Settings Settings { get; }

    public void HandleA() => _implementor.HandleA();

    public void HandleB() => _implementor.HandleB();
}

public class PremiumService : IService
{
    public PremiumService(Settings settings)
    {
        Settings = settings;
    }

    public Settings Settings { get; }

    public void HandleA()
    {
        Console.WriteLine("A is handled by Premium");
    }

    public void HandleB()
    {
        Console.WriteLine("B is handled by Premium");
    }
}

public class UltimateService : IService
{
    public UltimateService(Settings settings)
    {
        Settings = settings;
    }

    public Settings Settings { get; }

    public void HandleA()
    {
        Console.WriteLine("A is handled by Ultimate");
    }

    public void HandleB()
    {
        Console.WriteLine("B is handled by Ultimate");
    }
}