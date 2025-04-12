<?xml version="1.0" encoding="UTF-8"?>
<tileset version="1.2" tiledversion="1.2.3" name="TileDungeon" tilewidth="16" tileheight="16" tilecount="15" columns="3">
 <image source="../SourceTile/Dungeon16.png" width="48" height="80"/>
 <terraintypes>
  <terrain name="Подземелье" tile="4"/>
 </terraintypes>
 <tile id="0" terrain=",,,0">
  <objectgroup draworder="index">
   <object id="1" x="0" y="0" width="16" height="16"/>
  </objectgroup>
 </tile>
 <tile id="1">
  <objectgroup draworder="index">
   <object id="1" x="0" y="0" width="16" height="16"/>
  </objectgroup>
 </tile>
 <tile id="2">
  <objectgroup draworder="index">
   <object id="1" x="0" y="0" width="16" height="16"/>
  </objectgroup>
 </tile>
 <tile id="3" terrain=",,0,0">
  <objectgroup draworder="index">
   <object id="1" x="0" y="0" width="16" height="16"/>
  </objectgroup>
 </tile>
 <tile id="4">
  <objectgroup draworder="index">
   <object id="3" x="0" y="0" width="16" height="16"/>
  </objectgroup>
 </tile>
 <tile id="5">
  <objectgroup draworder="index">
   <object id="1" x="0" y="0" width="16" height="16"/>
  </objectgroup>
 </tile>
 <tile id="6" terrain=",,0,">
  <objectgroup draworder="index">
   <object id="1" x="0" y="0" width="16" height="16"/>
  </objectgroup>
 </tile>
 <tile id="7">
  <objectgroup draworder="index">
   <object id="1" x="0" y="0" width="16" height="16"/>
  </objectgroup>
 </tile>
 <tile id="8">
  <objectgroup draworder="index">
   <object id="1" x="0" y="0" width="16" height="16"/>
  </objectgroup>
 </tile>
 <tile id="9" terrain=",0,,0">
  <objectgroup draworder="index">
   <object id="1" x="0" y="0" width="16" height="16"/>
  </objectgroup>
  <animation>
   <frame tileid="9" duration="100"/>
   <frame tileid="10" duration="100"/>
  </animation>
 </tile>
 <tile id="10">
  <objectgroup draworder="index">
   <object id="3" x="0" y="0" width="16" height="16"/>
  </objectgroup>
 </tile>
 <tile id="11">
  <objectgroup draworder="index">
   <object id="1" x="0" y="0" width="16" height="16"/>
  </objectgroup>
 </tile>
 <tile id="12" terrain="0,0,0,0"/>
</tileset>
