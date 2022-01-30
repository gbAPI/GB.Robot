using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot.Core
{
    /// <summary>
    /// Запрос, прошедший через систему
    /// </summary>
    public class BO_Query
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Дата сообщения
        /// </summary>
        public DateTime MessageDate { get; set; }

        /// <summary>
        /// Тип документа
        /// </summary>
        public string DocumentType { get; set; }

        /// <summary>
        /// Результирующий шаблон для печати
        /// </summary>
        public BO_Template OutputTemplate { get; set; }

        /// <summary>
        /// Описание ошибки. Если ошибки не было, то не заполняется
        /// </summary>
        public string ErrorDescription { get; set; }

    }
}
