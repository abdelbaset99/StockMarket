# StockMarket

This was a project as part of my internship at [EGID](https://www.egidegypt.com/).
The app was made using Angular 16 and MySQL database.
The Backend was first created using Flask then using ASP.Net Core 7.

### The app contains 3 pages: 
##### The Home Page
Implements registration and login functionalities.
![SignUp Arabic](https://github.com/Abdelbaset65/StockMarket/assets/50206880/b5c20e8d-03eb-49e3-95cd-55753d1364f8)
![SignIn Dark](https://github.com/Abdelbaset65/StockMarket/assets/50206880/c8e07a93-ef40-4a8b-9252-d8c3788b2459)

The app uses JWT to authenticate users

##### The Stocks Page
![Stocks](https://github.com/Abdelbaset65/StockMarket/assets/50206880/948f8e8e-c9f1-4494-b761-852479214324)

A table containing available stocks with their prices. The user can buy a stock and suggest a new price.
![Buy Stock](https://github.com/Abdelbaset65/StockMarket/assets/50206880/ce194d9a-b251-44be-9711-3206d54bab4f)

The app uses signalR to update the prices of the stocks every 10 seconds.
After confirming an order, the user is prompted to navigate to the orders page.
![Message](https://github.com/Abdelbaset65/StockMarket/assets/50206880/c3e974b9-2407-4292-8f06-9019b6a79b2e)


##### The Orders Page
The user can view the orders made
![Orders](https://github.com/Abdelbaset65/StockMarket/assets/50206880/b1b6e372-6da6-496d-bd14-0877c0771dc9)

The app uses i18n and ngx-translate to provide English and Arabic versions. 
It also provides light and dark themes.
![SignUp English](https://github.com/Abdelbaset65/StockMarket/assets/50206880/540f9b0a-e782-4d2c-bcad-1478a15e6c1e)
