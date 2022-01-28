using Robot.DAL.BaseEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Robot.DAL.Entities
{
    /// <summary>
    /// Доступные шаблоны. Внешняя таблица
    /// </summary>
    [Table("Templates")]
    public class Template : BaseEntity
    {
        /// <summary>
        /// Наименование шаблона
        /// </summary>
        [StringLength(250)]
        public string Name { get; set; }
    }
}
