using System.Xml.Serialization;

[XmlRoot("Message")]
public class DeliveryRequestMessage
{
    [XmlElement("MessageName")]
    public string? MessageName { get; set; }

    [XmlElement("MessageId")]
    public string? MessageId { get; set; }

    [XmlElement("MessageBody")]
    public DeliveryRequestBody? MessageBody { get; set; }
}

public class DeliveryRequestBody
{
    [XmlElement("DELIVERY_REQUEST_SYS1")]
    public DeliveryRequest? DeliveryRequest { get; set; }
}

public class DeliveryRequest
{
    [XmlElement("OrderNo")]
    public string? OrderNo { get; set; }

    [XmlElement("FromLoc")]
    public string? FromLoc { get; set; }

    [XmlElement("ToLoc")]
    public string? ToLoc { get; set; }

    [XmlElement("Pieces")]
    public int Pieces { get; set; }

    // Include other elements as needed
}
