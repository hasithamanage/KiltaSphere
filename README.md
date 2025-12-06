# 🌟 KiltaSphere: Modern Full-Stack Registry & Operations Portal

> **A professional demonstration of building secure, scalable and localized business solutions using .NET 8 and Vue 3, tailored for critical organizational management.**

This project simulates a high-availability, full-stack application designed to manage complex member data, payment tracking and communication logs-mirroring the functionality required for large Finnish organizations and unions.

---

## 🛠️ Technology Stack

This project explicitly targets the Full Stack .NET Development

| Category | Technology | Version / Framework | Focus Area |
| :--- | :--- | :--- | :--- |
| **Backend** | **C# / .NET 8** | ASP.NET Core Web API | High-performance RESTful API design. |
| **Frontend** | **Vue.js** | Vue 3 (Composition API, Pinia) | Modern, responsive Single Page Application (SPA) with centralized State Management. |
| **Architecture** | **Clean/Layered** | Service/Repository Pattern, DTOs | Scalable, Maintainable and Testable code structure and Input Validation. |
| **Database** | **MS SQL Server** | Entity Framework Core (EF Core) | Relational database modeling, complex queries and data integrity. |
| **DevOps/Tools** | **Docker** | Containerization | Environment Independence (allows running frontend/backend without local SDKs) and consistent development setup. |

---

## ✨ Core Features & Business Logic (Current Progress: **CRUD Complete**)

The KiltaSphere application focuses on demonstrating competency in handling critical business data:

1.  **Jäsenrekisterin Hallinta (Member Registry Management):** Secure **CRUD** operations (Create, Read, Update, Delete) for member profiles. **(API Implementation Complete)**
2.  **Maksuseuranta (Payment Tracking):** Detailed system for tracking membership fees, payments due and historical payment status.
3.  **Viestintälokitus (Communication Logging):** Automatic logging of all vital interactions (emails, SMS reminders) with members.
4.  **Tietoturva (Security):** Implementation of secure authentication and authorization protocols to protect sensitive member data.

---

## 🖼️ Visual Preview

*(Replace this placeholder with screenshot or GIF once the UI is functional.)*



---

## 🚀 Getting Started (Instructions for Reviewers)

The client uses **Docker** for environment setup which means you only need to run two projects.

### Prerequisites

* **Docker Desktop** (Required to run the containerized frontend).
* **.NET 8 SDK** (Required to run the backend API).
* **MS SQL Server** (or LocalDB).

---

### 1. Backend Setup (`KiltaSphereAPI`)

1.  **Navigate to the project directory:**
    ```bash
    cd KiltaSphereAPI
    ```

2.  **Database Migration:** Apply the schema and seed data to your local database.
    ```bash
    dotnet ef database update
    ```

3.  **Run API:** The API will run on `https://localhost:5001`.
    ```bash
    dotnet run
    ```
    *(Keep this terminal/Visual Studio running.)*

---

### 2. Frontend Setup (`KiltaSphereClient`)

The frontend runs entirely inside a Docker container using a pre-built image.

1.  **Navigate to the client directory:**
    ```bash
    cd KiltaSphereClient
    ```

2.  **Run Client (Requires Docker Desktop to be running):** This command installs dependencies (if not present) and starts the Vue development server.
    ```bash
    docker run -d -p 5173:5173 -v "$(pwd):/app" --name kiltasphere-dev-server kiltasphere-client:dev npm run dev
    ```

3.  **Access the Application:**
    Open your browser and navigate to: **`http://localhost:5173/`**

---

**Keywords:** C#, .NET 8, Vue.js, Pinia, Docker, Full Stack, Member Management, Suomi, Kilta, Rekisteri, REST API.