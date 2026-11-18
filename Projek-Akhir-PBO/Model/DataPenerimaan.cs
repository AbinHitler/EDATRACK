using System;

namespace EDATRACK.Model
{
    public enum KondisiBahanBaku
    {
        Baik,
        Biasa,
        Buruk
    }

    public class DataPenerimaan
    {
        private string kodeBatch;
        private decimal beratKg;

        public int Id { get; set; }

        public string KodeBatch
        {
            get { return kodeBatch; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new Exception("Kode batch tidak boleh kosong.");
                kodeBatch = value;
            }
        }

        public DateTime TanggalPenerimaan { get; set; }
        public string AsalBahanBaku { get; set; }

        public decimal BeratKg
        {
            get { return beratKg; }
            set
            {
                if (value <= 0)
                    throw new Exception("Berat harus lebih dari 0.");
                beratKg = value;
            }
        }

        public KondisiBahanBaku KondisiAwal { get; set; }
        public string Keterangan { get; set; }
        public int IdPekerja { get; set; }
    }
}