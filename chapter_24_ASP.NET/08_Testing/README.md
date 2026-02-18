<details>
<summary>🧪 Unit Testing in ASP.NET Core</summary>

# 🧪 Unit Testing in ASP.NET Core

## Key Ideas

- **Solution structure**

  - One `.sln` file can include multiple projects (e.g. `BlogApi` + `BlogApi.Tests`).
  - Keeping them as siblings avoids circular references.

- **xUnit**

  - Testing framework we use in .NET.
  - `[Fact]` → for individual tests.
  - `[Theory]` → for parameterized tests.

- **Testing layers**
  - **Unit tests** → test business logic in services (isolated).
  - **Integration tests** → boot the whole API and test endpoints via HTTP.

## Approaches

### EF Core InMemory

- Simulates a real database but stores everything in memory.
- Faster and simpler than mocking DbContext.
- Good for testing CRUD operations (e.g. `UserService`, `PostService`).

### Mocking with Moq

- A **mock** is a fake version of a dependency.
- Instead of running the real object, you define its behavior yourself.
- Useful for complex dependencies that are hard to set up (e.g. `UserManager`, `IConfiguration`).
- Lets you control scenarios (success, failure, exceptions) and verify interactions.

## Patterns

- **AAA (Arrange → Act → Assert)** → clear test structure.
- **Repository Pattern** → in larger apps, services depend on repo interfaces, making mocking easier.
- **Isolated Tests** → each test creates its own in-memory DB.
- **Exception Testing** → use `Assert.ThrowsAsync<T>` to check errors.
- **Verification** → with Moq you can confirm a method was called with expected parameters.

## Commands

Run API in development mode:

```bash
dotnet watch run --project BlogApi
```

</details>

<details>
<summary>❓Why Separate API and Tests into Sibling Projects?</summary>

## Why Separate API and Tests into Sibling Projects?

In .NET, the common convention is to place the **application project** (our API) and the **test project** as **sibling directories** under the same solution (`.sln` file).

**Reasons:**

- **Isolation** → keeps the test code separate from the production code.
- **Clarity** → makes it clear what belongs to the API and what belongs to testing.
- **References** → the test project must reference the API project.
  - If they were mixed in one place, we could run into **circular references** (the API accidentally referencing its own test code).
- **Maintainability** → easier to manage builds, CI/CD pipelines, and package dependencies when projects are separated.

**Structure Example:**

```bash
BlogApiTesting/       # Solution root (.sln file here)
├── BlogApi/          # Main API project
│ ├── BlogApi.csproj
│ ├── Program.cs
│ └── ...
└── BlogApi.Tests/    # Test project
├── BlogApi.Tests.csproj
└── Unit/
└── UserServiceTests.cs
```

```bash
# Create a new directory
mkdir BlogApi
# Move our application code into it
mv BlogApi.csproj BlogApi.sln Program.cs appsettings*.json blog.db Api/ Dtos/ Infrastructure/ Models/ Properties/ Services/ Migrations/ BlogApi/
# Create new testing project
dotnet new xunit -o BlogApi.Tests
# Create our new parent solution
dotnet new sln -n BlogApiTesting
# Register both projects in solution
dotnet sln add BlogApi/BlogApi.csproj BlogApi.Tests/BlogApi.Tests.csproj
# Make the testing project reference the application project
dotnet add BlogApi.Tests reference BlogApi/BlogApi.csproj
# Helpful packages for testing
# WebApplicationFactory for integration testing
 dotnet add BlogApi.Tests package Microsoft.AspNetCore.Mvc.Testing
# EF Core InMemory provider for DB tests
 dotnet add BlogApi.Tests package Microsoft.EntityFrameworkCore.InMemory
# Optional: Moq for mocking interfaces
 dotnet add BlogApi.Tests package Moq
```

<details>
<summary>🧪 xUnit </summary>

## xUnit is our testing framework

In xUnit we use attributes to mark test methods.

### `[Fact]`

- Used for a single, independent test.
- No parameters, always runs with the same setup.

```csharp
[Fact]
public void Add_TwoNumbers_ReturnsSum()
{
    // Arrange
    var a = 2;
    var b = 3;

    // Act
    var result = a + b;

    // Assert
    Assert.Equal(5, result);
}
```

