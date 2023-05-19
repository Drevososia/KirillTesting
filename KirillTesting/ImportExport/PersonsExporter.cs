using System.Text;
using System.Xml.Linq;
using System.Xml;
using KirillTesting.Models;

namespace KirillTesting.ImportExport
{
    public class PersonsExporter
    {
        public PersonsExporter()
        {

        }

        public async Task<MemoryStream> ExportAsync(List<object> people)
        {
            XmlWriterSettings writerSettings = new();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            writerSettings.Encoding = Encoding.GetEncoding("windows-1251"); ;
            writerSettings.Indent = true;

            MemoryStream output = new MemoryStream();
            using (var writer = XmlWriter.Create(output, writerSettings))
            {
                writer.WriteStartElement("PERSON_LIST");

                Write_ZGLV(writer);
                Write_PEOPLE(writer, people);

                writer.WriteEndElement();
            }

            return output;
        }

        private void Write_ZGLV(XmlWriter writer)
        {
            var DATA = DateTime.Now.ToString("yyyy-MM-dd");

            new XElement("ZGLV",
                    new XElement("VERSION", "1.0"),
                    new XElement("DATA", DATA)
                   ).WriteTo(writer);
        }

       
        private void Write_PEOPLE(XmlWriter writer, List<object> people)
        {

            foreach (Person person in people)
            {
                writer.WriteStartElement("PERSON");

                var elems = new List<KeyValuePair<string, string>>();

                elems.Add(new("FAM", person.Lastname));
                elems.Add(new("IM", person.Firstname));
                elems.Add(new("OT", person.Patronymic));
                elems.Add(new("AGE", person.Age.ToString()));
                elems.Add(new("GENDER", person.Gender?.ToString()));
                elems.Add(new("DR", person.DateOfBirth.GetValueOrDefault().ToString("yyyy-MM-dd")));


                foreach (var elem in elems)
                {
                    if (elem.Value != "" && elem.Value != null)
                    {
                        new XElement(elem.Key, elem.Value).WriteTo(writer);
                    }
                }
                Write_PEOPLE_POLICY(writer, person.Policy);

                writer.WriteEndElement();
            }
       
        }


        private void Write_PEOPLE_POLICY(XmlWriter writer, Policy policy)
        {
            var elems = new List<KeyValuePair<string, string>>();

           
            elems.Add(new("VPOLIS", policy.Type.ToString()));
            elems.Add(new("SPOLIS", policy.Serial));
            elems.Add(new("NPOLIS", policy.Number));
            elems.Add(new("ENP", policy.Enp));

            writer.WriteStartElement("POLICY");
            foreach (var elem in elems)
            {
                if (elem.Value != "" && elem.Value != null)
                {
                    new XElement(elem.Key, elem.Value).WriteTo(writer);
                }
            }
            writer.WriteEndElement();
        }
    }
}
