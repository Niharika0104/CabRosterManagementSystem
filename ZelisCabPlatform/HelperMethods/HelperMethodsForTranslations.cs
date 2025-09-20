using ZelisCabPlatform.Models;
using ZelisCabPortalCoreLayer.Models;


namespace ZelisCabPlatform.HelperMethods
{
    public class HelperMethodsForTranslations
    {
        public static List<RosterInfoRequest> ConvertToRosterInfoList(List<RosterInfo> rosterlist)
        {
            List<RosterInfoRequest> returnlist = new List<RosterInfoRequest>();
            foreach(RosterInfo roster in rosterlist)
            {
                returnlist.Add(new RosterInfoRequest()
                {
                    dateofbooking = roster.dateofbooking,
                    drop = roster.drop,
                    shift = roster.shift,
                    employeeid = roster.employeeid,
                    pickup = roster.pickup,
                    droptime = roster.droptime,
                    pickupTime = roster.pickupTime,
                    StatusId = roster.StatusId

                });
               
            }
            return returnlist;
        }
    }
}
