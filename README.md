# AI Student RAG 📚🤖

AI-powered student assistant that lets students upload study documents (PDFs), ask questions about them using **Retrieval-Augmented Generation (RAG)**, generate auto quizzes, and track their learning progress — built with .NET 8 and Google Gemini.

## ✨ Features

- 📄 **PDF Upload & Processing** — upload study material, extracted and chunked automatically in the background
- 🔍 **RAG-based Q&A** — ask questions about your documents; answers are grounded in your own content via vector search + Gemini
- 🧠 **Vector Search** — semantic chunk retrieval for accurate, context-aware answers
- 📝 **Auto-generated Quizzes** — quizzes generated from uploaded material to test understanding
- 📊 **Student Analytics** — track progress and identify weak subjects over time
- 🔐 **JWT Authentication** — secure auth with refresh tokens and role-based access

## 🏗️ Architecture

Built with **Clean Architecture** and **CQRS** (via MediatR) for clear separation of concerns:

```
AI-Student-RAG/
├── AIStudy/            # API layer — controllers, Program.cs, configuration
├── Application/         # Use cases — CQRS commands/queries, DTOs, interfaces, validation
├── Domain/              # Core entities and repository contracts
└── Infrastructure/      # EF Core, external services (Gemini, Email, Vector, PDF, etc.)
```

**Patterns used:**
- Clean Architecture (Domain → Application → Infrastructure → API)
- CQRS with MediatR
- Repository Pattern
- FluentValidation for request validation

## 🛠️ Tech Stack

| Layer | Technology |
|---|---|
| Backend | .NET 8 / C# |
| Database | SQL Server (EF Core) |
| AI | Google Gemini API |
| Auth | JWT + Refresh Tokens |
| Validation | FluentValidation |
| Mapping | AutoMapper |

## 🚀 Getting Started

### Prerequisites
- .NET 8 SDK
- SQL Server (LocalDB or SQL Express)
- A Google Gemini API key

### Setup

1. **Clone the repo**
   ```bash
   git clone https://github.com/omarayman19531-create/AI-Student-RAG.git
   cd AI-Student-RAG
   ```

2. **Configure secrets** (never commit real secrets — use `appsettings.Local.json` or `dotnet user-secrets`)
   ```bash
   cd AIStudy
   dotnet user-secrets init
   dotnet user-secrets set "ConnectionStrings:sql" "<your-connection-string>"
   dotnet user-secrets set "jwt:Key" "<your-jwt-key>"
   dotnet user-secrets set "Gemini:ApiKey" "<your-gemini-api-key>"
   dotnet user-secrets set "Email:MyEmail" "<your-email>"
   dotnet user-secrets set "Email:password" "<your-app-password>"
   ```

3. **Apply database migrations**
   ```bash
   dotnet ef database update --project Infrastructure --startup-project AIStudy
   ```

4. **Run the project**
   ```bash
   dotnet run --project AIStudy
   ```

5. Open Swagger at `https://localhost:7024/swagger` to explore the API.

## 📌 Project Status

🚧 **In progress** — Auth module complete; currently building the PDF-to-text extraction, chunking, and vector embedding pipeline (RAG core).

## 📄 License

This project is currently unlicensed / for educational and portfolio purposes.
