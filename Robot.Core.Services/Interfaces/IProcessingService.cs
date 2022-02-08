using Robot.Core.Integration;

namespace Robot.Core
{
    /// <summary>
    /// Сервис, отвечающий за обработку входящего сообщения 
    /// </summary>
    public interface IProcessingService
    {
        /// <summary>
        /// Обработчик входящего сообщения отсканированного документа
        /// </summary>
        /// <param name="inputData">Входящее сообщение</param>
        /// <param name="templateId">Идентификатор результирующего шаблона. При неуспешности обработки = -1</param>
        /// <returns>Признак успешности обработки</returns>
        bool ProcessInputData(IMessageData inputData, out int templateId);
    }
}
