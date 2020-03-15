@Echo Off
REM 
REM Using ImageMagic convert.exe to convert png source file to 256, 128, 64, 32, 16 sizes and then convert the result into an icon file
REM
Set SourcePngBaseFileName=NCmdLiner
convert %SourcePngBaseFileName%.png -resize 256x256   %SourcePngBaseFileName%-256.png
convert %SourcePngBaseFileName%-256.png -resize 16x16   %SourcePngBaseFileName%-16.png
convert %SourcePngBaseFileName%-256.png -resize 32x32   %SourcePngBaseFileName%-32.png
convert %SourcePngBaseFileName%-256.png -resize 64x64   %SourcePngBaseFileName%-64.png
convert %SourcePngBaseFileName%-256.png -resize 128x128 %SourcePngBaseFileName%-128.png
convert %SourcePngBaseFileName%-16.png %SourcePngBaseFileName%-32.png %SourcePngBaseFileName%-64.png %SourcePngBaseFileName%-128.png %SourcePngBaseFileName%-256.png %SourcePngBaseFileName%.ico
