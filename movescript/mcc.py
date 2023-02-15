#!/bin/python

import os
import re
import pyperclip
import shutil
import time

#only turn on if in nano games
NANO_GAMES = True
MOVE_ZIP_AUTOMATICALLY = True

# TO CHANGE WHEN SWITCHING GAME TO SAVE
MAIN_GAME = "Nano Games"
GAME_PATH = f"/home/nix/Mineplex Backup/{MAIN_GAME}/"

SAVES = "/home/nix/.local/share/multimc/instances/1.8.9 pvp - worlddl/.minecraft/saves/"
LATEST = "/home/nix/.local/share/multimc/instances/1.8.9 pvp - worlddl/.minecraft/logs/latest.log"


def grab_map_creator(file_str: str) -> tuple[str]:
    REGEX = r"\[Client thread\/INFO]: \[CHAT\] Map - (.*) created by (.*)"
    matches = re.finditer(REGEX, file_str, re.MULTILINE)

    for _, match in enumerate(matches, start=1):
        map = match.groups()[0]
        creator = match.groups()[1]
    
    return map, creator

def grab_game(file_str: str) -> str:
    REGEX = r"\[Client thread\/INFO]: \[CHAT\] Game - (.*)"
    matches = re.finditer(REGEX, file_str, re.MULTILINE)

    for _, match in enumerate(matches, start=1):
        game = match.groups()[0]
    
    return game


def process_zips() -> str | None:
    # Rename ZIP file to map name
    zips: list[str] = []
    for file in os.listdir(SAVES):
        # Remove folders
        if file in ["eu_mineplex_com", "us_mineplex_com", "clans_mineplex_com"]:
            os.system(f"rm -r \"{SAVES}{file}\"") # dangeroous
        
        # Add all zips to list
        if file.split('.')[-1] == "zip":
            zips.append(file)

    if len(zips) > 1:
        print("Renaming first zip only.")
    if len(zips) > 0:
        zipname = SAVES + map + ".zip"
        os.rename(SAVES + zips[0], zipname)
        return zipname
    else:
        print("No files found")


def print_info(map: str, creator: str, game=None) -> None:
    print("====================")
    if game:
        print(f"Game: {game}")
    print(f"Map name: {map}")
    print(f"Creator: {creator}")
    print("====================")


def move_zip(zipname: str, map: str, game: str) -> bool:
    # adapt path if nano game
    newdir = GAME_PATH
    if game:
        newdir += game + '/'
    
    if not os.path.isdir(newdir):
        print("Made directory !")
        os.makedirs(newdir)

    try:
        # check if file already present
        for elem in os.listdir(newdir):
            if elem == map + ".zip":
                print("Map already downloaded")
                # if already is, move to the "Newer" dict (+add game & nano game name in filename)
                copy_path = f"{SAVES}Duplicates/{MAIN_GAME} - "
                if NANO_GAMES: copy_path += f"{game} - "
                copy_path += f"{map} - {int(time.time())}.zip"
                

                shutil.copyfile(SAVES + map + ".zip", copy_path)
                os.remove(SAVES + map + ".zip")
                return False
    
        # if not copy it
        shutil.copyfile(zipname, newdir + map + ".zip")
        print("File moved successfully")
        # then delete it
        os.remove(zipname)
    
    except Exception as e:
        print("Exception happened when moving/checking for file")
        print(e)
        return False

    return True


def add_to_txt(game, map, creator):
    with open("owo.csv", "a") as file:
        if game:
            file.write(f"{game}\t{map}\t{creator}\t\n")
        else:
            file.write(f"{map}\t{creator}\t\n")



if __name__ == "__main__":
    # Read whole file lmao
    with open(LATEST, 'r') as openFile:
        file_str = openFile.read()

    # Prints 
    map, creator = grab_map_creator(file_str)
    game = None
    if NANO_GAMES:
        game = grab_game(file_str)
    print_info(map, creator, game=game)

    # Process the files
    zipname = process_zips()

    # Copy author name to clipboard
    pyperclip.copy(creator)

    # Move the zip
    move_success = False
    if zipname:
        move_success = move_zip(zipname, map, game)
    
    if move_success:
        add_to_txt(game, map, creator)
