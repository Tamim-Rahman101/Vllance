# Vllance Authentication System

## Overview
The Vllance application now has a complete role-based authentication system with three user roles: **Admin**, **User**, and **Guard**.

## Features Implemented

### 1. **User Models with Authentication**
All three user models (Admin, User, Guard) now include:
- `PasswordHash`: Securely hashed password
- `CreatedAt`: Account creation timestamp
- `LastLoginAt`: Last successful login timestamp

### 2. **Authentication Service**
- **Password Hashing**: Uses SHA256 (can be upgraded to BCrypt/Argon2 for production)
- **Email Validation**: Prevents duplicate emails across all roles
- **Role-based Authentication**: Validates credentials against the appropriate database table

#### Key Methods:
```csharp
AuthenticateAsync(email, password, role) 
// Returns: (Success, Role, UserId, ErrorMessage)

RegisterAsync(role, fullName, email, phoneNumber, institute, password, shift)
// Returns: (Success, ErrorMessage)

HashPassword(password)
// Returns: hashed password string

VerifyPassword(password, passwordHash)
// Returns: boolean
```

### 3. **Session Management**
- **Session Timeout**: 30 minutes of inactivity
- **Secure Cookies**: HttpOnly, HTTPS-only, SameSite=Strict
- **Session Data Stored**:
  - `UserRole`: "Admin", "User", or "Guard"
  - `UserId`: Database ID of the logged-in user
  - `UserEmail`: Email address of the user

### 4. **Role-Based Authorization**
Custom `[RoleAuthorization]` attribute protects dashboard routes:

```csharp
[RoleAuthorization("Admin")]  // Only admins can access
public class AdminDashboardController : Controller { }

[RoleAuthorization("User")]   // Only users can access
public class UserDashboardController : Controller { }

[RoleAuthorization("Guard")]  // Only guards can access
public class GuardDashboardController : Controller { }
```

### 5. **Protected Dashboard Controllers**
Each dashboard now:
- Requires authentication and correct role
- Displays role-specific data
- Shows user information from session
- Redirects unauthorized access to AccessDenied page

## User Workflows

### Registration Flow
1. User selects role (Admin/User/Guard)
2. Fills in required information:
   - Full Name
   - Email (must be unique)
   - Phone Number
   - Institute/Organization
   - Password & Confirm Password
   - Shift (Guard only)
3. System validates input
4. Password is hashed and stored
5. User is redirected to Login page

### Login Flow
1. User selects role (Admin/User/Guard)
2. Enters email and password
3. System validates credentials against correct database table
4. On success:
   - Session is created with user info
   - Last login timestamp is updated
   - User is redirected to role-specific dashboard
5. On failure:
   - Error message is displayed
   - User remains on login page

### Dashboard Access
1. User attempts to access a dashboard
2. `RoleAuthorization` filter checks:
   - Is user logged in? (session exists)
   - Does user have correct role?
3. If authorized: Dashboard loads with user-specific data
4. If not authorized: Redirect to AccessDenied page

### Logout Flow
1. User clicks logout
2. Session is cleared
3. User is redirected to homepage

## Security Features

### ? Implemented
- Password hashing (SHA256)
- Session-based authentication
- Role-based authorization
- HTTPS-only cookies
- Anti-forgery tokens on forms
- Duplicate email prevention
- Last login tracking

### ?? Production Recommendations
1. **Upgrade Password Hashing**: Replace SHA256 with BCrypt or Argon2
2. **Add Password Requirements**: Minimum length, complexity rules
3. **Implement Rate Limiting**: Prevent brute force attacks
4. **Add Email Verification**: Confirm email before activation
5. **Add Two-Factor Authentication (2FA)**
6. **Add Password Reset**: Complete forgot password flow
7. **Add Account Lockout**: After failed login attempts
8. **Add Audit Logging**: Track all authentication events

## API Endpoints

### Account Controller
- `GET /Account/Login` - Display login page
- `POST /Account/Login` - Process login
- `GET /Account/Register` - Display registration page
- `POST /Account/Register` - Process registration
- `GET /Account/ForgotPassword` - Display forgot password page
- `GET /Account/AccessDenied` - Display access denied page
- `POST /Account/Logout` - Log out user
- `GET /Account/LogoutGet` - Alternative logout (convenience)

### Dashboard Controllers (Protected)
- `GET /AdminDashboard/Index` - Admin dashboard (Admin only)
- `GET /UserDashboard/Index` - User dashboard (User only)
- `GET /GuardDashboard/Index` - Guard dashboard (Guard only)

## Database Schema Changes

### Added to All User Tables (Admins, Users, Guards)
```sql
PasswordHash nvarchar(max) NOT NULL
CreatedAt datetime2 NOT NULL DEFAULT GETDATE()
LastLoginAt datetime2 NULL
```

### Modified Guard Table
```sql
ZoneId int NULL  -- Changed from NOT NULL to allow registration before zone assignment
```

## Configuration

### Program.cs Setup
```csharp
// Register services
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.Strict;
});

// Use middleware
app.UseSession();
```

## Testing the System

### Test Accounts (Create via Registration)
```
Admin:
- Email: admin@example.com
- Password: Admin@123
- Role: Admin

User:
- Email: user@example.com
- Password: User@123
- Role: User

Guard:
- Email: guard@example.com
- Password: Guard@123
- Role: Guard
- Shift: Morning
```

### Test Scenarios
1. ? Register with each role
2. ? Login with correct credentials
3. ? Login with wrong password (should fail)
4. ? Login with wrong role (should fail)
5. ? Access dashboard with correct role
6. ? Try to access wrong dashboard (should redirect to AccessDenied)
7. ? Try to access dashboard without login (should redirect to Login)
8. ? Logout and verify session cleared
9. ? Try to register with duplicate email (should fail)

## Error Handling

### Common Error Messages
- "Please fill in all fields." - Missing required input
- "Invalid email or password." - Authentication failed
- "An account with this email already exists." - Duplicate registration
- "Passwords do not match." - Confirmation mismatch
- "Shift is required for Guard role." - Missing guard shift
- "You don't have permission to access this page." - Authorization failed

## Future Enhancements

### High Priority
1. Implement password reset functionality
2. Add email verification
3. Upgrade to BCrypt password hashing
4. Add password strength requirements
5. Implement account lockout after failed attempts

### Medium Priority
1. Add "Remember Me" functionality
2. Add session activity tracking
3. Add user profile management
4. Add admin user management
5. Add password change functionality

### Low Priority
1. Add social login (Google, Microsoft)
2. Add two-factor authentication (2FA)
3. Add biometric authentication
4. Add device management
5. Add login history view

## Troubleshooting

### Issue: "Access Denied" on Dashboard
**Solution**: Check that:
1. You're logged in
2. You're using the correct role account
3. Session hasn't expired

### Issue: Registration Fails
**Solution**: Check that:
1. All required fields are filled
2. Email is unique (not already registered)
3. Passwords match
4. Guard role has shift selected

### Issue: Login Redirects to Wrong Dashboard
**Solution**: This shouldn't happen. If it does:
1. Clear browser cookies
2. Log out completely
3. Log in again

## Support
For issues or questions, contact the development team or check the project repository.