[Theory]

- Used for running the same test logic with multiple inputs.
- Works with [InlineData(...)] to supply test cases.

## Arrange → Act → Assert (AAA Pattern)

Most tests follow a simple and clear structure known as **AAA**:

1. **Arrange** → set up your data, dependencies, and context.
2. **Act** → call the method or functionality you want to test.
3. **Assert** → check the result matches your expectation.
</details>
</details>

<details>
<summary>🧪 Testing Strategy for BlogApi</summary>

### 🧪 Unit Tests

Focus: **business logic in services** (no HTTP layer).  
We use **EF Core InMemory** to simulate the database.

- **UserService** → basic CRUD operations (create, get, update, delete).
- **PostService** → business rule validation (user must exist before creating a post).
- **AuthService** → uses **mocks** (UserManager, IConfiguration) since dependencies are too complex for InMemory DB.

### Why EF Core InMemory?

- Faster than using a real database.
- Runs real EF Core logic (LINQ, SaveChanges).
- Each test gets its own isolated database.
- Easier and more realistic than mocking DbContext.

### 🌐 Integration Tests

Focus: **end-to-end behaviour of HTTP endpoints**.  
We use **WebApplicationFactory** to spin up the API in memory.

- **Public endpoints** → `GET /users`, `POST /users` (no authentication).
- **Protected endpoints** → `POST /posts`, `GET /auth/me` (requires JWT – we use fake auth in tests).

### 🗂️ Test Directory Structure

```bash
BlogApiTesting/
├── BlogApiTesting.sln            # Solution file grouping both projects
├── BlogApi/                      # Main application project
│   ├── BlogApi.csproj
│   ├── Program.cs
│   ├── Services/
│   └── ...
└── BlogApi.Tests/                # Test project (sibling to BlogApi)
    ├── BlogApi.Tests.csproj
    ├── Unit/
    │   ├── UserServiceTests.cs   # EF InMemory examples (Create, Get, Update, Delete)
    │   ├── PostServiceTests.cs   # Business logic validation (user exists check)
    │   └── AuthServiceTests.cs   # Mocking complex dependencies (UserManager)
    ├── Integration/
    │   ├── TestHost.cs           # WebApplicationFactory setup with fake auth
    │   ├── UserEndpointsTests.cs # GET /users, POST /users (public endpoints)
    │   └── PostEndpointsTests.cs # POST /posts (protected endpoint)
    └── UnitTest1.cs              # Delete this default file
```

### Why Run in Development Mode?

- Uses `appsettings.Development.json`.
- Shows detailed error pages for debugging.
- Supports hot reload (`dotnet watch run`).
- Perfect for local development (not for deployment).

#### Run API in development mode:

`dotnet watch run --project BlogApi`

<!-- #### Run API in production

`dotnet run --project BlogApi` -->

#### Run tests

`dotnet test`

> ⚠️ **Note**  
> When running tests with `dotnet test`, you **do not need** to start the API manually.  
> The test project will spin up the API in memory (using `WebApplicationFactory`) when needed.

</details>

<details>
<summary>🧪 Unit Tests with EF InMemory</summary>

## Why EF Core InMemory Instead of Mocking?

Our `UserService` talks directly to **ApplicationDbContext**.  
To test it we have two options:

- **Mock the entire EF Core framework**  
  → would require mocking `DbContext`, `DbSet<T>`, `FindAsync()`, `SaveChangesAsync()`, LINQ methods, etc.  
  → very complex and fragile.

- **Use EF Core InMemory database** ✅  
  → real EF Core behavior (LINQ, queries, SaveChanges) but data is stored only in memory.  
  → faster than a real DB and much simpler than mocking.

📌 That’s why we use **InMemory** for services like `UserService` and `PostService`.

## A Note on Repository Pattern

For larger applications, a better architectural approach is to use the **Repository Pattern**.

Instead of services talking directly to `DbContext`, we define an interface for common CRUD operations and let the repository handle EF Core logic.  
This makes services simpler and testing easier (you only need to mock a few repository methods instead of the whole EF Core framework).

