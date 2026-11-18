using System;

namespace EDATRACK.Model
{
    public class ProsesProduksi : TahapProduksi
    {
        public string JenisProses { get; set; }
        public decimal Suhu { get; set; }
        public int Durasi { get; set; }
        public DateTime WaktuMulai { get; set; }
        public DateTime WaktuSelesai { get; set; }
        public string NamaPetugas { get; set; }

        public void HitungWaktuSelesai()
        {
            WaktuSelesai = WaktuMulai.AddMinutes(Durasi);
        }

        public override string GetJenisProses()
        {
            return string.IsNullOrEmpty(JenisProses) ? "Proses Produksi" : JenisProses;
        }
    }
}