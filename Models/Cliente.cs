namespace EmprestimoLibrary.Models
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public string NomeCliente{ get; set; } = string.Empty;
        public string CpfCliente{ get; set; } = string.Empty;
        public string EnderecoCliente { get; set; } = string.Empty;
        public string TelefoneCliente { get; set; } = string.Empty;

    }
}
