# API Automation Framework – RestSharp & NUnit

A clean, scalable API automation framework built using **C#, NUnit, and RestSharp**, validating real-world REST APIs with both positive and negative test scenarios.

---

## 🚀 Project Overview

This repository demonstrates **real-world API automation practices** by testing a public REST service:

🔗 https://api.restful-api.dev/objects

The framework validates:
- CRUD operations (POST, GET, PUT, PATCH, DELETE)
- HTTP status codes
- Error handling and API behavior for invalid and reserved resources

The focus is on **maintainability, readability, and correctness**, similar to enterprise API automation projects.

---

## 🛠 Tech Stack

- **Language:** C# (.NET 10)
- **Test Framework:** NUnit
- **API Client:** RestSharp
- **JSON Handling:** Newtonsoft.Json
- **Build Tool:** dotnet CLI

---

## 📁 Project Structure

ApiAutomation/
│
├── Base/
│ └── TestBase.cs # Common setup & teardown
│
├── Tests/
│ ├── CreateUser.cs # POST tests
│ ├── GetUserTest.cs # GET tests
│ ├── UpdateUser_Put.cs # PUT tests
│ ├── PartialUpdate_Patch.cs # PATCH tests
│ └── DeleteUserTest.cs # DELETE tests
│
├── Models/
│ └── ObjectResponse.cs # Response deserialization model
│
├── ApiAutomation.csproj
└── README.md


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
|--------|---------|
| `[SetUp]` | Initializes RestClient before each test |
| `[TearDown]` | Cleans up resources after each test |
| `[Test]` | Marks a test method |
| `[Category]` | Groups tests for selective execution |

---

## ▶️ How to Run Tests

### Run all tests
```bash
dotnet restore
dotnet test

## Run specific category (example: DELETE tests)
dotnet test --filter Category=DELETE

Key Highlights

Centralized API client setup using BaseTest

Strong assertion strategy (status code, content, headers)

Deserialization for response validation

Realistic negative test cases

Clean and maintainable test naming conventions

🎯 Why This Project?

This project reflects industry-level API automation fundamentals, including:

Understanding API contracts

Handling error responses

Writing maintainable automated tests

Using NUnit effectively in CI-ready test suites


## 🚧 In Progress / Planned Enhancements

The current framework focuses on core API automation fundamentals.
The following enhancements are planned to align the project further with enterprise-level automation frameworks:

- Externalized configuration using `appsettings.json` (multi-environment support)
- Request and response builder utilities to reduce duplication
- Authentication handling (Bearer token / API key simulation)
- JSON schema validation for response contracts
- Centralized assertion helpers
- Integration with CI pipelines (GitHub Actions)
- HTML test reporting (Allure / Extent Reports)

These enhancements will be added incrementally to maintain clarity and stability of the framework.

🤝 Contributions

Suggestions, refactoring ideas, and improvements are welcome.
Feel free to fork the repo and raise a pull request.
