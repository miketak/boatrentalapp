echo off

rem batch file to run a script to create a db
rem 9/4/2016

sqlcmd -S localhost -E -i boatDatabase.sql

ECHO .
ECHO if no error messages appear DB was created
PAUSE