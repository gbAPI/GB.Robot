namespace Robot.Core
{
    /// <summary>
    /// Сервис работающий с запросами системы
    /// </summary>
    public interface IQueriesService
    {
        /// <summary>
        /// Получает список всех прошедших запросов
        /// </summary>
        /// <returns>Список прошедших запросов</returns>
        BO_Query[] GetAll();


        /// <summary>
        /// Добавляет сообщение
        /// </summary>
        /// <param name="query">Сообщение</param>
        void Add(BO_Query query);
    }
}
