namespace Robot.Core
{
    /// <summary>
    /// Сервис для работы с правилами
    /// </summary>
    public interface IRulesService
    {
        /// <summary>
        /// Получает список всех правил
        /// </summary>
        /// <returns>Массив всех настроенных правил (деревьев решений)</returns>
        BO_Rule[] GetAll();

        /// <summary>
        /// Получает правило по идентификатору
        /// </summary>
        /// <param name="Id">Идентификатор для поиска</param>
        /// <returns>Правило</returns>
        BO_Rule GetById(int Id);

        /// <summary>
        /// Добавляет новое правило, присваивая новый идентификатор (код)
        /// </summary>
        /// <param name="rule">Правило к добавлению</param>
        /// <returns>Флаг успешности операции</returns>
        bool Add(BO_Rule rule);

        /// <summary>
        /// Обновлеяет данные состояния правила
        /// </summary>
        /// <param name="rule">Правило к обновлению</param>
        /// <returns>Флаг успешности операции</returns>
        bool Update(BO_Rule rule);

        /// <summary>
        /// Удаляет правило
        /// </summary>
        /// <param name="rule">Правило к удалению</param>
        /// <returns>Флаг успешности операции</returns>
        bool Delete(BO_Rule rule);
    }
}
