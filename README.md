# 🏨 Booking System API
A multi-user booking system built with ASP.NET Core 9

## 🚀 How to Run the Project

### 1️⃣ Clone the Repository
```sh
git clone git@github.com:just-smsm/Booking-System-API.git
```

### 2️⃣ Navigate to the Project Folder
```sh
cd Booking-System-API
```

### 3️⃣ Restore Dependencies
```sh
dotnet restore
```

### 4️⃣ Open the Project in Visual Studio  
- Navigate to the project folder and open the solution file (`.sln`) in **Visual Studio 2022**.  
- Go to **Package Manager Console** and set **`BookingSystem.Infrastructure`** as the **Default Project**.

### 5️⃣ Apply Migrations & Seed Data  
While still in the **Package Manager Console**, run:
```sh
update-database
```

### 5️⃣ Run the Project  
Press **Ctrl + F5** in Visual Studio

### 6️⃣ Access Swagger UI for API Testing
- Open your browser and go to:  
  **`https://localhost:7063`**
