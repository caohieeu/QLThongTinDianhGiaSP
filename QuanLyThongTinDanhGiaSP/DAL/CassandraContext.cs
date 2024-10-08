using Cassandra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyThongTinDanhGiaSP.DAL
{
    public class CassandraContext
    {
        private readonly Cluster _cluster;
        private readonly ISession _session;
        public CassandraContext(string keyspace)
        {
            _cluster = Cluster.Builder()
                .AddContactPoint("127.0.0.1")
                .Build();

            _session = _cluster.Connect(keyspace);
            Console.WriteLine("Connected to Cassandra");
        }
        public void Dispose()
        {
            _session?.Dispose();
            _cluster?.Dispose();
            Console.WriteLine("Cassandra connection closed");
        }

        public RowSet executeQuery(string query)
        {
            RowSet rows = null;
            try
            {
                var result = _session.Execute(query);
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return rows;
        }
    }
}
