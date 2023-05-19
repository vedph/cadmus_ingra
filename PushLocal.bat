@echo off
echo PRESS ANY KEY TO INSTALL Cadmus Libraries TO LOCAL NUGET FEED
echo Remember to generate the up-to-date package.
c:\exe\nuget add .\Cadmus.Ingra.Parts\bin\Debug\Cadmus.Ingra.Parts.3.0.2.nupkg -source C:\Projects\_NuGet
c:\exe\nuget add .\Cadmus.Seed.Ingra.Parts\bin\Debug\Cadmus.Seed.Ingra.Parts.3.0.2.nupkg -source C:\Projects\_NuGet
c:\exe\nuget add .\Cadmus.Ingra.Services\bin\Debug\Cadmus.Ingra.Services.3.0.2.nupkg -source C:\Projects\_NuGet
pause
