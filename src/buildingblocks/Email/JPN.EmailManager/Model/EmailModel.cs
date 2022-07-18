using Microsoft.AspNetCore.Http;

namespace JPN.EmailService.Model
{
    public class EmailModel
    {
        public string Destinatario { get; set; }
        public string Assunto { get; set; }
        public string Conteudo { get; set; }
        public IFormFile Anexo { get; set; }
    }
}