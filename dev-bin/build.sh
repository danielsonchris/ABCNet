#!/usr/bin/env bash

cd `dirname $0`/..

if [ -n "$DOTNETCORE" ]; then

  echo Using .NET CLI

  dotnet restore

  # Running Example Test
  dotnet run -f netcoreapp1.1 -c $CONFIGURATION --project ./ExampleConsole/ExampleConsole.csproj

  # Running Unit Tests
  #dotnet test -f netcoreapp1.0 -c $CONFIGURATION ./ABCNet.Test

else

  echo Using Mono

  cd mono

  nuget restore

  xbuild /p:Configuration=$CONFIGURATION

  mono ../mono/packages/NUnit.ConsoleRunner.3.4.1/tools/nunit3-console.exe --where "cat != BreaksMono" ./bin/$CONFIGURATION/ABCNet.Test.dll

fi
