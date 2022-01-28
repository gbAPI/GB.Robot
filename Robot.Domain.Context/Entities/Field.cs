using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Robot.DAL.BaseEntities;

namespace Robot.DAL.Entities
{
    /// <summary>
    /// Поле, по которому проходит валидация пакета
    /// </summary>
    [Table("Fields")]
    public class Field : BaseEntity
    {

        /// <summary>
        /// Имя поля
        /// </summary>
        [Required]
        [Column("Name")]
        public string FieldName { get; set; }

        /// <summary>
        /// Описание поля
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Правило - владелец поля
        /// </summary>
        [Required]
        [ForeignKey("RuleID")]
        public virtual Decision Rule { get; set; }
    }
}
