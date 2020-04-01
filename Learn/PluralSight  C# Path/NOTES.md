# .NET Fundamentals

## Intro to .NET

There are two flavours of .NET

    1. Framework (e.g. .NET 4.8) 
        * traditional one, runs on windows only

    2. Core (e.g. .NET Core 3.0) 
        * open source version, multi-platform

In general .NET is composed of the following

    1. CLR --> Common Language Runtime  
        -- Runtime for all .NET languages

    2. FCL -- Framework Class Library  
        -- Common Libraries including networking, cryptography etc.

Software Development Kit (SDK) - contains all the components needed to build apps

Command-Line Interface (CLI) basic commands

    -- dotnet --info
    -- dotnet --help
    
    -- dotnet new
    -- dotnet run
    -- dotnet run --project src/ProjectName

donet run:

- dotnet restore --> nuget restore
- dotnet build   --> compile into DLLs (Dynamic Link Library) a.k.a. assembly

Unit Testing Fundamentals

```csharp
using System;
using Xunit;

namespace GradeBook.Tests
{
    public class BookTests
    {
        [Fact]
        public class Test1
        {
            // arrange
            var x = 5;
            var y = 2;
            var expected = 7;

            // act
            var actual = x+y;

            // assert
            Assert.Equal(expected, actual);
        }
    }
}
```

## C# Language Basics

```csharp
// String Interpolation vs String Concatenation

var interpolation = $"hello {args[0]}!";
var concatenation = "hello " + args[0]" + "!"
```

to pass args[] into a program use

    -- dotnet run -- param1, param2

implicit typing vs explicit typing
```csharp
var param1 = args[o];
int param1 = int.TryParse(args[0]);
```

CTRL + .  is used to bring context menu

### Access Modifiers

```csharp
public    // code outside of this class will have access to that field; Name
private   // default, available to code withing the class definition; name
protected // accessible withing the same namespace
static    // not associated with an instance but the type
```

### Reference vs Value Type

```csharp
var b = new Book("Grades"); // reference type, b is an address to the value
var x = 3; // value type, as x holds actual value not a pointer to the value

Object.ReferenceEquals(ins1, ins2); // check if the same instance

// WHEN PASSING PARAMETERS INTO METHODS IT'S ALWAYS BY VALUE UNLESS REF KEYWORD IS USED
public void PassByValue(int x) {}
public void PassByRef(ref int x){}

// OUT UNLIKE REF ASSUMES THE  VARIABLE IS ALREADY INITIALIZED
public void PassOut(out int x){}
```

## OOP Basics

    Encapsulation -- hide implementation details/ complexity

    Polymorphism -- many forms

    Abstraction -- generic rather than concrete

```csharp
namespace GradeBook
{
    public class Book
    {
        int Id {get;set;}
        double Grade {get;set;}
        private List<double> Grades = new List<double>();
    }

    public AddGrade double(grade)
    {
        Grades += grade;  // this. is optional
    }
}

```
