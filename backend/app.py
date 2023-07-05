from flask import Flask, request
from flask_cors import CORS
import csv
import json

app = Flask(__name__)
CORS(app)

@app.route("/", methods=["GET"])
def market():
    stocks = []
    with open("./stocks.csv", "r") as f:
        reader = csv.DictReader(f)
        for row in reader:
            stocks.append(row)
        
    return json.dumps(stocks)

@app.route("/", methods=["POST"])
def makeOrder():
    name = request.json["name"]
    quantity = request.json["quantity"]
    stock = request.json["stock"]
    price = request.json["price"]
    
    with open("./orders.csv", "a") as f:       
        writer = csv.writer(f)
        writer.writerow([name, stock, price, quantity])
        
    order = {
        "name": name,
        "stock": stock,
        "price": price,
        "quantity": quantity
    }
    return json.dumps(order)
    

@app.route("/orders", methods=["GET"])
def orders():
    orders = []
    with open("./orders.csv", "r") as f:
        reader = csv.DictReader(f)
        for row in reader:
            orders.append(row)
            
    return json.dumps(orders, indent=2)


if __name__ == '__main__':
    app.debug = True
    app.run()