using System.Collections.Generic;
using System.Data;

namespace SiyinPractice.Framework.Extensions
{
    public class TableByImprot
    {
        public DataTable dataTable { get; set; } //tabel
        public List<DataRow> EmptyDaraRow { get; set; } //空列
        public List<string> ListIndex { get; set; } //空数据的列
        public List<string> ListSn { get; set; } //snlist
        public List<string> ListOrgPnSn { get; set; } //希捷SN
        public List<string> ListDept { get; set; }//dept部门
        public List<string> ListBin { get; set; }//bin
        public List<string> ListLocation { get; set; }///库位
        public List<string> ListProject { get; set; } //项目
        public List<string> ListNum { get; set; }///批次号
        public List<string> ListPlantSn { get; set; }//厂内sn
        public List<string> ListNumEmpty { get; set; }//厂内sn
        
    }
}