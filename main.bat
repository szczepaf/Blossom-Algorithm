@echo off
title Blossom Algorithm
echo Blossom Algorithm, Frantisek Szczepanik, MFF UK 2022
IF EXIST output.txt DEL /F output.txt
start ./bin/Debug/net6.0/BlossomAlgorithm
pause
echo "Computed the matching, press a key to see the results."


pause
python BlossomResults/BlossomResults.py



