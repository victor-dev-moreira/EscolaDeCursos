
using EscolaDeCursos.WebApp.Modulos.ModuloInstrutor.Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public sealed class InstrutorConfiguration : IEntityTypeConfiguration<Instrutor>
{
    public void Configure(EntityTypeBuilder<Instrutor> builder)
    {
        builder.ToTable("TBInstrutores");

        // Colunas da tabela TBInstrutores
        builder.HasKey(i => i.Id); // Chave primaria
        builder.Property(i => i.Id).ValueGeneratedNever();

        builder.Property(i => i.Nome)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(i => i.Telefone)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(i => i.Cpf)
            .HasMaxLength(14)
            .IsRequired();

        builder.HasIndex(i => i.Telefone)
            .IsUnique();

        builder.HasIndex(i => i.Cpf)
            .IsUnique();
    }
}
