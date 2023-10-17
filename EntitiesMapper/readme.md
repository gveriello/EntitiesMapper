
# EntitiesMapper

Package that allows mapping between classes in a very simple way.





## Features

- Map classes
- Dependency injection
- Developed with .NET Core 6


## How to install

To install this nuget package:

```bash
  dotnet add package EntitiesMapper
```


## Usage/Examples

### 1 scenario: equals classes
```javascript
public class PersonEntity
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public int Age { get; set; }
    public int BirthdayYear { get; set; }
    public string Address { get; set; }
}

public class PersonDto
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public int Age { get; set; }
    public int BirthdayYear { get; set; }
    public string Address { get; set; }
}
```

you can do this:

```javascript
internal class Program
{
    static void Main(string[] args)
    {
        var entity = new PersonEntity() //source
        {
            Name = "John",
            Surname = "Doe",
            Age = 30,
            BirthdayYear = 1992,
            Address = "Via del Sole, 123"
        };
        var dto = new PersonDto(); //destination
        Mapper.CopyTo<PersonEntity, PersonDto>(entity, dto);
    }
}
```

### 3 scenario: destination class can be mapped by multiple sources classes
```javascript
public class PersonEntity
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public int Age { get; set; }
    public string Rule { get; set; }
    public int BirthdayYear { get; set; }
    public string Address { get; set; }
}

public class PersonEntity2
{
    public string NomeNascita { get; set; }
    public string SurnameNascita { get; set; }
    public int EtaAttuale { get; set; }
}

internal class PersonDto
{
    [MapTo(typeof(PersonEntity), "Name")]
    [MapTo(typeof(PersonEntity2), "NomeNascita")]
    public string Nome { get; set; }
    [MapTo(typeof(PersonEntity), "Surname")]
    [MapTo(typeof(PersonEntity2), "SurnameNascita")]
    public string Cognome { get; set; }
    [MapTo(typeof(PersonEntity), "Age")]
    [MapTo(typeof(PersonEntity2), "EtaAttuale")]
    public int Eta { get; set; }
    [MapTo(typeof(PersonEntity), "BirthdayYear")]
    public int AnnoNascita { get; set; }
    [MapTo(typeof(PersonEntity), "Address")]
    public string ViaResidenza { get; set; }
}
```

you can add multiple [MapToAttribute] on each destination property class like example and then do this:

```javascript
internal class Program
{
    static void Main(string[] args)
    {
        var entity = new PersonEntity() //source
        {
            Name = "John",
            Surname = "Doe",
            Age = 30,
            BirthdayYear = 1992,
            Address = "Via del Sole, 123"
        };
        var entity2 = new PersonEntity2()
        {
            NomeNascita = "John",
            SurnameNascita = "Doe",
            EtaAttuale = 30
        };

        var dto = new PersonDto(); //destination 1
        var dto2 = new PersonDto(); //destination 2

        Mapper.CopyTo<PersonEntity, PersonDto>(entity, dto);
        Mapper.CopyTo<PersonEntity2, PersonDto>(entity2, dto2);
    }
}
```

You can also copy list in another of same or different type.
Please visit EntitiesMapper.Tests folder for other examples. 


## Roadmap

- Performance improvements 

- Mapping classes to register map properties (if you don't like using [MapToAttribute])

- Custom properties conversion


# Hi, I'm Giuseppe! 👋

- Visit my github profile: [@gveriello](https://github.com/gveriello)


