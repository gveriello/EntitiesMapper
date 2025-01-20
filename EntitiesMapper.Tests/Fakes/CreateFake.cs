namespace EntitiesMapper.Tests.Fakes
{
    public static class CreateFake
    {
        private static readonly Random _random = new();
        private static readonly string[] _firstNames = ["Mario", "Luigi", "Giovanni", "Paolo", "Marco", "Andrea", "Giuseppe", "Antonio", "Francesco", "Roberto"];
        private static readonly string[] _lastNames = ["Rossi", "Bianchi", "Verdi", "Ferrari", "Romano", "Gallo", "Costa", "Fontana", "Conti", "Esposito"];
        private static readonly string[] _cities = ["Roma", "Milano", "Napoli", "Torino", "Palermo", "Genova", "Bologna", "Firenze", "Bari", "Catania"];
        private static readonly string[] _domains = ["example.com", "test.com", "fake.com", "demo.com", "sample.com"];
        private static readonly string[] _streetTypes = ["Via", "Viale", "Corso", "Piazza", "Largo"];
        private static readonly string[] _companies = ["Tech", "Solutions", "Systems", "Industries", "International", "Corp", "Ltd", "Srl", "SpA"];

        public static T ObjectOfType<T>() where T : class
        {
            var type = typeof(T);
            var instance = Activator.CreateInstance(type);

            foreach (var property in type.GetProperties())
            {
                if (!property.CanWrite) continue;

                var value = GenerateValueForType(property.PropertyType);
                property.SetValue(instance, value);
            }

            return (T)instance;
        }

        public static List<T> CollectionOfType<T>(int count) where T : class
        {
            return Enumerable.Range(0, count).Select(_ => ObjectOfType<T>()).ToList();
        }

        private static object GenerateValueForType(Type type)
        {
            if (type == typeof(string))
                return GenerateRandomString();

            if (type == typeof(int))
                return _random.Next(-1000, 1000);

            if (type == typeof(long))
                return _random.Next(-1000, 1000);

            if (type == typeof(double))
                return _random.NextDouble() * 1000;

            if (type == typeof(decimal))
                return (decimal)(_random.NextDouble() * 1000);

            if (type == typeof(float))
                return (float)(_random.NextDouble() * 1000);

            if (type == typeof(bool))
                return _random.Next(2) == 1;

            if (type == typeof(DateTime))
                return DateTime.Now.AddDays(-_random.Next(1000)).AddHours(_random.Next(24)).AddMinutes(_random.Next(60));

            if (type == typeof(TimeSpan))
                return TimeSpan.FromMinutes(_random.Next(1440)); // 24 ore in minuti

            if (type == typeof(Guid))
                return Guid.NewGuid();

            if (type.IsEnum)
                return GetRandomEnumValue(type);

            if (type.IsGenericType)
            {
                if (type.GetGenericTypeDefinition() == typeof(List<>))
                {
                    var listType = type.GetGenericArguments()[0];
                    var listInstance = Activator.CreateInstance(type);
                    var addMethod = type.GetMethod("Add");

                    for (int i = 0; i < _random.Next(1, 5); i++)
                    {
                        var element = GenerateValueForType(listType);
                        addMethod.Invoke(listInstance, new[] { element });
                    }

                    return listInstance;
                }

                if (type.GetGenericTypeDefinition() == typeof(Dictionary<,>))
                {
                    var keyType = type.GetGenericArguments()[0];
                    var valueType = type.GetGenericArguments()[1];
                    var dictInstance = Activator.CreateInstance(type);
                    var addMethod = type.GetMethod("Add");

                    for (int i = 0; i < _random.Next(1, 5); i++)
                    {
                        var key = GenerateValueForType(keyType);
                        var value = GenerateValueForType(valueType);
                        try
                        {
                            addMethod.Invoke(dictInstance, new[] { key, value });
                        }
                        catch
                        {
                            // Ignora eventuali chiavi duplicate
                            continue;
                        }
                    }

                    return dictInstance;
                }
            }

            if (type == typeof(string[]))
                return Enumerable.Range(0, _random.Next(1, 5))
                    .Select(_ => GenerateRandomString())
                    .ToArray();

            // Per i tipi nullable
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                if (_random.Next(2) == 0) // 50% di probabilità di null
                    return null;
                return GenerateValueForType(type.GetGenericArguments()[0]);
            }

            // Per i tipi complessi (classi custom)
            if (!type.IsPrimitive && !type.IsEnum && type != typeof(string) && type != typeof(decimal))
            {
                try
                {
                    return GenerateComplexType(type);
                }
                catch
                {
                    return null;
                }
            }

            return null;
        }

        private static object GenerateComplexType(Type type)
        {
            var instance = Activator.CreateInstance(type);
            foreach (var property in type.GetProperties().Where(p => p.CanWrite))
            {
                try
                {
                    var value = GenerateValueForType(property.PropertyType);
                    property.SetValue(instance, value);
                }
                catch
                {
                    continue;
                }
            }
            return instance;
        }

        private static object GetRandomEnumValue(Type enumType)
        {
            var values = Enum.GetValues(enumType);
            return values.GetValue(_random.Next(values.Length));
        }

        private static string GenerateRandomString()
        {
            var types = new Func<string>[]
            {
            () => $"{GetRandom(_firstNames)} {GetRandom(_lastNames)}", // Nome completo
            () => GetRandom(_firstNames), // Nome
            () => GetRandom(_lastNames), // Cognome
            () => $"{GetRandom(_streetTypes)} {GenerateWord()} {_random.Next(1, 100)}", // Indirizzo
            () => GetRandom(_cities), // Città
            () => $"{GenerateWord()}@{GetRandom(_domains)}", // Email
            () => $"+39{_random.Next(300000000, 399999999)}", // Telefono
            () => $"{GenerateWord()} {GetRandom(_companies)}", // Nome azienda
            () => GenerateWord() // Parola casuale
            };

            return types[_random.Next(types.Length)]();
        }

        private static string GenerateWord(int minLength = 4, int maxLength = 10)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz";
            var length = _random.Next(minLength, maxLength + 1);
            return new string(Enumerable.Range(0, length)
                .Select(_ => chars[_random.Next(chars.Length)])
                .ToArray());
        }

        private static T GetRandom<T>(T[] array)
        {
            return array[_random.Next(array.Length)];
        }

        #region Helper Methods for Specific Types
        public static string GenerateEmail() => $"{GenerateWord()}@{GetRandom(_domains)}";
        public static string GeneratePhoneNumber() => $"+39{_random.Next(300000000, 399999999)}";
        public static string GenerateAddress() => $"{GetRandom(_streetTypes)} {GenerateWord()} {_random.Next(1, 100)}";
        public static string GenerateFullName() => $"{GetRandom(_firstNames)} {GetRandom(_lastNames)}";
        public static string GenerateCompanyName() => $"{GenerateWord()} {GetRandom(_companies)}";
        public static string GenerateCity() => GetRandom(_cities);
        #endregion
    }
}
