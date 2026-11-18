namespace EDATRACK.Model
{
    public class ProsesBlanching : TahapProduksi
    {
        public decimal SuhuCelsius { get; set; }
        public decimal DurasiMenit { get; set; }
        

        public override string GetJenisProses()
        {
            return "Blanching";
        }
    }
}