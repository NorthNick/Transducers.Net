# Transducers.Net
Transducers.Net is a [Transducers](http://clojure.org/transducers) library for .NET, written in C#.

[![Build Status](https://ci.appveyor.com/api/projects/status/github/NorthNick/Transducers.Net?branch=master&svg=true)](https://ci.appveyor.com/project/NorthNick/transducers-net)

Transducers.Net is currently an experiment for my own benefit to help me understand them, but it is a fully functional
library, with decent performance. Initial timings suggest that it is faster than the equivalent LINQ code for lists
up to 100,000 elements, but there has been no thorough performance testing.

If you are interested in transducers for .NET, you might also like to look at Bryan Murphy's
[transducers-dotnet](https://github.com/bmurphy1976/transducers-dotnet) library,
to see an alternative implementation. For more on
transducers in general see the [Clojure documentation](http://clojure.org/transducers), or [Rich
Hickey's video](https://www.youtube.com/watch?v=6mTbuzafcII) explaining them.

##Transducers
A *transducer* is a transformation applied to a sequence of items. They are like the Select/While/Take etc methods
in LINQ, but an transducer is independent of the sequence type, so the same transducer can be applied an IEnumerable
and an IObservable. If you know LINQ, then you know transducers.

##Transducers in action
The standard LINQ methods each have corresponding transducers (note that the library is a work in progress, so
currently only a few methods have transducers). For example the statements:
```C#
var transducer1 = Where<int>(x => x % 2 == 0);
var transducer2 = Select<int, int>(x => x * 2);
var transducer3 = Take<string>(10);
```
declare transducers that respectively keep only even numbers from a sequence of integers, multiply the elements
of a sequence of integers by two, and take the first ten elements of a sequence of strings.

Transducers can be composed using the Then method:
```C#
var transducer = Select<int, int>(x => x + 1).Then(Where<int>(x => x % 2 == 0).Then(Take<int>(3)));
```
declares a transducer that adds 1 to each of a sequences, then removes the non-even values, and
then takes the first 3 elements.

Transducers are applied to a sequence in either of two ways. Using the transducer above:
```C#
var input = Enumerable.Range(1, 15);
var output = input.Transduce(transducer);
```
gives an IEnumerable<int> output with the values {2, 4, 6}. Declaring input as Observable.Range(1, 15) gives an
IObservable output with the same values.

Alternatively:
```C#
var input = Enumerable.Range(1, 15);
var output = input.Transduce(transducer (x, y) => x + y, 0);
```
behaves like LINQ Aggregate by summing the values to give 12. When the input is an IObservable,
the output is also an IObservable, of length one, where the single element is the result.

##Source code
The main library is written in C#5 and should work in VS2013 and above. The Tests (and examples above) use the new
C#6 "using static" statement, so only work with VS2015 and above. In C#5, you need to qualify the transducer methods
e.g. Transducers.Select.

The library consists of two main projects: Transducers.Net and Transducers.Net.Reactive. The latter contains only the
implementations of the Transduce method for IObservables, so is not needed if you are not using the Reactive
Extensions. The separate projects saves pulling in the Reactive Extensions NuGet packages if they are not needed.

There is also a Tests project, containing the NUnit tests. These are executed as part of the
[Appveyor CI build](https://ci.appveyor.com/project/NorthNick/transducers-net), whose status can be seen above.