#!/bin/sh

sed -ne '/MCCScript/,$ p' script.cs > MineplexBot.cs
sed -i 's/\/\/ MCC.LoadBot/MCC.LoadBot/g' MineplexBot.cs

# cp MineplexBot.cs ../a4y/