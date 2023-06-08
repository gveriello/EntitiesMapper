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

        static void Main(string[] args)
        {
            MapperEntities.LoadEntities(typeof(PersonEntity), typeof(PersonEntity2), typeof(PersonDto));
            var dto = new PersonDto();
            var dto2 = new PersonDto();

            Mapper.CopyTo(entity, dto);
            Mapper.CopyTo(entity2, dto2);

        }
    }
}