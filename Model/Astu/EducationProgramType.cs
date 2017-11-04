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
        [FieldName("ID_VIDPROG")]
        [FieldType(DatabaseFieldType.Integer)]
        public int Id { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        [FieldName("NAME_VIDPROG")]
        [FieldType(DatabaseFieldType.String)]
        public string Name { get; set; }

        /// <summary>
        /// Краткое наименование
        /// </summary>
        [FieldName("FOLDER_OMKO")]
        [FieldType(DatabaseFieldType.String)]
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
