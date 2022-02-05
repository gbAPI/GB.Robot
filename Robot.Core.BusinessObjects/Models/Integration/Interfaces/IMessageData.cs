
using System.Collections.Generic;


namespace Robot.Core.Integration
{
    public interface IMessageData
    {
        string DocumentType { get; set; }

        Dictionary<string, IEnumerable<string>> InputData { get; set; }
    }
}
