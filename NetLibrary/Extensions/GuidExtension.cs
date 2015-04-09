using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetLibrary.Extensions
{
    public static class GuidExtension
    {
        public static GuidDate GuidFromDate(this System.Guid guid)
        {
            return GuidFromDate(guid, DateTime.Now.Ticks);
        }

        public static GuidDate GuidFromDate(this System.Guid guid, DateTime date)
        {
            return GuidFromDate(guid, date);
        }

        public static GuidDate GuidFromDate(this System.Guid guid, long auxInformation)
        {
            return GuidFromDate(guid, DateTime.Now, auxInformation);
        }

        public static GuidDate GuidFromDate(this System.Guid guid, DateTime date, long auxInformation)
        {
            return NetLibrary.GuidDate.NewGuid(date, auxInformation);
        }

        public static DateTime GuidToDate(this System.Guid guid, out long auxInformation)
        {
            return NetLibrary.GuidDate.GuidToDate(guid, out auxInformation);
        }

        public static DateTime GuidToDate(this System.Guid guid)
        {
            return NetLibrary.GuidDate.GuidToDate(guid);
        }
    }
}
