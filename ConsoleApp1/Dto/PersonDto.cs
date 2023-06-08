using ConsoleApp1.Entities;
using EntitiesMapper.CustomAttribute;

namespace ConsoleApp1.Dto
{
    internal class PersonDto
    {
        [MapTo(typeof(PersonEntity), "Name")]
        [MapTo(typeof(PersonEntity2), "NomeNascita")]
        public string Nome { get; set; }

        [MapTo(typeof(PersonEntity), "Surname")]
        [MapTo(typeof(PersonEntity2), "SurnameNascita")]
        public string Cognome { get; set; }

        [MapTo(typeof(PersonEntity), "Age")]
        [MapTo(typeof(PersonEntity2), "EtàAttuale")]
        public int Età { get; set; }

        [MapTo(typeof(PersonEntity), "Rule")]
        [MapTo(typeof(PersonEntity2), "ProfessioneAttuale")]
        public string Professione { get; set; }

        [MapTo(typeof(PersonEntity), "BirthdayYear")]
        public int AnnoNascita { get; set; }

        [MapTo(typeof(PersonEntity), "Address")]
        public string ViaResidenza { get; set; }
    }
}
