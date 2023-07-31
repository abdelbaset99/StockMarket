# StockMarket

This was a project as part of my internship at [EGID](https://www.egidegypt.com/).
The app was made using Angular 16 and MySQL database.
The Backend was first created using Flask then using ASP.Net Core 7.
The app uses i18n and ngx-translate to provide English and Arabic versions. 
It also provides light and dark themes.

#### The Home Page
Implements registration and login functionalities.
![Screenshot 2023-07-31 113921](https://github.com/Abdelbaset65/StockMarket/assets/50206880/bea7f967-6df2-4a22-bedf-90e9d930db2b)

![Screenshot 2023-07-31 114007](https://github.com/Abdelbaset65/StockMarket/assets/50206880/9e9ca561-a271-42c5-8a34-966e7cd05656)

The app uses JWT to authenticate users

#### The Stocks Page
![Screenshot 2023-07-31 114044](https://github.com/Abdelbaset65/StockMarket/assets/50206880/7c383b8b-5eef-4df0-916a-7e9012250856)

A table containing available stocks with their prices. The user can buy a stock and suggest a new price.

![Screenshot 2023-07-31 114057](https://github.com/Abdelbaset65/StockMarket/assets/50206880/9b623e23-ec5c-483f-88da-f5d6e3c53b45)

The app uses signalR to update the prices of the stocks every 10 seconds.
After confirming an order, the user is prompted to navigate to the orders page.
![Screenshot 2023-07-31 114107](https://github.com/Abdelbaset65/StockMarket/assets/50206880/ac4dc00d-c758-4ff3-aaa1-5c356e81f746)

#### The Orders Page
The user can view the orders they made.
![Screenshot 2023-07-31 114119](https://github.com/Abdelbaset65/StockMarket/assets/50206880/f95c25be-850a-4c67-9ccc-68b102f00958)
