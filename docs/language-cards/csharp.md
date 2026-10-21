# C# card: xUnit

Practice app language for the course. Block 1 content; later blocks extend this card.

## Setup

```
dotnet new xunit -n MyProject.Tests -o tests/MyProject.Tests
dotnet sln add tests/MyProject.Tests/MyProject.Tests.csproj
dotnet add tests/MyProject.Tests/MyProject.Tests.csproj reference src/MyProject/MyProject.csproj
dotnet test
```

Needs the .NET 10 SDK (LTS). VS Code: C# Dev Kit, Testing view (beaker icon).

## Anatomy

```csharp
using Xunit;

public class UnitConverterTests
{
    [Fact]
    public void ToTonnes_ReturnsSameValue_WhenUnitIsTonnes()
    {
        var result = UnitConverter.ToTonnes(100.0, PayloadUnit.Tonnes);
        Assert.Equal(100.0, result);
    }

    [Theory]
    [InlineData(1.0, 0.90718474)]
    [InlineData(100.0, 90.718474)]
    public void ToTonnes_ConvertsShortTons(double shortTons, double expected)
    {
        Assert.Equal(expected, UnitConverter.ToTonnes(shortTons, PayloadUnit.ShortTons), precision: 6);
    }

    [Fact]
    public void ToTonnes_Throws_WhenNegative()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => UnitConverter.ToTonnes(-1.0, PayloadUnit.Tonnes));
    }
}
```

## The loop

Red: `dotnet test` fails on an assertion. Green: least code. Refactor with tests green. Commit red before green.

Run one test: `dotnet test --filter "FullyQualifiedName~ToTonnes"`.
