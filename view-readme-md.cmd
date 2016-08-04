@ECHO OFF
python -m pip install grip --upgrade pip
call start iexplore http://localhost:6419/
python -m grip

