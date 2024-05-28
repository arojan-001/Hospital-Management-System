using HMS.DataAccess.Data.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace HMS.DataAccess.Data.Repository
{
    public class UnitofWork : IUnitofWork
    {
        private readonly ApplicationDbContext _db;

        public UnitofWork(ApplicationDbContext db)
        {
            _db = db;
            Patients = new PatientsRepo(_db);
            Nurses = new NursesRepo(_db);
            Room = new RoomRepo(_db);
            OnCall = new OnCallRepo(_db);
            Usage = new UsageRepo(_db);
            Doctors = new DoctorsRepo(_db);
            Appointments = new AppointmentsRepo(_db);
            Medication = new MedicationRepo(_db);
            Perscribtion = new PerscribtionRepo(_db);
            Department = new DepartmentRepo(_db);
            Affiliated = new AffiliatedRepo(_db);
            Procedure = new ProcedureRepo(_db);
            Treatment = new TreatmentRepo(_db);
            Undergoes = new UndergoesRepo(_db);
            User = new UserRepo(_db);
        }

        public IPatientsRepo Patients { get; private set; }

        public INursesRepo Nurses { get; private set; }

        public IRoomRepo Room { get; private set; }

        public IOnCallRepo OnCall { get; private set; }

        public IUsageRepo Usage { get; private set; }

        public IDoctorsRepo Doctors { get; private set; }

        public IAppointmentsRepo Appointments { get; private set; }

        public IMedicationRepo Medication { get; private set; }

        public IPerscribtionRepo Perscribtion { get; private set; }

        public IDepartmentRepo Department { get; private set; }

        public IAffiliatedRepo Affiliated { get; private set; }

        public IProcedureRepo Procedure { get; private set; }

        public ITreatmentRepo Treatment { get; private set; }

        public IUndergoesRepo Undergoes { get; private set; }

        public IUserRepo User { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