```csharp
public interface IUserRepository
{
    Task<User?> GetAsync(Guid id);
    Task<IReadOnlyList<User>> ListAsync();
    Task<User> CreateAsync(User user);
    Task<User?> UpdateAsync(User user);
    Task<bool> DeleteAsync(Guid id);
}
```

- Services depend only on simple repository interfaces.
- Mocks become much easier (you mock 5–10 methods, not the entire EF Core).
- For this lesson, we stick with the direct DbContext approach to focus on testing concepts.

</details>

<details>
<summary> 1️⃣ Unit Test Example: UserService with EF InMemory</summary>

### Unit Test Example: UserService with EF InMemory

```csharp
// BlogApi.Tests/Unit/UserServiceTests.cs
using Microsoft.EntityFrameworkCore;
using BlogApi.Infrastructure;
using BlogApi.Services;
using BlogApi.Models;
using Xunit;

namespace BlogApi.Tests.Unit;

public class UserServiceTests
{
    // Helper method → each test gets its own isolated in-memory DB
    private static ApplicationDbContext CreateInMemoryDb()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new ApplicationDbContext(options);
    }

    [Fact] // used for a single, independent test.
    // No parameters, always runs with the same setup.
    public async Task CreateAsync_ValidInput_CreatesUserSuccessfully()
    {
      // ARRANGE (setup phase)
      // Here we create a fresh in-memory database and a UserService that uses it.
      await using var db = CreateInMemoryDb();
      var service = new UserService(db);

      // ACT (execution phase)
      // Call the method under test: we try to create a new user.
      var user = await service.CreateAsync("Alice Johnson", "alice@example.com");

      // ASSERT (verification phase)
      // Now we check that the result matches our expectations.
      Assert.NotEqual(Guid.Empty, user.Id); // user.Id should not be the default empty Guid
      Assert.Equal("Alice Johnson", user.Name); // Name should match input
      Assert.Equal("alice@example.com", user.Email); // Email should match input
      Assert.True(user.CreatedAt <= DateTimeOffset.UtcNow); // CreatedAt should be set to now or earlier

      // EXTRA ASSERT: Verify persistence in the database
      // We query the in-memory DB directly to confirm the user was actually saved.
      var savedUser = await db.Users.FindAsync(user.Id);
      Assert.NotNull(savedUser); // The user should exist in the database
      Assert.Equal(user.Name, savedUser!.Name); // Saved user's name should match

    }

    [Fact]
    public async Task GetAsync_ExistingUser_ReturnsUser()
    {
        await using var db = CreateInMemoryDb();
        var service = new UserService(db);
        var created = await service.CreateAsync("Bob Smith", "bob@example.com");

        var retrieved = await service.GetAsync(created.Id);

        Assert.NotNull(retrieved);
        Assert.Equal(created.Id, retrieved!.Id);
        Assert.Equal("Bob Smith", retrieved.Name);
    }

    [Fact]
    public async Task GetAsync_NonExistentUser_ReturnsNull()
    {
        await using var db = CreateInMemoryDb();
        var service = new UserService(db);

        var user = await service.GetAsync(Guid.NewGuid());

        Assert.Null(user);
    }
}

```

## Arrange → Act → Assert (AAA Pattern)

Most tests follow a simple and clear structure known as **AAA**:

1. **Arrange** → set up your data, dependencies, and context.
2. **Act** → call the method or functionality you want to test.
3. **Assert** → check the result matches your expectation.

## Key Testing Patterns Demonstrated

- **Arrange → Act → Assert** → clear test structure separating setup, execution, and verification.
- **Isolated Tests** → each test gets its own database instance (`Guid.NewGuid().ToString()`).
- **Realistic Testing** → tests actual EF Core behavior, not mocked behavior.
- **Fast Execution** → in-memory database is much faster than a real database.
- **Database Verification** → tests verify data was actually persisted.

Run the tests with: `dotnet test`

</details>

<details>
<summary>2️⃣ Unit Test Example: PostService with Business Logic Validation</summary>

### Unit Test Example: PostService with Business Logic Validation

## PostService Tests

`PostService` contains more complex business logic than `UserService`:

