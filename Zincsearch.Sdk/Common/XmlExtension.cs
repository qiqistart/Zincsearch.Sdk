using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Zincsearch.Sdk.Common;

public static class XmlExtension
{
    public static string SerializationXml(this object obj)
    {
        using MemoryStream memoryStream = new MemoryStream();
        XmlSerializer xmlSerializer = new XmlSerializer(obj.GetType());
        xmlSerializer.Serialize(memoryStream, obj);
        return Encoding.UTF8.GetString(memoryStream.ToArray());
    }

    public static T? DeserializationXml<T>(this string xml)
    {
        using StringReader textReader = new StringReader(xml);
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
        object data = xmlSerializer.Deserialize(textReader);
        return data.ChangeType<T>();
    }
}
