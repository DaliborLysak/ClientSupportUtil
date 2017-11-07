using System.Runtime.Serialization;

namespace ClientSupport
{
    [DataContract]  
    public class PersonNames
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string FullName { get; set; }
    }
}
