# City Events & Entertainment 

An ASP.NET Core MVC application to explore city museums, facilities, sports teams/players, and to manage **bookings** and **feedbacks**.  
Includes **role-based access control**: public/Users can browse and submit bookings/feedback; **Admins** can manage all data.

---

### Admin login
- Email: admin@cityevents.local
- Password: Admin#123
  
##  Features

- **CRUD**: Museums, Facilities, Teams, Players, Bookings, Feedbacks
- **Relationships**:
  - Museum ↔ Facilities (many-to-many via `MuseumFacility`)
  - Museum → Bookings (one-to-many)
  - Museum → Feedbacks (one-to-many)
  - Team → Players (one-to-many)
  - Museum → Team (optional FK)
- **Auth & Roles**: ASP.NET Core Identity, `Admin`/`User`
  - Public & Users: browse lists/details; create Bookings, add Feedback
  - Admins: full CRUD + relationship management pages
- **Admin Dashboard**: quick stats + recent bookings/feedbacks
- **API layer**: Swagger/OpenAPI for key endpoints
- **Bootstrap 5 UI** with Razor views and validation

---

##  Tech Stack

- **Backend**: ASP.NET Core 7/8 MVC, C#
- **Data**: Entity Framework Core (SQL Server)
- **Auth**: ASP.NET Core Identity (UserManager/RoleManager)
- **UI**: Razor, Bootstrap 5
- **Docs**: XML Comments + Swagger

---


##  Quickstart 

### 1) Prerequisites
- .NET SDK 7 or 8
- SQL Server (LocalDB is fine)  
- Visual Studio 2022 / VS Code

### 2) Clone & Restore
```bash
git clone https://github.com/<your-username>/City-events-and-entertainment.git
cd City-events-and-entertainment

# In Package Manager Console OR terminal
dotnet ef database update
# or
Update-Database

