using System;
using System.Collections.Generic;
using System.Text;

namespace Data.University
{
    /// <summary>
    /// Факультет
    /// </summary>
    public class Faculty
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Краткое наименование
        /// </summary>
        public string ShortName { get; set; }

        /// <summary>
        /// Группы
        /// </summary>
        public List<Group> Groups { get; set; }

        public Faculty()
        {
            Groups = new List<Group>();
        }
    }
}
