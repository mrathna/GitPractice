@echo off

echo "Checking for Chrome Driver exe and Killing it if its present"
tasklist /fi "imagename eq chromedriver.exe" |find ":" > nul
if errorlevel 1 taskkill /f /im "chromedriver.exe"

echo "Checking for Chrome exe and Killing it if its present"
tasklist /fi "imagename eq chrome.exe" |find ":" > nul
if errorlevel 1 taskkill /f /im "chrome.exe"

echo "Checking for Gecko Driver exe and Killing it if its present"
tasklist /fi "imagename eq geckodriver.exe" |find ":" > nul
if errorlevel 1 taskkill /f /im "geckodriver.exe"

echo "Checking for Firefox exe and Killing it if its present"
tasklist /fi "imagename eq firefox.exe" |find ":" > nul
if errorlevel 1 taskkill /f /im "firefox.exe"

echo "Checking for IE Driver Server exe and Killing it if its present"
tasklist /fi "imagename eq IEDriverServer.exe" |find ":" > nul
if errorlevel 1 taskkill /f /im "IEDriverServer.exe"

echo "Checking for Internet Explorer exe and Killing it if its present"
tasklist /fi "imagename eq iexplore.exe" |find ":" > nul
if errorlevel 1 taskkill /f /im "iexplore.exe"

echo "Checking for PhantomJs exe and Killing it if its present"
tasklist /fi "imagename eq phantomjs.exe" |find ":" > nul
if errorlevel 1 taskkill /f /im "phantomjs.exe"

echo "Checking for WinAppDriver exe and Killing it if its present"
tasklist /fi "imagename eq WinAppDriver.exe" |find ":" > nul
if errorlevel 1 taskkill /f /im "WinAppDriver.exe"


