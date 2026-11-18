using System;

namespace EDATRACK.Model
{
    public enum KondisiWarnaEdamame
    {
        HijauSegar,
        HijauPucat,
        Kekuningan
    }

    public enum KondisiFisikEdamame
    {
        Utuh,
        CacatRingan,
        Rusak
    }

    public class Grading : TahapProduksi
    {
        public DateTime TanggalGrading { get; set; }
        public int JumlahSampel { get; set; }
        public KondisiWarnaEdamame KondisiWarna { get; set; }
        public KondisiFisikEdamame KondisiFisik { get; set; }
        public string UkuranPolong { get; set; }
        public decimal BeratGradeA { get; set; }
        public decimal BeratGradeB { get; set; }
        public decimal BeratReject { get; set; }
        public string NamaPetugas { get; set; }

        public override string GetJenisProses()
        {
            return "Grading";
        }
    }
}