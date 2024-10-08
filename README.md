# ToDoApp

Đề bài:

    - Create a to-do list application with user registration and login functionality.

    - Allow users to add, edit, delete, and mark tasks as complete.

    - Search task

    - Implement categories or tags for tasks.

    - Requirement:
        + Repository pattern
        + MVC architecture

Đã hoành thành:

    - CRUD cơ bản.

    - Thực hiện gán tags cho các tasks.

    - Triển khai theo mô hình MVC.

    - Sử dụng PostgreSQL.

Chưa hoành thành

    - Chức năng đăng nhập, đăng ký.

    - Chức năng tìm kiếm tasks.

    - Chức năng đánh dấu hoành thành một cách thuận tiện.

    - Triển khai: Repository pattern.

## Database

![Database Design](img/week1_1.png "Database design")

Tạo database, tài khoản và cấp quyền trong PostgreSQL :

```sql
CREATE DATABASE EXAMPLE_DB;
CREATE USER EXAMPLE_USER WITH ENCRYPTED PASSWORD 'Sup3rS3cret';
GRANT ALL PRIVILEGES ON DATABASE EXAMPLE_DB TO EXAMPLE_USER;
\c EXAMPLE_DB postgres
# You are now connected to database "EXAMPLE_DB" as user "postgres".
GRANT ALL ON SCHEMA public TO EXAMPLE_USER;
```

## Entity Framework

Các cài đặt thêm:

    - Microsoft.EntityFrameworkCore.Design

    - Npgsql.EntityFrameworkCore.PostgreSQL

    - Microsoft.EntityFrameworkCore.Tools

Câu lệnh chuyển tables từ database sang class trong Models:

    - dotnet ef dbcontext scaffold "Host=localhost;Port=5432;Database=todoappdb;Username=appuser;Password=123456" Npgsql.EntityFrameworkCore.PostgreSQL --output-dir Models

Hoặc nếu khai báo ConnectionString trong appsettings.json:

    - dotnet ef dbcontext scaffold "Name=ConnectionStrings:DefaultConnection" Npgsql.EntityFrameworkCore.PostgreSQL --output-dir Models

Nếu gặp lỗi không tìm thấy lệnh dotnet ef:

    - dotnet tool install --global dotnet-ef --version n.* // n là số version sử dụng

## Kết nối database

Mở file Program.cs thêm câu lệnh:

```C#
builder.Services.AddDbContext<TodoappdbContext>();
```

## Xây dựng chức năng

Xử lý logic nghiệp sẽ được xây dựng ở folder Controllers.

Cấu trúc giao diện được xây dựng ở folder Views.
