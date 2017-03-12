# ABCNet  [![Build Status](https://travis-ci.org/danielsonchris/ABCNet.svg?branch=master)](https://travis-ci.org/danielsonchris/ABCNet)
An extensible Artificial Bee Colony .Net Library.  Initial usage was for a distributed clustering solution.  We effectively required this algorithm for spreading processes across a multitude of servers that may or may not have been available.

A parent process dynamically acted as the hive entity and from that vantage point healthy foraging resources were selected for processing long running scheduled items or event driven tasks.

# Example Usage
```csharp
using ABCNet;
Colony colony = new Colony(10, ...);
colony.Run();
```

# Troubleshooting Notes
* If using dotnet core, make sure to apply a "dotnet restore" in order to reload the nuget dependencies.

