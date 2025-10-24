using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Gerenciador_de_Curso.Data.Context;
using Gerenciador_de_Curso.Bussiness.Entities;
using Gerenciador_de_Curso.Bussiness.Interfaces.IServices;

namespace Gerenciador_de_Curso.Bussiness.Services
{
    // Responsável por lidar com a lógica de negócios dos conteúdos
    public class ConteudoService : IConteudoService
    {
        private readonly DataContext _context; // conexão com o banco

        // construtor com injeção do contexto do banco
        public ConteudoService(DataContext context)
        {
            _context = context;
        }

        // Retorna todos os conteúdos do banco
        public async Task<IEnumerable<Conteudo>> GetAllAsync()
        {
            return await _context.Conteudos.ToListAsync();
        }

        // Retorna um conteúdo específico
        public async Task<Conteudo> GetByIdAsync(Guid id)
        {
            return await _context.Conteudos.FindAsync(id);
        }

        // Cria um novo conteúdo no banco
        public async Task<Conteudo> CreateAsync(Conteudo conteudo)
        {
            conteudo.Id = Guid.NewGuid(); // Gera novo ID único
            _context.Conteudos.Add(conteudo);
            await _context.SaveChangesAsync();
            return conteudo;
        }

        public async Task<bool> UpdateAsync(Guid id, Conteudo conteudo)
        {
            var existente = await _context.Conteudos.FindAsync(id);
            if (existente == null)
                return false;

            existente.Titulo = conteudo.Titulo;
            existente.TextoInformativo = conteudo.TextoInformativo;
            existente.Midias = conteudo.Midias;

            _context.Conteudos.Update(existente);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var conteudo = await _context.Conteudos.FindAsync(id);
            if (conteudo == null)
                return false;

            _context.Conteudos.Remove(conteudo);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

