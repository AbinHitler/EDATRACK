using System;
using Npgsql;

namespace EDATRACK.Database
{
    public class KoneksiDB
    {
        private static string connectionString =
            "Host=localhost;Port=5432;Database=edatrack;Username=postgres;Password=Pnjwiw!1";

        public static NpgsqlConnection BukaKoneksi()
        {
            NpgsqlConnection koneksi = new NpgsqlConnection(connectionString);
            koneksi.Open();
            return koneksi;
        }
    }
}