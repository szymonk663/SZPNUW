using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SZPNUW.Base.Resources;
using SZPNUW.Data;
using SZPNUW.DBService.Model;

namespace SZPNUW.DBService
{
    public partial class DBService
    {
        public List<ReportModel> GetReportsBySectionId(int sectionId)
        {
            using (SZPNUWContext context = new SZPNUWContext())
            {
                return context.Reports
                    .Where(x => x.Sectionid == sectionId)
                    .Select(x => new ReportModel()
                    {
                        Id = x.Id,
                        Name = x.Filename,
                        SectionId = x.Sectionid
                    }).ToList();
            }
        }

        public FileModel GetReport(int reportId, ref string errorMessage)
        {
            using (SZPNUWContext context = new SZPNUWContext())
            {
                Reports report = context.Reports.FirstOrDefault(x => x.Id == reportId);
                if(report != null)
                {
                    return new FileModel { FileName = report.Filename, Content = report.Content };
                }
                errorMessage = PortalMessages.NoSuchElement;
                return null;
            }
        }

        public void SaveReport(FileModel model)
        {
            using (SZPNUWContext context = new SZPNUWContext())
            {
                Reports report = new Reports() { Sectionid = model.SectionId.Value, Filename = model.FileName, Content = model.Content };
                context.Reports.Add(report);
                context.SaveChanges();
            }
        }

        public void DeleteReport(int id)
        {
            using (SZPNUWContext context = new SZPNUWContext())
            {
                Reports report = new Reports { Id = id };
                context.Reports.Attach(report);
                context.Reports.Remove(report);
                context.SaveChanges();
            }
        }
    }
}
