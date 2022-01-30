using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot.Core
{
    /// <summary>
    /// Правило (дерево решений)
    /// </summary>
    public class BO_Rule
    {
        /// <summary>
        /// Идентификатор 
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Наименование 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание, дополнительная информация
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Тип документа, к которому привязано правило
        /// </summary>
        public string DocumentType { get; set; }

        /// <summary>
        /// Результирующий шаблон, который будет передан далее в соответствии с набором полей по данным пакета
        /// </summary>
        public BO_Template Template { get; set; }

        /// <summary>
        /// Обязательные поля, по которым будет осуществляться поиск шаблона
        /// </summary>
        public List<BO_Field> RequiredFields { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreationDate { get; set; }

    }
}
