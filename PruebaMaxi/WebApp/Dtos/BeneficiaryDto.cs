namespace WebApp.Dtos
{
    public class BeneficiaryDto
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public DateTime Fecha { get; set; }
        public int Porcentaje { get; set; }
        public string CURP { get; set; }
        public string SSN { get; set; }
        public string Telefono { get; set; }
        public string Nacionalidad { get; set; }
        public int IdEmployee { get; set; }
    }
}
