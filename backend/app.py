import time
from flask import Flask, request, render_template
from flask_socketio import SocketIO, send, emit
from flask_cors import CORS
import csv
import json
import random
import threading
import mysql.connector as mysql 
import random

app = Flask(__name__)
# app.config['SECRET_KEY'] = 'secret!'
CORS(app)
socketio = SocketIO(app, cors_allowed_origins="*")

db = mysql.connect(
    host = "localhost",
    user = "root",
    passwd = "root",
    database = "StockMarket"
)    

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
def stocks():
    cur = db.cursor()
    sql = "SELECT * FROM stocks"
    try:
        cur.execute(sql)
        stocklist = cur.fetchall()
        db.commit()
    except mysql.Error as e:
        print(e)
    finally:
        cur.close()
        # db.close()
    stocks = []
    for stock in stocklist:
        stocks.append({
            "id": stock[0],
            "name": stock[1],
            "price": stock[2]})
    # with open("D:\\EGID\\backend\\stocks.csv", "r") as f:
    #     reader = csv.DictReader(f)
    #     for row in reader:
    #         stocks.append(row)

    # socketio.emit('stocks', json.dumps(stocks))
    th = threading.Thread(target=randomize_stock_prices, args=(stocks,))
    th.start()
    
    return json.dumps(stocks)


@app.route("/", methods=["POST"])
def makeOrder():
    stockid = request.json["stockid"]
    name = request.json["name"]
    quantity = request.json["quantity"]
    stock = request.json["stock"]
    price = request.json["price"]
    orderid = random.randint(1, 100000)
    
    cur = db.cursor()
    sql = "INSERT INTO orders (ID, BuyerName, StockID, Price, Quantity) VALUES (%s, %s, %s, %s, %s)"
    vals = (orderid, name, stockid, price, quantity)
    try:
        cur.execute(sql, vals)
        db.commit()
    except mysql.Error as e:
        print(e)
        db.rollback()
    finally:
        cur.close()
        # db.close()
   
    # with open("D:\\EGID\\backend\\orders.csv", "a") as f:       
    #     writer = csv.writer(f)
    #     writer.writerow([name, stock, price, quantity])
        
    order = {
        "id": orderid, 
        "name": name,
        "stock": stock,
        "price": price,
        "quantity": quantity
    }
    return json.dumps(order)
    

@app.route("/orders", methods=["GET"])
def orders():
    cur = db.cursor()
    sql = "SELECT * FROM orders"
    try:
        cur.execute(sql)
        orderlist = cur.fetchall()
        db.commit()
    except mysql.Error as e:
        print(e)
    finally:
        cur.close()
        # db.close() 
    orders = []
    for order in orderlist:
        orders.append({
            "id": order[0],
            "stockid": order[1],
            "name": order[2],
            "price": str(order[4]),
            "quantity": order[3]})
        
    # with open("D:\\EGID\\backend\\orders.csv", "r") as f:
    #     reader = csv.DictReader(f)
    #     for row in reader:
    #         orders.append(row)
            
    # print(orders)
            
    return json.dumps(orders)


if __name__ == '__main__':
    socketio.run(app, debug=True, allow_unsafe_werkzeug=True)