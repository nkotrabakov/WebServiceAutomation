using System.Collections.Generic;
using System.Xml.Serialization;

namespace WebServiceAutomation.Model.XmlModel
{
    [XmlRoot(ElementName = "Features")]
    public class Features
    {
        [XmlElement(ElementName = "Feature")]
        public List<string> Feature { get; set; }

    }
}
