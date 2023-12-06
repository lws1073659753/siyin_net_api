using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SiyinPractice.Framework.Extensions
{
    public sealed class SerialNoHelper
    {
        private static volatile SerialNoHelper helper;
        private static readonly Object syncRoot = new Object();

        private static string lastdate;
        private static int lastno;

        private SerialNoHelper()
        {
        }

        public static SerialNoHelper Helper
        {
            get
            {
                if (helper == null)
                {
                    lock (syncRoot)
                    {
                        if (helper == null)
                            helper = new SerialNoHelper();
                    }
                }
                return helper;
            }
        }

        /// <summary>
        /// 生成流水号
        /// </summary>
        /// <param name="serialno">从数据库读取最大的流水号</param>
        /// <param name="istime">是否启用时间</param>
        /// <param name="timecount">时间长度</param>
        /// <param name="healdName"></param>
        /// <returns></returns>
        public static string GetSerialno(string serialno,  string healdName, int sarialcount=0)
        {
            lock (syncRoot)
            {
                var today = "";
                lastno = 0;
                var last = "";

                today = DateTime.Today.ToString("yyyyMM");
                if (!string.IsNullOrEmpty(serialno))
                {
                    var codeyear = (serialno.Substring(healdName.Length, 6));
                    lastno = Convert.ToInt32(serialno.Substring(healdName.Length + today.Length));
                    if (today != codeyear)
                    {
                        lastno = 0;
                    }
                    if (sarialcount > 0)
                    {
                        last = (++lastno).ToString().PadLeft(sarialcount-1, '0');
                    }
                    else
                    {
                        last = (++lastno).ToString().PadLeft(4, '0');
                    }

                    return $"{healdName}{today}{last}";
                }
                else
                {
                    if (sarialcount > 0)
                    {
                        last = (++lastno).ToString().PadLeft(sarialcount-1, '0');
                    }
                    else
                    {
                        last = (++lastno).ToString().PadLeft(4, '0');
                    }

                    //lastno = Convert.ToInt32(serialno.Substring(healdName.Length));
                    return $"{healdName}{today}{last}";
                }
            }
        }
    }
}
