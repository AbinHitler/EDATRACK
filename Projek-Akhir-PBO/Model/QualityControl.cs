using Projek_Akhir_PBO.Model;
using System;

namespace EDATRACK.Model
{
    public enum KondisiFisikQC
    {
        Utuh,
        CacatRingan,
        Rusak
    }

    public enum KondisiWarnaQC
    {
        HijauSegar,
        HijauPucat,
        Kekuningan
    }

    public enum KondisiKemasanQC
    {
        Baik,
        KurangRapat,
        Rusak
    }

    public class QualityControl : TahapProduksi
    {
        public DateTime TanggalQC { get; set; }
        public KondisiFisikQC KondisiFisik { get; set; }
        public KondisiWarnaQC KondisiWarna { get; set; }
        public KondisiKemasanQC KondisiKemasan { get; set; }
        public string HasilQC { get; set; }
        public string CatatanQC { get; set; }
        public string NamaPetugas { get; set; }

        public Pengemasan DataPengemasan { get; set; }

        public bool IsLulus
        {
            get
            {
                return !string.IsNullOrEmpty(HasilQC) &&
                       HasilQC.Equals("Lulus", StringComparison.OrdinalIgnoreCase);
            }
        }

        public override string GetJenisProses()
        {
            return "Quality Control dan Pengemasan";
        }
    }
}