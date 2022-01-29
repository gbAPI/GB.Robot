using System.Collections.Generic;

namespace Robot.DAL.Repos.Interfaces
{
    public interface IRepo<T>
    {
        /// <summary>
        /// Добавляет в бд новый объект
        /// </summary>
        /// <param name="entity">Новый объект</param>
        /// <returns>Идентификатор сохраненного объекта</returns>
        int Add(T entity);

        /// <summary>
        /// Получает запись бд по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор искомого объекта</param>
        /// <returns>Найденная сущность</returns>
        T Get(int id);

        /// <summary>
        /// Сохраняет изменения в объекте бд
        /// </summary>
        /// <param name="entity">Сущность, по которой произошли изменения</param>
        /// <returns>Идентификатор сохраняемой сущности</returns>
        int Save(T entity);

        /// <summary>
        /// Удаляет запись базы данных
        /// </summary>
        /// <param name="entity">Сущность, которую надо удалить</param>
        /// <returns>Флаг успешности удаления</returns>
        bool Delete(T entity);

        /// <summary>
        /// Получает все записи таблицы типа
        /// </summary>
        /// <returns></returns>
        List<T> GetAll();
    }
}
