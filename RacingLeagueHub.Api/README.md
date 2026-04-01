
### 1. Generate ENCRYPTION KEY:
```bash
# Run this once to generate your key, paste the output into your env variable
dotnet run --project KeyGen

# Or a quick one-liner in C#:
Convert.ToBase64String(RandomNumberGenerator.GetBytes(32))
# "k7Gx9mP2qR5vN8wL3hJ6yT1dF4cB0eA=" (example)
```
### 2. Set the environment variable:

```bash
# Linux/macOS
export RACINGLEAGUEHUB_ENCRYPTION_KEY="your-base64-key-here"

# Windows
setx RACINGLEAGUEHUB_ENCRYPTION_KEY "your-base64-key-here"

# Docker / docker-compose
environment:
  - RACINGLEAGUEHUB_ENCRYPTION_KEY=your-base64-key-here
```