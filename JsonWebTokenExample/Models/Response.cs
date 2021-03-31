using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace JsonWebTokenExample.Models
{
    [XmlRoot(ElementName = "response")]
    public class Response<T>
    {
        [JsonProperty(PropertyName = "status")]
        [XmlElement(ElementName = "status")]
        public string Status;

        [JsonProperty(PropertyName = "responsemessage")]
        [XmlElement(ElementName = "responsemessage")]
        public string ResponseMessage;

        [JsonProperty(PropertyName = "responsedata")]
        [XmlElement(ElementName = "responsedata")]
        public T ResponseData;
    }
}
