using DeviceManagement_WebApp.Data;
using DeviceManagement_WebApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace DeviceManagement_WebApp.Repository
{
    public class ZonesRepository : GenericRepository<Zone>, IZoneRepository
    {
        public ZonesRepository(ConnectedOfficeContext context) : base(context)
        {

        }
    }
}
