#!/bin/sh

sed -ne '/MCCScript/,$ p' MineplexBot.cs > ./client/scripts/MineplexBot.cs
sed -i 's/\/\/ MCC.LoadBot/MCC.LoadBot/g' ./client/scripts/MineplexBot.cs