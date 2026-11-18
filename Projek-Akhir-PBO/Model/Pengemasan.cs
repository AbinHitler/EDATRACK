using System;

namespace EDATRACK.Model
{
    public class Pengemasan
    {
        private decimal beratPerKemasan;
        private int jumlahKemasan;

        public int Id { get; set; }
        public string KodeBatch { get; set; }
        public DateTime TanggalPengemasan { get; set; }

        public decimal BeratPerKemasan
        {
            get { return beratPerKemasan; }
            set
            {
                if (value <= 0)
                    throw new Exception("Berat per kemasan harus lebih dari 0.");
                beratPerKemasan = value;
            }
        }

        public int JumlahKemasan
        {
            get { return jumlahKemasan; }
            set
            {
                if (value <= 0)
                    throw new Exception("Jumlah kemasan harus lebih dari 0.");
                jumlahKemasan = value;
            }
        }

        // TotalBerat dihitung otomatis — tidak perlu disimpan manual
        public decimal TotalBerat
        {
            get { return beratPerKemasan * jumlahKemasan; }
        }

        public string JenisKemasan { get; set; }
        public string StatusPengemasan { get; set; }
        public int IdPekerja { get; set; }
    }
}