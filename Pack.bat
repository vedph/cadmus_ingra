@echo off
echo BUILD Cadmus Ingra Packages
del .\Cadmus.Ingra.Parts\bin\Debug\*.snupkg
del .\Cadmus.Ingra.Parts\bin\Debug\*.nupkg

del .\Cadmus.Ingra.Services\bin\Debug\*.snupkg
del .\Cadmus.Ingra.Services\bin\Debug\*.nupkg

del .\Cadmus.Seed.Ingra.Parts\bin\Debug\*.snupkg
del .\Cadmus.Seed.Ingra.Parts\bin\Debug\*.nupkg

cd .\Cadmus.Ingra.Parts
dotnet pack -c Debug -p:IncludeSymbols=true -p:SymbolPackageFormat=snupkg
cd..

cd .\Cadmus.Ingra.Services
dotnet pack -c Debug -p:IncludeSymbols=true -p:SymbolPackageFormat=snupkg
cd..

cd .\Cadmus.Seed.Ingra.Parts
dotnet pack -c Debug -p:IncludeSymbols=true -p:SymbolPackageFormat=snupkg
cd..

pause
