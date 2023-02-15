#!/bin/sh

sed -ne '/MCCScript/,$ p' test.cs > owo.cs
sed -i 's/\/\/ MCC.LoadBot(new ExampleChatBot());/MCC.LoadBot(new ExampleChatBot());/g' owo.cs

cp owo.cs ../a4y/