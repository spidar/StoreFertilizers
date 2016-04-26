REM dnu restore
REM dnvm use 1.0.0-rc1-final -p

dnx ef migrations add StoreFertilizersDB
dnx ef database update
pause