﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.WorkOk
{
    [TableName("spgrup")]
    public class Group: Entity
    {
        [PrimaryKey]
        [DbFieldInfo("pin", DbFieldType.Integer)]
        public int Id { get; set; }

        [DbFieldInfo("name")]
        public string Name { get; set; }

        [DbFieldInfo("ASTU_Code")]
        public string AstuId { get; set; }
    }
}
