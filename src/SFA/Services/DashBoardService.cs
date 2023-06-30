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
        Task<List<ServiceCountOneYear>> GetServiceCountOneYear();
        Task<List<MacroScheduleDetailThirtyDayCount>> GetMacroScheduleDetailThirtyDayCount();
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
            try
            {
                var counts = await _context.MacroScheduleCounts.ToListAsync();
                return counts;
            }
            catch(Exception ex)
            {
                return null;
            }
           
            
        }

        public async Task<List<ServiceCountOneYear>> GetServiceCountOneYear()
        {
            var counts = await _context.ServiceCountOneYear.ToListAsync();
            return counts;
        }

        public async Task<List<MacroScheduleDetailThirtyDayCount>> GetMacroScheduleDetailThirtyDayCount()
        {
            var counts = await _context.MacroScheduleDetailThirtyDayCount.ToListAsync();
            return counts;
        }


    }
}
