# BeatBox E-Commerce Backend API 🎧

BeatBox is a robust, premium e-commerce backend platform built for an audio equipment store (specializing in headphones, earbuds, and speakers). This RESTful API serves as the backbone for the BeatBox platform, handling everything from high-fidelity product catalog management to secure checkout processing and real-time live notifications.

The backend is built following **Clean Architecture** principles to ensure scalability, maintainability, and separation of concerns. It is completely decoupled from the frontend, serving data exclusively via JSON APIs and SignalR WebSockets.

---

## 🚀 Features

- **Product & Inventory Management:** Full CRUD operations for products, categories, variants, and stock tracking. Includes support for product engraving.
- **Secure Authentication & Authorization:** Role-based access control (Admin/Customer) using ASP.NET Core Identity and JWT tokens.
- **Shopping Cart & Wishlist:** Persistent cart sessions and wishlist management for registered users.
- **Order Processing & Invoicing:** Seamless checkout flow with automated invoice generation using QuestPDF.
- **Razorpay Integration:** Secure end-to-end payment processing with Razorpay webhooks and verification.
- **Real-Time Notifications:** Live order tracking and admin dashboard updates powered by SignalR WebSockets.
- **Email & OTP Services:** Automated email notifications (via MailKit) for order confirmations and OTP-based verification.
- **Advanced Logging:** Comprehensive structured logging to SQL Server and flat files using Serilog.

---

## 🛠 Tech Stack

### Core Framework & Architecture
- **Framework:** .NET 9 (ASP.NET Core Web API)
- **Language:** C# 13
- **Architecture:** Clean Architecture (Domain, Application, Infrastructure, Presentation/API layers)

### Database & ORM
- **Database:** Microsoft SQL Server
- **ORM:** Entity Framework Core (Code-First Approach with Migrations)

### Security & Identity
- **Authentication:** ASP.NET Core Identity
- **Tokens:** JSON Web Tokens (JWT)
- **CORS:** Configured for cross-origin integration (e.g., Vercel Frontend)

### Real-Time & Communications
- **WebSockets:** ASP.NET Core SignalR
- **Email Provider:** MailKit / MimeKit (SMTP)

### Utilities & Integrations
- **Payment Gateway:** Razorpay SDK
- **API Documentation:** Swagger / OpenAPI
- **Logging:** Serilog (MSSQL Sink, File Sink, Console)
- **Object Mapping:** AutoMapper
- **Document Generation:** QuestPDF (Community License)

---

## 📂 Project Structure

The project strictly follows the Clean Architecture pattern:

* **`Domain/`**: Contains all enterprise logic, entities (e.g., `Product`, `Order`, `AppUser`), enums, and exceptions. No external dependencies.
* **`Application/`**: Contains the business logic, DTOs, interfaces (e.g., `IProductRepository`, `IPaymentService`), and mapping profiles.
* **`Infrastructure/`**: Implements the interfaces defined in Application. Contains the `AppDbContext`, EF Core migrations, Repository implementations, SignalR Hubs, and external service integrations (Razorpay, Email, Auth).
* **`API/`**: The Presentation layer. Contains the Controllers, Middleware (Exception Handling, Request Logging), `Program.cs`, and `appsettings.json`.

---

## 🌐 Deployment

The backend is configured for deployment on Windows/IIS environments and is currently hosted on **RunASP.net / MonsterASP**. 

### Environment Setup
The API requires the following environment variables / `appsettings.json` configurations:
- `ConnectionStrings:DefaultConnection` (SQL Server)
- `JWT:Key` (Super secret key for token signing)
- `Razorpay:Key` & `Razorpay:Secret`
- `Email:SenderEmail` & `Email:AppPassword` (SMTP Credentials)

### Frontend Integration
The frontend is built with **React/Vite** and hosted independently on **Vercel**. The frontend communicates with this backend via the exposed REST API endpoints and connects to the SignalR Hubs using the provided JWT tokens.
