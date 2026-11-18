using System.Data;

namespace EDATRACK.Model
{
    public class Admin : Pengguna
    {
        public Admin()
        {
            Role = "Admin";
            StatusAktif = true;
        }

        public override string TampilkanDashboard()
        {
            return "Dashboard Admin: Kelola akun pekerja dan pantau data produksi.";
        }
    }
}