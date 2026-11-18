using System.Data;

namespace EDATRACK.Model
{
    public class Pekerja : Pengguna
    {
        public Pekerja()
        {
            Role = "Pekerja";
            StatusAktif = true;
        }

        public override string TampilkanDashboard()
        {
            return "Dashboard Pekerja: Catat penerimaan, grading, proses produksi, dan QC.";
        }
    }
}