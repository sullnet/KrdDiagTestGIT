using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

namespace Contoso.Storage.Table {
    public class Employee : TableEntity {
        public int YearsAtCompany { get; set; }

        public override string ToString() {
            return RowKey + "\t\t[" + YearsAtCompany + "]";
        }
    }
}
