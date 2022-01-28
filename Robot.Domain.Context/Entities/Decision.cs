using Robot.DAL.BaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Robot.DAL.Entities
{
    /// <summary>
    /// Элемент дерева решений. Правило, по которому определяется выходной шаблон
    /// </summary>
    [Table("Decisions")]
    public class Decision : BaseEntity
    {

        /// <summary>
        /// Наименование
        /// </summary>
        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        /// <summary>
        /// Тип документа
        /// </summary>
        [Required]
        [StringLength(350)]
        public string DocumentType { get; set; }

        /// <summary>
        /// ID выходного шаблона
        /// </summary>
        [Required]
        [Column("OutputTemplateID")]
        public int OutputTemplateID { get; set; }


        /// <summary>
        /// Дата создания
        /// </summary>
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime CreationDate { get; set; }


        /// <summary>
        /// Описание правила
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Поля, по которому проверяется пакет
        /// </summary>
        public virtual ICollection<Field> RequiredFields { get; set; }
    }
}
