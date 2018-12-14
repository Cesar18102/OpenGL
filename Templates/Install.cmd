@echo off

echo OpenGL project template:

if exist  %USERPROFILE%"\Documents\Visual Studio 2010\Templates\" (
    echo Visual Studio 2010 install
    xcopy *.zip %USERPROFILE%"\Documents\Visual Studio 2010\Templates\"  /S /Q 
) else (
    echo Visual Studio 2010 not found
)

if exist  %USERPROFILE%"\Documents\Visual Studio 2012\Templates\" (
    echo Visual Studio 2012 install
    xcopy *.zip %USERPROFILE%"\Documents\Visual Studio 2012\Templates\"  /S /Q 
) else (
    echo Visual Studio 2012 not found
)

if exist  %USERPROFILE%"\Documents\Visual Studio 2013\Templates\" (
    echo Visual Studio 2013 install
    xcopy *.zip %USERPROFILE%"\Documents\Visual Studio 2013\Templates\"  /S /Q 
) else (
    echo Visual Studio 2013 not found
)

if exist  %USERPROFILE%"\Documents\Visual Studio 2015\Templates\" (
    echo Visual Studio 2015 install
    xcopy *.zip %USERPROFILE%"\Documents\Visual Studio 2015\Templates\"  /S /Q 
) else (
    echo Visual Studio 2015 not found
)

if exist  %USERPROFILE%"\Documents\Visual Studio 2017\Templates\" (
    echo Visual Studio 2017 install
    xcopy *.zip %USERPROFILE%"\Documents\Visual Studio 2017\Templates\"  /S /Q 
) else (
    echo Visual Studio 2017 not found
)