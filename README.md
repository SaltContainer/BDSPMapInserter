# BDSP Map Inserter

WIP Proof of Concept Map Inserter for BDSP. This is very much a proof of concept and is only meant to add empty assets in every necessary file for you to then edit with other tools.

## Features
- Selectable AreaID
- Adds entries in MapInfo for the new map by cloning the data for an existing map
- Adds new Attribute and AttributeEx files to the gamesettings bundle
- Adds a new area name in the Message files for ALL languages (All the same text for now)
  - x_dp_fld_areaname
  - x_dp_fld_areaname_display
  - x_dp_fld_areaname_indirect
  - x_dp_fld_areaname_townmap
- Adds new MapWarp, PlaceData, and StopData files for the new area to the Dpr/masterdatas bundle, if applicable
- Adds two new EvScript files in the ev_script bundle with dummied out scripts, ready to be edited
  - Uses the chosen "area code" for the name of the files (ex: c01 and sp_c01)
  - The first file is regular scripts, and the second is map scripts

## Things you still need to do after
- Adjust your exefs patches with your chosen "area code"
- Edit these to your liking:
  - MapInfo data
  - Attribute and AttributeEx data
  - MapWarps, PlaceData, and StopData
  - Scripts
- Add new map asset bundles if necessary
- If the map is in the Sinnoh matrix, adjusting that matrix to use the new created Attribute and AttributeEx files
- Any other misc. changes
