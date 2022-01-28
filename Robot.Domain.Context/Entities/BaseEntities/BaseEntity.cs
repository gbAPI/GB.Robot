using System.ComponentModel.DataAnnotations;

namespace Robot.DAL.BaseEntities
{
    /// <summary>
    /// Базовая сущность модели базы данных
    /// </summary>
    public abstract class BaseEntity
    {
        [Key]
        public int ID { get; set; }
    }
}
