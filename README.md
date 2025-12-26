# API Automation Framework – RestSharp & NUnit

A clean, scalable **API automation framework** built using **C#, NUnit, and RestSharp**, validating real-world REST APIs with both positive and negative test scenarios.

---

## 🚀 Project Overview

This framework implements **real-world API automation practices** by testing a public REST service:

🔗 https://api.restful-api.dev/objects

The framework validates:
- CRUD operations (POST, GET, PUT, PATCH, DELETE)
- HTTP status codes
- Error handling and API behavior for invalid and reserved resources

The primary focus is on **maintainability, readability, and correctness**, aligning with enterprise-level API automation standards.

---

## 🛠 Tech Stack

- **Language:** C# (.NET 10)
- **Test Framework:** NUnit
- **API Client:** RestSharp
- **JSON Handling:** Newtonsoft.Json
- **Build Tool:** dotnet CLI

---

## 📁 Project Structure

ApiAutomation
│
├── Base
│ └── TestBase.cs # Common setup & teardown
│
├── Tests
│ ├── CreateUser.cs # POST tests
│ ├── GetUserTest.cs # GET tests
│ ├── UpdateUser_Put.cs # PUT tests
│ ├── PartialUpdate_Patch.cs # PATCH tests
│ └── DeleteUserTest.cs # DELETE tests
│
├── Models
│ └── ObjectResponse.cs # Response deserialization model
│
├── ApiAutomation.csproj
└── README.md

yaml
Copy code

---

## 🧪 Test Coverage

### ✔ Positive Scenarios
- Create new objects (POST)
- Fetch object list and individual objects (GET)
- Update objects (PUT)
- Partial updates (PATCH)
- Delete dynamically created objects (DELETE)

### ❌ Negative Scenarios
- Invalid endpoints → `404 Not Found`
- Empty or malformed request body → `400 Bad Request`
- Operations on reserved IDs → `405 Method Not Allowed`

---

## 🔄 NUnit Lifecycle Usage

| Attribute | Purpose |
|---------|---------|
| `[SetUp]` | Initializes RestClient before each test |
| `[TearDown]` | Cleans up resources after each test |
| `[Test]` | Marks a test method |
| `[Category]` | Groups tests for selective execution |

---

## ▶️ How to Run Tests

### Prerequisites
- .NET SDK 10 installed
- Internet connection (public API)

### Clone Repository
```bash
git clone https://github.com/mamata-salunkhe/api-automation-csharp.git
cd api-automation-csharp
Run All Tests
bash
Copy code
dotnet restore
dotnet test
Run Specific Category (example: DELETE tests)
bash
Copy code
dotnet test --filter Category=DELETE
⭐ Key Highlights
Centralized API client setup using base test classes

Strong assertion strategy (status codes, response content, headers)

Proper deserialization for response validation

Realistic negative test scenarios

Clean and maintainable test naming conventions

CI-ready structure

🎯 Project Objective
This project reflects industry-level API automation fundamentals, including:

Understanding API contracts

Handling error responses gracefully

Writing scalable and maintainable automated tests

Using NUnit effectively in CI-ready test suites

🚧 Planned Enhancements
The current framework focuses on core API automation fundamentals.
The following enhancements are planned to further align with enterprise automation frameworks:

Externalized configuration using appsettings.json

Multi-environment execution support

Request and response builder utilities

Authentication handling (Bearer token / API key simulation)

JSON schema validation

Centralized assertion helpers

CI integration using GitHub Actions

HTML reporting (Allure / Extent Reports)

🤝 Contributions
Suggestions, refactoring ideas, and improvements are welcome.
Feel free to fork the repository and raise a pull request.