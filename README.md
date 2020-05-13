# PaymentGateway

This API simulates an intermediate between e-commerces and payment processors. It send the payment processing to another API and saves the request into its own database.

## Endpoints

1. Process payment: this endpoint receives the details of the payments and sends this request to another API to be approved or not. This request is saved into an internal database to be recovered later.

2. Get payment request: this endpoint searches for a payment request made before by its GUID.
