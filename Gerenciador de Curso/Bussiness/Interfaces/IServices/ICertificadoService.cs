using Gerenciador_de_Curso.Bussiness.Entities;

namespace Gerenciador_de_Curso.Bussiness.Interfaces.IServices
{
    public interface ICertificadoService
    {
        byte[] GerarCertificadoPdf(Guid certificadoId); /// aq ele pega um certf. existebte e gera um pdf e incorpora no banco
        Context CriarCertificadoPdf(Guid certificadoId); // cria e salva no bdd
        Context ObterPorId(Guid id); //busca
        IEnumerable<Context> ListarTodos();
        void Atualizar(Context certificado);
        void Excluir(Guid id);

    }
}
