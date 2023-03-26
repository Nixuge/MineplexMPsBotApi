#!/bin/python

# TODO:
# Replicate features of mccOriginal
# but strip the unneccessary
# by using provided info by MineplexBot

from dataclasses import dataclass
import os
from pprint import pprint
import pyperclip
import shutil
import time

# only turn on if in nano games
MOVE_ZIP_AUTOMATICALLY = True

GAME_PATH = f"/home/nix/Mineplex Backup/"

SAVES = "/home/nix/.local/share/multimc/instances/1.8.9 pvp - worlddl/.minecraft/saves/"
BACKUPS = "/home/nix/.local/share/multimc/instances/1.8.9 pvp - worlddl/.minecraft/backups/"

class bcolors:
    HEADER = '\033[95m'
    OKBLUE = '\033[94m'
    OKCYAN = '\033[96m'
    OKGREEN = '\033[92m'
    WARNING = '\033[93m'
    FAIL = '\033[91m'
    ENDC = '\033[0m'
    BOLD = '\033[1m'
    UNDERLINE = '\033[4m'


GAME_NAME_FIXES = {
    "Cake Wars Solo": "Cake Wars Solo & Duos",
    "Cake Wars Duos": "Cake Wars Solo & Duos"
}

class Map:
    nano: bool
    game: str
    name: str
    builder: str

    folderPath: str
    fullFolderPath: str
    filePath: str
    fullFilePath: str

    def __init__(self, infoFilePath: str):
        with open(infoFilePath, "r") as openFile:
            infos = openFile.read().split("\t")

            if len(infos) == 4:
                self.nano = False
            elif len(infos) == 5:
                self.nano = True
                infos.pop(0)
            else:
                print("Nasty error")
                1 / 0  # I never remember how to throw errors in py so just going w that

            self.game = infos[0]
            self.name = infos[1]
            self.builder = infos[2]
        
        self.folderPath = self._getFolderPath()
        self.fullFolderPath = self._getFullFolderPath()
        self.filePath = self._getFilePath()
        self.fullFilePath = self._getFullFilePath()
    
    def _getFolderPath(self) -> str:
        path = ""
        if self.nano:
            path += "Nano Games/"
        if self.game in GAME_NAME_FIXES:
            self.game = GAME_NAME_FIXES[self.game]
        path += self.game + "/"
        return path

    def _getFullFolderPath(self) -> str:
        return GAME_PATH + self.folderPath

    def _getFilePath(self) -> str:
        return self.folderPath + self.name + ".zip"

    def _getFullFilePath(self) -> str:
        return GAME_PATH + self.filePath


def process_zips_forge(map: Map) -> str | None:
    for file in os.listdir(SAVES):
        os.system(f"rm -r \"{SAVES}{file}\"")  # dangeroous
    
    zips = os.listdir(BACKUPS)

    if len(zips) > 1:
        print(bcolors.WARNING + "You've got more than 1 zip in the backup folder. Using the latest one." + bcolors.ENDC)
    elif len(zips) == 0:
        print(bcolors.FAIL + "No zips found to process." + bcolors.ENDC)
        return

    zip = sorted(zips)[-1]
    zipname = BACKUPS + map.name + ".zip"
    os.rename(BACKUPS + zip, zipname)
    
    return zipname

def process_zips_1_8_9a_liteloader(map: Map) -> str | None:
    # Will probably be deleted soon, as the Forge mod has everything you need

    # Rename ZIP file to map name
    zips: list[str] = []
    toRemove: list[str] = []
    for file in os.listdir(SAVES):
        # Remove folders
        if "mineplex_com" in file and not ".zip" in file:
            toRemove.append(file)

        # Add all zips to list
        if file.split('.')[-1] == "zip":
            zips.append(file)

    if len(zips) > 1:
        print("Renaming first zip only.")
    
    if len(zips) > 0:
        for file in toRemove:
            os.system(f"rm -r \"{SAVES}{file}\"")  # dangeroous
        
        zipname = SAVES + map.name + ".zip"
        os.rename(SAVES + zips[0], zipname)
        return zipname

    elif len(toRemove) > 0:
        print(bcolors.FAIL + "No zip to move but a folder found, perhaps try saving the map" + bcolors.ENDC)


def print_info(map: Map) -> None:
    print("====================")
    if map.nano:
        print(f"Game: Nano Games")
        print(f"Minigame: {map.game}")
    else:
        print(f"Game: {map.game}")
    print(f"Map name: {map.name}")
    print(f"Creator: {map.builder}")
    print("====================")


def move_zip(zipname: str, map: Map) -> bool:
    # adapt path if nano game

    if not os.path.isdir(map.fullFolderPath):
        print("Made directory !")
        os.makedirs(map.fullFolderPath)

    try:
        # check if file already present
        for elem in os.listdir(map.fullFolderPath):
            if elem == map.name + ".zip":
                print("Map already downloaded")
                # if already is, move to the "Newer" dict (+add game & nano game name in filename)
                # edit: as i got access to Mps again, this is deprecated
                # copy_path = f"{SAVES}Duplicates/{MAIN_GAME} - "
                # if NANO_GAMES:
                #     copy_path += f"{game} - "
                # copy_path += f"{map} - {int(time.time())}.zip"

                # shutil.copyfile(SAVES + map + ".zip", copy_path)
                # os.remove(SAVES + map + ".zip")
                return False

        # if not copy it
        shutil.copyfile(zipname, map.fullFilePath)
        # print("File moved successfully")
        # then delete it
        os.remove(zipname)

    except Exception as e:
        print("Exception happened when moving/checking for file")
        print(e)
        return False

    return True


if __name__ == "__main__":
    map = Map(".data/info.txt")

    # print_info(map)

    # Process the files
    zipname = process_zips_forge(map)

    # Move the zip
    move_success = False
    if zipname:
        move_success = move_zip(zipname, map)

    if move_success:
        print(f"--> {map.game}\n--> {bcolors.UNDERLINE}{map.name}{bcolors.ENDC}\n{bcolors.OKGREEN}> Move success: True{bcolors.ENDC}")
    else:
        if zipname:
            print(f"--> {map.game}\n--> {bcolors.UNDERLINE}{map.name}{bcolors.ENDC}\n{bcolors.FAIL}> Move success: False{bcolors.ENDC}")
        else:
            print(f"--> {map.game}\n--> {bcolors.UNDERLINE}{map.name}{bcolors.ENDC}\n{bcolors.WARNING}> No files found{bcolors.ENDC}")

