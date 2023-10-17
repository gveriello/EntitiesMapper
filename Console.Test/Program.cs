using Console.Test.Dto;
using Console.Test.Entities;
using EntitiesMapper;

namespace Console.Test
{
    internal class Program
    {
        static readonly PersonEntity entity = new()
        {
            Name = "John",
            Surname = "Doe",
            Age = 30,
            Rule = "Ingegnere",
            BirthdayYear = 1992,
            Address = "Via del Sole, 123"
        };

        static readonly PersonEntity2 entity2 = new()
        {
            NomeNascita = "John",
            SurnameNascita = "Doe",
            EtàAttuale = 30,
            ProfessioneAttuale = "Ingegnere"
        };

        static readonly List<PersonEntity> personEntities = new()
        {
            entity,
            entity,
            entity,
            entity,
            entity,
            entity,
            entity
        };

        static readonly List<PersonEntity2> personEntities2 = new()
        {
            entity2,
            entity2,
            entity2,
            entity2,
            entity2,
            entity2,
            entity2
        };

        static void Main(string[] args)
        {
            MapperEntities.LoadEntities(typeof(PersonEntity), typeof(PersonEntity2), typeof(PersonDto));

            Mapper.CopyObject(entity, out PersonDto dto);
            Mapper.CopyObject(entity2, out PersonDto dto2);

            Mapper.CopyList<PersonEntity, PersonEntity>(personEntities, out var entityList);
            Mapper.CopyList<PersonEntity2, PersonDto>(personEntities2, out var entityList1);
        }
    }
}