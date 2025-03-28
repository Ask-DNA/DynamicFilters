## Tutorial

+ [Creation](#creation)
+ [Filter options declaration](#filter-options-declaration)
+ [Ignoring options](#ignoring-options)
+ [Usage](#usage)
+ [Composite filters](#composite-filters)
+ [Exceptions](#exceptions)

### Creation
To create a filter, you need to create a class that inherits from ```DynamicFilterBase<T>```, where ```T``` is the type of entity to be filtered:
```csharp
class User
{
    public string Name { get; set; } = "";
    public int Age { get; set; } = 0;
}

class UserFilter : DynamicFilterBase<User>
{
    //...
}
```

### Filter options declaration
A filter consists of options represented by its public fields and properties. To set an option, apply the ```FilterOptionAttribute``` to a class member:
```csharp
class UserFilter : DynamicFilterBase<User>
{
    [FilterOption(Option = FilterOptionType.Inequality, TargetName = nameof(User.Name))]
    public string ExcludeName { get; set; } = "John Doe";

    [FilterOption(Option = FilterOptionType.GreaterThanOrEqual, TargetName = nameof(User.Age))]
    public int MinAge { get; set; } = 18;
}
```
The above class will generate the following expression: ```(User u) => u.Name != "John Doe" && u.Age >= 18```

List of filter option types:
+ ```FilterOptionType.Equality```
+ ```FilterOptionType.Inequality```
+ ```FilterOptionType.GreaterThan```
+ ```FilterOptionType.GreaterThanOrEqual```
+ ```FilterOptionType.LessThan```
+ ```FilterOptionType.LessThanOrEqual```

If the filter option is ```FilterOptionType.Equality```, then it can be omitted.
It is also possible to omit the definition of the target member name if it is the same as the option name.
Thus, the option declaration
```csharp
[FilterOption(Option = FilterOptionType.Equality, TargetName = nameof(User.Age))]
public int Age { get; set; } = 18;
```
is similar to the following:
```csharp
[FilterOption]
public int Age { get; set; } = 18;
```

You can disable target automapping by using ```AllowTargetAutoMappingAttribute```:
```csharp
// Default settings, AllowTargetAutoMappingAttribute can be ommited
[AllowTargetAutoMapping(Allow = true)]
class UserFilter1 : DynamicFilterBase<User>
{
    // Valid, because automapping is enabled
    [FilterOption(Option = FilterOptionType.Equality)]
    public int Name { get; set; } = 18;
    
    // Valid, because target is specified
    [FilterOption(Option = FilterOptionType.Equality, TargetName = nameof(User.Age))]
    public int Age { get; set; } = 18;
}

[AllowTargetAutoMapping(Allow = false)]
class UserFilter2 : DynamicFilterBase<User>
{
    // Invalid, because automapping is disabled
    [FilterOption(Option = FilterOptionType.Equality)]
    public int Name { get; set; } = 18;
    
    // Valid, because target is specified
    [FilterOption(Option = FilterOptionType.Equality, TargetName = nameof(User.Age))]
    public int Age { get; set; } = 18;
}
```

### Ignoring options
An option can refer to an ignore flag – a public property or field of boolean type:
```csharp
class UserFilter : DynamicFilterBase<User>
{
    public bool IgnoreFlagForAgeFilterOption { get; set; } = false;

    [FilterOption(IgnoreFlagName = nameof(IgnoreFlagForAgeFilterOption))]
    public int Age { get; set; } = 18;
}
```
Ignore flag reference can be ommited if the flag name is ```"Ignore"``` + the option name:
```csharp
class UserFilter : DynamicFilterBase<User>
{
    public bool IgnoreAge { get; set; } = false;

    [FilterOption]
    public int Age { get; set; } = 18;
}
```
When ```IgnoreAge == false```, the above class will generate the following expression: ```(User u) => u.Age == 18```

When ```IgnoreAge == true```: ```(User u) => true```

It is also possible to disable ignore flag automapping using ```AllowIgnoreFlagAutoMappingAttribute```:
```csharp
// Default settings, AllowIgnoreFlagAutoMappingAttribute can be ommited
[AllowIgnoreFlagAutoMapping(Allow = true)]
class UserFilter1 : DynamicFilterBase<User>
{
    public bool IgnoreName { get; set; } = false;
    public bool IgnoreAge { get; set; } = false;
    
    // Ignore flag is specified
    [FilterOption(Option = FilterOptionType.Equality, IgnoreFlagName = nameof(IgnoreName))]
    public int Name { get; set; } = 18;
    
    // Ignore flag is also specified because of automapping possibilities
    [FilterOption(Option = FilterOptionType.Equality)]
    public int Age { get; set; } = 18;
}

[AllowIgnoreFlagAutoMapping(Allow = false)]
class UserFilter2 : DynamicFilterBase<User>
{
    public bool IgnoreName { get; set; } = false;
    public bool IgnoreAge { get; set; } = false;
    
    // Ignore flag is specified
    [FilterOption(Option = FilterOptionType.Equality, IgnoreFlagName = nameof(IgnoreName))]
    public int Name { get; set; } = 18;
    
    // Ignore flag is not specified because automapping is disabled
    [FilterOption(Option = FilterOptionType.Equality)]
    public int Age { get; set; } = 18;
}
```

### Usage
To generate a predicate, create an object and call the ```AsDelegate()``` or ```AsExpression()``` method:
```csharp
UserFilter userFilter = new() { MinAge = 18 };
Func<User, bool> predicate = userFilter.AsDelegate();
Expression<Func<User, bool>> expression = userFilter.AsExpression();
```
The code below demonstrates filter usage example:
```csharp
internal class Program
{
    class User
    {
        public string Name { get; set; } = "";
        public int Age { get; set; } = 0;
    }

    class UserFilter : DynamicFilterBase<User>
    {
        public bool IgnoreMinAge { get; set; } = false;

        [FilterOption(Option = FilterOptionType.GreaterThanOrEqual, TargetName = nameof(User.Age))]
        public int MinAge { get; set; } = 18;
    }

    static void Main(string[] args)
    {
        User[] users = [
            new User() { Name = "John", Age = 20 },
            new User() { Name = "Ann", Age = 18 },
            new User() { Name = "Robert", Age = 25 },
            new User() { Name = "Leonard", Age = 15 },
            new User() { Name = "Jane", Age = 14 },
            ];

        UserFilter userFilter = new() { MinAge = 18, IgnoreMinAge = false };
        PrintUserNames(users.Where(userFilter.AsDelegate()));
        // John Ann Robert

        userFilter.MinAge = 20;
        PrintUserNames(users.Where(userFilter.AsDelegate()));
        // John Robert

        userFilter.IgnoreMinAge = true;
        PrintUserNames(users.Where(userFilter.AsDelegate()));
        // John Ann Robert Leonard Jane
    }

    static void PrintUserNames(IEnumerable<User> users)
    {
        foreach (var user in users)
            Console.Write(user.Name + ' ');
        Console.WriteLine();
    }
}
```

### Composite filters

Logic operations are defined for ```DynamicFilterBase``` class and ```IDynamicFilter``` interface, which result is a composite filter that implements the same interface.

> [!IMPORTANT]
> When using composite filters, keep in mind that they refer to the options of the original filters. Changing the values ​​of these options affects the behavior of the composite filter.

The following example demonstrates the process of creating and using a composite filter:
```csharp
internal class Program
{
    class User
    {
        public string Name { get; set; } = "";

        public int Age { get; set; }
    }

    class MinAgeFilter : DynamicFilterBase<User>
    {
        [FilterOption(Option = FilterOptionType.GreaterThanOrEqual, TargetName = nameof(User.Age))]
        public int MinAge { get; set; }
    }

    class NameFilter : DynamicFilterBase<User>
    {
        [FilterOption(Option = FilterOptionType.Equality, TargetName = nameof(User.Name))]
        public string Name { get; set; } = "";
    }

    static void Main(string[] args)
    {
        User[] users = [
            new User() { Name = "John", Age = 20 },
            new User() { Name = "Ann", Age = 18 },
            new User() { Name = "Robert", Age = 25 },
            new User() { Name = "Leonard", Age = 15 },
            new User() { Name = "Jane", Age = 14 },
            ];

        IDynamicFilter<User> minAgeFilter = new MinAgeFilter() { MinAge = 18 };
        IDynamicFilter<User> nameFilter = new NameFilter() { Name = "John" };

        IDynamicFilter<User> compositeFilter = minAgeFilter & nameFilter;
        PrintUserNames(users.Where(compositeFilter.AsDelegate()));
        // John

        compositeFilter = minAgeFilter & !nameFilter;
        PrintUserNames(users.Where(compositeFilter.AsDelegate()));
        // Ann Robert

        compositeFilter = !minAgeFilter | nameFilter;
        PrintUserNames(users.Where(compositeFilter.AsDelegate()));
        // John Leonard Jane
    }

    static void PrintUserNames(IEnumerable<User> users)
    {
        foreach (var user in users)
            Console.Write(user.Name + ' ');
        Console.WriteLine();
    }
}
```

### Exceptions
Predicate generation with ```AsDelegate()``` and ```AsExpression()``` methods can throw ```InvalidFilterConfigurationException``` or ```InvalidInnerFilterConfigurationException``` in case of invalid filter configuration.

> [!TIP]
> To prevent throwing, check the ```Valid``` property.

> [!WARNING]
> Avoid throwing exceptions in ignore flag and filter option getters.
> When an exception occurs, it will be wrapped in a ```TargetInvocationException```, which cannot be caught with the default IDE settings.

+ ```InvalidFilterConfigurationException : AggregateException```

  Aggregates exceptions of type ```InvalidFilterOptionConfigurationException``` that occurred during filter configuration processing.

+ ```InvalidFilterOptionConfigurationException : InvalidOperationException```

  Basic exception type to describe errors that occurs at the concrete filter option level.

+ ```TargetNotFoundException : InvalidFilterOptionConfigurationException```

  Occurs when the specified target property or field is not found among public class members.

+ ```TargetNotSpecifiedException : InvalidFilterOptionConfigurationException```

  Occurs when target automapping is disabled with ```AllowTargetAutoMappingAttribute``` and filter option configuration does not contain explicitly specified target.

+ ```TypeMismatchException : InvalidFilterOptionConfigurationException```

  Occurs when the types of the target member and the filter option do not match.

+ ```IgnoreFlagNotFoundException : InvalidFilterOptionConfigurationException```

  Occurs when the specified ignore flag property or field is not found among public members of the filter.

+ ```OperatorIsRequiredException : InvalidFilterOptionConfigurationException```

  Occurs when using types that do not support the operators required by the selected filter option type.
  For example, for ```FilterOptionType.GreaterThanOrEqual```, the exception will be thrown when using a type that does not support the ```>=``` operator.

+ ```InvalidInnerFilterConfigurationException : InvalidOperationException```

  Occurs when using composite filters, which inner filters are invalid.
