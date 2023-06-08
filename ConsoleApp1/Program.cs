using ConsoleApp1.Dto;
using ConsoleApp1.Entities;
using EntitiesMapper;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var entity = new PersonEntity()
            {
                Name = "John",
                Surname = "Doe",
                Age = 30,
                Rule = "Ingegnere",
                BirthdayYear = 1992,
                Address = "Via del Sole, 123"
            };
            var entity2 = new PersonEntity2()
            {
                NomeNascita = "John",
                SurnameNascita = "Doe",
                EtàAttuale = 30,
                ProfessioneAttuale = "Ingegnere"
            };

            var dto = new PersonDto();
            var dto2 = new PersonDto();

            Mapper.CopyTo<PersonEntity, PersonDto>(entity, dto);
            Mapper.CopyTo<PersonEntity2, PersonDto>(entity2, dto2);
        }
    }
}