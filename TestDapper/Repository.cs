using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dapper;

namespace TestDapper
{
    public class Repository
    {
        public static string DbFile
        {
            //get { return Environment.CurrentDirectory + "\\SimpleDb.sqlite"; }
            // it turns out you CANNOT use an absolute path on a Windows machine
            get { return "SimpleDb.sqlite"; }
        }

        public static SQLiteConnection SimpleDbConnection()
        {
            return new SQLiteConnection("Data Source=" + DbFile);
        }

        public void CreateDatabase()
        {
            // Database file will be created
            using (var db = SimpleDbConnection())
            {
              db.Open();
              db.Execute(
                    @"create table Customer
              (
                 ID                                  integer primary key AUTOINCREMENT,
                 FirstName                           varchar(100) not null,
                 LastName                            varchar(100) not null,
                 DateOfBirth                         datetime not null
              )");
            }
        }

        public Customer FindCustomer(int id)
        {
            using (var db = SimpleDbConnection())
            {
                db.Open();
                var customer =
                    db.Query<Customer>(@"SELECT Id,FirstName,LastName,DateOfBirth FROM Customer WHERE ID = @id",
                        new {id}).FirstOrDefault();
                return customer;
            }
        }

        public IList<Customer> GetAllCustomers()
        {
            using (var db = SimpleDbConnection())
            {
                db.Open();
                var customers = db.Query<Customer>(@"SELECT Id,FirstName,LastName,DateOfBirth FROM Customer").ToList();
                return customers;
            }
        }

        public void SaveCustomer(Customer customer)
        {
            using (var db = SimpleDbConnection())
            {
                db.Open();
                customer.Id = db.Query<int>(
                    @"INSERT INTO Customer 
                    ( FirstName, LastName, DateOfBirth ) VALUES 
                    ( @FirstName, @LastName, @DateOfBirth );
                    select last_insert_rowid()", customer).First();
            }
        }
    }
}
