using Microsoft.VisualStudio.TestTools.UnitTesting;
using InfiMobile.Core.Models;
using Cirrious.MvvmCross.Plugins.Sqlite;
using InfiMobile.Core.Services;
using Cirrious.CrossCore.IoC;
using Cirrious.MvvmCross.Plugins.Sqlite.Console;
using System;

namespace InfiMobile.Test
{
    [TestClass]
    public class PatientTest
    {
        public void InitDatabase()
        {
            DatabaseService.Instance = new DatabaseService(new MvxConsoleSQLiteConnectionFactory());
        }

        [TestMethod]
        public void CreatePatient()
        {
            InitDatabase();

            var patient = new Patient()
                {
                    FirstName = "Hugo",
                    LastName = "Terelle",
                    DateOfBirth = "19751111",
                    Niss = "1975111133319",
                    OARegistration = "",
                    OAIdentification = ""
                };

            DatabaseService.Connection.Insert(patient);
            Assert.IsTrue(patient.AutoID > 0);
            DatabaseService.Connection.Delete(patient);
        }


        [TestMethod]
        public void UpdatePatient()
        {
            InitDatabase();

            var patient = new Patient()
            {
                FirstName = "Hugo",
                LastName = "Terelle",
                DateOfBirth = "19751111",
                Niss = "1975111133319",
                OARegistration = "",
                OAIdentification = ""
            };

            DatabaseService.Connection.Insert(patient);
            Assert.IsTrue(patient.AutoID > 0);
            var id = patient.AutoID;

            patient.DateOfBirth = "19751110";
            DatabaseService.Connection.Update(patient);
            Assert.IsTrue(patient.AutoID == id);
            var p2 = DatabaseService.Connection.Find<Patient>(id);
            Assert.IsTrue(p2.AutoID == id);

            DatabaseService.Connection.Delete<Patient>(id);
        }

        [TestMethod]
        public void SaveModelPatient()
        {
            InitDatabase();

            var patient = new Patient()
            {
                FirstName = "Hugo",
                LastName = "Terelle",
                DateOfBirth = "19751111",
                Niss = "1975111133319",
                OARegistration = "",
                OAIdentification = ""
            };

            Assert.IsTrue(patient.State == PersistentState.Created);

            DatabaseService.Instance.Save(patient);
            Assert.IsTrue(patient.AutoID > 0);
            var id = patient.AutoID;

            Assert.IsTrue(patient.State == PersistentState.Loaded);

            patient.DateOfBirth = "19751110";
            DatabaseService.Instance.Save(patient);
            Assert.IsTrue(patient.AutoID == id);
            var p2 = DatabaseService.Connection.Find<Patient>(id);
            Assert.IsTrue(p2.AutoID == id);

            Assert.IsTrue(p2.State == PersistentState.Loaded);

            DatabaseService.Instance.Delete(p2);
        }
    }
}
