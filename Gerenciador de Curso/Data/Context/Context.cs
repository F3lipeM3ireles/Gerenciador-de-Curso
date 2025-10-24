using Microsoft.EntityFrameworkCore;
using Gerenciador_de_Curso.Bussiness.Entities;

namespace Gerenciador_de_Curso.Data.Context;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    // DbSets
    public DbSet<Aluno> Alunos { get; set; }
    public DbSet<Instrutor> Instrutores { get; set; }
    public DbSet<Curso> Cursos { get; set; }
    public DbSet<AlunoCurso> AlunosCursos { get; set; }
    public DbSet<Modulo> Modulos { get; set; }
    public DbSet<ItemModulo> ItensModulos { get; set; }
    public DbSet<Conteudo> Conteudos { get; set; }
    public DbSet<Questao> Questoes { get; set; }
    public DbSet<Alternativa> Alternativas { get; set; }
    public DbSet<RespostaAluno> RespostasAlunos { get; set; }
    public DbSet<Avaliacao> Avaliacoes { get; set; }
    public DbSet<AvaliacaoQuestao> AvaliacoesQuestoes { get; set; }
    public DbSet<ResultadoAvaliacao> ResultadosAvaliacoes { get; set; }
    public DbSet<Certificado> Certificados { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuração de Aluno
        modelBuilder.Entity<Aluno>(aluno =>
        {
            aluno.HasKey(aluno => aluno.Id);
            aluno.Property(aluno => aluno.Nome).IsRequired().HasMaxLength(200);
            aluno.Property(aluno => aluno.Email).IsRequired().HasMaxLength(200);
            aluno.Property(aluno => aluno.Endereco).HasMaxLength(500);
            aluno.Property(aluno => aluno.Telefone).HasMaxLength(20);
            aluno.Property(aluno => aluno.CPF).IsRequired();
            aluno.Property(aluno => aluno.DataNascimento).IsRequired();
            aluno.Property(aluno => aluno.DataCadastro).IsRequired();

            // Índice único para Email e CPF
            aluno.HasIndex(aluno => aluno.Email).IsUnique();
            aluno.HasIndex(aluno => aluno.CPF).IsUnique();
        });

        // Configuração de Instrutor
        modelBuilder.Entity<Instrutor>(instrutor =>
        {
            instrutor.HasKey(instrutor => instrutor.Id);
            instrutor.Property(instrutor => instrutor.Nome).IsRequired().HasMaxLength(200);
            instrutor.Property(instrutor => instrutor.Email).IsRequired().HasMaxLength(200);
            instrutor.Property(instrutor => instrutor.Endereco).HasMaxLength(500);
            instrutor.Property(instrutor => instrutor.Telefone).HasMaxLength(20);
            instrutor.Property(instrutor => instrutor.CPF).IsRequired();
            instrutor.Property(instrutor => instrutor.DataNascimento).IsRequired();
            instrutor.Property(instrutor => instrutor.DataCadastro).IsRequired();

            // Índice único para Email e CPF
            instrutor.HasIndex(instrutor => instrutor.Email).IsUnique();
            instrutor.HasIndex(instrutor => instrutor.CPF).IsUnique();
        });

        // Configuração de Curso
        modelBuilder.Entity<Curso>(curso =>
        {
            curso.HasKey(curso => curso.Id);
            curso.Property(curso => curso.Nome).IsRequired().HasMaxLength(200);
            curso.Property(curso => curso.Descricao).HasMaxLength(1000);
            curso.Property(curso => curso.CargaHoraria).IsRequired();
            curso.Property(curso => curso.LimiteTurma).IsRequired();
            curso.Property(curso => curso.DataCriacao).IsRequired();

            // Relacionamento Curso -> Instrutor (N:1)
            curso.HasOne(curso => curso.Instrutor)
                .WithMany(instrutor => instrutor.Cursos)
                .HasForeignKey(curso => curso.InstrutorId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Configuração de AlunoCurso (Tabela de junção)
        modelBuilder.Entity<AlunoCurso>(alunoCurso =>
        {
            alunoCurso.HasKey(alunoCurso => alunoCurso.Id);
            alunoCurso.Property(alunoCurso => alunoCurso.Progresso).IsRequired();
            alunoCurso.Property(alunoCurso => alunoCurso.Concluido).IsRequired();
            alunoCurso.Property(alunoCurso => alunoCurso.Inicio).IsRequired();
            alunoCurso.Property(alunoCurso => alunoCurso.Termino).IsRequired();

            // Relacionamento AlunoCurso -> Aluno (N:1)
            alunoCurso.HasOne(alunoCurso => alunoCurso.Aluno)
                .WithMany(aluno => aluno.Cursos)
                .HasForeignKey(alunoCurso => alunoCurso.AlunoId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relacionamento AlunoCurso -> Curso (N:1)
            alunoCurso.HasOne(alunoCurso => alunoCurso.Curso)
                .WithMany(curso => curso.Alunos)
                .HasForeignKey(alunoCurso => alunoCurso.CursoId)
                .OnDelete(DeleteBehavior.Cascade);

            // Índice único para evitar duplicação de aluno no mesmo curso
            alunoCurso.HasIndex(alunoCurso => new { alunoCurso.AlunoId, alunoCurso.CursoId }).IsUnique();
        });

        // Configuração de Modulo
        modelBuilder.Entity<Modulo>(modulo =>
        {
            modulo.HasKey(modulo => modulo.Id);
            modulo.Property(modulo => modulo.Titulo).IsRequired().HasMaxLength(200);
            modulo.Property(modulo => modulo.Descricao).HasMaxLength(1000);
            modulo.Property(modulo => modulo.Ordem).IsRequired();
            modulo.Property(modulo => modulo.DataCriacao).IsRequired();

            // Relacionamento Modulo -> Curso (N:1)
            modulo.HasOne(modulo => modulo.Curso)
                .WithMany(curso => curso.Modulos)
                .HasForeignKey(modulo => modulo.CursoId)
                .OnDelete(DeleteBehavior.Cascade);

            // Índice para ordenação
            modulo.HasIndex(modulo => new { modulo.CursoId, modulo.Ordem });
        });

        // Configuração de ItemModulo
        modelBuilder.Entity<ItemModulo>(itemModulo =>
        {
            itemModulo.HasKey(itemModulo => itemModulo.Id);
            itemModulo.Property(itemModulo => itemModulo.Tipo).IsRequired();
            itemModulo.Property(itemModulo => itemModulo.Ordem).IsRequired();
            itemModulo.Property(itemModulo => itemModulo.Obrigatorio).IsRequired();
            itemModulo.Property(itemModulo => itemModulo.DataCriacao).IsRequired();

            // Relacionamento ItemModulo -> Modulo (N:1)
            itemModulo.HasOne(itemModulo => itemModulo.Modulo)
                .WithMany(modulo => modulo.Itens)
                .HasForeignKey(itemModulo => itemModulo.ModuloId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relacionamento ItemModulo -> Conteudo (N:1) - Opcional
            itemModulo.HasOne(itemModulo => itemModulo.Conteudo)
                .WithMany()
                .HasForeignKey(itemModulo => itemModulo.ConteudoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento ItemModulo -> Questao (N:1) - Opcional
            itemModulo.HasOne(itemModulo => itemModulo.Questao)
                .WithMany()
                .HasForeignKey(itemModulo => itemModulo.QuestaoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento ItemModulo -> Avaliacao (N:1) - Opcional
            itemModulo.HasOne(itemModulo => itemModulo.Avaliacao)
                .WithMany()
                .HasForeignKey(itemModulo => itemModulo.AvaliacaoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Índice para ordenação dos itens no módulo
            itemModulo.HasIndex(itemModulo => new { itemModulo.ModuloId, itemModulo.Ordem });
        });

        // Configuração de Conteudo
        modelBuilder.Entity<Conteudo>(conteudo =>
        {
            conteudo.HasKey(conteudo => conteudo.Id);
            conteudo.Property(conteudo => conteudo.Titulo).IsRequired().HasMaxLength(300);
            conteudo.Property(conteudo => conteudo.TextoInformativo).HasMaxLength(5000);
            conteudo.Property(conteudo => conteudo.TipoMidia).HasMaxLength(100);
            conteudo.Property(conteudo => conteudo.DataCriacao).IsRequired();
        });

        // Configuração de Questao
        modelBuilder.Entity<Questao>(questao =>
        {
            questao.HasKey(questao => questao.Id);
            questao.Property(questao => questao.Titulo).IsRequired().HasMaxLength(300);
            questao.Property(questao => questao.Descricao).HasMaxLength(1000);
            questao.Property(questao => questao.Enunciado).IsRequired().HasMaxLength(2000);
            questao.Property(questao => questao.RespostaCorreta).IsRequired();
            questao.Property(questao => questao.Pontuacao).IsRequired();
            questao.Property(questao => questao.DataCriacao).IsRequired();

            // Relacionamento Questao -> Curso (N:1)
            questao.HasOne(questao => questao.Curso)
                .WithMany()
                .HasForeignKey(questao => questao.CursoId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Configuração de Alternativa
        modelBuilder.Entity<Alternativa>(alternativa =>
        {
            alternativa.HasKey(alternativa => alternativa.Id);
            alternativa.Property(alternativa => alternativa.AlternativaTitulo).IsRequired().HasMaxLength(500);
            alternativa.Property(alternativa => alternativa.Ordem).IsRequired();

            // Relacionamento Alternativa -> Questao (N:1)
            alternativa.HasOne(alternativa => alternativa.Questao)
                .WithMany(questao => questao.Alternativas)
                .HasForeignKey(alternativa => alternativa.QuestaoId)
                .OnDelete(DeleteBehavior.Cascade);

            // Índice para ordenação das alternativas
            alternativa.HasIndex(alternativa => new { alternativa.QuestaoId, alternativa.Ordem });
        });

        // Configuração de Avaliacao
        modelBuilder.Entity<Avaliacao>(avaliacao =>
        {
            avaliacao.HasKey(avaliacao => avaliacao.Id);
            avaliacao.Property(avaliacao => avaliacao.Titulo).IsRequired().HasMaxLength(200);
            avaliacao.Property(avaliacao => avaliacao.Descricao).HasMaxLength(1000);
            avaliacao.Property(avaliacao => avaliacao.NotaMinimaAprovacao).IsRequired();
            avaliacao.Property(avaliacao => avaliacao.TempoLimiteMinutos).IsRequired();
            avaliacao.Property(avaliacao => avaliacao.DataCriacao).IsRequired();

            // Relacionamento Avaliacao -> Curso (N:1)
            avaliacao.HasOne(avaliacao => avaliacao.Curso)
                .WithMany()
                .HasForeignKey(avaliacao => avaliacao.CursoId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Configuração de AvaliacaoQuestao (Tabela de junção)
        modelBuilder.Entity<AvaliacaoQuestao>(avaliacaoQuestao =>
        {
            avaliacaoQuestao.HasKey(avaliacaoQuestao => avaliacaoQuestao.Id);
            avaliacaoQuestao.Property(avaliacaoQuestao => avaliacaoQuestao.Ordem).IsRequired();
            avaliacaoQuestao.Property(avaliacaoQuestao => avaliacaoQuestao.Peso).IsRequired().HasDefaultValue(1.0f);

            // Relacionamento AvaliacaoQuestao -> Avaliacao (N:1)
            avaliacaoQuestao.HasOne(avaliacaoQuestao => avaliacaoQuestao.Avaliacao)
                .WithMany(avaliacao => avaliacao.Questoes)
                .HasForeignKey(avaliacaoQuestao => avaliacaoQuestao.AvaliacaoId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relacionamento AvaliacaoQuestao -> Questao (N:1)
            avaliacaoQuestao.HasOne(avaliacaoQuestao => avaliacaoQuestao.Questao)
                .WithMany()
                .HasForeignKey(avaliacaoQuestao => avaliacaoQuestao.QuestaoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Índice único para evitar questão duplicada na mesma avaliação
            avaliacaoQuestao.HasIndex(avaliacaoQuestao => new { avaliacaoQuestao.AvaliacaoId, avaliacaoQuestao.QuestaoId }).IsUnique();

            // Índice para ordenação
            avaliacaoQuestao.HasIndex(avaliacaoQuestao => new { avaliacaoQuestao.AvaliacaoId, avaliacaoQuestao.Ordem });
        });

        // Configuração de RespostaAluno
        modelBuilder.Entity<RespostaAluno>(respostaAluno =>
        {
            respostaAluno.HasKey(respostaAluno => respostaAluno.Id);
            respostaAluno.Property(respostaAluno => respostaAluno.RespostaEscolhida).IsRequired();
            respostaAluno.Property(respostaAluno => respostaAluno.Acertou).IsRequired();
            respostaAluno.Property(respostaAluno => respostaAluno.Nota).IsRequired();
            respostaAluno.Property(respostaAluno => respostaAluno.DataResposta).IsRequired();

            // Relacionamento RespostaAluno -> Aluno (N:1)
            respostaAluno.HasOne(respostaAluno => respostaAluno.Aluno)
                .WithMany()
                .HasForeignKey(respostaAluno => respostaAluno.AlunoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento RespostaAluno -> Questao (N:1)
            respostaAluno.HasOne(respostaAluno => respostaAluno.Questao)
                .WithMany()
                .HasForeignKey(respostaAluno => respostaAluno.QuestaoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento RespostaAluno -> Avaliacao (N:1) - Opcional
            respostaAluno.HasOne(respostaAluno => respostaAluno.Avaliacao)
                .WithMany()
                .HasForeignKey(respostaAluno => respostaAluno.AvaliacaoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Índice para buscar respostas de um aluno em uma questão
            respostaAluno.HasIndex(respostaAluno => new { respostaAluno.AlunoId, respostaAluno.QuestaoId });
        });

        // Configuração de ResultadoAvaliacao
        modelBuilder.Entity<ResultadoAvaliacao>(resultadoAvaliacao =>
        {
            resultadoAvaliacao.HasKey(resultadoAvaliacao => resultadoAvaliacao.Id);
            resultadoAvaliacao.Property(resultadoAvaliacao => resultadoAvaliacao.NotaFinal).IsRequired();
            resultadoAvaliacao.Property(resultadoAvaliacao => resultadoAvaliacao.Aprovado).IsRequired();
            resultadoAvaliacao.Property(resultadoAvaliacao => resultadoAvaliacao.QuestoesCorretas).IsRequired();
            resultadoAvaliacao.Property(resultadoAvaliacao => resultadoAvaliacao.TotalQuestoes).IsRequired();
            resultadoAvaliacao.Property(resultadoAvaliacao => resultadoAvaliacao.DataInicio).IsRequired();
            resultadoAvaliacao.Property(resultadoAvaliacao => resultadoAvaliacao.DataConclusao).IsRequired();
            resultadoAvaliacao.Property(resultadoAvaliacao => resultadoAvaliacao.TempoGastoMinutos).IsRequired();

            // Relacionamento ResultadoAvaliacao -> Avaliacao (N:1)
            resultadoAvaliacao.HasOne(resultadoAvaliacao => resultadoAvaliacao.Avaliacao)
                .WithMany()
                .HasForeignKey(resultadoAvaliacao => resultadoAvaliacao.AvaliacaoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento ResultadoAvaliacao -> Aluno (N:1)
            resultadoAvaliacao.HasOne(resultadoAvaliacao => resultadoAvaliacao.Aluno)
                .WithMany()
                .HasForeignKey(resultadoAvaliacao => resultadoAvaliacao.AlunoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Índice único para evitar múltiplos resultados do mesmo aluno na mesma avaliação
            resultadoAvaliacao.HasIndex(resultadoAvaliacao => new { resultadoAvaliacao.AlunoId, resultadoAvaliacao.AvaliacaoId }).IsUnique();
        });

        // Configuração de Certificado
        modelBuilder.Entity<Certificado>(certificado =>
        {
            certificado.HasKey(certificado => certificado.Id);
            certificado.Property(certificado => certificado.NomeCurso).IsRequired().HasMaxLength(200);
            certificado.Property(certificado => certificado.NomeAluno).IsRequired().HasMaxLength(200);
            certificado.Property(certificado => certificado.CargaHoraria).IsRequired();
            certificado.Property(certificado => certificado.Inicio).IsRequired();
            certificado.Property(certificado => certificado.Termino).IsRequired();
        });
    }
}