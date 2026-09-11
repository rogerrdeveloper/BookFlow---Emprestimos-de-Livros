namespace EmprestimoLibrary.DTOs
{
    public record EmprestimoResponseDto(
        int IdEmprestimo,
        int IdCliente,
        int IdLivro,
        DateTime DataEmprestimo,
        DateTime? DataDevolucao,
        bool Devolvido
        );
    
}
