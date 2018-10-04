@echo off
set h=%TIME:~0,2%
set m=%TIME:~3,2%
set s=%TIME:~6,2%
set ms=%TIME:~9,2%
set curtime=%h%_%m%_%s%_%ms%
set dd=%DATE:~0,2%
set mm=%DATE:~3,2%
set yyyy=%DATE:~6,4%
set curdate=%dd%_%mm%_%yyyy%
set curdatetime=%curdate%__%curtime%

set migration_name=DataMigration%curdatetime%
echo Creating migration: "%migration_name%"...
dotnet ef migrations add %migration_name%

echo Applying changes...
dotnet ef database update
pause