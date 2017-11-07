using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;

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
                    CorrectionNames = (List<PersonNames>)serializer.ReadObject(stream);
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
