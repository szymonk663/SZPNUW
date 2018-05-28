using System;
using System.Collections.Generic;
using System.Text;
using SZPNUW.Data;

namespace SZPNUW.DBService
{
    public partial class Service
    {
        public bool RegisterInstructor(InstructorModel model, ref string errorMessage)
        {
            return service.RegisterInstructor(model, ref errorMessage);
        }

        public List<InstructorModel> GetInstructors()
        {
            return service.GetInstructors();
        }

        public InstructorModel GetInstructorById(int id)
        {
            return service.GetInstructorById(id);
        }

        public bool UpdateInstructor(InstructorModel model, ref string errorMessage)
        {
            return service.UpdateInstructor(model, ref errorMessage);
        }
    }
}
