using System;
using System.Collections.Generic;
using System.Text;

namespace HMS.DataAccess.Data.IRepository
{
    public interface IUnitofWork : IDisposable
    {
        IPatientsRepo Patients { get; }

        INursesRepo Nurses { get; }

        IRoomRepo Room { get; }

        IOnCallRepo OnCall { get; }

        IUsageRepo Usage { get; }

        IDoctorsRepo Doctors { get; }

        IAppointmentsRepo Appointments { get; }

        IMedicationRepo Medication { get; }

        IPerscribtionRepo Perscribtion { get; }

        IDepartmentRepo Department { get; }

        IAffiliatedRepo Affiliated { get; }

        IProcedureRepo Procedure { get; }

        ITreatmentRepo Treatment { get; }

        IUndergoesRepo Undergoes { get; }

        IUserRepo User { get; }

        void Save();
    }
}
