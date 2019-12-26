# CSGO-Settings-Unifier

<img src="https://i.imgur.com/YFFg1dJ.png" alt="drawing" width="200" heigth="200"/>

Program to unify CSGO settings across multiple accounts

## How to use
1. Run CSGO-Settings-Unifier once - It automatically creates a settings folder with a json-config file
2. Go to `./Exe-file/Config` and open Settings.json
3. Ensure `CSGOPath` is specified to your userdata folder (Should be automatically set based on the Steam Registry key)
4. Specify whether Video- or Config-settings should be changed by changing the `false` value to `true`
5. Inside the ./Config folder, put the files you want to replace the settings with, these are supported (Usually used files):	
	* Any \*.txt files (video.txt and videodefaults.txt)
	* Any \*.cfg files (config.cfg and autoexec.cfg)
	
