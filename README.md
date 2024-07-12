# BoxMania
A small indicator built for Quantower that allows for custom boxes to be made at specific prices for easy visualization of zones.

This indicator is built in C# and will utilize the Quantower builtin Library "Indicator". 

The indicator will start at the beginning of the session for futures (3pm PDT, 5pm CDT, 6pm EDT) and continue until the end of the chart. 

![image](https://github.com/user-attachments/assets/be8ec93a-3ffb-4e95-a49c-58641b2470d7)

A key update to the indicator, which I will not be doing as this indicator is a testament to the first project completion I've had in C#, is to change the zonecolors to a smaller alpha number so that you can see the candles behind them by default. Current the alpha is set to 100 but a value of 30 is probably more appropriate for those that don't use color wheels by default. The user can still edit this by default for each individual box and you can store a maximum of 15 boxes per day with simply selecting a checkbox.

public Color zoneColor15 = Color.FromArgb(100, 0, 155, 0) -> public Color zoneColor15 = Color.FromArgb(30, 0, 155, 0)
