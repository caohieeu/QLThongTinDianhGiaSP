using Cassandra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThongTinDanhGiaSP.Services
{
    public class CassandraService : IDisposable
    {
        private readonly Cluster _cluster;
        private readonly ISession _session;

        public CassandraService(string keyspace)
        {
            _cluster = Cluster.Builder()
                .AddContactPoint("127.0.0.1")
                .Build();

            _session = _cluster.Connect(keyspace);
            Console.WriteLine("Connected to Cassandra");
        }
        public void InsertData(string table, Guid id, string firstName, string lastName)
        {
            string query = $"INSERT INTO {table} (id, first_name, last_name) VALUES ({id}, '{firstName}', '{lastName}')";
            _session.Execute(query);
            Console.WriteLine("Data inserted successfully");
        }
        public void Dispose()
        {
            _session?.Dispose();
            _cluster?.Dispose();
            Console.WriteLine("Cassandra connection closed");
        }
    }
}
