using System;
using System.Collections.Generic;
using System.Text;
using SZPNUW.Data;

namespace SZPNUW.DBService
{
    public partial class Service
    {
        public List<ReportModel> GetReportsBySectionId(int sectionId)
        {
            return service.GetReportsBySectionId(sectionId);
        }

        public FileModel GetReport(int reportId, ref string errorMessage)
        {
            return service.GetReport(reportId, ref errorMessage);
        }

        public void SaveReport(FileModel model)
        {
            service.SaveReport(model);
        }

        public void DeleteReport(int id)
        {
            service.DeleteReport(id);
        }
    }
}
