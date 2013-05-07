REM @ECHO OFF
perl "%~dp0tls\Markdown.pl" "%~dp0README.md"  --html > "%~dp0bld\README.html"
start iexplore "%~dp0bld\README.html"

