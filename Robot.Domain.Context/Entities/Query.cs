using Robot.DAL.BaseEntities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Robot.DAL.Entities
{
    /// <summary>
    /// Таблица истории запросов
    /// </summary>
    [Table("Queries")]
    public class Query : BaseEntity
    {
        /// <summary>
        /// Дата обработки пакета
        /// </summary>
        [Required]
        public DateTime MessageDate { get; set; }

        /// <summary>
        /// Тип документа входящего пакета
        /// </summary>
        [Required]
        public string InputDocumentType { get; set; }

        /// <summary>
        /// Описание ошибки, если шаблон подобран не был
        /// </summary>
        [Column("ErrorDescription")]
        public string ErrorDescription { get; set; }

        /// <summary>
        /// Наименование шаблона, который был подобран
        /// </summary>
        public string OutputTemplate { get; set; }

    }
}
