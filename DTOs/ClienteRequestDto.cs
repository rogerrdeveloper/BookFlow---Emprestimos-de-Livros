using System.ComponentModel.DataAnnotations;

namespace EmprestimoLibrary.DTOs
{
    public record ClienteRequestDto(
        // feito como request da api pro usuario,
        // para que ele possa criar um cliente

        //Validador dados que entram no DTO
        [Required(ErrorMessage = "Nome é obrigatório.")]
        [StringLength(100, MinimumLength = 3,
            ErrorMessage = "Nome deve ter entre 3 e 100 caracteres.")]
        string NomeCliente,

        [Required(ErrorMessage = "CPF é obrigatório.")]
        [StringLength(11, MinimumLength = 11,
            ErrorMessage = "CPF deve ter entre 11 caracteres.")]
        string CpfCliente,

        [Required(ErrorMessage = "Endereço é obrigatório.")]
        [StringLength(200,
            ErrorMessage = "Endereço deve ter no máximo 200 caracteres.")]
        string EnderecoCliente,

        [Required(ErrorMessage = "Telefone é obrigatório.")]
        [StringLength(14, MinimumLength = 10,
            ErrorMessage = "Telefone deve ter entre 10 e 14 caracteres.")]
        string TelefoneCliente
        );
    
}
