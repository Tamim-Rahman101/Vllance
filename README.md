# Vllance - Web-Based Vehicle Surveillance System

## ?? Running the Project

### Prerequisites
- .NET 9 SDK installed
- Visual Studio 2022 or VS Code

### Run the Application

1. **Using Visual Studio:**
   - Open the solution in Visual Studio
   - Press `F5` or click the "Run" button
   - The application will open in your default browser

2. **Using Command Line:**
   ```bash
   cd Vllance
   dotnet run
   ```
   - Open your browser and navigate to `https://localhost:5001` or `http://localhost:5000`

3. **Using VS Code:**
   - Open the integrated terminal
   - Run `dotnet watch run` for hot reload
   - Navigate to the URL shown in the terminal

## ?? Project Structure (MVC Pattern)

```
Vllance/
??? Controllers/          # MVC Controllers
?   ??? HomeController.cs # Handles home page requests
??? Models/              # Data models
?   ??? Admin.cs         # Admin user model
?   ??? User.cs          # User model with vehicles
?   ??? Guard.cs         # Security guard model
?   ??? Vehicle.cs       # Vehicle model
?   ??? ErrorViewModel.cs # Error page model
??? Views/               # Razor views
?   ??? Home/
?   ?   ??? Index.cshtml # Homepage view
?   ??? Shared/
?   ?   ??? _Layout.cshtml     # Shared layout
?   ?   ??? Error.cshtml       # Error page
?   ??? _ViewImports.cshtml    # View imports
?   ??? _ViewStart.cshtml      # View start
??? wwwroot/             # Static files
?   ??? css/
?   ?   ??? homepage.css # Homepage styles
?   ??? js/
?       ??? homepage.js  # Homepage interactions
??? Program.cs           # Application entry point (MVC configuration)
```

## ?? MVC Architecture

This project follows the **Model-View-Controller (MVC)** pattern:

- **Models** - Data structures and business logic
- **Views** - User interface (Razor files in Views folder)
- **Controllers** - Handle HTTP requests and coordinate between Models and Views

### Routing
The default route is configured as:
```
{controller=Home}/{action=Index}/{id?}
```

This means:
- `/` ? HomeController.Index()
- `/Home/Index` ? HomeController.Index()
- `/Home/Privacy` ? HomeController.Privacy()

## ?? Homepage Features

- **Modern, Minimalistic Design** - Clean UI with dark navy theme
- **Responsive Layout** - Works on desktop, tablet, and mobile
- **Smooth Animations** - Fade-in effects and scroll animations
- **Interactive Elements** - Hover effects on cards and buttons
- **Sticky Navigation** - Fixed navbar with scroll effects
- **Feature Showcase** - 4 key features displayed in cards
- **How It Works** - 3-step process visualization
- **Professional Footer** - Links and copyright information

## ?? Next Steps

You can now:
1. Create additional controllers (e.g., `AccountController` for login/register)
2. Add more views in the `Views` folder
3. Build the dashboard
4. Implement authentication
5. Add database context for the models

## ?? Current Navigation Links

The homepage has navigation links ready for:
- `/Login` - Login page (to be created)
- `/Register` - Registration page (to be created)
- `/Privacy` - Privacy policy (to be created)
- `/Terms` - Terms of service (to be created)

## ?? Design Features

- **Colors:**
  - Primary: Dark Navy (#0f172a)
  - Secondary: Slate (#1e293b)
  - Accent: Cyan/Teal (#06b6d4)
  
- **Typography:**
  - Headings: Poppins (Google Fonts)
  - Body: Inter (Google Fonts)

- **Animations:**
  - Scroll reveal on feature cards
  - Pulse effect on security cameras
  - Smooth transitions on hover
  - Parallax effect on hero illustration

Enjoy building your Vehicle Surveillance System! ????
