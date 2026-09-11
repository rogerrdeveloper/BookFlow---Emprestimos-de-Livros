namespace EmprestimoLibrary.Models
{
    public class Emprestimo
    {
        public int IdEmprestimo { get; set; }
        public int IdCliente { get; set; }
        public int IdLivro { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime? DataDevolucao { get; set; }
        public bool Devolvido { get; set; }
    }
}
