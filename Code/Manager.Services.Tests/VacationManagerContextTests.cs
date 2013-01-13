using System.Data.Entity;
using System.Linq;
using FizzWare.NBuilder;
using NUnit.Framework;
using VacationManager.Common.DataContracts;
using VacationManager.Services;
using VacationManager.Services.Model;

namespace Manager.Services.Tests
{
    [TestFixture]
    public class VacationManagerContextTests
    {
        private VacationManagerContext _ctx;

        [TestFixtureSetUp]
        public void SuiteInitialization()
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<VacationManagerContext>());
            HibernatingRhinos.Profiler.Appender.EntityFramework.EntityFrameworkProfiler.Initialize();
        }

        [SetUp]
        public void TestInitialization()
        {
            _ctx = new VacationManagerContext();
        }

        [TearDown]
        public void TestTearDown()
        {
            _ctx.Dispose();
        }

        [Test]
        public void Can_insert_an_Employee_record()
        {
            var employee = BuildEmployee();
            
            _ctx.Employees.Add(employee);
            _ctx.SaveChanges();

            Assert.IsNotNull(_ctx.Employees.SingleOrDefault(e => e.Id == employee.Id));
        }

        [Test]
        public void Can_update_an_Employee_record()
        {
            const string expectedEmail = "diffrent mail";
            var employee = BuildEmployee();
            _ctx.Employees.Add(employee);
            _ctx.SaveChanges();
            Assert.AreNotEqual(employee.EmailAddress, expectedEmail);

            employee.EmailAddress = expectedEmail;
            _ctx.SaveChanges(); // TODO: check why commenting this line the test still passes, 
                                // when commented update is not performend on db, I saw that with profiler

            Assert.AreEqual(expectedEmail, _ctx.Employees.Single(e => e.Id == employee.Id).EmailAddress);
        }

        [Test]
        public void Can_delete_an_Employee_record()
        {
            var employee = BuildEmployee();
            _ctx.Employees.Add(employee);
            _ctx.SaveChanges();

            _ctx.Employees.Remove(employee);
            _ctx.SaveChanges();

            Assert.IsNull(_ctx.Employees.SingleOrDefault(e => e.Id == employee.Id));
        }

        [Test]
        public void Can_insert_a_VacationRequest_record()
        {
            var employee = BuildEmployee();
            var request = BuildVacationRequest(employee);
            _ctx.Employees.Add(employee);
            
            _ctx.Requests.Add(request);
            _ctx.SaveChanges();

            Assert.IsNotNull(_ctx.Requests.SingleOrDefault(r => r.RequestNumber == request.RequestNumber));
        }

        [Test]
        public void Can_update_a_VacationRequest_record()
        {
            var employee = BuildEmployee();
            var request = BuildVacationRequest(employee);
            _ctx.Employees.Add(employee);
            _ctx.Requests.Add(request);
            _ctx.SaveChanges();
            Assert.AreNotEqual(request.State, VacationRequestState.Rejected);

            request.State = VacationRequestState.Rejected;
            _ctx.SaveChanges(); // TODO: check why commenting this line the test still passes, 
                                // when commented update is not performend on db, I saw that with profiler

            Assert.AreEqual(VacationRequestState.Rejected, _ctx.Requests.Single(r => r.RequestNumber == request.RequestNumber).State);
        }

        [Test]
        public void Can_delete_a_VacationRequest_record()
        {
            var employee = BuildEmployee();
            var request = BuildVacationRequest(employee);
            _ctx.Employees.Add(employee);
            _ctx.Requests.Add(request);
            _ctx.SaveChanges();

            _ctx.Requests.Remove(request);
            _ctx.SaveChanges();

            Assert.IsNull(_ctx.Requests.SingleOrDefault(r => r.RequestNumber == request.RequestNumber));
        }

        #region Private methods

        private static Employee BuildEmployee()
        {
            return Builder<Employee>
                .CreateNew()
                    .With(x => x.Id = 0)
                    .And(x => x.Roles = EmployeeRoles.Executive)
                .Build();
        }

        private static VacationRequest BuildVacationRequest(Employee employee)
        {
            return Builder<VacationRequest>
                .CreateNew()
                    .With(x => x.RequestNumber = 0)
                    .And(x => x.Employee = employee)
                .Build();
        }

        #endregion
    }
}
