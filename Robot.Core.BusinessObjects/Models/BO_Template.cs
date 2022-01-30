using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot.Core
{
    /// <summary>
    /// Модель шаблона из другой системы (шаблонизатора)
    /// </summary>
    public class BO_Template
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Наименование шаблона
        /// </summary>
        public string Name { get; set; }
    }
}
