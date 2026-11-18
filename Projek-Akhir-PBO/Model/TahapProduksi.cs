using System;

namespace EDATRACK.Model
{
    public abstract class TahapProduksi
    {
        private string kodeBatch;
        private DateTime waktuMulai;
        private DateTime waktuSelesai;
        private string status;
        private int idPekerja;

        public string KodeBatch
        {
            get { return kodeBatch; }
            set { kodeBatch = value; }
        }

        public DateTime WaktuMulai
        {
            get { return waktuMulai; }
            set { waktuMulai = value; }
        }

        public DateTime WaktuSelesai
        {
            get { return waktuSelesai; }
            set
            {
                if (value < waktuMulai)
                    throw new Exception("Waktu selesai tidak boleh lebih awal dari waktu mulai.");
                waktuSelesai = value;
            }
        }

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        public int IdPekerja
        {
            get { return idPekerja; }
            set { idPekerja = value; }
        }

        public abstract string GetJenisProses();
    }
}