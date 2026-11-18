using System;

namespace EDATRACK.Model
{
    public abstract class Pengguna
    {
        private int id;
        private string namaLengkap;
        private string username;
        private string password;
        private string role;
        private bool statusAktif;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string NamaLengkap
        {
            get { return namaLengkap; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new Exception("Nama tidak boleh kosong.");
                namaLengkap = value;
            }
        }

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string Role
        {
            get { return role; }
            set { role = value; }
        }

        public bool StatusAktif
        {
            get { return statusAktif; }
            set { statusAktif = value; }
        }

        public abstract string TampilkanDashboard();
    }
}