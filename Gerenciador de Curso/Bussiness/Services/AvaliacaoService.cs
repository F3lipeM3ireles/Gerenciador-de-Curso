using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Gerenciador_de_Curso.Data.Context;
using Gerenciador_de_Curso.Bussiness.Entities;
using Gerenciador_de_Curso.Bussiness.Interfaces.IServices;

namespace Gerenciador_de_Curso.Bussiness.Services
{
    public class AvaliacaoService : IAvaliacaoService
    {
        private readonly DataContext _context;

        // Injeta o contexto do banco de dados
        public AvaliacaoService(DataContext context)
        {
            _context = context;
        }

        // Retorna todas as avaliações cadastradas
        public async Task<IEnumerable<Avaliacao>> GetAllAsync()
        {
            return await _context.Avaliacoes.ToListAsync();
        }

        // Busca uma avaliação específica pelo ID (Guid)
        public async Task<Avaliacao> GetByIdAsync(Guid id)
        {
            return await _context.Avaliacoes.FindAsync(id);
        }

        // Cria uma nova avaliação
        public async Task<Avaliacao> CreateAsync(Avaliacao avaliacao)
        {
            avaliacao.Id = Guid.NewGuid(); // Gera novo GUID para o ID
            _context.Avaliacoes.Add(avaliacao);
            await _context.SaveChangesAsync();
            return avaliacao;
        }

        // Atualiza uma avaliação existente
        public async Task<bool> UpdateAsync(Guid id, Avaliacao avaliacao)
        {
            var existente = await _context.Avaliacoes.FindAsync(id);

            if (existente == null)
                return false;

            // Atualiza os campos
            existente.Titulo = avaliacao.Titulo;
            existente.Descricao = avaliacao.Descricao;
            existente.Questoes = avaliacao.Questoes;
            existente.NotaMinimaAprovacao = avaliacao.NotaMinimaAprovacao;
            existente.TempoLimiteMinutos = avaliacao.TempoLimiteMinutos;
            existente.CursoId = avaliacao.CursoId;

            _context.Avaliacoes.Update(existente);
            await _context.SaveChangesAsync();
            return true;
        }

        // Remove uma avaliação do banco
        public async Task<bool> DeleteAsync(Guid id)
        {
            var avaliacao = await _context.Avaliacoes.FindAsync(id);

            if (avaliacao == null)
                return false;

            _context.Avaliacoes.Remove(avaliacao);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
