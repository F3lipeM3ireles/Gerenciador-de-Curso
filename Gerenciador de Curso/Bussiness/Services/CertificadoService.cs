using Gerenciador_de_Curso.Bussiness.Entities;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using Gerenciador_de_Curso.Data.Context;

namespace Gerenciador_de_Curso.Bussiness.Services
{
    public class CertificadoService
    {
        private readonly DataContext _context; //acesso ao banco, vai dar erro pq ainda não foi feito o Dbcontext

        public CertificadoService(DataContext context)
        {
            _context = context;
        }
        public async Task<List<Context>> GetAllAsync()
        {
            return await _context.Certificados.ToListAsync();
        }
        public async Task<Context> GetByIdAsync(Guid id)
        {
            return await _context.Certificados.FindAsync(id);
        }
        public async Task AddAsync(Context certificado) //add certificado
        {
            _context.Certificados.Add(certificado); // inseriri no context
            await _context.SaveChangeAsync(); //salva no banco 
        }
        public async Task UpdateAsync(Context certificado)
        {
            _context.Certificados.Update(certificado);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync (Guid id) {
            var certificado = await _context.Certificados.FindAsync(id);
            if (certificado != null)
            {
                _context.Certificados.Remove(certificado);
                await _context.SaveChangesAsync();
            }
        }


    }
}
