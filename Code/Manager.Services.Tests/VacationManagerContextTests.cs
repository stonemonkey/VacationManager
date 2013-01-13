﻿using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Xml;
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
            // Set EF to allways recreate DB before starting to run any of the following tests.
            Database.SetInitializer(new DropCreateDatabaseAlways<VacationManagerContext>());
            // Initialize EF Profiler. Because of this you will be able to check SQL generated by EF.
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
        [Ignore("This is not realy a test. Run this test in order to generate edmx file.")]
        public void GenerateEdmx()
        {
            var settings = new XmlWriterSettings { Indent = true };

            using (var writer = XmlWriter.Create(@"VacationManagerModel.edmx", settings))
            {
                EdmxWriter.WriteEdmx(_ctx, writer);
            } 
        }

        [Test]
        [Ignore("This is not realy a test. Run this test in order to generate sql file for creating DB.")]
        public void GenerateSql()
        {
            var script = ((IObjectContextAdapter)_ctx).ObjectContext.CreateDatabaseScript();

            using (var writer = new StreamWriter(@"VacationManagerDb.sql"))
            {
                writer.Write(script);
            }
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
        public void Can_insert_an_Employee_record_having_a_manager()
        {
            var manager = BuildEmployee(EmployeeRoles.Executive | EmployeeRoles.Manager);
            _ctx.Employees.Add(manager);
            _ctx.SaveChanges();
            var employee = BuildEmployee(EmployeeRoles.Executive, manager);
            Assert.AreNotEqual(manager.Id, employee.Id);

            _ctx.Employees.Add(employee);
            _ctx.SaveChanges();

            Assert.IsNotNull(_ctx.Employees.Single(e => e.Id == employee.Id).Manager);
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

        private static Employee BuildEmployee(EmployeeRoles roles = EmployeeRoles.Executive, Employee manager = null)
        {
            return Builder<Employee>
                .CreateNew()
                    .With(x => x.Id = 0)
                    .And(x => x.Roles = roles)
                    .And(x => x.Manager = manager)
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
