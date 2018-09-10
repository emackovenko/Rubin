using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.WorkOk
{
    [TableName("platn.accplat")]
    public abstract class Contragent: Entity
    {

        [PrimaryKey]
        [DbFieldInfo("pin", DbFieldType.Integer)]
        public int Id { get; set; }

        [DbFieldInfo("name")]
        public string Name { get; set; }

        [DbFieldInfo("adress")]
        public string Address { get; set; }

        [DbFieldInfo("tip", DbFieldType.Integer)]
        public int? ContragentType { get; set; }

        public string ContragentTypeName
        {
            get
            {
                if (ContragentType == 1)
                {
                    return "Физическое лицо";
                }
                if (ContragentType == 2)
                {
                    return "Юридическое лицо";
                }
                return "Самостоятельно";
            }
        }
    }
}
