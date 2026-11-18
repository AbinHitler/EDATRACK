namespace EDATRACK.Model
{
    public class ProsesIQF : TahapProduksi
    {
        public decimal SuhuCelsius { get; set; }
        public decimal DurasiMenit { get; set; }

        public override string GetJenisProses()
        {
            return "IQF";
        }
    }
}