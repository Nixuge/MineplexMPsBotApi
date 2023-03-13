#!/bin/python3

import shutil
import os
import json


def grab_data() -> dict | None:
    if not os.path.isfile("install_config.json"):
        print("install_config.json file missing, please add it. You can refeer to the install_config_example.json file.")
        return

    with open("install_config.json") as f:
        try:
            data: dict = json.load(f)
        except:
            print("install_config.json file isn't a valid json file.")
            return
        if type(data) != dict:
            print("install_config.json file must be a dictionary.")
            return
        if type(data.get("server_name")) != str:
            print("key \"server_name\" in install_config.json must be a string.")
            return
        if type(data.get("pathes")) != list:
            print("key \"pathes\" in install_config.json must be a list.")
            return

        return data


class FileLoader():
    pathes = list[str]
    mccini: str
    mpbotcs: str

    def __init__(self, data: dict) -> None:
        self.pathes = data["pathes"]

        server_name: str = data["server_name"]

        with open("files/config/MinecraftClient.ini") as f:
            self.mccini = f.read() \
                .replace("<SERVERNAME>", server_name)

        with open("files/MineplexBot.cs") as f:
            self.mpbotcs = f.read() \
                .split("//<SPLITHERE>\n")[1] \
                .replace("<SERVERNAME>", server_name) \
                .replace("//<LOADBOT>", "MCC.LoadBot(new MineplexBot());")

    def save_to_path(self, path: str):
        # Path check
        if path[-1] != '/':
            path += '/'

        if not os.path.isdir(path):
            print(f"Path \"{path}\" isn't a folder")
            return

        # Mkdir folders
        for key in [".data", "CSVs", "scripts", "scripts/libs"]:
            if not os.path.isdir(path + key):
                os.mkdir(path + key)

        # Write important files
        with open(path + "MinecraftClient.ini", "w") as f:
            f.write(self.mccini)

        with open(path + "scripts/MineplexBot.cs", "w") as f:
            f.write(self.mpbotcs)

        # Copy untouched files
        shutil.copy("files/start.sh", path + "start.sh")
        shutil.copy("files/mover.py", path + "mover.py")
        shutil.copy("files/libs/Newtonsoft.Json.dll",
                    path + "scripts/libs/Newtonsoft.Json.dll")

    def save_to_every_path(self):
        for path in self.pathes:
            self.save_to_path(path)


def main():
    data = grab_data()
    if not data:
        return
    floader = FileLoader(data)
    floader.save_to_every_path()


if __name__ == "__main__":
    main()
