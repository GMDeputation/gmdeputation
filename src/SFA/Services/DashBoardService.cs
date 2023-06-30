using SFA.Models;
using SFA.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoogleMaps.LocationServices;

namespace SFA.Services
{
    public interface IDashBoardService
    {
        Task<List<MacroScheduleDetailCount>> GetMacroScheduleWithFiveServicesOrLess();
       
    }
    public class DashBoardService : IDashBoardService
    {
        private readonly SFADBContext _context = null;

        public DashBoardService(SFADBContext context)
        {
            _context = context;
        }

         public async Task<List<MacroScheduleDetailCount>> GetMacroScheduleWithFiveServicesOrLess()
        {
            var counts = await _context.MacroScheduleCounts.ToListAsync();
            return counts;
        }
       
    }
}