- It requires a valid user before creating a post.
- It supports updates, deletions, and listing posts by user.
- This makes it perfect for testing both **happy path scenarios** and **error conditions**.

### Example: PostServiceTests

```csharp
using Microsoft.EntityFrameworkCore;
using BlogApi.Infrastructure;
using BlogApi.Services;
using BlogApi.Models;

namespace BlogApi.Tests.Unit;

public class PostServiceTests
{
    // Helper → every test gets its own unique in-memory database
    private static ApplicationDbContext CreateInMemoryDb()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // unique name for isolation
            .Options;
        return new ApplicationDbContext(options);
    }

    [Fact]
    public async Task CreateAsync_ValidUser_CreatesPostSuccessfully()
    {
        // ARRANGE
        // First set up DB + services and insert a valid user
        await using var db = CreateInMemoryDb();
        var userService = new UserService(db);
        var postService = new PostService(db);

        var user = await userService.CreateAsync("Alice Johnson", "alice@example.com");

        // ACT
        // Try to create a post for that user
        var post = await postService.CreateAsync(user.Id, "My First Post", "This is the content");

        // ASSERT
        // Check that the returned post has correct values
        Assert.NotEqual(Guid.Empty, post.Id);               // ID should be generated
        Assert.Equal(user.Id, post.UserId);                 // Linked to correct user
        Assert.Equal("My First Post", post.Title);          // Title must match input
        Assert.Equal("This is the content", post.Content);  // Content must match input
        Assert.True(post.PublishedAt <= DateTimeOffset.UtcNow); // should be set

        // EXTRA ASSERT → confirm it was really saved in DB
        var savedPost = await db.Posts.FindAsync(post.Id);
        Assert.NotNull(savedPost); // Post exists in DB
    }

    [Fact]
    public async Task CreateAsync_NonExistentUser_ThrowsArgumentException()
    {
        // ARRANGE
        await using var db = CreateInMemoryDb();
        var postService = new PostService(db);
        var fakeUserId = Guid.NewGuid(); // random userId not in DB

        // ACT + ASSERT
        // Creating a post with invalid userId should throw an exception
        var exception = await Assert.ThrowsAsync<ArgumentException>(
            () => postService.CreateAsync(fakeUserId, "Post Title", "Post Content")
        );

        Assert.Equal("User not found (Parameter 'userId')", exception.Message);
    }

    [Fact]
    public async Task ListByUserAsync_ExistingUserWithPosts_ReturnsUserPosts()
    {
        // ARRANGE
        await using var db = CreateInMemoryDb();
        var userService = new UserService(db);
        var postService = new PostService(db);

        // Create two users
        var alice = await userService.CreateAsync("Alice", "alice@example.com");
        var bob = await userService.CreateAsync("Bob", "bob@example.com");

        // Create posts for both users
        await postService.CreateAsync(alice.Id, "Alice Post 1", "Content 1");
        await postService.CreateAsync(alice.Id, "Alice Post 2", "Content 2");
        await postService.CreateAsync(bob.Id, "Bob Post 1", "Content 3");

        // ACT
        // Fetch only Alice's posts
        var alicePosts = await postService.ListByUserAsync(alice.Id);

        // ASSERT
        Assert.Equal(2, alicePosts.Count);                      // Alice has 2 posts
        Assert.All(alicePosts, p => Assert.Equal(alice.Id, p.UserId)); // all linked to Alice
    }

    [Fact]
    public async Task UpdateAsync_ExistingPost_UpdatesSuccessfully()
    {
        // ARRANGE
        await using var db = CreateInMemoryDb();
        var userService = new UserService(db);
        var postService = new PostService(db);

        // Insert user + original post
        var user = await userService.CreateAsync("Author", "author@example.com");
        var post = await postService.CreateAsync(user.Id, "Original Title", "Original Content");

        // ACT
        // Update only the title, leave content unchanged
        var updatedPost = await postService.UpdateAsync(post.Id, "Updated Title", null);

        // ASSERT
        Assert.NotNull(updatedPost);
        Assert.Equal("Updated Title", updatedPost!.Title);          // Title updated
        Assert.Equal("Original Content", updatedPost.Content);      // Content unchanged

        // EXTRA ASSERT → verify changes persisted in DB
        var savedPost = await db.Posts.FindAsync(post.Id);
        Assert.Equal("Updated Title", savedPost!.Title);
    }

    [Fact]
    public async Task DeleteAsync_ExistingPost_DeletesSuccessfully()
    {
        // ARRANGE
        await using var db = CreateInMemoryDb();
        var userService = new UserService(db);
        var postService = new PostService(db);

        var user = await userService.CreateAsync("Author", "author@example.com");
        var post = await postService.CreateAsync(user.Id, "Post to Delete", "Content");

        // ACT
        var result = await postService.DeleteAsync(post.Id);

        // ASSERT
        Assert.True(result); // Delete should return true
        Assert.Null(await db.Posts.FindAsync(post.Id)); // Post should be gone
    }

    [Fact]
    public async Task DeleteAsync_NonExistentPost_ReturnsFalse()
    {
        // ARRANGE
        await using var db = CreateInMemoryDb();
        var postService = new PostService(db);

        // ACT
        var result = await postService.DeleteAsync(Guid.NewGuid());

        // ASSERT
        Assert.False(result); // Nothing to delete → returns false
    }
}
```

