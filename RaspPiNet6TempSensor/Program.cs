using System.Device.Gpio;
using Iot.Device.Common;
using Iot.Device.DHTxx;
using UnitsNet;

Console.WriteLine("Hello DHT!");
Console.WriteLine("Press any key to stop the reading");

Random _random = new Random(2);
var controller = new GpioController(PinNumberingScheme.Board);

if (controller.IsPinOpen(7)) {
    Console.WriteLine("Pin 7 open, so closing");
    controller.ClosePin(7); 
}

Console.WriteLine("Opening Pin 7");
controller.OpenPin(7);

controller.RegisterCallbackForPinValueChangedEvent(7, PinEventTypes.Falling, (a,b) =>
    {
        Console.WriteLine($"falling: {b.ChangeType.ToString()}");
    });

    controller.RegisterCallbackForPinValueChangedEvent(7, PinEventTypes.Rising, (a, b) =>
    {
        Console.WriteLine($"raising: {b.ChangeType.ToString()}");
    });

controller.RegisterCallbackForPinValueChangedEvent(7, PinEventTypes.None, (a, b) =>
{
    Console.WriteLine($"none: {b.ChangeType.ToString()}");
});

Console.WriteLine("Closing Pin 7");
controller.ClosePin(7);

Dht(new Dht11(7, PinNumberingScheme.Board, controller));

void Dht(DhtBase dht)
{
    while (!Console.KeyAvailable)
    {        
        Temperature temperature = default;
        RelativeHumidity humidity = default;
        bool success = dht.TryReadHumidity(out humidity) && dht.TryReadTemperature(out temperature);
        // You can only display temperature and humidity if the read is successful otherwise, this will raise an exception as
        // both temperature and humidity are NAN

        //if (dht.IsLastReadSuccessful)
        if (success)
        {            
            Console.WriteLine($"Temperature: {temperature.DegreesCelsius:F1}\u00B0C, Relative humidity: {humidity.Percent:F1}% {dht.MinTimeBetweenReads}");

            // WeatherHelper supports more calculations, such as saturated vapor pressure, actual vapor pressure and absolute humidity.
            Console.WriteLine(
                $"Heat index: {WeatherHelper.CalculateHeatIndex(temperature, humidity).DegreesCelsius:F1}\u00B0C");
            Console.WriteLine(
                $"Dew point: {WeatherHelper.CalculateDewPoint(temperature, humidity).DegreesCelsius:F1}\u00B0C");
        }else
        {
            Console.Write(".");            
        }
        
        //Thread.Sleep(_random.Next(100,5000));
        Thread.Sleep(2500);
    }
}