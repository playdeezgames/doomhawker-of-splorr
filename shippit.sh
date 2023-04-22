dotnet publish ./src/Doomhawker/Doomhawker.vbproj -o ./pub-linux -c Release --sc -r linux-x64
dotnet publish ./src/Doomhawker/Doomhawker.vbproj -o ./pub-windows -c Release --sc -r win-x64
dotnet publish ./src/Doomhawker/Doomhawker.vbproj -o ./pub-mac -c Release --sc -r osx-x64
butler push pub-windows thegrumpygamedev/doomhawker:windows
butler push pub-linux thegrumpygamedev/doomhawker:linux
butler push pub-mac thegrumpygamedev/doomhawker:mac
git add -A
git commit -m "shipped it!"