using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
namespace HelloWorld
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connString = @"Data Source=localhost;Initial Catalog=ZeroToHeroDapperDb;User ID=SA;Password=qweewq123321;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            using (IDbConnection conn = new SqlConnection(connString))
            {
                //var sql = "insert into [denemeguid](product) values(@product)";
                //conn.Open();
                //conn.Execute(sql, new DenemeGuid { product = "sa" });

                var list = conn.Query<DenemeGuid>("select * from [denemeguid]").ToList();
                
                //https://codewithmukesh.com/blog/dapper-in-aspnet-core/

            }

        }

        private static void SelectingScalerValue(IDbConnection conn)
        {
            //InsertData(conn);
            var sql = "select count(*) from [dbo].[User]";
            var count = conn.ExecuteScalar(sql);
            Console.WriteLine(count);
            //conn.Close();
        }

        private static void InsertData(IDbConnection conn)
        {
            conn.Execute("insert into [User](Name,Surname) values (@Name,@Surname)",
                new User { Name = "Fahrican", Surname = "Kaçan" });



            List<User> userList = conn.Query<User>("select * from [User]").ToList();

            userList.ForEach(x =>
            {
                Console.WriteLine($"{x.Id}-Kişi: {x.Name} {x.Surname}");
            });
        }

        public class User
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Surname{ get; set; }
        }

        public class DenemeGuid
        {
            public Guid Id { get; set; }
            public string product { get; set; }
        }
    }
}
