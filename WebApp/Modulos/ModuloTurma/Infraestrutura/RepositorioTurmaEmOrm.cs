using EscolaDeCursos.WebApp.Compartilhado.Infraestrutura.Orm;
using EscolaDeCursos.WebApp.Modulos.ModuloTurma.Dominio;
using Microsoft.EntityFrameworkCore;

namespace EscolaDeCursos.WebApp.Modulos.ModuloTurma.Infraestrutura;

public sealed class RepositorioTurmaEmOrm : IRepositorioTurma
{
    private readonly EscolaDeCursosDbContext dbContext;

    public RepositorioTurmaEmOrm(EscolaDeCursosDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public void Cadastrar(Turma entidade)
    {
        dbContext.Turmas.Add(entidade);

        dbContext.SaveChanges();
    }

    public bool Editar(Guid idSelecionado, Turma entidadeAtualizada)
    {
        Turma? turmaSelecionado = SelecionarPorId(idSelecionado);

        if (turmaSelecionado == null)
            return false;

        turmaSelecionado.Atualizar(entidadeAtualizada);

        dbContext.SaveChanges();

        return true;
    }

    public bool Excluir(Guid idSelecionado)
    {
        Turma? turmaSelecionado = SelecionarPorId(idSelecionado);

        if (turmaSelecionado == null)
            return false;

        dbContext.Turmas.Remove(turmaSelecionado);

        dbContext.SaveChanges();

        return true;
    }
    public Turma? SelecionarPorId(Guid idSelecionado)
    {
        return dbContext.Turmas
        .Include(t => t.Curso)
        .ThenInclude(c => c.Aulas)
        .Include(t => t.Instrutor)
        .Include(t => t.Matriculas)
        .ThenInclude(m => m.Aluno)
        .SingleOrDefault(t => t.Id == idSelecionado);
    }
    public List<Turma> SelecionarTodos()
    {
        return dbContext.Turmas
        .Include(t => t.Curso)
        .ThenInclude(c => c.Aulas)
        .Include(t => t.Instrutor)
        .Include(t => t.Matriculas)
        .ToList();
    }
    public bool ExistePorCursoId(Guid cursoId)
    {
        return dbContext.Turmas.Any(t => t.CursoId == cursoId);
    }
    public bool ExistePorInstrutorId(Guid instrutorId)
    {
        return dbContext.Turmas.Any(t => t.InstrutorId == instrutorId);
    }
}
