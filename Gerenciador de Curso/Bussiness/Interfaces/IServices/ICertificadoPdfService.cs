using Gerenciador_de_Curso.Bussiness.Entities;

namespace Gerenciador_de_Curso.Bussiness.Interfaces.IServices
{
    public interface ICertificadoPdfService
    {
        byte[] GerarPdf(Context cert); //isso vai gerar o pdf em bytes para ser baixado 
    }
}
