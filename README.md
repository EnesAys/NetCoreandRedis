# NetCoreandRedis
ASP .Net Core 2.1 and Redis example

For Redis installation run cmd as a administrator and write commands
1- @"%SystemRoot%\System32\WindowsPowerShell\v1.0\powershell.exe" -NoProfile -ExecutionPolicy Bypass -Command "iex ((New-Object System.Net.WebClient).DownloadString('https://chocolatey.org/install.ps1'))" && SET "PATH=%PATH%;%ALLUSERSPROFILE%\chocolatey\bin"

2- choco install redis-64

if installation is success you should see ...

https://github.com/EnesAys/NetCoreandRedis/blob/master/RedisExample/Images/redis-server.JPG

Than make sure your redis is running


https://github.com/EnesAys/NetCoreandRedis/blob/master/RedisExample/Images/getSetRedis.JPG

Add your Redis configuration in startup.cs ConfigureServlces 

   services.AddDistributedRedisCache(options =>
                {
                    options.InstanceName = "RedisNetCoreSample";
                    options.Configuration = "localhost:6379"; //Your Redis Connection
                });

Add your nuget packages Microsoft.Extensions.Caching.Redis

You are ready!
