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
    }
}
