using Robot.DAL.BaseEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Robot.DAL.Entities
{
    /// <summary>
    /// Настроенные для пакета поля. Внешняя таблица
    /// </summary>
    [Table("ScannerFields")]
    public class ScannerField : BaseEntity
    {
        /// <summary>
        /// Наименование поля
        /// </summary>
        
        [StringLength(250)]
        public string Name { get; }
    }
}
