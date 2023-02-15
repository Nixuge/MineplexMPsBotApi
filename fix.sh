#!/bin/sh

sed -ne '/MCCScript/,$ p' test.cs > owo.cs
sed -i 's/\/\/ MCC.LoadBot/MCC.LoadBot/g' owo.cs

cp owo.cs ../a4y/