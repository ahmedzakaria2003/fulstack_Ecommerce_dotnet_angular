# Full Stack E-Commerce App (Angular 11 + ASP.NET Core 8)

This is a full-stack E-Commerce web application developed using Angular 11 on the frontend and ASP.NET Core 8 on the backend, following Onion Architecture and real-world design patterns like Repository, Unit of Work, and Specification.

The project includes core e-commerce features like product browsing, cart, wishlist, authentication, and checkout.


## Technologies Used

### Frontend (Angular 11)

- Angular Modules & Routing
- Reactive Forms (including Multi-step Checkout)
- HTTP Client & Interceptors
- RxJS (Observables)
- Input/Output Communication
- Bootstrap for UI Styling
- Product Filtering, Sorting, and Pagination

### Backend (ASP.NET Core 8)

- Onion Architecture with Clean Code Principles
- Entity Framework Core + Code First Migrations
- ASP.NET Identity with JWT Authentication
- AutoMapper for Mapping Entities to DTOs
- Middleware for Global Error Handling
- Specification Pattern for Query Abstraction

## Features

- Add to Cart / Manage Cart
- Add to Wishlist / Remove from Wishlist
- User Registration & JWT-based Login
- Multi-step Checkout (Address > Summary)
- Filtering, Sorting, and Pagination of Products
- Clean Separation of Logic Using Layers & Patterns

## How Project is structured
The following is a high-level overview of the main project structure:

![Screenshot 2025-07-09 033011](https://github.com/user-attachments/assets/83ebf036-1728-40bb-8702-25eaa1826098)

Angular Project looks like : 

![image](https://github.com/user-attachments/assets/632b0c4e-8d4f-4cc0-8261-8ed5999159e0)

The frontend is organized by features, with separate modules and components for each core functionality:

![image](https://github.com/user-attachments/assets/9afd1dbd-3f8d-4aaa-a9c6-ca2198df7076)

The Core layer represents the heart of the application and contains:

![image](https://github.com/user-attachments/assets/7aa8c7c6-1c79-44ed-9d29-2fecce1f241b)

The Infrastructure project contains the implementation details that interact with external resources such as the database and API endpoints. It is divided into:

![image](https://github.com/user-attachments/assets/223395e0-527f-4fce-a3ce-8f43915dbd4a)

The `E-Commerce.Web` project serves as the entry point of the application :

![image](https://github.com/user-attachments/assets/de65263b-efd6-44dc-9aa6-73b452106b35)

The `Shared` project contains common components used across different layers of the application. It acts as a contract and utility layer between the Web, Core, and Infrastructure projects:

![Screenshot 2025-07-09 034816](https://github.com/user-attachments/assets/0196210d-f65c-43aa-be2c-5f008e980649)


## Client

This project was generated using Angular CLI version 11.  
To run the frontend locally:

1. Navigate to the `client` folder:

cd client

2. Install dependencies:

npm install
3. Start the development server:

ng serve
The app will be served on `http://localhost:4200/` by default.

## NuGet Packages Used (Backend)

### ORM (Entity Framework Core)

`bash
Install-Package Microsoft.EntityFrameworkCore
Install-Package Microsoft.EntityFrameworkCore.Design
Install-Package Microsoft.EntityFrameworkCore.SqlServer

Redis (Caching)
Install-Package StackExchange.Redis
Used for storing basket & wishlist items on the server in-memory for fast access and improved performance.

Identity & Authentication
Install-Package Microsoft.AspNetCore.Identity.EntityFrameworkCore
Install-Package Microsoft.AspNetCore.Identity
Install-Package Microsoft.IdentityModel.Tokens
Install-Package System.IdentityModel.Tokens.Jwt
Used for implementing secure user authentication using JWT tokens and ASP.NET Core Identity.


Data Storage
Using SQL Server for all environments (development and production) to store products, orders, and related entities.

Using Redis to persist basket items in memory on the server.

Using Stripe for handling payment processing, with support for test and secure 3D transactions.

## Screenshots
Here’s a quick visual overview of the application in action :

![Screenshot 2025-07-04 231128](https://github.com/user-attachments/assets/f4341012-9af6-4267-a804-04a1007a7051)



![Screenshot 2025-07-04 231150](https://github.com/user-attachments/assets/99c85487-b077-42d5-b876-2f15b8457fce)



![Screenshot 2025-07-04 231242](https://github.com/user-attachments/assets/6f2e6aa0-ec31-44a1-b4b1-a1028e71085f)



![Screenshot 2025-07-04 231251](https://github.com/user-attachments/assets/593995ba-6bb8-4afc-bfba-95accb6267e4)



![Screenshot 2025-07-04 231324](https://github.com/user-attachments/assets/c4f9e2ef-7cf7-46bf-8e6a-c857131158dc)



![Screenshot 2025-07-04 231340](https://github.com/user-attachments/assets/7de05097-3a54-4dba-9374-874c0d4bca35)



![Screenshot 2025-07-04 231349](https://github.com/user-attachments/assets/4b4e140e-5e14-4f89-ae16-8d1b1d2aff57)



![Screenshot 2025-07-04 230918](https://github.com/user-attachments/assets/0c97ea30-bf90-4284-907b-14fcae686c8d)



![Screenshot 2025-07-04 230935](https://github.com/user-attachments/assets/a9d0754b-e015-45e6-92da-61c43969e29f)



![Screenshot 2025-07-04 231057](https://github.com/user-attachments/assets/3b9c6720-e941-4c35-8088-b6844be5292b)



![Screenshot 2025-07-04 231110](https://github.com/user-attachments/assets/e6889c3e-c857-46dd-983b-d32b9aeb6aee)


## Running the Project Locally

To run this project locally, make sure you have the following installed:

- [.NET SDK 8.0+](https://dotnet.microsoft.com/)
- [Node.js 14+](https://nodejs.org/) (to run the Angular frontend)
- [Git](https://git-scm.com/)
- [Stripe CLI](https://stripe.com/docs/stripe-cli) (only if you want to test real webhook events)

### 1. Clone the repository

```bash
git clone https://github.com/ahmedzakaria2003/fulstack_Ecommerce_dotnet_angular
cd Ecommerce_dotnet_angular

2. Configure Stripe

{
  "StripeSettings": {
    "PublishableKey": "pk_test_REPLACEME",
    "SecretKey": "sk_test_REPLACEME",
    "WhSecret": "whsec_REPLACEME"
  },
  "AllowedHosts": "*"
}

Run Stripe CLI and login:

stripe login


Start listening to webhook events and forward them to your local API:

stripe listen --forward-to https://localhost:5001/api/payments/webhook -e payment_intent.succeeded

3. Run the Backend (.NET)
cd API
dotnet restore
dotnet run

4. Run the Frontend (Angular)
cd client
npm install
ng serve


Stripe Testing
Use Stripe test card numbers to simulate payments.
Do not use real cards. This project is for educational/demo purposes only.

