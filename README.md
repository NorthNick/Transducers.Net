# Transducers.Net
Transducers.Net is a [Transducers](http://clojure.org/transducers) library for .NET, written in C#.
It is currently an experiment for my own benefit to help me understand them, but it is a fully functional
library, and the performance is about the same as the equivalent LINQ code; slightly faster in some
cases and slightly slower in others.

Documentation is yet to come, so take a look at the Tests project to see some transducers in use.

If you are interested in transducers for .NET, you might also like to look at Bryan Murphy's
[transducers-dotnet](https://github.com/bmurphy1976/transducers-dotnet) library,
which takes a slightly different approach, and has the advantage of some documentation. For more on
transducers in general see the [Clojure documentation](http://clojure.org/transducers), or [Rich
Hickey's video](https://www.youtube.com/watch?v=6mTbuzafcII) explaining them.

The main library is written in C#5 and should work in VS2013 and above. The Tests use the new
C# "using static" statement, so only work with VS2015 and above.