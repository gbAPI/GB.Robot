using Robot.DAL.BaseEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Robot.DAL.Entities
{
    /// <summary>
    /// Типы документов. Внешняя таблица
    /// </summary>
    [Table("DocumentTypes")]
    public class DocumentType : BaseEntity
    {
        /// <summary>
        /// Наименование шаблона
        /// </summary>
        [StringLength(250)]
        public string Name { get; set; }
    }
}
