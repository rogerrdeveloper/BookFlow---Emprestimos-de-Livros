# BookFlow

**Sistema de gerenciamento de empréstimos de livros desenvolvido com C# e ASP.NET Core.**

O BookFlow foi desenvolvido com propósito **acadêmico e de aprendizado**, buscando aplicar na prática conceitos de desenvolvimento de APIs REST, organização de código, modelagem de dados e regras de negócio.

Antes da implementação, foi realizada uma **modelagem dos dados**, identificando as principais entidades do sistema, seus atributos, relacionamentos e responsabilidades. A partir dessa estrutura, o banco de dados e a aplicação foram desenvolvidos de forma organizada e coerente com as necessidades do sistema.

Mais do que um CRUD simples, o projeto possui regras reais de uma biblioteca, como controle de disponibilidade dos livros, registro de devoluções, identificação de empréstimos atrasados e validações de clientes.

## Funcionalidades

* Cadastro, consulta, atualização e exclusão de clientes
* Cadastro, consulta, atualização e exclusão de livros
* Registro de empréstimos
* Devolução de livros
* Consulta de empréstimos ativos
* Consulta de empréstimos devolvidos
* Consulta de empréstimos atrasados
* Controle automático da quantidade disponível de livros
* Validação de CPF duplicado
* Validação de dados de entrada
* Tratamento global de exceções

## Modelagem de Dados

A estrutura do sistema foi planejada antes do desenvolvimento, partindo da identificação das entidades principais:

* **Cliente**
* **Livro**
* **Empréstimo**

Foram definidos seus atributos, chaves, relacionamentos e regras de integridade, servindo como base para a criação do banco de dados e das entidades da aplicação.

O relacionamento entre clientes, livros e empréstimos permite manter o histórico das operações e controlar a disponibilidade dos livros.

## Tecnologias

* C#
* ASP.NET Core Web API
* Entity Framework Core
* SQL Server
* REST API
* Dependency Injection
* Async/Await
* Data Annotations
* Migrations

## Arquitetura

O projeto foi organizado seguindo uma separação de responsabilidades:

```text
Controller
    ↓
Service
    ↓
Repository
    ↓
DbContext
    ↓
SQL Server
```

Os **Controllers** lidam com as requisições HTTP, os **Services** concentram as regras de negócio e os **Repositories** são responsáveis pelo acesso aos dados.

Também foram utilizados **DTOs**, interfaces e um middleware para tratamento global de exceções.

## Regras de negócio

Algumas regras implementadas:

* Um cliente não pode possuir CPF duplicado.
* Um livro não pode possuir quantidade negativa.
* Um empréstimo só pode ser realizado se o cliente e o livro existirem.
* Não é possível emprestar um livro sem estoque.
* Ao realizar um empréstimo, a quantidade disponível é reduzida.
* Ao devolver um livro, a quantidade disponível é restaurada.
* Um empréstimo não pode ser devolvido duas vezes.
* Empréstimos com mais de 7 dias em aberto são considerados atrasados.

## Objetivo

O principal objetivo do BookFlow foi transformar conhecimentos teóricos em uma aplicação funcional, praticando conceitos importantes para o desenvolvimento backend com **C# e .NET**.

O projeto também representa minha transição dos estudos em **Java/Spring Boot para o ecossistema .NET**, utilizando conceitos que já conhecia como referência para aprender uma nova tecnologia.

## Status

Projeto concluído como projeto acadêmico e de portfólio.

Novos recursos poderão ser adicionados futuramente conforme a evolução dos estudos.

---

**Roger Vitor**
Estudante de Análise e Desenvolvimento de Sistemas | Desenvolvedor Backend