## Key Testing Patterns for Business Logic

- **Testing prerequisites** → `CreateAsync_ValidUser_CreatesPostSuccessfully` shows how to set up required data first (a post needs an existing user).
- **Exception testing** → `CreateAsync_NonExistentUser_ThrowsArgumentException` uses `Assert.ThrowsAsync<T>()` to verify proper error handling.
- **Data filtering** → `ListByUserAsync_ExistingUserWithPosts_ReturnsUserPosts` ensures queries only return the correct user’s posts.
- **Partial updates** → `UpdateAsync_ExistingPost_UpdatesSuccessfully` tests updating some fields while leaving others unchanged.
- **Boolean returns** → `DeleteAsync_*` verifies both success (`true`) and failure (`false`) cases for delete operations.

📌 These tests go beyond basic CRUD and validate the **business rules** in `PostService`, including error conditions and edge cases.

</details>

<details>
<summary>3️⃣ Unit Test Example: AuthService with Complex Dependencies (Mocking Required)</summary>

## Why Mocking?

The `AuthService` depends on **complex external classes**:

- `UserManager<User>` (ASP.NET Identity)
- `IConfiguration` (for JWT settings)

These are:

- ❌ Hard to set up with real DB/Identity
- ❌ Slow and fragile in tests
- ✅ Perfect candidates for **mocking**

A **mock** = a fake implementation that you control in tests.  
Instead of using the real dependency, you define:

- what methods should return,
- when they should throw,
- and verify which methods were called.

## What is Moq?

