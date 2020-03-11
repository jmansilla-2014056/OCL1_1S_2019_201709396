using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Proyecto1_OLC1.models
{
    public class ErrorGroup
    {
        [XmlElement]
        public Trampa[] error { get; set; }
    }
}
