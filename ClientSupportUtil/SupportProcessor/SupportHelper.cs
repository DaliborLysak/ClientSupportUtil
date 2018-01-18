using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;

namespace ClientSupport
{
    public class SupportHelper
    {
        public SupportHelper(string correctionNamesPath)
        {
            // nechceme mit nase jmena na githubu, takze correction file
            if (File.Exists(correctionNamesPath))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<PersonNames>));
                using (var stream = new FileStream(correctionNamesPath, FileMode.Open, FileAccess.Read))
                {
                    // musi byt v utf* jinak se json serializer osype
                    var reader = new StreamReader(stream);
                    var data = reader.ReadToEnd();
                    using (var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(data)))
                    {
                        CorrectionNames = (List<PersonNames>)serializer.ReadObject(memoryStream);
                    }
                }
            }

            LineLengthCorrection = CorrectionNames.Select(i => i.FullName.Length).Max();
        }

        private List<PersonNames> CorrectionNames;
        public int LineLengthCorrection { get; private set; }

        public string TranslateNames(string name)
        {
            var newName = name;
            if (CorrectionNames != null)
            {
                var personNames = CorrectionNames.FirstOrDefault(n => n.Name.Equals(name));
                if (personNames != null)
                {
                    newName = personNames.FullName;
                }
            }

            return newName;
        }
    }
}
