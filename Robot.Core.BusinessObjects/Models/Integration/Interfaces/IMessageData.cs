
using System.Collections.Generic;


namespace Robot.Core.Integration
{
    /// <summary>
    /// Описание структуры входящего сообщения
    /// </summary>
    public interface IMessageData
    {
        /// <summary>
        /// Тип отсканированного документа
        /// </summary>
        string DocumentType { get; set; }

        /// <summary>
        /// Метаданные отсканированного документа.
        /// </summary>
        Dictionary<string, IEnumerable<string>> InputData { get; set; }
    }
}
