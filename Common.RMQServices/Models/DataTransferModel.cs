using Robot.Core.Integration;
using System.Collections.Generic;

namespace Common.RMQServices.Models
{
#pragma warning disable CS1591 // Отсутствует комментарий XML для открытого видимого типа или члена
    public class DataTransferModel : IMessageData
    {
        private Dictionary<string, IEnumerable<string>> _inputData;

        public int MessageID { get; set; }

        public string DocumentType { get; set; }

        public int OutputTemplateID { get; set; }

        public bool PackageProcessed { get; set; }
        public Dictionary<string, IEnumerable<string>> InputData { get => _inputData; set => _inputData = value; }
        public Dictionary<string, IEnumerable<string>> Data { get => _inputData; set => _inputData = value; }
    }
#pragma warning restore CS1591 // Отсутствует комментарий XML для открытого видимого типа или члена
}
