using System;
using Npgsql;

namespace EDATRACK.Database
{
    public class KoneksiDB
    {
        private static string connectionString =
            "Host=localhost;Port=5432;Database=sesuaikan dengan nama databse postgresql mu;Username=postgres;Password=sesuaikan dengan pw postgres sqlmu";

        public static NpgsqlConnection BukaKoneksi()
        {
            NpgsqlConnection koneksi = new NpgsqlConnection(connectionString);
            koneksi.Open();
            return koneksi;
        }
    }
}