[Moq](https://github.com/moq/moq4) is the most popular .NET mocking framework.  
It lets you:

- **Create fakes** → `var mock = new Mock<IUserRepository>();`
- **Setup behavior** → `mock.Setup(repo => repo.GetAsync(...))`
- **Use fake object** → `var fakeRepo = mock.Object;`
- **Verify interactions** → `mock.Verify(..., Times.Once);`

```csharp
// BlogApi.Tests/Unit/AuthServiceTests.cs
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;
using BlogApi.Services;
using BlogApi.Models;
using BlogApi.Dtos.Auth;

namespace BlogApi.Tests.Unit;

public class AuthServiceTests
{
    // Helper → create a fake UserManager with minimal setup
    private static Mock<UserManager<User>> CreateMockUserManager()
    {
        var store = new Mock<IUserStore<User>>();
        return new Mock<UserManager<User>>(
            store.Object, null, null, null, null, null, null, null, null);
    }

    // Helper → create a fake IConfiguration with JWT values
    private static Mock<IConfiguration> CreateMockConfiguration()
    {
        var mockConfig = new Mock<IConfiguration>();
        mockConfig.Setup(c => c["Jwt:Key"]).Returns("super-secret-jwt-signing-key-that-is-at-least-256-bits-long-for-hmac-sha256");
        mockConfig.Setup(c => c["Jwt:Issuer"]).Returns("BlogApi");
        mockConfig.Setup(c => c["Jwt:Audience"]).Returns("BlogApi-Users");
        mockConfig.Setup(c => c["Jwt:ExpiryMinutes"]).Returns("60");
        return mockConfig;
    }

    [Fact]
    public async Task RegisterAsync_ValidInput_ReturnsSuccess()
    {
        // Arrange
        var mockUserManager = CreateMockUserManager();
        var mockConfig = CreateMockConfiguration();
        var authService = new AuthService(mockUserManager.Object, mockConfig.Object);

        var request = new RegisterRequestDto
        {
            Name = "Alice Johnson",
            Email = "alice@example.com",
            Password = "SecurePassword123!"
        };

        // Fake → user creation always succeeds
        mockUserManager.Setup(um => um.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                       .ReturnsAsync(IdentityResult.Success);

        // Act
        var result = await authService.RegisterAsync(request);

        // Assert
        Assert.True(result.Success);
        Assert.Empty(result.Errors);

        // Verify → UserManager.CreateAsync was called once with expected params
        mockUserManager.Verify(um => um.CreateAsync(
            It.Is<User>(u => u.Email == "alice@example.com" && u.Name == "Alice Johnson"),
            "SecurePassword123!"
        ), Times.Once);
    }

    [Fact]
    public async Task LoginAsync_ValidCredentials_ReturnsAuthToken()
    {
        // Arrange
        var mockUserManager = CreateMockUserManager();
        var mockConfig = CreateMockConfiguration();
        var authService = new AuthService(mockUserManager.Object, mockConfig.Object);

        var request = new LoginRequestDto
        {
            Email = "alice@example.com",
            Password = "SecurePassword123!"
        };

        // Fake → existing user in DB
        var existingUser = new User
        {
            Id = Guid.NewGuid(),
            Email = "alice@example.com",
            Name = "Alice Johnson",
            UserName = "alice@example.com"
        };

        // Fake → user lookup succeeds, password check passes
        mockUserManager.Setup(um => um.FindByEmailAsync("alice@example.com"))
                       .ReturnsAsync(existingUser);
        mockUserManager.Setup(um => um.CheckPasswordAsync(existingUser, "SecurePassword123!"))
                       .ReturnsAsync(true);

        // Act
        var result = await authService.LoginAsync(request);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result!.Token);
        Assert.True(result.ExpiresAtUtc > DateTime.UtcNow);
        Assert.True(result.Token.Length > 50); // JWT should be long
    }

    [Fact]
    public async Task LoginAsync_InvalidPassword_ReturnsNull()
    {
        // Arrange
        var mockUserManager = CreateMockUserManager();
        var mockConfig = CreateMockConfiguration();
        var authService = new AuthService(mockUserManager.Object, mockConfig.Object);

        var request = new LoginRequestDto
        {
            Email = "alice@example.com",
            Password = "WrongPassword123!"
        };

        var existingUser = new User { Id = Guid.NewGuid(), Email = "alice@example.com", Name = "Alice Johnson" };

        // Fake → user found but password check fails
        mockUserManager.Setup(um => um.FindByEmailAsync("alice@example.com"))
                       .ReturnsAsync(existingUser);
        mockUserManager.Setup(um => um.CheckPasswordAsync(existingUser, "WrongPassword123!"))
                       .ReturnsAsync(false);

        // Act
        var result = await authService.LoginAsync(request);

        // Assert
        Assert.Null(result); // login should fail
    }
}

```

## When to Use Mock vs EF InMemory

- **Use Mock** → for **external services** such as
  - `UserManager`, `IConfiguration`
  - `HttpClient`, third-party APIs
  - complex framework classes
- **Use EF InMemory** → for **database operations** where you want real EF Core behavior (CRUD, queries, relationships).

## Key Benefits of Moq

- **Speed** → tests run in milliseconds.
- **Reliability** → no external dependencies to fail.
- **Isolation** → you test only _your_ code.
- **Verification** → confirm methods were called with correct parameters.
- **Edge cases** → easily simulate errors, exceptions, and timeouts.

## References

- [xUnit Documentation](https://xunit.net)
- [EF Core InMemory Provider](https://learn.microsoft.com/ef/core/testing/in-memory)

</details>
