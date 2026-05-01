# Vllance - Web-Based Vehicle Surveillance System

![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?logo=dotnet)
![C#](https://img.shields.io/badge/C%23-13.0-239120?logo=csharp)
![SQL Server](https://img.shields.io/badge/SQL%20Server-2022-CC2927?logo=microsoft-sql-server)
![ASP.NET](https://img.shields.io/badge/ASP.NET-Core%209.0-512BD4?logo=asp.net)
![Entity Framework](https://img.shields.io/badge/Entity%20Framework-Core%209.0-512BD4?logo=entity-framework)
![FastAPI](https://img.shields.io/badge/FastAPI-0.95.2-009688?logo=fastapi)



**Vllance** is an intelligent web-based vehicle surveillance and security system that enables users to monitor, lock, and protect their vehicles remotely from anywhere, at any time.

## Features

### Core Functionality
- **Real-Time Vehicle Detection** - Advanced AI-powered monitoring with instant vehicle detection capabilities
- **Remote Lock/Unlock** - Control your vehicle's security from anywhere with one-tap access
- **Movement Detection** - Real-time alerts for unauthorized vehicle movement
- **Live Video Feed** - Stream and monitor camera feeds via RTSP or video files
- **Interactive Vehicle Selection** - Click-to-select vehicles from video frames for targeted monitoring

### Security & Management
- **Movement Threshold Monitoring** - Configurable pixel-based movement detection
- **Instant Alerts** - Get notified immediately when suspicious activity is detected
- **User Authentication** - Secure login and registration system
- **Admin Dashboard** - Comprehensive zone and user management

### Monitoring Dashboard
- **Video Display Panel** - Real-time video feed with detection overlays
- **Control Panel** - Start/stop monitoring with live statistics
- **Detection Stats** - Track detected vehicles and movement distance

## Tech Stack

### Backend
- **Framework**: ASP.NET Core 9.0 (MVC)
- **Language**: C# 13.0
- **Database**: SQL Server with Entity Framework Core 9.0
- **ORM**: Entity Framework Core

### Frontend
- **UI**: Razor Pages with custom CSS
- **Fonts**: Google Fonts (Inter, Poppins)
- **Icons**: Custom SVG illustrations
- **Animations**: CSS transitions and keyframes

### AI & Detection
- **Integration**: FastAPI backend (Python)
- **Detection**: YOLO-based vehicle detection
- **Video Processing**: Support RTSP stream and prerecorded video file

## 📋 Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [SQL Server](https://www.microsoft.com/sql-server)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)
- FastAPI Backend Server (for vehicle detection features)

## 🚀 Getting Started

### Installation

1. **Clone the repository**
```bash
git clone https://github.com/Tamim-Rahman101/Vllance.git
```

2. **Restore dependencies**
```bash
cd Vllance
dotnet restore
```


3. **Update database connection string** - Edit `appsettings.json` with your SQL Server connection details
```bash
"ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=VllanceDB;TrustServerCertificate=true"
}
```

4. **Apply database migrations**
```bash
dotnet ef database update
```


5. **Configure FastAPI backend**
- Update the `apiBaseUrl` in Demo.cshtml (default: `http://localhost:8000`)
- Ensure your FastAPI server is running with the following endpoints:
  - `/api/detect_first_frame`
  - `/api/select_vehicle`
  - `/api/monitor_control`
  - `/api/monitor_vehicle`
  - `/api/current_frame`


6. **Run the application**

```bash
dotnet run
```
7. **Access the application**
```bash
http://localhost:5000
```



## Usage

### User Flow
1. **Register/Login** - Register an account or log in
2. **Access Demo** - Try the vehicle detection demo from the homepage
3. **Load Video Feed** - Enter RTSP URL or video file path
4. **Detect Vehicles** - System automatically detects vehicles in the frame
5. **Select Vehicle** - Click on a detected vehicle to monitor it
6. **Start Monitoring** - Begin real-time movement tracking
7. **Receive Alerts** - Get notified of any unauthorized movement



## Author

**Tamim Rahman**
- GitHub: [@Tamim-Rahman101](https://github.com/Tamim-Rahman101)

## Acknowledgments

- YOLO for vehicle detection capabilities
- Entity Framework Core team
- ASP.NET Core community
- Github copilot for AI assistance
