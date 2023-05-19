using System.Xml.Linq;
using System.Xml;
using KirillTesting.Models;
using KirillTesting.Helpers;
using System.Configuration;
using KirillTesting.Repositories.Interfaces;

namespace KirillTesting.ImportExport
{
    public class PersonsImporter
    {
        private readonly IPersonsRepository _personsRepository;
        public PersonsImporter(IPersonsRepository personsRepository)
        {
            _personsRepository = personsRepository;
        }
        public async Task ImportAsync(Stream stream)
        {
            using (XmlReader reader = XmlReader.Create(stream, new XmlReaderSettings() { IgnoreWhitespace = true, IgnoreComments = true, DtdProcessing = DtdProcessing.Ignore }))
            {

                reader.ReadToFollowing("PERSON");

                var personElem = XElement.ReadFrom(reader) as XElement;
                
                while (personElem != null)
                {
                    await ReadPersonAsync(personElem);
                    if (reader.Name != "PERSON_LIST")
                        personElem = XElement.ReadFrom(reader) as XElement;
                    else
                        personElem = null;
                }
            }
        }

        private async Task ReadPersonAsync(XElement element )
        {
            var person = new Person();

            person.Firstname = element.Element("IM")?.Value;
            person.Lastname = element.Element("FAM")?.Value;
            person.Patronymic = element.Element("OT")?.Value;

            person.Age = ConverterForImport.ConvertToInt(element.Element("AGE")?.Value);
            person.DateOfBirth = ConverterForImport.ConvertToNullubleDateTime(element.Element("DR")?.Value);
            person.Gender = ConverterForImport.ConvertToInt(element.Element("GENDER")?.Value);

            person.Policy = ReadPolicy(element.Element("POLICY"));

            await _personsRepository.AddPersonAsync(person);
        } 
        private Policy ReadPolicy(XElement element)
        {
            var policy = new Policy();

            policy.Serial = element.Element("SPOLIS")?.Value;
            policy.Number = element.Element("NPOLIS")?.Value;
            policy.Enp = element.Element("ENP")?.Value;
            policy.Type = ConverterForImport.ConvertToInt(element.Element("VPOLIS")?.Value);

            return policy;
        }
    }
}
