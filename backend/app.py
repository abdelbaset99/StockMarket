import time
from flask import Flask, request, render_template
from flask_socketio import SocketIO, send, emit
from flask_cors import CORS
import csv
import json
import random
import threading

app = Flask(__name__)
# app.config['SECRET_KEY'] = 'secret!'
CORS(app)
socketio = SocketIO(app, cors_allowed_origins="*")


# @socketio.on('stocks')
# def get_stocks():
#     stocks = []
#     with open("D:\\EGID\\backend\\stocks.csv", "r") as f:
#         reader = csv.DictReader(f)
#         for row in reader:
#             stocks.append(row)
        
#     send(json.dumps(stocks), json=True)


def randomize_stock_prices(stocks: list):
    socketio.emit('stocks', stocks)
    while True:
        for stock in stocks:
            stock["price"] = random.randint(1, 100)
        socketio.emit('stocks', stocks)
        time.sleep(100)
    
    

@app.route("/", methods=["GET"])
def market():
    stocks = []
    with open("D:\\EGID\\backend\\stocks.csv", "r") as f:
        reader = csv.DictReader(f)
        for row in reader:
            stocks.append(row)

    # socketio.emit('stocks', json.dumps(stocks))
    
    th = threading.Thread(target=randomize_stock_prices, args=(stocks,))
    th.start()
    
    return json.dumps(stocks)

@app.route("/", methods=["POST"])
def makeOrder():
    name = request.json["name"]
    quantity = request.json["quantity"]
    stock = request.json["stock"]
    price = request.json["price"]
    
    with open("D:\\EGID\\backend\\orders.csv", "a") as f:       
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
    with open("D:\\EGID\\backend\\orders.csv", "r") as f:
        reader = csv.DictReader(f)
        for row in reader:
            orders.append(row)
            
    # print(orders)
            
    return json.dumps(orders, indent=2)


if __name__ == '__main__':
    socketio.run(app, debug=True, allow_unsafe_werkzeug=True)