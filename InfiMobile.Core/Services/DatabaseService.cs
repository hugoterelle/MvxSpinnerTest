using Cirrious.CrossCore.IoC;
using Cirrious.MvvmCross.Plugins.Sqlite;
using InfiMobile.Core.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfiMobile.Core.Services
{
    public class DatabaseService
    {
        private static DatabaseService _instance = null;

        public static DatabaseService Instance
        {
            get
            {
                return _instance ?? (_instance = new DatabaseService(Mvx.Resolve<ISQLiteConnectionFactory>()));
            }

            set
            {
                _instance = value;
            }
        }

        public static ISQLiteConnection Connection { get; set; }
        private int _transactionCount = 0;

        public DatabaseService(ISQLiteConnectionFactory factory)
        {
            //var factory = Mvx.Resolve<ISQLiteConnectionFactory>();
            Connection = factory.Create("InfiMobile.db");
            CreateTables();
        }

        private void CreateTables()
        {
            Connection.CreateTable<Patient>();
        }

        public virtual bool Save(Model model)
        {
            if (model == null)
                return false;

            if (model.State == PersistentState.Deleted || model.State == PersistentState.Unknown)
                return false;

            if (model.State == PersistentState.Loaded)
                return Connection.Update(model) > 0;

            if (model.State == PersistentState.Created)
            {
                if (Connection.Insert(model) > 0)
                {
                    if (model.AutoID > 0)
                    {
                        model.State = PersistentState.Loaded;
                        return true;
                    }
                }
            }
            return false;
        }

        public virtual bool Delete(Model model)
        {
            if (model == null)
                return false;

            if (model.State == PersistentState.Unknown)
                return false;

            if (model.State == PersistentState.Deleted || model.State == PersistentState.Created)
            {
                model.State = PersistentState.Deleted;
                return true;
            }

            if (model.State == PersistentState.Loaded)
            {
                if (Connection.Delete(model) > 0)
                {
                    model.State = PersistentState.Deleted;
                    return true;
                }
            }

            return false;
        }

        public virtual int Execute(string query, params object[] args)
        {
            return Connection.Execute(query, args);
        }

        public virtual T ExecuteScalar<T>(string sql) where T : new()
        {
            return Connection.ExecuteScalar<T>(sql);
        }

        public virtual T Load<T>(int id) where T : Model, new()
        {
            return (T)Load(typeof(T), id);
        }

        public virtual object Load(Type t, int id)
        {
            return Connection.Find(id, Connection.GetMapping(t));
        }

        public virtual T Load<T>(string condition) where T : Model, new()
        {
            return (T)Load(typeof(T), condition);
        }

        public virtual object Load(Type t, string condition)
        {
            var mapping = Connection.GetMapping(t);
            var sql = string.Format("SELECT * FROM {0} {1}", mapping.TableName, condition);
            var result = Connection.Query(mapping, sql);
            return (result != null && result.Count > 0) ? result[0] : null;
        }

        public virtual List<T> List<T>() where T : Model, new()
        {
            var mapping = Connection.GetMapping(typeof(T));
            return Connection.Query<T>("SELECT * FROM " + mapping.TableName);
        }

        public virtual IList List(Type t)
        {
            var m = Connection.GetMapping(t);
            return Connection.Query(m, "SELECT * FROM " + m.TableName);
        }

        public virtual IList List(Type t, string sql)
        {
            var m = Connection.GetMapping(t);
            return Connection.Query(m, sql);
        }

        public virtual List<T> QueryList<T>(string sql) where T : new()
        {
            return Connection.Query<T>(sql);
        }

        public virtual List<T> List<T>(string condition, bool overwrite = false) where T : Model, new()
        {
            var mapping = Connection.GetMapping(typeof(T));
            string sql = "";
            if (overwrite)
                sql = condition;
            else
            {
                sql = "SELECT * FROM {0} {1}";
                sql = string.Format(sql, mapping.TableName, condition);
            }
            return Connection.Query<T>(sql);
        }

        public virtual void BeginTransaction()
        {
            if (_transactionCount == 0)
                Connection.BeginTransaction();
            _transactionCount += 1;
        }

        public virtual void Commit()
        {
            if (Connection.IsInTransaction)
            {
                if (_transactionCount == 1)
                    Connection.Commit();
                _transactionCount -= 1;
            }
            else
                _transactionCount = 0;
        }

        public virtual void Rollback()
        {
            if (Connection.IsInTransaction)
            {
                if (_transactionCount == 1)
                    Connection.Rollback();
                _transactionCount -= 1;
            }
            else
                _transactionCount = 0;
        }
    }
}
