using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Model.Astu
{
    /// <summary>
    /// Вид образовательной программы
    /// </summary>
    [TableName("VIDPROG")]
    public class EducationProgramType: Entity
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [PrimaryKey]
        [DbFieldInfo("ID_VIDPROG", DbFieldType.Integer)]
        public int Id { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        [DbFieldInfo("NAME_VIDPROG")]
        public string Name { get; set; }

        /// <summary>
        /// Краткое наименование
        /// </summary>
        [DbFieldInfo("FOLDER_OMKO")]
        public string ShortName { get; set; }

        ObservableCollection<Direction> _directions;

        /// <summary>
        /// Направления подготовки образовательной программы
        /// </summary>
        public ObservableCollection<Direction> Directions
        {
            get
            {
                if (_directions == null)
                {
                    _directions = new ObservableCollection<Direction>(Astu.Directions.Where(d => d.EducationProgramTypeId == Id));
                }
                return _directions;
            }
            set
            {
                _directions = value;
            }
        }

    }
}
