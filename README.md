# Coffresso Vending Machine

Coffresso is a VaaS (Vending as a Service) company. The business model is just installing their machines in company offices, charging a fixed price for it and a variable based on how much they consume.

These machines has a microchip to interact with the machine and the business logic developed by Coffresso. Through it, Coffresso can take control about things like stock, items, prices, etc...

## Current Status

The microchip is connected to an internal API. This API was developed by the first developer of the company, with any kind of experience in APIs nor .Net, so now you could imagine...

The API has 1 endpoint (`/vending`) to control the entire flow of the machine.
Through this endpoint you could execute multiple operations:

* `GET /vending/drinks` returns every drink you set-up previously in the machine.

* `POST /vending` allows you to set-up the drinks in the machine, this action overrides any previous drink.

* `PUT /vending` allows you update an existing drink (if you want to renew the stock, update the price, and so on). If the desired drink does not exists, it will return an error.

* `GET /vending?drinkName={drink}&money={$}` is the action performed directly by the users, when they touch and confirm through the machine.

As you could see there is just one class dealing with the entire flow of the application (validation, HTTP request/response, business logic, etc...) and with any kind of test.

## What you have to do?

Our CEO wants to scale the business and, due to the bad experience with the previous developer, she wants to create a well-experienced team to help her.

To do so, she wants to start with some features and fixes of the actual API.

* She wants to be able to control how much money has been spent, totally and per item.

* She also wants to offer a 5% discount per drink if the company has a higher consume ratio (> 5 items per day).

The actual CTO has defined some technical constraints that you should follow on every refactor or new code of this API.
These are the restrictions for your PRs to be accepted:

* Ensure you are not coupling the business logic with any framework. May be tomorrow we have to change something due to scalability issues...

* The mission of the team is to create a better API, so please use design patterns to improve the actual base code and decouple this mess.

* Code should be tested, ensuring that the API fulfills the business logic and we do not break nothing with a change.

* The API should follows the REST API design principles (at least do not break the most important ones).

## Other considerations

* Avoid using a database, assume that you could store everything in memory.

* Please provide instructions or any comments to the solution, if it's needed.

## Example requests

### Add items to the vending machine

```bash
curl -XPOST http://localhost:8000/vending  \
-H 'Content-Type:application/json' \
-d '[
   {
      "name": "water",
      "price": 1.2,
      "stock": 2
   },
   {
      "name": "coffee",
      "price": 1.5,
      "stock": 1
   }
]'
```

### Update an item stock

```bash
curl -XPUT http://localhost:8000/vending \
-H 'Content-Type:application/json' \
-d '{
   "name": "water",
   "price": 1.2,
   "stock": 10
}'
```

### Get actual items

```bash
curl -XGET http://localhost:8000/vending/drinks
```

### Order an item (by name & money)

```bash
curl -XGET http://localhost:8000/vending?drinkName=water&money=2
```
