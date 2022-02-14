using Robot.Core.Integration;
using System.Collections.Generic;

namespace Common.RMQServices.Models
{
#pragma warning disable CS1591 // Отсутствует комментарий XML для открытого видимого типа или члена
    public class DataTransferModel : IMessageData
    {
        public int MessageID { get; set; }

        public string DocumentType { get; set; }

        public int OutputTemplateID { get; set; }

        public bool PackageProcessed { get; set; }
        public Dictionary<string, IEnumerable<string>> InputData { get ; set; }
    }
#pragma warning restore CS1591 // Отсутствует комментарий XML для открытого видимого типа или члена
}
