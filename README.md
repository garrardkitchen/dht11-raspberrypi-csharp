# Getting started

_Assuming you've already installed dotnet on your Pi_

Target platform is **ARM64**.

Create a deploy PS script, similar to this, and `. .\deploy.ps1` each time you want to deploy a change:

**deploy.ps1**:

```powershell
scp -r /C:\Users\<user>\source\RaspPiNet6TempSensor\RaspPiNet6TempSensor\bin\Debug\net6.0/* <username>@<ip-address>:/home/<username>/dotnet6/RaspPiNet6TempSensor/
```

To run:

**Pi**:

```bash
cd /home/<username>/dotnet6/RaspPiNet6TempSensor/
sudo dotnet RaspPiNet6TempSensor.dll
```

output:

```
Press any key to stop the reading
Opening Pin 7
Closing Pin 7
.......Temperature: 22.4°C, Relative humidity: 89.0% 00:00:02.5000000
Heat index: 23.0°C
Dew point: 20.4°C
Temperature: 22.4°C, Relative humidity: 86.0% 00:00:02.5000000
Heat index: 22.9°C
Dew point: 19.8°C
Temperature: 22.4°C, Relative humidity: 89.0% 00:00:02.5000000
Heat index: 23.0°C
Dew point: 20.4°C
Temperature: 22.4°C, Relative humidity: 89.0% 00:00:02.5000000
Heat index: 23.0°C
Dew point: 20.4°C
...Temperature: 22.4°C, Relative humidity: 86.0% 00:00:02.5000000
Heat index: 22.9°C
Dew point: 19.8°C
```

_👆 the sensor isn't consistently read, newer sensors are less problematic (for example, the DHT12 - has both an input and output pin)_