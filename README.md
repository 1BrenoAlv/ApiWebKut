# WebKut API ✨

Bem-vindo ao repositório do backend do **WebKut**, uma API RESTful robusta e segura construída com ASP.NET Core. Este projeto serve como a espinha dorsal da plataforma WebKut, gerenciando toda a lógica de negócio, acesso a dados e autenticação.

## 📜 Sobre o Projeto

A WebKut API foi desenvolvida para ser a fonte de dados central para qualquer cliente, incluindo o  [front end](https://github.com/1BrenoAlv/Web-Kut-FrontEnd) em Vue.js. Ela expõe uma série de endpoints para manipulação de recursos como usuários, posts, likes e outros, seguindo as melhores práticas de desenvolvimento de APIs, como o padrão de repositório e injeção de dependência.

-----

## 🚀 Funcionalidades Principais

  * **API RESTful Completa:** Endpoints bem definidos para operações CRUD (Criar, Ler, Atualizar, Deletar) em todos os recursos principais.
  * **Autenticação e Autorização via JWT:** Sistema de segurança baseado em JSON Web Tokens (JWT) para proteger os endpoints e garantir que apenas usuários autenticados acessem os recursos permitidos.
  * **Hashing de Senhas:** Utiliza a biblioteca **BCrypt.Net-Next** para garantir que as senhas dos usuários sejam armazenadas de forma segura e irreversível.
  * **Persistência de Dados com Entity Framework Core:** Mapeamento objeto-relacional (ORM) para interagir com um banco de dados SQL Server de forma eficiente.
  * **Documentação de API Interativa:** Geração automática de documentação com **Swagger (OpenAPI)**, permitindo fácil visualização e teste dos endpoints.
  * **Arquitetura em Camadas:** Código organizado com uma clara separação de responsabilidades (Controllers, Services, Repositories, DTOs).

-----

## 🛠️ Tecnologias Utilizadas

Este projeto foi construído sobre a plataforma .NET, utilizando um conjunto de tecnologias modernas e confiáveis.

  * **Framework Principal:** [ASP.NET Core 8](https://dotnet.microsoft.com/en-us/apps/aspnet)
  * **ORM (Object-Relational Mapper):** [Entity Framework Core 8](https://docs.microsoft.com/en-us/ef/core/)
  * **Banco de Dados:** [SQL Server](https://www.microsoft.com/pt-br/sql-server)
  * **Autenticação:** [JWT Bearer Tokens](https://jwt.io/)
  * **Hashing de Senhas:** [BCrypt.Net-Next](https://github.com/BcryptNet/bcrypt.net)
  * **Documentação da API:** [Swashbuckle (Swagger)](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)

-----

## ⚙️ Pré-requisitos

Antes de começar, certifique-se de ter o SDK do .NET 8 e um servidor SQL Server (pode ser a versão Express ou Developer) instalados.

  * [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
  * [SQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)

-----

## 🏁 Como Começar

Siga os passos abaixo para configurar e rodar o projeto em seu ambiente local.

**1. Clone o repositório:**

```bash
git clone https://github.com/1BrenoAlv/ApiWebKut.git
cd ApiWebKut
```

**2. Configure a String de Conexão:**

Abra o arquivo `appsettings.json` e altere a `DefaultConnection` para apontar para a sua instância local do SQL Server.

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=SEU_SERVIDOR;Database=WebKutDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

*(Substitua `SEU_SERVIDOR` pelo nome da sua instância, como `localhost\\SQLEXPRESS` ou `.`)*

**3. Aplique as Migrations do Banco de Dados:**

O Entity Framework Core usará as "migrations" para criar o banco de dados e todas as tabelas para você. Rode o seguinte comando no terminal, na raiz do projeto:

```bash
dotnet ef database update
```

**4. Rode a Aplicação:**

Execute o projeto usando a CLI do .NET ou diretamente pela sua IDE (Visual Studio / VS Code).

```bash
dotnet run
```

A API estará rodando, geralmente em `https://localhost:7132` (ou outra porta indicada no terminal).

-----

## 📖 Documentação da API (Swagger)

Com a aplicação rodando, você pode acessar a documentação interativa da API gerada pelo Swagger. Basta navegar para:

**`https://localhost:7132/swagger`**

Nesta página, você poderá ver todos os endpoints disponíveis, seus parâmetros, e até mesmo testá-los diretamente pelo navegador.
