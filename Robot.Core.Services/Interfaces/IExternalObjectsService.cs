using System.Collections.Generic;


namespace Robot.Core
{
    /// <summary>
    /// Сервис, отвечающий за получение списка значений из внешних таблиц
    /// </summary>
    public interface IExternalObjectsService
    {
        /// <summary>
        /// Получает поля из таблицы сканировщика
        /// </summary>
        /// <returns>Список полей</returns>
        BO_Field[] GetScannerFields();

        /// <summary>
        /// Получает все шаблоны из таблицы шаблонизатора
        /// </summary>
        /// <returns></returns>
        BO_Template[] GetTAllTemplates();

        /// <summary>
        /// Получает список наименований всех типов документов,настроенных на сканировщике
        /// </summary>
        /// <returns>Коллекция наименований</returns>
        ICollection<string> GetAllDocumentTypes();
    }
}
