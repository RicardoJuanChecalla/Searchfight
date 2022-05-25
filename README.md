dotnet new sln -n Searchfight
dotnet new console -f net6.0 -o Searchfight.Presentation
dotnet new classlib -f net6.0 -o Searchfight.Core
dotnet new classlib -f net6.0 -o Searchfight.Infrastructure
dotnet sln Searchfight.sln add Searchfight.Presentation\Searchfight.Presentation.csproj
dotnet sln Searchfight.sln add Searchfight.Core\Searchfight.Core.csproj
dotnet sln Searchfight.sln add Searchfight.Infrastructure\Searchfight.Infrastructure.csproj

dotnet add Searchfight.Presentation/Searchfight.Presentation.csproj reference Searchfight.Core\Searchfight.Core.csproj
dotnet add Searchfight.Presentation/Searchfight.Presentation.csproj reference Searchfight.Infrastructure\Searchfight.Infrastructure.csproj
dotnet add Searchfight.Infrastructure\Searchfight.Infrastructure.csproj reference Searchfight.Core\Searchfight.Core.csproj
dotnet add Searchfight.UnitTests/Searchfight.UnitTests.csproj reference Searchfight.Core\Searchfight.Core.csproj
dotnet add Searchfight.UnitTests/Searchfight.UnitTests.csproj reference Searchfight.Infrastructure\Searchfight.Infrastructure.csproj

dotnet new xunit -f net6.0 -o Searchfight.UnitTests
dotnet sln Searchfight.sln add Searchfight.UnitTests\Searchfight.UnitTests.csproj --solution-folder Tests

dotnet restore ./Searchfight.Core/Searchfight.Core.csproj
dotnet restore ./Searchfight.Infrastructure/Searchfight.Infrastructure.csproj
dotnet restore ./Searchfight.Presentation/Searchfight.Presentation.csproj

dotnet add ./Searchfight.UnitTests/Searchfight.UnitTests.csproj package xunit.runner.console --version 2.4.1
dotnet add ./Searchfight.Presentation/Searchfight.Presentation.csproj package Newtonsoft.Json --version 13.0.1 
dotnet add ./Searchfight.Infrastructure/Searchfight.Infrastructure.csproj package Newtonsoft.Json --version 13.0.1 
dotnet add ./Searchfight.Core/Searchfight.Core.csproj package Newtonsoft.Json --version 13.0.1 


dotnet run --project ./Searchfight.Presentation/Searchfight.Presentation.csproj
dotnet test ./Searchfight.UnitTests/Searchfight.UnitTests.csproj

https://rapidapi.com/apigeek/api/google-search3/
https://app.quicktype.io/
https://xunit.net/docs/comparisons

NUnit 3.x (Restricción)	xUnit.net 2.x
Is.EqualTo	Equal
Is.Not.EqualTo	NotEqual
Is.Not.SameAs	NotSame
Is.SameAs	Same
Does.Contain	Contains
Does.Not.Contain	DoesNotContain
Throws.Nothing	n / A
n / A	n / A
Is.GreaterThan	n / A
Is.InRange	InRange
Is.AssignableFrom	IsAssignableFrom
Is.Empty	Empty
Is.False	False
Is.InstanceOf<T>	IsType<T>
Is.NaN	n / A
Is.Not.AssignableFrom<T>	n / A
Is.Not.Empty	NotEmpty
Is.Not.InstanceOf<T>	IsNotType<T>
Is.Not.Null	NotNull
Is.Null	Null
Is.True	True
Is.LessThan	n / A
Is.Not.InRange	NotInRange
Throws.TypeOf<T>	Throws<T>