# PaymentGateway

This API simulates an intermediate between e-commerces and payment processors. It sends the payment processing to another API and saves the request into its own database to be recovered later using the GUID of the processing.

## Endpoints

### Process payment | POST - api/Payment

This endpoint receives the details of the payments and sends this request to another API to be approved or not. It's returned an object with the GUID of the transaction and the status (approved or denied). This request is saved into an internal database to be recovered later.

Request format example:
```
{
  "Name": "John McCorey",
  "Number": "4485497536390135",
  "ExpiryMonth": 12,
  "ExpiryYear": 2021,
  "Amount": 50,
  "Currency": "EUR",
  "CVV": 132

}
```

Response format example:
```
{
  "id": "13d7c01c-14bc-4917-95bb-d53feec9074c",
  "name": "John McCorey",
  "lastCardNumbers": "0135",
  "expiryMonth": 12,
  "expiryYear": 2021,
  "amount": 50,
  "currency": "EUR",
  "status": "Denied"
}
```

### Get payment request | GET - api/Payment

This endpoint searches for a payment request made before by its GUID.

Reponse format example:
```
{
  "id": "13d7c01c-14bc-4917-95bb-d53feec9074c",
  "name": "John McCorey",
  "lastCardNumbers": "0135",
  "expiryMonth": 12,
  "expiryYear": 2021,
  "amount": 50,
  "currency": "EUR",
  "cvv": 132,
  "status": "Denied",
  "creationDate": "2020-06-09T09:48:13.5788019"
}
```

## Payment processing service

There is a fake payment processing service running. It's returning a random response (approved or denied) and a new GUID for the payment processing.

In the feature this service can be easily replaced by a real service and can be toggled changing the appsetting ```UseMockedPaymentProcessingService``` to ```false```

## Pending features
- [x] Loggin
- [x] Build script / CI
- [ ] Application metrics
- [ ] Authentication
- [ ] API client
- [ ] Performance testing
- [ ] Data storage
- [ ] Containerization
- [ ] Circuit breaker when calling the real bank service
- [ ] CQRS