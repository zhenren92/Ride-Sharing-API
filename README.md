# Ride-Sharing-API
Ride Sharing System API

Requirement untuk Client side :
- Browser, contoh: Mozilla Firefox / Chrome

Requirement untuk Development :
- Visual Studio 2017 64bit
- .Net Core 2.0 64bit : Ref: https://www.microsoft.com/net/download/dotnet-core/2.0
- Windows 7, 10 64bit

Requirement untuk Server Side :
- Host .Net Core 2.0.9 64bit : Ref: https://www.microsoft.com/net/permalink/dotnetcore-current-windows-runtime-bundle-installer
- Internet Information Services (IIS)
- Windows 7, 10 64bit/ Windows Server 2008+ 64bit
- MS SQL Server 2008 atau 2016 32bit/64bit

Installasi :
- Install Visual Studio 2017 (Development Side)
- Install .Net Core 2.0 (Development side)
- Install Host .Net Core 2.0.9 (Server side)
- Install MS Sql Server 2008 atau 2016 (Server side)

Cara Menjalankan :
- Clone / Download Project ini
- Buka Visual Studio 2017 dan open file "Ride_Sharing_API.sln"
- Klik kanan pada project pilih "Publish" to "Folder" untuk mendistribusikan Web API 
yang akan diimplementasikan mengunakan IIS

Cara menjalankan IIS :
- Buka IIS 
- Pilih Sites -> Create New Sites -> Add Website :

-> Sitename : api.ride-sharing.com
-> Physical Path : Pilih Lokasi Folder yang dimana sudah didistribusikan
-> Port : 2829
-> Hostname : Kosongkan

- Selanjutnya Tekan Application Pools - > Klik kanan pada "api.ride-sharing.com" -> Pilih Basic Setting -> 
.NET CLR Version Pilih "No Managed Code"

Cara Pengetesan :
- Buka Browser Contoh : Mozilla Firefox / Chrome
- Masukan URL Contoh : http://localhost:2829/api/RideUser/GetRideUser
- Kemudian enter dan lihat hasil yang ditampilkan oleh browser yang berupa JSON
