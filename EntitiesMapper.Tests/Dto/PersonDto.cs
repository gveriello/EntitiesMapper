using EntitiesMapper.CustomAttribute;
using EntitiesMapper.Tests.Entities;

namespace EntitiesMapper.Tests.Dto
{
    internal class PersonDto
    {
        [MapTo<PersonEntity>(nameof(PersonEntity.Name))]
        [MapTo<PersonEntity2>(nameof(PersonEntity2.NomeNascita))]
        public string Nome { get; set; }

        [MapTo<PersonEntity>(nameof(PersonEntity.Surname))]
        [MapTo<PersonEntity2>(nameof(PersonEntity2.SurnameNascita))]
        public string Cognome { get; set; }

        [MapTo<PersonEntity>(nameof(PersonEntity.Age))]
        [MapTo<PersonEntity2>(nameof(PersonEntity2.EtàAttuale))]
        public int Età { get; set; }

        [MapTo<PersonEntity>(nameof(PersonEntity.Rule))]
        [MapTo<PersonEntity2>(nameof(PersonEntity2.ProfessioneAttuale))]
        public string Professione { get; set; }

        [MapTo<PersonEntity>(nameof(PersonEntity.BirthdayYear))]
        public int AnnoNascita { get; set; }

        [MapTo<PersonEntity>(nameof(PersonEntity.Address))]
        public string ViaResidenza { get; set; }
    }
}
