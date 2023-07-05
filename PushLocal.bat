@echo off
echo PRESS ANY KEY TO INSTALL Cadmus Libraries TO LOCAL NUGET FEED
echo Remember to generate the up-to-date package.
c:\exe\nuget add .\Cadmus.Ingra.Parts\bin\Debug\Cadmus.Ingra.Parts.4.0.0.nupkg -source C:\Projects\_NuGet
c:\exe\nuget add .\Cadmus.Seed.Ingra.Parts\bin\Debug\Cadmus.Seed.Ingra.Parts.4.0.0.nupkg -source C:\Projects\_NuGet
c:\exe\nuget add .\Cadmus.Ingra.Services\bin\Debug\Cadmus.Ingra.Services.4.0.0.nupkg -source C:\Projects\_NuGet
pause
